using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        public static List<string>? ReadStringsFrom(this byte[]? input, int charLimit = 5)
        {
            // Validate the data
            if (input == null || input.Length == 0)
                return null;

            // Check for ASCII strings
            var asciiStrings = input.ReadStringsWithEncoding(charLimit, Encoding.ASCII);

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
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.ASCII, 1);
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.Latin1, 1);
#endif
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return bytes.ReadFixedWidthEncodingStrings(charLimit, Encoding.Unicode, 2);
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
                if (char.IsControl(c) || (c & 0xFF00) != 0)
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

        #region Fixed Byte-Width Encoding Helpers

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
                        string str = sb.ToString();
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

        #endregion
    }
}
