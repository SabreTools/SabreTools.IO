using System;
using System.Collections.Generic;
using System.Text;
using SabreTools.Numerics.Extensions;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
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
    }
}
