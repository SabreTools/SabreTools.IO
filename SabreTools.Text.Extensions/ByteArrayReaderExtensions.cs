using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.Numerics.Extensions;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
        #region Read Null Terminated

        /// <summary>
        /// Read a null-terminated string from the array
        /// </summary>
        public static string? ReadNullTerminatedString(this byte[] content, ref int offset, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return content.ReadNullTerminatedAnsiString(ref offset);
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return content.ReadNullTerminatedAnsiString(ref offset);
#endif
            else if (encoding.CodePage == Encoding.UTF8.CodePage)
                return content.ReadNullTerminatedUTF8String(ref offset);
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return content.ReadNullTerminatedUnicodeString(ref offset);
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return content.ReadNullTerminatedBigEndianUnicodeString(ref offset);
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
                return content.ReadNullTerminatedUTF32String(ref offset);

            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                if (ch == '\0')
                    break;

                buffer.Add(ch);
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a null-terminated Latin1 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedLatin1String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated UTF-8 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(content, ref offset);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedBigEndianUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(content, ref offset);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(content, ref offset);
            return Encoding.UTF32.GetString(buffer);
        }

        #endregion

        #region Read Prefixed

        /// <summary>
        /// Read a byte-prefixed ASCII string from the byte array
        /// </summary>
        public static string? ReadPrefixedAnsiString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte size = content.ReadByteValue(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a byte-prefixed Latin1 string from the byte array
        /// </summary>
        public static string? ReadPrefixedLatin1String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte size = content.ReadByteValue(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + (size * 2) >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedBigEndianUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + (size * 2) >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size * 2);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        #endregion

        #region Read All Strings

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
            if (input is null || input.Length == 0)
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
            if (bytes is null || bytes.Length == 0)
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

        #endregion

        #region Read With Encoding Helpers

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

        #region Read Until Null Helpers

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                byte next = content.ReadByte(ref offset);
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                ushort next = content.ReadUInt16(ref offset);
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                uint next = content.ReadUInt32(ref offset);
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        #endregion
    }
}
