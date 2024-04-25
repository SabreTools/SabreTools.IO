using System;
using System.Collections.Generic;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    /// <remarks>TODO: Add U/Int48 methods</remarks>
    public static class ByteArrayReaderExtensions
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
            => content.ReadByte(ref offset);

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static byte[] ReadBytes(this byte[] content, ref int offset, int count)
            => ReadToBuffer(content, ref offset, count);

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ReadBytesBigEndian(this byte[] content, ref int offset, int count)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>
        /// Read an Int8 and increment the pointer to an array
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
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
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
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt24(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt24(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt32(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
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
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        public static float ReadSingle(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        public static long ReadInt64(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
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
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        public static double ReadDouble(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        public static decimal ReadDecimal(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);

            int i1 = BitConverter.ToInt32(buffer, 0);
            int i2 = BitConverter.ToInt32(buffer, 4);
            int i3 = BitConverter.ToInt32(buffer, 8);
            int i4 = BitConverter.ToInt32(buffer, 12);

            return new decimal([i1, i2, i3, i4]);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);

            int i1 = BitConverter.ToInt32(buffer, 0);
            int i2 = BitConverter.ToInt32(buffer, 4);
            int i3 = BitConverter.ToInt32(buffer, 8);
            int i4 = BitConverter.ToInt32(buffer, 12);

            return new decimal([i1, i2, i3, i4]);
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
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        public static Int128 ReadInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        public static UInt128 ReadUInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return (UInt128)new BigInteger(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the array
        /// </summary>
        public static string? ReadNullTerminatedString(this byte[] content, ref int offset, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.Equals(Encoding.ASCII))
                return content.ReadNullTerminatedAnsiString(ref offset);
            else if (encoding.Equals(Encoding.Unicode))
                return content.ReadNullTerminatedUnicodeString(ref offset);

            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                buffer.Add(ch);
                if (ch == '\0')
                    break;
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

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return Encoding.ASCII.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated Unicode string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte[] ch = content.ReadBytes(ref offset, 2);
                buffer.AddRange(ch);
                if (ch[0] == '\0' && ch[1] == '\0')
                    break;
            }

            return Encoding.Unicode.GetString([.. buffer]);
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

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the byte array
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset)
            => content.ReadQuotedString(ref offset, Encoding.Default);

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the byte array
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset, Encoding encoding)
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes("\0");
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
        /// Read a <typeparamref name="T"/> from the byte array
        /// </summary>
        public static T? ReadType<T>(this byte[] content, ref int offset)
        {
            int typeSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = ReadToBuffer(content, ref offset, typeSize);

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var data = (T?)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return data;
        }

        /// <summary>
        /// Read a number of bytes from the byte array to a buffer
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
            if (offset + length > content.Length)
                throw new System.IO.EndOfStreamException(nameof(content));

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            Array.Copy(content, offset, buffer, 0, length);
            offset += length;

            return buffer;
        }
    }
}