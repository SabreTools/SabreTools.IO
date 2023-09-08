using System.IO;

namespace SabreTools.IO
{
    /// <summary>
    /// Extensions to Stream functionality
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Seek to a specific point in the stream, if possible
        /// </summary>
        /// <param name="input">Input stream to try seeking on</param>
        /// <param name="offset">Optional offset to seek to</param>
        public static long SeekIfPossible(this Stream input, long offset = 0)
        {
            // If the stream is null, don't even try
            if (input == null)
                return -1;

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
                else if (offset >= 0)
                    return input.Seek(offset, SeekOrigin.Begin);

                return input.Position;
            }
            catch
            {
                return -1;
            }
        }
    }
}
