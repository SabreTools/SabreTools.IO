using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Extensions;

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

        /// <summary>
        /// Interleave two files into a single output
        /// </summary>
        /// <param name="even">First file to interleave</param>
        /// <param name="odd">Second file to interleave</param>
        /// <param name="output">Path to the output file</param>
        /// <param name="blockSize">Number of bytes read before switching input</param>
        /// <returns>True if the files were interleaved successfully, false otherwise</returns>
        public static bool Interleave(string even, string odd, string output, int blockSize)
        {
            // If either file does not exist
            if (!File.Exists(even) || !File.Exists(odd))
                return false;

            try
            {
                // Get the input streams
                using var evenStream = File.Open(even, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var oddStream = File.Open(odd, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Interleave the streams
                using var interleaved = Interleave(evenStream, oddStream, blockSize);
                if (interleaved is null)
                    return false;

                // Open the output file
                using var outputStream = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                // Write the interleaved data
                interleaved.CopyTo(outputStream);
                outputStream.Flush();
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }

            return true;
        }

        /// <summary>
        /// Interleave two streams into a single output
        /// </summary>
        /// <param name="even">First stream to interleave</param>
        /// <param name="odd">Second stream to interleave</param>
        /// <param name="output">Path to the output file</param>
        /// <param name="blockSize">Number of bytes read before switching input</param>
        /// <returns>A filled stream on success, null otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="blockSize"/> is non-positive.
        /// </exception>
        public static Stream? Interleave(Stream even, Stream odd, int blockSize)
        {
            // If either stream is unreadable
            if (!even.CanRead || !odd.CanRead)
                return null;

            // If the block size is invalid
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            try
            {
                // Create an output stream
                var outputStream = new MemoryStream();

                // Alternate between inputs during reading
                bool useEven = true;
                while (even.Position < even.Length || odd.Position < odd.Length)
                {
                    byte[] read = new byte[blockSize];
                    int actual = (useEven ? even : odd).Read(read, 0, blockSize);
                    outputStream.Write(read, 0, actual);
                    outputStream.Flush();
                    useEven = !useEven;
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
            catch
            {
                // Absorb all errors for now
                return null;
            }
        }

        #endregion

        #region Split

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

        #endregion

        #region Swap

        /// <summary>
        /// Transform an input file using the given rule
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="output">Output file name</param>
        /// <param name="operation">Transform operation to carry out</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        public static bool Swap(string input, string output, SwapOperation operation)
        {
            // If the file does not exist
            if (!File.Exists(input))
                return false;

            // Create the output directory if it doesn't already
            string? outputDirectory = Path.GetDirectoryName(Path.GetFullPath(output));
            if (outputDirectory is not null && !Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            try
            {
                // Get the input stream
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Transform the stream
                var transformed = Swap(inputStream, operation);
                if (transformed is null)
                    return false;

                // Open the output file
                using var outputStream = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                // Write the transformed data
                transformed.CopyTo(outputStream);
                outputStream.Flush();
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }

            return true;
        }

        /// <summary>
        /// Transform an input stream using the given rule
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="operation">Transform operation to carry out</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="type"/> is not a recognized value.
        /// </exception>
        public static Stream? Swap(Stream input, SwapOperation operation)
        {
            // If the stream is unreadable
            if (!input.CanRead)
                return null;

            // If the operation is not defined
            if (!Enum.IsDefined(typeof(SwapOperation), operation))
                return null;

            try
            {
                // Create an output stream
                var output = new MemoryStream();

                // Determine the cutoff boundary for the operation
                long endBoundary = operation switch
                {
                    SwapOperation.Bitswap => input.Length,
                    SwapOperation.Byteswap => input.Length - (input.Length % 2),
                    SwapOperation.Wordswap => input.Length - (input.Length % 4),
                    SwapOperation.WordByteswap => input.Length - (input.Length % 4),
                    _ => throw new ArgumentOutOfRangeException(nameof(operation)),
                };

                // Loop over the input and process in blocks
                byte[] buffer = new byte[4];
                int pos = 0;
                while (input.Position < endBoundary)
                {
                    byte b = input.ReadByteValue();
                    switch (operation)
                    {
                        case SwapOperation.Bitswap:
                            uint r = b;
                            int s = 7;
                            for (b >>= 1; b != 0; b >>= 1)
                            {
                                r <<= 1;
                                r |= (byte)(b & 1);
                                s--;
                            }

                            r <<= s;
                            buffer[pos] = (byte)r;
                            break;
                        case SwapOperation.Byteswap:
                            if (pos % 2 == 1)
                                buffer[pos - 1] = b;
                            else
                                buffer[pos + 1] = b;

                            break;
                        case SwapOperation.Wordswap:
                            buffer[(pos + 2) % 4] = b;
                            break;
                        case SwapOperation.WordByteswap:
                            buffer[3 - pos] = b;
                            break;
                        default:
                            buffer[pos] = b;
                            break;
                    }

                    // Set the buffer position to default write to
                    pos = (pos + 1) % 4;

                    // If the buffer pointer has been reset
                    if (pos == 0)
                    {
                        output.Write(buffer);
                        output.Flush();
                        buffer = new byte[4];
                    }
                }

                // If there's anything more in the buffer
                for (int i = 0; i < pos; i++)
                {
                    output.Write(buffer[i]);
                }

                // If the stream still has data
                if (input.Position < input.Length)
                {
                    byte[] bytes = input.ReadBytes((int)(input.Length - input.Position));
                    output.Write(bytes);
                    output.Flush();
                }

                output.Seek(0, SeekOrigin.Begin);
                return output;
            }
            catch
            {
                // Absorb all errors for now
                return null;
            }
        }

        #endregion
    }
}
