using System;
using System.IO;

namespace SabreTools.IO.Transform
{
    /// <summary>
    /// Helpers to split inputs
    /// </summary>
    public static class Split
    {
        /// <summary>
        /// Split an input file into two outputs
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="outputDir">Path to the output directory</param>
        /// <param name="blockSize">Number of bytes read before switching output</param>
        /// <returns>True if the file could be split, false otherwise</returns>
        public static bool BlockSplit(string input, string? outputDir, int blockSize)
        {
            // If the file does not exist
            if (!File.Exists(input))
                return false;

            try
            {
                // Get the input stream
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Split the stream
                if (!BlockSplit(inputStream, blockSize, out Stream? evenStream, out Stream? oddStream))
                    return false;
                else if (evenStream is null || oddStream is null)
                    return false;

                // Get the base filename for output files
                outputDir ??= Path.GetDirectoryName(input);
                string baseFilename = Path.GetFileName(input);
                if (!string.IsNullOrEmpty(outputDir))
                    baseFilename = Path.Combine(outputDir, baseFilename);

                // Create the output directory, if possible
                if (outputDir is not null && !Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // Open the output files
                using var outEvenStream = File.Open($"{baseFilename}.even", FileMode.Create, FileAccess.Write, FileShare.None);
                using var outOddStream = File.Open($"{baseFilename}.odd", FileMode.Create, FileAccess.Write, FileShare.None);

                // Write the split data
                evenStream.CopyTo(outEvenStream);
                outEvenStream.Flush();
                oddStream.CopyTo(outOddStream);
                outOddStream.Flush();
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }

            return true;
        }

        /// <summary>
        /// Split an input stream into two output streams
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="blockSize">Number of bytes read before switching output</param>
        /// <param name="even">Even block output stream on success, null otherwise</param>
        /// <param name="odd">Odd block output stream on success, null otherwise</param>
        /// <returns>True if the stream could be split, false otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="blockSize"/> is non-positive.
        /// </exception>
        public static bool BlockSplit(Stream input, int blockSize, out Stream? even, out Stream? odd)
        {
            // Set default values for the outputs
            even = null;
            odd = null;

            // If the stream is unreadable
            if (!input.CanRead)
                return false;

            // If the block size is invalid
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            try
            {
                // Create the output streams
                even = new MemoryStream();
                odd = new MemoryStream();

                // Alternate between inputs during reading
                bool useEven = true;
                while (input.Position < input.Length)
                {
                    byte[] read = new byte[blockSize];
                    int actual = input.Read(read, 0, blockSize);
                    (useEven ? even : odd).Write(read, 0, actual);
                    (useEven ? even : odd).Flush();
                    useEven = !useEven;
                }

                even.Seek(0, SeekOrigin.Begin);
                odd.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch
            {
                // Absorb all errors for now
                even = null;
                odd = null;
                return false;
            }
        }

        /// <summary>
        /// Split an input file into files of up to <paramref name="size"/> bytes
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="outputDir">Path to the output directory</param>
        /// <param name="size">Maximum number of bytes to split on</param>
        /// <returns>True if the file could be split, false otherwise</returns>
        public static bool SizeSplit(string input, string? outputDir, int size)
        {
            // If the file does not exist
            if (!File.Exists(input))
                return false;

            // If the size is invalid
            if (size <= 0)
                return false;

            try
            {
                // Get the input stream
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Get the base filename for output files
                outputDir ??= Path.GetDirectoryName(input);
                string baseFilename = Path.GetFileName(input);
                if (!string.IsNullOrEmpty(outputDir))
                    baseFilename = Path.Combine(outputDir, baseFilename);

                // Create the output directory, if possible
                if (outputDir is not null && !Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // Loop while there is data left
                int part = 0;
                while (inputStream.Position < inputStream.Length)
                {
                    // Create the next output file
                    using var partStream = File.Open($"{baseFilename}.{part++}", FileMode.Create, FileAccess.Write, FileShare.None);

                    // Process the next block of data
                    byte[] data = new byte[size];
                    int actual = inputStream.Read(data, 0, size);
                    partStream.Write(data, 0, actual);
                    partStream.Flush();
                }

                return true;
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }
        }
    }
}
