using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for BinaryReader
    /// </summary>
    public static class BinaryReaderExtensions
    {
        #region Read Null Terminated

        /// <summary>
        /// Read a null-terminated string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return reader.ReadNullTerminatedAnsiString();
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return reader.ReadNullTerminatedLatin1String();
#endif
            else if (encoding.CodePage == Encoding.UTF8.CodePage)
                return reader.ReadNullTerminatedUTF8String();
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return reader.ReadNullTerminatedUnicodeString();
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return reader.ReadNullTerminatedBigEndianUnicodeString();
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
                return reader.ReadNullTerminatedUTF32String();

            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            List<byte> buffer = [];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte ch = reader.ReadByte();
                if (ch == '\0')
                    break;

                buffer.Add(ch);
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a null-terminated Latin1 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedLatin1String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated UTF-8 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(reader);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedBigEndianUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(reader);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(reader);
            return Encoding.UTF32.GetString(buffer);
        }

        #endregion

        #region Read Prefixed

        /// <summary>
        /// Read a byte-prefixed ASCII string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedAnsiString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte size = reader.ReadByte();
            if (reader.BaseStream.Position + size >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            ushort size = reader.ReadUInt16();
            if (reader.BaseStream.Position + (size * 2) >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedBigEndianUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            ushort size = reader.ReadUInt16();
            if (reader.BaseStream.Position + (size * 2) >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size * 2);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        #endregion

        #region Read Until Null Helpers

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte next = reader.ReadByte();
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                ushort next = reader.ReadUInt16();
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                uint next = reader.ReadUInt32();
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        #endregion
    }
}
