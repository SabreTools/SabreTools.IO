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
            return content[offset++];
        }

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
#if NET48
        public static byte[] ReadBytes(this byte[] content, ref int offset, int count)
#else
        public static byte[]? ReadBytes(this byte[] content, ref int offset, int count)
#endif
        {
            // If there's an invalid byte count, don't do anything
            if (count <= 0)
                return null;

            byte[] buffer = new byte[count];
            Array.Copy(content, offset, buffer, 0, Math.Min(count, content.Length - offset));
            offset += count;
            return buffer;
        }

        /// <summary>
        /// Read a Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            return (sbyte)content[offset++];
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static char ReadChar(this byte[] content, ref int offset)
        {
            return (char)content[offset++];
        }

        /// <summary>
        /// Read a Int16 and increment the pointer to an array
        /// </summary>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            short value = BitConverter.ToInt16(content, offset);
            offset += 2;
            return value;
        }

        /// <summary>
        /// Read a Int16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static short ReadInt16BigEndian(this byte[] content, ref int offset)
        {
#if NET48
            byte[] buffer = content.ReadBytes(ref offset, 2);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 2);
#endif
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
            ushort value = BitConverter.ToUInt16(content, offset);
            offset += 2;
            return value;
        }

        /// <summary>
        /// Read a UInt16 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 2);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 2);
#endif
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
            int value = BitConverter.ToInt32(content, offset);
            offset += 4;
            return value;
        }

        /// <summary>
        /// Read a Int32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static int ReadInt32BigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 4);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 4);
#endif
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
            uint value = BitConverter.ToUInt32(content, offset);
            offset += 4;
            return value;
        }

        /// <summary>
        /// Read a UInt32 in big-endian format and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 4);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 4);
#endif
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
            long value = BitConverter.ToInt64(content, offset);
            offset += 8;
            return value;
        }

        /// <summary>
        /// Read a Int64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static long ReadInt64BigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 8);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 8);
#endif
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
            ulong value = BitConverter.ToUInt64(content, offset);
            offset += 8;
            return value;
        }

        /// <summary>
        /// Read a UInt64 in big-endian format and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 8);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 8);
#endif
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
#if NET48
            byte[] buffer = content.ReadBytes(ref offset, 16);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 16);
#endif
            if (buffer == null)
                return default;

            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid in big-endian format and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            #if NET48
            byte[] buffer = content.ReadBytes(ref offset, 16);
#else
            byte[]? buffer = content.ReadBytes(ref offset, 16);
#endif
            if (buffer == null)
                return default;

            Array.Reverse(buffer);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
#if NET48
        public static string ReadString(this byte[] content, ref int offset) => content.ReadString(ref offset, Encoding.Default);
#else
        public static string? ReadString(this byte[] content, ref int offset) => content.ReadString(ref offset, Encoding.Default);
#endif

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
#if NET48
        public static string ReadString(this byte[] content, ref int offset, Encoding encoding)
#else
        public static string? ReadString(this byte[] content, ref int offset, Encoding encoding)
#endif
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes(new char[] { '\0' });
            int charWidth = nullTerminator.Length;

            List<char> keyChars = new List<char>();
            while (offset < content.Length)
            {
                char c = encoding.GetChars(content, offset, charWidth)[0];
                keyChars.Add(c);
                offset += charWidth;

                if (c == '\0')
                    break;
            }

            return new string(keyChars.ToArray()).TrimEnd('\0');
        }
    }
}