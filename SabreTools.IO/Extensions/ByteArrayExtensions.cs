using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.IO.Matching;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero
        /// </summary>
        public static bool IsNullOrEmpty(this Array? array)
        {
            return array == null || array.Length == 0;
        }

        #region Matching

        /// <summary>
        /// Find all positions of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static List<int> FindAllPositions(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return FindAllPositions(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find all positions of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static List<int> FindAllPositions(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // Get the outgoing list
            List<int> positions = [];

            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return positions;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return positions;

            // Normalize the end value, if necessary
            if (end == -1)
                end = stack.Length;

            // Validate the start and end values
            if (start < 0 || start >= stack.Length)
                return positions;
            if (end < -1 || end < start || end > stack.Length)
                return positions;

            // Loop while there is data to check
            while (start < end)
            {
                // Create a new matcher for this segment
                var matcher = new ContentMatch(needle, start, end);

                // Get the next matching position
                int position = matcher.Match(stack, reverse: false);
                if (position < 0)
                    break;

                // Append the position and reset the start index
                positions.Add(position);
                start = position + 1;
            }

            return positions;
        }

        /// <summary>
        /// Find the first position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int FirstPosition(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return FirstPosition(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find the first position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int FirstPosition(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return -1;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return -1;

            var matcher = new ContentMatch(needle, start, end);
            return matcher.Match(stack, reverse: false);
        }

        /// <summary>
        /// Find the last position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int LastPosition(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return LastPosition(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find the last position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int LastPosition(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return -1;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return -1;

            var matcher = new ContentMatch(needle, start, end);
            return matcher.Match(stack, reverse: true);
        }

        /// <summary>
        /// Check if a byte array exactly matches another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EqualsExactly(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return EqualsExactly(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array exactly matches another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EqualsExactly(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is not the exact length of the stack
            if (needle.Length != stack.Length)
                return false;

            return FirstPosition(stack, needle, start: 0, end: 1) == 0;
        }

        /// <summary>
        /// Check if a byte array starts with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool StartsWith(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return StartsWith(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array starts with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool StartsWith(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return false;

            return FirstPosition(stack, needle, start: 0, end: 1) > -1;
        }

        /// <summary>
        /// Check if a byte array ends with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EndsWith(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return EndsWith(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array ends with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EndsWith(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return false;

            return FirstPosition(stack, needle, start: stack.Length - needle.Length) > -1;
        }

        #endregion

        #region Strings

        /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        public static string? ToHexString(this byte[]? bytes)
        {
            // If we get null in, we send null out
            if (bytes == null)
                return null;

            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", string.Empty).ToLowerInvariant();
        }

        /// <summary>
        /// Convert a hex string to a byte array
        /// </summary>
        public static byte[]? FromHexString(this string? hex)
        {
            // If we get null in, we send null out
            if (string.IsNullOrEmpty(hex))
                return null;

            try
            {
                int chars = hex!.Length;
                byte[] bytes = new byte[chars / 2];
                for (int i = 0; i < chars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }

                return bytes;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read string data from a byte array
        /// </summary>
        /// <param name="charLimit">Number of characters needed to be a valid string, default 5</param>
        /// <returns>String list containing the requested data, null on error</returns>
#if NET5_0_OR_GREATER
        /// <remarks>This reads both Latin1 and UTF-16 strings from the input data</remarks>
#else
        /// <remarks>This reads both ASCII and UTF-16 strings from the input data</remarks>
#endif
        public static List<string>? ReadStringsFrom(this byte[]? input, int charLimit = 5)
        {
            // Validate the data
            if (input == null || input.Length == 0)
                return null;

#if NET5_0_OR_GREATER
            // Check for Latin1 strings
            var asciiStrings = input.ReadStringsWithEncoding(charLimit, Encoding.Latin1);
#else
            // Check for ASCII strings
            var asciiStrings = input.ReadStringsWithEncoding(charLimit, Encoding.ASCII);
#endif

            // Check for Unicode strings
            // We are limiting the check for Unicode characters with a second byte of 0x00 for now
            var unicodeStrings = input.ReadStringsWithEncoding(charLimit, Encoding.Unicode);

            // Ignore duplicate strings across encodings
            List<string> sourceStrings = [.. asciiStrings, .. unicodeStrings];

            // Sort the strings and return
            sourceStrings.Sort();
            return sourceStrings;
        }

        /// <summary>
        /// Read string data from a byte array with an encoding
        /// </summary>
        /// <param name="bytes">Byte array representing the source data</param>
        /// <param name="charLimit">Number of characters needed to be a valid string</param>
        /// <param name="encoding">Character encoding to use for checking</param>
        /// <returns>String list containing the requested data, empty on error</returns>
        /// <remarks>Characters with the higher bytes set are unused</remarks>
#if NET20
        public static List<string> ReadStringsWithEncoding(this byte[]? bytes, int charLimit, Encoding encoding)
#else
        public static HashSet<string> ReadStringsWithEncoding(this byte[]? bytes, int charLimit, Encoding encoding)
#endif
        {
            if (bytes == null || bytes.Length == 0)
                return [];
            if (charLimit <= 0 || charLimit > bytes.Length)
                return [];

            // Short-circuit for some encoding types
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return bytes.ReadAsciiStrings(charLimit);
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.Latin1, 1);
#endif
            else if (encoding.IsSingleByte)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, encoding, 1);
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.Unicode, 2);
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.BigEndianUnicode, 2);
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.UTF32, 4);

            // Create the string set to return
#if NET20
            var strings = new List<string>();
#else
            var strings = new HashSet<string>();
#endif

            // Open the text reader with the correct encoding
            using var ms = new MemoryStream(bytes);
            using var reader = new StreamReader(ms, encoding);

            // Create a string builder for the loop
            var sb = new StringBuilder();

            // Check for strings
            long lastOffset = 0;
            while (!reader.EndOfStream)
            {
                // Read the next character from the stream
                char c = (char)reader.Read();

                // If the character is invalid
                if (char.IsControl(c) || (c & 0xFFFFFF00) != 0)
                {
                    // Seek to the end of the last found string
                    string str = sb.ToString();
                    lastOffset += encoding.GetByteCount(str) + 1;
                    ms.Seek(lastOffset, SeekOrigin.Begin);
                    reader.DiscardBufferedData();

                    // If there is no cached string
                    if (str.Length == 0)
                        continue;

                    // Add the string if long enough
                    if (str.Length >= charLimit)
                        strings.Add(str);

                    // Clear the builder and continue
#if NET20 || NET35
                    sb = new();
#else
                    sb.Clear();
#endif
                    continue;
                }

                // Otherwise, add the character to the builder and continue
                sb.Append(c);
            }

            // Handle any remaining data
            if (sb.Length >= charLimit)
                strings.Add(sb.ToString());

            return strings;
        }

        /// <summary>
        /// Read string data from a byte array using an encoding with a fixed width
        /// </summary>
        /// <param name="bytes">Byte array representing the source data</param>
        /// <param name="charLimit">Number of characters needed to be a valid string</param>
        /// <param name="encoding">Character encoding to use for checking</param>
        /// <param name="width">Character width of the encoding</param>
        /// <returns>String list containing the requested data, empty on error</returns>
        /// <remarks>Characters with the higher bytes set are unused</remarks>
#if NET20
        private static List<string> ReadFixedWidthEncodingStrings(this byte[] bytes, int charLimit, Encoding encoding, int width)
#else
        private static HashSet<string> ReadFixedWidthEncodingStrings(this byte[] bytes, int charLimit, Encoding encoding, int width)
#endif
        {
            if (charLimit <= 0 || charLimit > bytes.Length)
                return [];

            // Create the string set to return
#if NET20
            var strings = new List<string>();
#else
            var strings = new HashSet<string>();
#endif

            // Create a string builder for the loop
            var sb = new StringBuilder();

            // Check for strings
            int offset = 0;
            while (offset <= bytes.Length - width)
            {
                // Read the next character from the stream
                char c = encoding.GetChars(bytes, offset, width)[0];
                offset += width;

                // If the character is invalid
                if (char.IsControl(c) || (c & 0xFFFFFF00) != 0)
                {
                    // Pretend only one byte was read
                    offset -= width - 1;

                    // If there is no cached string
                    if (sb.Length == 0)
                        continue;

                    // Add the string if long enough
                    if (sb.Length >= charLimit)
                        strings.Add(sb.ToString());

                    // Clear the builder and continue
#if NET20 || NET35
                    sb = new();
#else
                    sb.Clear();
#endif
                    continue;
                }

                // Otherwise, add the character to the builder and continue
                sb.Append(c);
            }

            // Handle any remaining data
            if (sb.Length >= charLimit)
                strings.Add(sb.ToString());

            return strings;
        }

        /// <summary>
        /// Read string data from a byte array using ASCII encoding
        /// </summary>
        /// <param name="bytes">Byte array representing the source data</param>
        /// <param name="charLimit">Number of characters needed to be a valid string</param>
        /// <returns>String list containing the requested data, empty on error</returns>
        /// <remarks>Handling for 7-bit ASCII needs to be done differently than other fixed-width encodings</remarks>
#if NET20
        private static List<string> ReadAsciiStrings(this byte[] bytes, int charLimit)
#else
        private static HashSet<string> ReadAsciiStrings(this byte[] bytes, int charLimit)
#endif
        {
            if (charLimit <= 0 || charLimit > bytes.Length)
                return [];

            // Create the string set to return
#if NET20
            var strings = new List<string>();
#else
            var strings = new HashSet<string>();
#endif

            // Create a string builder for the loop
            var sb = new StringBuilder();

            // Check for strings
            int offset = 0;
            while (offset < bytes.Length)
            {
                // Read the next character from the stream
                char c = bytes.ReadChar(ref offset);

                // If the character is invalid
                if (char.IsControl(c) || c > 0x7F)
                {
                    // If there is no cached string
                    if (sb.Length == 0)
                        continue;

                    // Add the string if long enough
                    if (sb.Length >= charLimit)
                        strings.Add(sb.ToString());

                    // Clear the builder and continue
#if NET20 || NET35
                    sb = new();
#else
                    sb.Clear();
#endif
                    continue;
                }

                // Otherwise, add the character to the builder and continue
                sb.Append(c);
            }

            // Handle any remaining data
            if (sb.Length >= charLimit)
                strings.Add(sb.ToString());

            return strings;
        }

        #endregion
    }
}
