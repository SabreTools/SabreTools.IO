using System;
using System.Collections.Generic;
using System.IO;

namespace SabreTools.IO
{
    public static class Transform
    {
        #region Combine

        /// <summary>
        /// Concatenate all files in the order provided, if possible
        /// </summary>
        /// <param name="paths">List of paths to combine</param>
        /// <param name="output">Path to the output file</param>
        /// <returns>True if the files were concatenated successfully, false otherwise</returns>
        public static bool Concatenate(List<string> paths, string output)
        {
            // If the path list is empty
            if (paths.Count == 0)
                return false;

            // If the output filename is invalid
            if (string.IsNullOrEmpty(output))
                return false;

            try
            {
                // Try to build the new output file
                using var ofs = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                for (int i = 0; i < paths.Count; i++)
                {
                    // Get the next file
                    string next = paths[i];
                    if (!File.Exists(next))
                        break;

                    // Copy the next input to the output
                    using var ifs = File.Open(next, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    // Write in blocks
                    int read = 0;
                    do
                    {
                        byte[] buffer = new byte[3 * 1024 * 1024];

                        read = ifs.Read(buffer, 0, buffer.Length);
                        if (read == 0)
                            break;

                        ofs.Write(buffer, 0, read);
                        ofs.Flush();
                    } while (read > 0);
                }

                return true;
            }
            catch
            {
                // Absorb the exception right now
                return false;
            }
        }

        #endregion

        #region Split

        /// <summary>
        /// Split an input file into two outputs
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="even">Output file name for even blocks, must be distinct from <paramref name="odd"/></param>
        /// <param name="odd">Output file name for odd blocks, must be distinct from <paramref name="even"/></param>
        /// <param name="blockSize">Number of bytes read before switching output</param>
        /// <returns>True if the file could be split, false otherwise</returns>
        /// <remarks>
        /// If <paramref name="even"/> and <paramref name="odd"/> point to the same file, then there will be an
        /// internal exception when trying to create the output files which is absorbed by this method.
        /// </remarks>
        public static bool SplitToEvenOdd(string input, string even, string odd, int blockSize)
        {
            // If the file does not exist
            if (!File.Exists(input))
                return false;

            try
            {
                // Get the input and output streams
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var evenStream = File.Open(even, FileMode.Create, FileAccess.Write, FileShare.None);
                using var oddStream = File.Open(odd, FileMode.Create, FileAccess.Write, FileShare.None);

                // Split the stream
                return SplitToEvenOdd(inputStream, evenStream, oddStream, blockSize);
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }
        }

        /// <summary>
        /// Split an input stream into two output streams
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="even">Output stream for even blocks</param>
        /// <param name="odd">Output stream for odd blocks</param>
        /// <param name="blockSize">Number of bytes read before switching output</param>
        /// <returns>True if the stream could be split, false otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="blockSize"/> is non-positive.
        /// </exception>
        /// <remarks>
        /// If <paramref name="even"/> and <paramref name="odd"/> point to the same stream, then only half
        /// of the expected output will exist because both streams will not be pointing to the same index.
        /// </remarks>
        public static bool SplitToEvenOdd(Stream input, Stream even, Stream odd, int blockSize)
        {
            // If the stream is unreadable
            if (!input.CanRead)
                return false;

            // If either output is unwritable
            if (!even.CanWrite || !odd.CanWrite)
                return false;

            // If the block size is invalid
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            try
            {
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

                return true;
            }
            catch
            {
                // Absorb all errors for now
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

        #endregion
    }
}
