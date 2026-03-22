using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.Numerics.Extensions;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for Streams
    /// </summary>
    public static class StreamReaderExtensions
    {
        #region Read Null Terminated

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadNullTerminatedString(this Stream stream, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return stream.ReadNullTerminatedAnsiString();
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return stream.ReadNullTerminatedLatin1String();
#endif
            else if (encoding.CodePage == Encoding.UTF8.CodePage)
                return stream.ReadNullTerminatedUTF8String();
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return stream.ReadNullTerminatedUnicodeString();
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return stream.ReadNullTerminatedBigEndianUnicodeString();
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
                return stream.ReadNullTerminatedUTF32String();

            if (stream.Position >= stream.Length)
                return null;

            List<byte> buffer = [];
            while (stream.Position < stream.Length)
            {
                byte ch = stream.ReadByteValue();
                if (ch == '\0')
                    break;

                buffer.Add(ch);
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the stream
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(stream);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a null-terminated Latin1 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedLatin1String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(stream);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated UTF-8 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(stream);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(stream);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the stream
        /// </summary>
        public static string? ReadNullTerminatedBigEndianUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(stream);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(stream);
            return Encoding.UTF32.GetString(buffer);
        }

        #endregion

        #region Read Prefixed

        /// <summary>
        /// Read a byte-prefixed ASCII string from the stream
        /// </summary>
        public static string? ReadPrefixedAnsiString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte size = stream.ReadByteValue();
            if (stream.Position + size >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a byte-prefixed Latin1 string from the stream
        /// </summary>
        public static string? ReadPrefixedLatin1String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte size = stream.ReadByteValue();
            if (stream.Position + size >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the stream
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            ushort size = stream.ReadUInt16();
            if (stream.Position + (size * 2) >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the stream
        /// </summary>
        public static string? ReadPrefixedBigEndianUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            ushort size = stream.ReadUInt16();
            if (stream.Position + (size * 2) >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size * 2);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        #endregion

        #region Read Until Null Helpers

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                byte next = stream.ReadByteValue();
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                ushort next = stream.ReadUInt16();
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                uint next = stream.ReadUInt32();
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        #endregion
    }
}
