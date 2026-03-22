using System;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Align the array position to a byte-size boundary
        /// </summary>
        /// <param name="input">Input array to try aligning</param>
        /// <param name="offset">Offset into the byte array</param>
        /// <param name="alignment">Number of bytes to align on</param>
        /// <returns>True if the array could be aligned, false otherwise</returns>
        public static bool AlignToBoundary(this byte[]? input, ref int offset, byte alignment)
        {
            // If the array is invalid
            if (input is null || input.Length == 0)
                return false;

            // If already at the end of the array
            if (offset >= input.Length)
                return false;

            // Align the stream position
            while (offset % alignment != 0 && offset < input.Length)
            {
                offset++;
            }

            // Return if the alignment completed
            return offset % alignment == 0;
        }

        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero
        /// </summary>
        public static bool IsNullOrEmpty(this Array? array)
        {
            return array is null || array.Length == 0;
        }
    }
}
