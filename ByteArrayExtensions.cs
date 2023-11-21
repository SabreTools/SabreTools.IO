using System;
using System.Collections.Generic;
using System.Text;

namespace SabreTools.IO
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
            byte[]? buffer = content.ReadBytes(ref offset, 1);
            if (buffer == null)
                return default;

            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static byte[]? ReadBytes(this byte[]? content, ref int offset, int count)
        {
            // If the byte array is invalid, don't do anything
            if (content == null)
                return null;

            // If there's an invalid byte count, don't do anything
            if (count <= 0 || offset >= content.Length)
                return null;

            // Allocate enough space for the data requested
            byte[] buffer = new byte[count];

            // If we have less data left than requested, only read until the end
            if (offset + count >= content.Length)
                count = content.Length - offset;

            // If we have a non-zero count, copy the data into the array
            if (count > 0)
                Array.Copy(content, offset, buffer, 0, Math.Min(count, content.Length - offset));

            // Increment the offset and return
            offset += count;
            return buffer;
        }

        /// <summary>
        /// Read a Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 1);
            if (buffer == null)
                return default;

            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static char ReadChar(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 1);
            if (buffer == null)
                return default;

            return (char)buffer[0];
        }

        /// <summary>
        /// Read a Int16 and increment the pointer to an array
        /// </summary>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 2);
            if (buffer == null)
                return default;

            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a Int16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static short ReadInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 2);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 2);
            if (buffer == null)
                return default;

            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 2);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt32(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 4);
            if (buffer == null)
                return default;

            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Int32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static int ReadInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 4);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 4);
            if (buffer == null)
                return default;

            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 4);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 and increment the pointer to an array
        /// </summary>
        public static long ReadInt64(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 8);
            if (buffer == null)
                return default;

            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static long ReadInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 8);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 8);
            if (buffer == null)
                return default;

            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 8);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuid(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 16);
            if (buffer == null)
                return default;

            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid in big-endian format and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[]? buffer = content.ReadBytes(ref offset, 16);
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadString(this byte[] content, ref int offset) => content.ReadString(ref offset, Encoding.Default);

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadString(this byte[] content, ref int offset, Encoding encoding)
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes(new char[] { '\0' });
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
    }
}