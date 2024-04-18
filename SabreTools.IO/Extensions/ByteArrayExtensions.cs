using System;
using System.Collections.Generic;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static byte ReadByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static byte ReadByteValue(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static byte[] ReadBytes(this byte[] content, ref int offset, int count)
            => ReadToBuffer(content, ref offset, count);

        /// <summary>
        /// Read a Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static char ReadChar(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read a Int16 and increment the pointer to an array
        /// </summary>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a Int16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static short ReadInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt32(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Int32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static int ReadInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 and increment the pointer to an array
        /// </summary>
        public static long ReadInt64(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static long ReadInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuid(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid in big-endian format and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read a Int128 and increment the pointer to an array
        /// </summary>
        public static Int128 ReadInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return new Int128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a Int128 in big-endian format and increment the pointer to an array
        /// </summary>
        public static Int128 ReadInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new Int128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        public static UInt128 ReadUInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return new UInt128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a UInt128 in big-endian format and increment the pointer to an array
        /// </summary>
        public static UInt128 ReadUInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new UInt128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the byte array
        /// </summary>
        public static string? ReadString(this byte[] content, ref int offset)
            => content.ReadString(ref offset, Encoding.Default);

        /// <summary>
        /// Read a null-terminated string from the byte array
        /// </summary>
        public static string? ReadString(this byte[] content, ref int offset, Encoding encoding)
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes(['\0']);
            int charWidth = nullTerminator.Length;

            var keyChars = new List<char>();
            while (offset < content.Length)
            {
                char c = encoding.GetChars(content, offset, charWidth)[0];
                keyChars.Add(c);
                offset += charWidth;

                if (c == '\0')
                    break;
            }

            return new string([.. keyChars]).TrimEnd('\0');
        }

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset)
            => content.ReadQuotedString(ref offset, Encoding.Default);

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset, Encoding encoding)
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes(['\0']);
            int charWidth = nullTerminator.Length;

            var keyChars = new List<char>();
            bool openQuote = false;
            while (offset < content.Length)
            {
                char c = encoding.GetChars(content, offset, charWidth)[0];
                keyChars.Add(c);
                offset += charWidth;

                // If we have a quote, flip the flag
                if (c == '"')
                    openQuote = !openQuote;

                // If we have a newline not in a quoted string, exit the loop
                else if (c == (byte)'\n' && !openQuote)
                    break;
            }

            return new string([.. keyChars]).TrimEnd();
        }

        /// <summary>
        /// Read a number of bytes from the current byte array to a buffer
        /// </summary>
        private static byte[] ReadToBuffer(byte[] content, ref int offset, int length)
        {
            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // If there are not enough bytes
            if (offset + length >= content.Length)
                throw new System.IO.EndOfStreamException(nameof(content));

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            Array.Copy(content, offset, buffer, 0, length);
            offset += length;

            return buffer;
        }
    }
}