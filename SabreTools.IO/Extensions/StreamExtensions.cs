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
    }
}
