using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.Numerics.Extensions;
using SabreTools.Text.Extensions;

namespace SabreTools.IO.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Align the stream position to a byte-size boundary
        /// </summary>
        /// <param name="input">Input stream to try aligning</param>
        /// <param name="alignment">Number of bytes to align on</param>
        /// <returns>True if the stream could be aligned, false otherwise</returns>
        public static bool AlignToBoundary(this Stream? input, int alignment)
        {
            // If the stream is invalid
            if (input is null || input.Length == 0 || !input.CanRead)
                return false;

            // If already at the end of the stream
            if (input.Position >= input.Length)
                return false;

            // Align the stream position
            while (input.Position % alignment != 0 && input.Position < input.Length)
            {
                _ = input.ReadByteValue();
            }

            // Return if the alignment completed
            return input.Position % alignment == 0;
        }

        /// <summary>
        /// Block-copy an input stream to an output stream, absorbing any errors
        /// </summary>
        /// <param name="input">Input stream to copy from</param>
        /// <param name="output">Ouput stream to copy to</param>
        /// <param name="blockSize">Number of bytes to read at a time, default 8192</param>
        /// <returns>True if the copy succeeded without an exception, false otherwise</returns>
        /// <remarks>This may result in incomplete outputs if an exception occurs</remarks>
        public static bool BlockCopy(this Stream? input, Stream? output, int blockSize = 8192)
        {
            // If either stream is invalid
            if (input is null || output is null)
                return false;

            // If the input is unreadable
            if (!input.CanRead)
                return false;

            // If the output is not writable
            if (!output.CanWrite)
                return false;

            // If the block size is invalid in some way
            if (blockSize <= 0)
                return false;

            try
            {
                // Copy the array in blocks
                byte[] buffer = new byte[blockSize];
                while (true)
                {
                    int read = input.Read(buffer, 0, blockSize);
                    if (read <= 0)
                        break;

                    output.Write(buffer, 0, read);
                }

                return true;
            }
            catch
            {
                // Absorb the error
                return false;
            }
        }

        #region InterleaveWith

        /// <summary>
        /// Interleave two files into a single output
        /// </summary>
        /// <param name="even">First file to interleave</param>
        /// <param name="odd">Second file to interleave</param>
        /// <param name="output">Path to the output file</param>
        /// <param name="blockSize">Number of bytes read before switching input</param>
        /// <returns>True if the files were interleaved successfully, false otherwise</returns>
        public static bool InterleaveWith(this string even, string odd, string output, int blockSize)
        {
            // If either file does not exist
            if (!File.Exists(even) || !File.Exists(odd))
                return false;

            try
            {
                // Get the input and output streams
                using var evenStream = File.Open(even, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var oddStream = File.Open(odd, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var outputStream = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                // Interleave the streams
                return evenStream.InterleaveWith(oddStream, outputStream, blockSize);
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }
        }

        /// <summary>
        /// Interleave two streams into a single output
        /// </summary>
        /// <param name="even">First stream to interleave</param>
        /// <param name="odd">Second stream to interleave</param>
        /// <param name="output">Output stream</param>
        /// <param name="blockSize">Number of bytes read before switching input</param>
        /// <returns>A filled stream on success, null otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="blockSize"/> is non-positive.
        /// </exception>
        public static bool InterleaveWith(this Stream even, Stream odd, Stream output, int blockSize)
        {
            // If either stream is unreadable
            if (!even.CanRead || !odd.CanRead)
                return false;

            // If the output is unwritable
            if (!output.CanWrite)
                return false;

            // If the block size is invalid
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            try
            {
                // Alternate between inputs during reading
                bool useEven = true;
                while (even.Position < even.Length || odd.Position < odd.Length)
                {
                    byte[] read = new byte[blockSize];
                    int actual = (useEven ? even : odd).Read(read, 0, blockSize);
                    output.Write(read, 0, actual);
                    output.Flush();
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

        #endregion

        /// <summary>
        /// Read a number of bytes from an offset in a stream, if possible
        /// </summary>
        /// <param name="input">Input stream to read from</param>
        /// <param name="offset">Offset within the stream to start reading</param>
        /// <param name="length">Number of bytes to read from the offset</param>
        /// <param name="retainPosition">Indicates if the original position of the stream should be retained after reading</param>
        /// <returns>Filled byte array on success, null on error</returns>
        /// <remarks>
        /// This method will return a null array if the length is greater than what is left
        /// in the stream. This is different behavior than a normal stream read that would
        /// attempt to read as much as possible, returning the amount of bytes read.
        /// </remarks>
        public static byte[]? ReadFrom(this Stream? input, long offset, int length, bool retainPosition)
        {
            if (input is null || !input.CanRead || !input.CanSeek)
                return null;
            if (offset < 0 || offset >= input.Length)
                return null;
            if (length < 0 || offset + length > input.Length)
                return null;

            // Cache the current location
            long currentLocation = input.Position;

            // Seek to the requested offset
            long newPosition = input.SeekIfPossible(offset);
            if (newPosition != offset)
                return null;

            // Read from the position
            byte[] data = input.ReadBytes(length);

            // Seek back if requested
            if (retainPosition)
                _ = input.SeekIfPossible(currentLocation);

            // Return the read data
            return data;
        }

        /// <summary>
        /// Read string data from a Stream
        /// </summary>
        /// <param name="charLimit">Number of characters needed to be a valid string, default 5</param>
        /// <param name="position">Position in the source to read from</param>
        /// <param name="length">Length of the requested data</param>
        /// <returns>String list containing the requested data, null on error</returns>
#if NET5_0_OR_GREATER
        /// <remarks>This reads both Latin1 and UTF-16 strings from the input data</remarks>
#else
        /// <remarks>This reads both ASCII and UTF-16 strings from the input data</remarks>
#endif
        public static List<string>? ReadStringsFrom(this Stream? input, int position, int length, int charLimit = 5)
        {
            // Read the data as a byte array first
            byte[]? data = input.ReadFrom(position, length, retainPosition: true);
            if (data is null)
                return null;

            return data.ReadStringsFrom(charLimit);
        }

        #region SeekIfPossible

        /// <summary>
        /// Seek to a specific point in the stream, if possible
        /// </summary>
        /// <param name="input">Input stream to try seeking on</param>
        /// <param name="offset">Optional offset to seek to</param>
        public static long SeekIfPossible(this Stream input, long offset = 0)
            => input.SeekIfPossible(offset, offset < 0 ? SeekOrigin.End : SeekOrigin.Begin);

        /// <summary>
        /// Seek to a specific point in the stream, if possible
        /// </summary>
        /// <param name="input">Input stream to try seeking on</param>
        /// <param name="offset">Optional offset to seek to</param>
        public static long SeekIfPossible(this Stream input, long offset, SeekOrigin origin)
        {
            // If the input is not seekable, just return the current position
            if (!input.CanSeek)
            {
                try
                {
                    return input.Position;
                }
                catch
                {
                    return -1;
                }
            }
            // Attempt to seek to the offset
            try
            {
                return input.Seek(offset, origin);
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// Check if a segment is valid in the stream
        /// </summary>
        /// <param name="input">Input stream to validate</param>
        /// <param name="offset">Position in the source</param>
        /// <param name="count">Length of the data to check</param>
        /// <returns>True if segment could be read fully, false otherwise</returns>
        public static bool SegmentValid(this Stream? input, long offset, long count)
        {
            if (input is null)
                return false;
            if (offset < 0 || offset > input.Length)
                return false;
            if (count < 0 || offset + count > input.Length)
                return false;

            return true;
        }

        #region SplitToEvenOdd

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
        public static bool SplitToEvenOdd(this string input, string even, string odd, int blockSize)
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
        public static bool SplitToEvenOdd(this Stream input, Stream even, Stream odd, int blockSize)
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

        #endregion

        #region Swap

        /// <summary>
        /// Transform an input file using the given rule
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="output">Output file name</param>
        /// <param name="operation">Transform operation to carry out</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        public static bool Swap(this string input, string output, SwapOperation operation)
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
                // Get the input and output streams
                using var inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var outputStream = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                // Transform the stream
                return Swap(inputStream, outputStream, operation);
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }
        }

        /// <summary>
        /// Transform an input stream using the given rule
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="operation">Transform operation to carry out</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="type"/> is not a recognized value.
        /// </exception>
        public static bool Swap(this Stream input, Stream output, SwapOperation operation)
        {
            // If the input is unreadable
            if (!input.CanRead)
                return false;

            // If the output is unwritable
            if (!output.CanWrite)
                return false;

            // If the operation is not defined
            if (!Enum.IsDefined(typeof(SwapOperation), operation))
                return false;

            try
            {
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
                    byte b = (byte)input.ReadByte();
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
                        output.Write(buffer, 0, buffer.Length);
                        output.Flush();
                        buffer = new byte[4];
                    }
                }

                // If there's anything more in the buffer
                if (pos > 0)
                    output.Write(buffer, 0, pos);

                // If the stream still has data
                if (input.Position < input.Length)
                {
                    int remaining = (int)(input.Length - input.Position);
                    byte[] bytes = new byte[remaining];
                    int read = input.Read(bytes, 0, remaining);
                    output.Write(bytes, 0, read);
                    output.Flush();
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
