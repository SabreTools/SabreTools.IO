using System;
using System.Collections.Generic;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static byte ReadByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 1);
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
            => ReadExactlyToBuffer(content, ref offset, count);

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ReadBytesBigEndian(this byte[] content, ref int offset, int count)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>
        /// Read an Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static char ReadChar(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt16LittleEndian(ref offset);
            else
                return content.ReadInt16BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return (short)(buffer[1]
                        | (buffer[0] << 8));
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return (short)(buffer[0]
                        | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadUInt16(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt16LittleEndian(ref offset);
            else
                return content.ReadUInt16BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return (ushort)(buffer[1]
                         | (buffer[0] << 8));
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return (ushort)(buffer[0]
                         | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadWORD(this byte[] content, ref int offset)
            => content.ReadUInt16(ref offset);

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadWORDBigEndian(this byte[] content, ref int offset)
            => content.ReadUInt16BigEndian(ref offset);

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadWORDLittleEndian(this byte[] content, ref int offset)
            => content.ReadUInt16LittleEndian(ref offset);

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return BitConverter.ToHalf(buffer, 0);
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToHalf(buffer, 0);
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt24LittleEndian(ref offset);
            else
                return content.ReadInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return (int)(buffer[2]
                      | (buffer[1] << 8)
                      | (buffer[0] << 16));
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt24LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt24LittleEndian(ref offset);
            else
                return content.ReadUInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return (uint)(buffer[2]
                       | (buffer[1] << 8)
                       | (buffer[0] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt24LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16));
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt32(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt32LittleEndian(ref offset);
            else
                return content.ReadInt32BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return (int)(buffer[3]
                      | (buffer[2] << 8)
                      | (buffer[1] << 16)
                      | (buffer[0] << 24));
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16)
                      | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt32(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt32LittleEndian(ref offset);
            else
                return content.ReadUInt32BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return (uint)(buffer[3]
                       | (buffer[2] << 8)
                       | (buffer[1] << 16)
                       | (buffer[0] << 24));
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16)
                       | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadDWORD(this byte[] content, ref int offset)
            => content.ReadUInt32(ref offset);

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadDWORDBigEndian(this byte[] content, ref int offset)
            => content.ReadUInt32BigEndian(ref offset);

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadDWORDLittleEndian(this byte[] content, ref int offset)
            => content.ReadUInt32LittleEndian(ref offset);

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static float ReadSingle(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt48LittleEndian(ref offset);
            else
                return content.ReadInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return ((long)buffer[5] << 0)
                 | ((long)buffer[4] << 8)
                 | ((long)buffer[3] << 16)
                 | ((long)buffer[2] << 24)
                 | ((long)buffer[1] << 32)
                 | ((long)buffer[0] << 40);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt48LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt48LittleEndian(ref offset);
            else
                return content.ReadUInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return ((ulong)buffer[5] << 0)
                 | ((ulong)buffer[4] << 8)
                 | ((ulong)buffer[3] << 16)
                 | ((ulong)buffer[2] << 24)
                 | ((ulong)buffer[1] << 32)
                 | ((ulong)buffer[0] << 40);
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt48LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt64(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt64LittleEndian(ref offset);
            else
                return content.ReadInt64BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return ((long)buffer[7] << 0)
                 | ((long)buffer[6] << 8)
                 | ((long)buffer[5] << 16)
                 | ((long)buffer[4] << 24)
                 | ((long)buffer[3] << 32)
                 | ((long)buffer[2] << 40)
                 | ((long)buffer[1] << 48)
                 | ((long)buffer[0] << 56);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40)
                 | ((long)buffer[6] << 48)
                 | ((long)buffer[7] << 56);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt64(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt64LittleEndian(ref offset);
            else
                return content.ReadUInt64BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return ((ulong)buffer[7] << 0)
                 | ((ulong)buffer[6] << 8)
                 | ((ulong)buffer[5] << 16)
                 | ((ulong)buffer[4] << 24)
                 | ((ulong)buffer[3] << 32)
                 | ((ulong)buffer[2] << 40)
                 | ((ulong)buffer[1] << 48)
                 | ((ulong)buffer[0] << 56);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40)
                 | ((ulong)buffer[6] << 48)
                 | ((ulong)buffer[7] << 56);
        }

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadQWORD(this byte[] content, ref int offset)
            => content.ReadUInt64(ref offset);

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadQWORDBigEndian(this byte[] content, ref int offset)
            => content.ReadUInt64BigEndian(ref offset);

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadQWORDLittleEndian(this byte[] content, ref int offset)
            => content.ReadUInt64LittleEndian(ref offset);

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static double ReadDouble(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal ReadDecimal(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid ReadGuid(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
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
            else if (encoding.Equals(Encoding.UTF8))
                return content.ReadNullTerminatedUTF8String(ref offset);
            else if (encoding.Equals(Encoding.Unicode))
                return content.ReadNullTerminatedUnicodeString(ref offset);
            else if (encoding.Equals(Encoding.UTF32))
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
        /// Read a <typeparamref name="T"/> from the stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static T? ReadType<T>(this byte[] content, ref int offset)
            => (T?)content.ReadType(ref offset, typeof(T));

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static object? ReadType(this byte[] content, ref int offset, Type type)
        {
            // Handle special struct cases
            if (type == typeof(Guid))
                return content.ReadGuid(ref offset);
#if NET6_0_OR_GREATER
            else if (type == typeof(Half))
                return content.ReadHalf(ref offset);
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return content.ReadInt128(ref offset);
            else if (type == typeof(UInt128))
                return content.ReadUInt128(ref offset);
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(content, ref offset, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(content, ref offset, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(content, ref offset, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadNormalType(byte[] content, ref int offset, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = ReadExactlyToBuffer(content, ref offset, typeSize);

                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var data = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
                handle.Free();

                return data;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadComplexType(byte[] content, ref int offset, Type type)
        {
            try
            {
                // Try to create an instance of the type
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                    return null;

                // Get the layout information
                var layoutAttr = MarshalHelpers.GetAttribute<StructLayoutAttribute>(type);
                LayoutKind layoutKind = MarshalHelpers.DetermineLayoutKind(layoutAttr, type);
                Encoding encoding = MarshalHelpers.DetermineEncoding(layoutAttr);

                // Cache the current offset
                int currentOffset = offset;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        offset = currentOffset + fieldOffset?.Value ?? 0;
                    }

                    SetField(content, ref offset, encoding, fields, instance, fi);
                }

                return instance;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set a single field on an object
        /// </summary>
        private static void SetField(byte[] content, ref int offset, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                var value = ReadStringType(content, ref offset, encoding, instance, fi);
                fi.SetValue(instance, value);
            }
            else if (fi.FieldType.IsArray)
            {
                var value = ReadArrayType(content, ref offset, fields, instance, fi);
                if (value.GetType() == fi.FieldType)
                    fi.SetValue(instance, value);
                else
                    fi.SetValue(instance, Convert.ChangeType(value, fi.FieldType));
            }
            else
            {
                var value = content.ReadType(ref offset, fi.FieldType);
                fi.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Read an array type field for an object
        /// </summary>
        private static Array ReadArrayType(byte[] content, ref int offset, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);
            if (marshalAsAttr == null)
                return new object[0];

            // Get the number of elements expected
            int elementCount = MarshalHelpers.GetArrayElementCount(marshalAsAttr, fields, instance);
            if (elementCount < 0)
                return new object[0];

            // Get the item type for the array
            Type elementType = fi.FieldType.GetElementType() ?? typeof(object);

            // Loop through and build the array
            Array arr = Array.CreateInstance(elementType, elementCount);
            for (int i = 0; i < elementCount; i++)
            {
                var value = ReadType(content, ref offset, elementType);
                if (value != null && elementType.IsEnum)
                    arr.SetValue(Enum.ToObject(elementType, value), i);
                else
                    arr.SetValue(value, i);
            }

            // Return the built array
            return arr;
        }

        /// <summary>
        /// Read a string type field for an object
        /// </summary>
        private static string? ReadStringType(byte[] content, ref int offset, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);

            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return content.ReadPrefixedAnsiString(ref offset);

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return content.ReadPrefixedUnicodeString(ref offset);

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = content.ReadBytes(ref offset, byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return content.ReadNullTerminatedAnsiString(ref offset);

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return content.ReadNullTerminatedUTF8String(ref offset);
#endif

                case UnmanagedType.LPWStr:
                    return content.ReadNullTerminatedUnicodeString(ref offset);

                // No other string types are recognized
                default:
                    return null;
            }
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

        /// <summary>
        /// Read a number of bytes from the byte array to a buffer
        /// </summary>
        private static byte[] ReadExactlyToBuffer(byte[] content, ref int offset, int length)
        {
            // If we have an invalid offset
            if (offset < 0 || offset >= content.Length)
                throw new ArgumentOutOfRangeException($"{nameof(offset)} must be between 0 and {content.Length}, {offset} provided");

            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value, {length} requested");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // If there are not enough bytes
            if (offset + length > content.Length)
                throw new System.IO.EndOfStreamException($"Requested to read {length} bytes from {nameof(content)}, {content.Length - offset} returned");

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            Array.Copy(content, offset, buffer, 0, length);
            offset += length;

            return buffer;
        }
    }
}
