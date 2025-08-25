using System.IO;

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
        public static bool AlignToBoundary(this Stream? input, byte alignment)
        {
            // If the stream is invalid
            if (input == null || input.Length == 0 || !input.CanRead)
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
            if (input == null || !input.CanRead || !input.CanSeek)
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
        /// Seek to a specific point in the stream, if possible
        /// </summary>
        /// <param name="input">Input stream to try seeking on</param>
        /// <param name="offset">Optional offset to seek to</param>
        public static long SeekIfPossible(this Stream input, long offset = 0)
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
                if (offset < 0)
                    return input.Seek(offset, SeekOrigin.End);
                else
                    return input.Seek(offset, SeekOrigin.Begin);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Check if a segment is valid in the stream
        /// </summary>
        /// <param name="input">Input stream to validate</param>
        /// <param name="offset">Position in the source</param>
        /// <param name="count">Length of the data to check</param>
        /// <returns>True if segment could be read fully, false otherwise</returns>
        public static bool SegmentValid(this Stream? input, long offset, long count)
        {
            if (input == null)
                return false;
            if (offset < 0 || offset > input.Length)
                return false;
            if (count < 0 || offset + count > input.Length)
                return false;

            return true;
        }
    }
}
