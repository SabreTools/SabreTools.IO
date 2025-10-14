using System;
using System.IO;
using SabreTools.IO.Extensions;

namespace SabreTools.IO.Transform
{
    /// <summary>
    /// Helpers to perform swapping operations
    /// </summary>
    public static class Swap
    {
        /// <summary>
        /// Transform an input file using the given rule
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="output">Output file name</param>
        /// <param name="operation">Transform operation to carry out</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        public static bool Process(string input, string output, Operation operation)
        {
            // If the file does not exist
            if (!File.Exists(input))
                return false;

            // Create the output directory if it doesn't already
            string? outputDirectory = Path.GetDirectoryName(Path.GetFullPath(output));
            if (outputDirectory != null && !Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            try
            {
                // Get the input stream
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Transform the stream
                var transformed = Process(inputStream, operation);
                if (transformed == null)
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
        public static Stream? Process(Stream input, Operation operation)
        {
            // If the stream is unreadable
            if (!input.CanRead)
                return null;

            // If the operation is not defined
            if (!Enum.IsDefined(typeof(Operation), operation))
                return null;

            try
            {
                // Create an output stream
                var output = new MemoryStream();

                // Determine the cutoff boundary for the operation
                long endBoundary = operation switch
                {
                    Operation.Bitswap => input.Length,
                    Operation.Byteswap => input.Length - (input.Length % 2),
                    Operation.Wordswap => input.Length - (input.Length % 4),
                    Operation.WordByteswap => input.Length - (input.Length % 4),
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
                        case Operation.Bitswap:
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
                        case Operation.Byteswap:
                            if (pos % 2 == 1)
                                buffer[pos - 1] = b;
                            else
                                buffer[pos + 1] = b;

                            break;
                        case Operation.Wordswap:
                            buffer[(pos + 2) % 4] = b;
                            break;
                        case Operation.WordByteswap:
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
    }
}
