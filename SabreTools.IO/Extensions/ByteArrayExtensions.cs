using System;
using System.Collections.Generic;
using System.Text;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Defines the maximum number of characters in a string
        /// as used in <see cref="ReadStringsWithEncoding"/> 
        /// </summary>
        private const int MaximumCharactersInString = 64;

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
        /// Read string data from the source
        /// </summary>
        /// <param name="charLimit">Number of characters needed to be a valid string, default 5</param>
        /// <returns>String list containing the requested data, null on error</returns>
        public static List<string>? ReadStringsFrom(this byte[]? input, int charLimit = 5)
        {
            // Validate the data
            if (input == null)
                return null;

            // Check for ASCII strings
            var asciiStrings = input.ReadStringsWithEncoding(charLimit, Encoding.ASCII);

            // Check for UTF-8 strings
            // We are limiting the check for Unicode characters with a second byte of 0x00 for now
            var utf8Strings = input.ReadStringsWithEncoding(charLimit, Encoding.UTF8);

            // Check for Unicode strings
            // We are limiting the check for Unicode characters with a second byte of 0x00 for now
            var unicodeStrings = input.ReadStringsWithEncoding(charLimit, Encoding.Unicode);

            // Ignore duplicate strings across encodings
            List<string> sourceStrings = [.. asciiStrings, .. utf8Strings, .. unicodeStrings];

            // Sort the strings and return
            sourceStrings.Sort();
            return sourceStrings;
        }

        /// <summary>
        /// Read string data from the source with an encoding
        /// </summary>
        /// <param name="bytes">Byte array representing the source data</param>
        /// <param name="charLimit">Number of characters needed to be a valid string</param>
        /// <param name="encoding">Character encoding to use for checking</param>
        /// <returns>String list containing the requested data, empty on error</returns>
        /// <remarks>
        /// This method has a couple of notable implementation details:
        /// - Strings can only have a maximum of 64 characters
        /// - Characters that fall outside of the extended ASCII set will be unused
        /// </remarks>
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

            // Create the string set to return
#if NET20
            var strings = new List<string>();
#else
            var strings = new HashSet<string>();
#endif

            // Check for strings
            int index = 0;
            while (index < bytes.Length)
            {
                // Get the maximum number of characters
                int maxChars = encoding.GetMaxCharCount(bytes.Length - index);
                int maxBytes = encoding.GetMaxByteCount(Math.Min(MaximumCharactersInString, maxChars));

                // Read the longest string allowed
                int maxRead = Math.Min(maxBytes, bytes.Length - index);
                string temp = encoding.GetString(bytes, index, maxRead);
                char[] tempArr = temp.ToCharArray();

                // Ignore empty strings
                if (temp.Length == 0)
                {
                    index++;
                    continue;
                }

                // Find the first instance of a control character
                int endOfString = Array.FindIndex(tempArr, c => char.IsControl(c) || (c & 0xFF00) != 0);
                if (endOfString > -1)
                    temp = temp.Substring(0, endOfString);

                // Otherwise, just add the string if long enough
                if (temp.Length >= charLimit)
                    strings.Add(temp);

                // Increment and continue
                index += Math.Max(encoding.GetByteCount(temp), 1);
            }

            return strings;
        }
    }
}
