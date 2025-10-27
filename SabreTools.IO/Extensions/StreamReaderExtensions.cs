using System;
using System.Collections.Generic;
using System.IO;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.IO.Numerics;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for Streams
    /// </summary>
    public static class StreamReaderExtensions
    {
        #region Exact Read

        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        public static byte ReadByteValue(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt8 ReadByteBothEndian(this Stream stream)
        {
            byte le = stream.ReadByteValue();
            byte be = stream.ReadByteValue();
            return new BothUInt8(le, be);
        }

        /// <summary>
        /// Read a UInt8[] from the stream
        /// </summary>
        public static byte[] ReadBytes(this Stream stream, int count)
            => ReadExactlyToBuffer(stream, count);

        /// <summary>
        /// Read an Int8 from the stream
        /// </summary>
        public static sbyte ReadSByte(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt8 ReadSByteBothEndian(this Stream stream)
        {
            sbyte le = stream.ReadSByte();
            sbyte be = stream.ReadSByte();
            return new BothInt8(le, be);
        }

        /// <summary>
        /// Read a Char from the stream
        /// </summary>
        public static char ReadChar(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static short ReadInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt16LittleEndian();
            else
                return stream.ReadInt16BigEndian();
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (short)(buffer[1]
                        | (buffer[0] << 8));
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (short)(buffer[0]
                        | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a Int16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt16 ReadInt16BothEndian(this Stream stream)
        {
            short le = stream.ReadInt16LittleEndian();
            short be = stream.ReadInt16BigEndian();
            return new BothInt16(le, be);
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadUInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt16LittleEndian();
            else
                return stream.ReadUInt16BigEndian();
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (ushort)(buffer[1]
                         | (buffer[0] << 8));
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (ushort)(buffer[0]
                         | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadUInt16BothEndian(this Stream stream)
        {
            ushort le = stream.ReadUInt16LittleEndian();
            ushort be = stream.ReadUInt16BigEndian();
            return new BothUInt16(le, be);
        }

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadWORD(this Stream stream)
            => stream.ReadUInt16();

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadWORDBigEndian(this Stream stream)
            => stream.ReadUInt16BigEndian();

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadWORDLittleEndian(this Stream stream)
            => stream.ReadUInt16LittleEndian();

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadWORDBothEndian(this Stream stream)
            => stream.ReadUInt16BothEndian();

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return BitConverter.ToHalf(buffer, 0);
        }

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            Array.Reverse(buffer);
            return BitConverter.ToHalf(buffer, 0);
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt24LittleEndian();
            else
                return stream.ReadInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (int)(buffer[2]
                      | (buffer[1] << 8)
                      | (buffer[0] << 16));
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt24LittleEndian();
            else
                return stream.ReadUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (uint)(buffer[2]
                       | (buffer[1] << 8)
                       | (buffer[0] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16));
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt32LittleEndian();
            else
                return stream.ReadInt32BigEndian();
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (int)(buffer[3]
                      | (buffer[2] << 8)
                      | (buffer[1] << 16)
                      | (buffer[0] << 24));
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16)
                      | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a Int32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt32 ReadInt32BothEndian(this Stream stream)
        {
            int le = stream.ReadInt32LittleEndian();
            int be = stream.ReadInt32BigEndian();
            return new BothInt32(le, be);
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt32LittleEndian();
            else
                return stream.ReadUInt32BigEndian();
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (uint)(buffer[3]
                       | (buffer[2] << 8)
                       | (buffer[1] << 16)
                       | (buffer[0] << 24));
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16)
                       | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadUInt32BothEndian(this Stream stream)
        {
            uint le = stream.ReadUInt32LittleEndian();
            uint be = stream.ReadUInt32BigEndian();
            return new BothUInt32(le, be);
        }

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadDWORD(this Stream stream)
            => stream.ReadUInt32();

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadDWORDBigEndian(this Stream stream)
            => stream.ReadUInt32BigEndian();

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadDWORDLittleEndian(this Stream stream)
            => stream.ReadUInt32LittleEndian();

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadDWORDBothEndian(this Stream stream)
            => stream.ReadUInt32BothEndian();

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static float ReadSingle(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt48LittleEndian();
            else
                return stream.ReadInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((long)buffer[5] << 0)
                 | ((long)buffer[4] << 8)
                 | ((long)buffer[3] << 16)
                 | ((long)buffer[2] << 24)
                 | ((long)buffer[1] << 32)
                 | ((long)buffer[0] << 40);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt48LittleEndian();
            else
                return stream.ReadUInt48BigEndian();
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((ulong)buffer[5] << 0)
                 | ((ulong)buffer[4] << 8)
                 | ((ulong)buffer[3] << 16)
                 | ((ulong)buffer[2] << 24)
                 | ((ulong)buffer[1] << 32)
                 | ((ulong)buffer[0] << 40);
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt64LittleEndian();
            else
                return stream.ReadInt64BigEndian();
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
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
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
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
        /// Read a Int64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt64 ReadInt64BothEndian(this Stream stream)
        {
            long le = stream.ReadInt64LittleEndian();
            long be = stream.ReadInt64BigEndian();
            return new BothInt64(le, be);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt64LittleEndian();
            else
                return stream.ReadUInt64BigEndian();
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
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
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
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
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadUInt64BothEndian(this Stream stream)
        {
            ulong le = stream.ReadUInt64LittleEndian();
            ulong be = stream.ReadUInt64BigEndian();
            return new BothUInt64(le, be);
        }

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadQWORD(this Stream stream)
            => stream.ReadUInt64();

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadQWORDBigEndian(this Stream stream)
            => stream.ReadUInt64BigEndian();

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadQWORDLittleEndian(this Stream stream)
            => stream.ReadUInt64LittleEndian();

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadQWORDBothEndian(this Stream stream)
            => stream.ReadUInt64BothEndian();

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static double ReadDouble(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal ReadDecimal(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid ReadGuid(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);
            return (UInt128)new BigInteger(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadNullTerminatedString(this Stream stream, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.Equals(Encoding.ASCII))
                return stream.ReadNullTerminatedAnsiString();
            else if (encoding.Equals(Encoding.UTF8))
                return stream.ReadNullTerminatedUTF8String();
            else if (encoding.Equals(Encoding.Unicode))
                return stream.ReadNullTerminatedUnicodeString();
            else if (encoding.Equals(Encoding.UTF32))
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
        /// Read a null-terminated UTF-32 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(stream);
            return Encoding.UTF32.GetString(buffer);
        }

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
        public static T? ReadType<T>(this Stream stream)
            => (T?)stream.ReadType(typeof(T));

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
        public static object? ReadType(this Stream stream, Type type)
        {
            // Handle special struct cases
            if (type == typeof(Guid))
                return stream.ReadGuid();
#if NET6_0_OR_GREATER
            else if (type == typeof(Half))
                return stream.ReadHalf();
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return stream.ReadInt128();
            else if (type == typeof(UInt128))
                return stream.ReadUInt128();
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(stream, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(stream, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(stream, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadNormalType(Stream stream, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = ReadExactlyToBuffer(stream, typeSize);

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
        private static object? ReadComplexType(Stream stream, Type type)
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
                long currentOffset = stream.Position;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        stream.Seek(currentOffset + fieldOffset?.Value ?? 0, SeekOrigin.Begin);
                    }

                    SetField(stream, encoding, fields, instance, fi);
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
        private static void SetField(Stream stream, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                var value = ReadStringType(stream, encoding, instance, fi);
                fi.SetValue(instance, value);
            }
            else if (fi.FieldType.IsArray)
            {
                var value = ReadArrayType(stream, fields, instance, fi);
                if (value.GetType() == fi.FieldType)
                    fi.SetValue(instance, value);
                else
                    fi.SetValue(instance, Convert.ChangeType(value, fi.FieldType));
            }
            else
            {
                var value = stream.ReadType(fi.FieldType);
                fi.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Read an array type field for an object
        /// </summary>
        private static Array ReadArrayType(Stream stream, FieldInfo[] fields, object instance, FieldInfo fi)
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
                var value = ReadType(stream, elementType);
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
        private static string? ReadStringType(Stream stream, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);

            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return stream.ReadPrefixedAnsiString();

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return stream.ReadPrefixedUnicodeString();

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = stream.ReadBytes(byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return stream.ReadNullTerminatedAnsiString();

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return stream.ReadNullTerminatedUTF8String();
#endif

                case UnmanagedType.LPWStr:
                    return stream.ReadNullTerminatedUnicodeString();

                // No other string types are recognized
                default:
                    return null;
            }
        }

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

        /// <summary>
        /// Read a number of bytes from the stream to a buffer
        /// </summary>
        private static byte[] ReadExactlyToBuffer(Stream stream, int length)
        {
            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value, {length} requested");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            int read = stream.Read(buffer, 0, length);
            if (read < length)
                throw new EndOfStreamException($"Requested to read {length} bytes from {nameof(stream)}, {read} returned");

            return buffer;
        }

        #endregion

        #region Peek Read

        /// <summary>
        /// Peek a UInt8 from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte PeekByte(this Stream stream)
        {
            byte value = stream.ReadByteValue();
            stream.SeekIfPossible(-1, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt8 from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte PeekByteValue(this Stream stream)
            => stream.PeekByte();

        /// <summary>
        /// Peek a UInt8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt8 PeekByteBothEndian(this Stream stream)
        {
            BothUInt8 value = stream.ReadByteBothEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt8[] from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte[] PeekBytes(this Stream stream, int count)
        {
            byte[] value = stream.ReadBytes(count);
            stream.SeekIfPossible(-count, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int8 from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static sbyte PeekSByte(this Stream stream)
        {
            sbyte value = stream.ReadSByte();
            stream.SeekIfPossible(-1, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt8 PeekSByteBothEndian(this Stream stream)
        {
            BothInt8 value = stream.ReadSByteBothEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Char from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static char PeekChar(this Stream stream)
        {
            char value = stream.ReadChar();
            stream.SeekIfPossible(-1, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt16LittleEndian();
            else
                return stream.PeekInt16BigEndian();
        }

        /// <summary>
        /// Peek an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16BigEndian(this Stream stream)
        {
            short value = stream.ReadInt16BigEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16LittleEndian(this Stream stream)
        {
            short value = stream.ReadInt16LittleEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt16 PeekInt16BothEndian(this Stream stream)
        {
            BothInt16 value = stream.ReadInt16BothEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt16LittleEndian();
            else
                return stream.PeekUInt16BigEndian();
        }

        /// <summary>
        /// Peek a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16BigEndian(this Stream stream)
        {
            ushort value = stream.ReadUInt16BigEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16LittleEndian(this Stream stream)
        {
            ushort value = stream.ReadUInt16LittleEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt16 PeekUInt16BothEndian(this Stream stream)
        {
            BothUInt16 value = stream.ReadUInt16BothEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORD(this Stream stream)
            => stream.PeekUInt16();

        /// <summary>
        /// Peek a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORDBigEndian(this Stream stream)
            => stream.PeekUInt16BigEndian();

        /// <summary>
        /// Peek a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORDLittleEndian(this Stream stream)
            => stream.PeekUInt16LittleEndian();

        /// <summary>
        /// Peek a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt16 PeekWORDBothEndian(this Stream stream)
            => stream.PeekUInt16BothEndian();

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Peek a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalf(this Stream stream)
        {
            Half value = stream.ReadHalf();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalfBigEndian(this Stream stream)
        {
            Half value = stream.ReadHalfBigEndian();
            stream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }
#endif

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt24LittleEndian();
            else
                return stream.PeekInt24BigEndian();
        }

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24BigEndian(this Stream stream)
        {
            int value = stream.ReadInt24BigEndian();
            stream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24LittleEndian(this Stream stream)
        {
            int value = stream.ReadInt24LittleEndian();
            stream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt24LittleEndian();
            else
                return stream.PeekUInt24BigEndian();
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24BigEndian(this Stream stream)
        {
            uint value = stream.ReadUInt24BigEndian();
            stream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24LittleEndian(this Stream stream)
        {
            uint value = stream.ReadUInt24LittleEndian();
            stream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt32LittleEndian();
            else
                return stream.PeekInt32BigEndian();
        }

        /// <summary>
        /// Peek an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32BigEndian(this Stream stream)
        {
            int value = stream.ReadInt32BigEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32LittleEndian(this Stream stream)
        {
            int value = stream.ReadInt32LittleEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt32 PeekInt32BothEndian(this Stream stream)
        {
            BothInt32 value = stream.ReadInt32BothEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt32LittleEndian();
            else
                return stream.PeekUInt32BigEndian();
        }

        /// <summary>
        /// Peek a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32BigEndian(this Stream stream)
        {
            uint value = stream.ReadUInt32BigEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32LittleEndian(this Stream stream)
        {
            uint value = stream.ReadUInt32LittleEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt32 PeekUInt32BothEndian(this Stream stream)
        {
            BothUInt32 value = stream.ReadUInt32BothEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORD(this Stream stream)
            => stream.PeekUInt32();

        /// <summary>
        /// Peek a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORDBigEndian(this Stream stream)
            => stream.PeekUInt32BigEndian();

        /// <summary>
        /// Peek a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORDLittleEndian(this Stream stream)
            => stream.PeekUInt32LittleEndian();

        /// <summary>
        /// Peek a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt32 PeekDWORDBothEndian(this Stream stream)
            => stream.PeekUInt32BothEndian();

        /// <summary>
        /// Peek a Single from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingle(this Stream stream)
        {
            float value = stream.ReadSingle();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingleBigEndian(this Stream stream)
        {
            float value = stream.ReadSingleBigEndian();
            stream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt48LittleEndian();
            else
                return stream.PeekInt48BigEndian();
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48BigEndian(this Stream stream)
        {
            long value = stream.ReadInt48BigEndian();
            stream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48LittleEndian(this Stream stream)
        {
            long value = stream.ReadInt48LittleEndian();
            stream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt48LittleEndian();
            else
                return stream.PeekUInt48BigEndian();
        }

        /// <summary>
        /// Peek an UInt48 encoded as an UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48BigEndian(this Stream stream)
        {
            ulong value = stream.ReadUInt48BigEndian();
            stream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an UInt48 encoded as an UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48LittleEndian(this Stream stream)
        {
            ulong value = stream.ReadUInt48LittleEndian();
            stream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt64LittleEndian();
            else
                return stream.PeekInt64BigEndian();
        }

        /// <summary>
        /// Peek an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64BigEndian(this Stream stream)
        {
            long value = stream.ReadInt64BigEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64LittleEndian(this Stream stream)
        {
            long value = stream.ReadInt64LittleEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt64 PeekInt64BothEndian(this Stream stream)
        {
            BothInt64 value = stream.ReadInt64BothEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt64LittleEndian();
            else
                return stream.PeekUInt64BigEndian();
        }

        /// <summary>
        /// Peek a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64BigEndian(this Stream stream)
        {
            ulong value = stream.ReadUInt64BigEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64LittleEndian(this Stream stream)
        {
            ulong value = stream.ReadUInt64LittleEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt64 PeekUInt64BothEndian(this Stream stream)
        {
            BothUInt64 value = stream.ReadUInt64BothEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORD(this Stream stream)
            => stream.PeekUInt64();

        /// <summary>
        /// Peek a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORDBigEndian(this Stream stream)
            => stream.PeekUInt64BigEndian();

        /// <summary>
        /// Peek a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORDLittleEndian(this Stream stream)
            => stream.PeekUInt64LittleEndian();

        /// <summary>
        /// Peek a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt64 PeekQWORDBothEndian(this Stream stream)
            => stream.PeekUInt64BothEndian();

        /// <summary>
        /// Peek a Double from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDouble(this Stream stream)
        {
            double value = stream.ReadDouble();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDoubleBigEndian(this Stream stream)
        {
            double value = stream.ReadDoubleBigEndian();
            stream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimal(this Stream stream)
        {
            decimal value = stream.ReadDecimal();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimalBigEndian(this Stream stream)
        {
            decimal value = stream.ReadDecimalBigEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuid(this Stream stream)
        {
            Guid value = stream.ReadGuid();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuidBigEndian(this Stream stream)
        {
            Guid value = stream.ReadGuidBigEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Peek an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128(this Stream stream)
        {
            Int128 value = stream.ReadInt128();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128BigEndian(this Stream stream)
        {
            Int128 value = stream.ReadInt128BigEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128(this Stream stream)
        {
            UInt128 value = stream.ReadUInt128();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128BigEndian(this Stream stream)
        {
            UInt128 value = stream.ReadUInt128BigEndian();
            stream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }
#endif

        #endregion

        #region Try Read

        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        public static bool TryReadByteValue(this Stream stream, out byte value)
        {
            if (stream.Position > stream.Length - 1)
            {
                value = default;
                return false;
            }

            value = stream.ReadByteValue();
            return true;
        }

        /// <summary>
        /// Read a UInt8[] from the stream
        /// </summary>
        public static bool TryReadBytes(this Stream stream, int count, out byte[] value)
        {
            if (stream.Position > stream.Length - count)
            {
                value = [];
                return false;
            }

            value = stream.ReadBytes(count);
            return true;
        }

        /// <summary>
        /// Read an Int8 from the stream
        /// </summary>
        public static bool TryReadSByte(this Stream stream, out sbyte value)
        {
            if (stream.Position > stream.Length - 1)
            {
                value = default;
                return false;
            }

            value = stream.ReadSByte();
            return true;
        }

        /// <summary>
        /// Read a Char from the stream
        /// </summary>
        public static bool TryReadChar(this Stream stream, out char value)
        {
            if (stream.Position > stream.Length - 1)
            {
                value = default;
                return false;
            }

            value = stream.ReadChar();
            return true;
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt16(this Stream stream, out short value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt16LittleEndian(out value);
            else
                return stream.TryReadInt16BigEndian(out value);
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt16BigEndian(this Stream stream, out short value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt16BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt16LittleEndian(this Stream stream, out short value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt16LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt16(this Stream stream, out ushort value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt16LittleEndian(out value);
            else
                return stream.TryReadUInt16BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt16BigEndian(this Stream stream, out ushort value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt16BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt16LittleEndian(this Stream stream, out ushort value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt16LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadWORD(this Stream stream, out ushort value)
            => stream.TryReadUInt16(out value);

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadWORDBigEndian(this Stream stream, out ushort value)
            => stream.TryReadUInt16BigEndian(out value);

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadWORDLittleEndian(this Stream stream, out ushort value)
            => stream.TryReadUInt16LittleEndian(out value);

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadHalf(this Stream stream, out Half value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadHalf();
            return true;
        }

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadHalfBigEndian(this Stream stream, out Half value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadHalfBigEndian();
            return true;
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt24(this Stream stream, out int value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt24LittleEndian(out value);
            else
                return stream.TryReadInt24BigEndian(out value);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt24BigEndian(this Stream stream, out int value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt24LittleEndian(this Stream stream, out int value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt24LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt24(this Stream stream, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt24LittleEndian(out value);
            else
                return stream.TryReadUInt24BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt24BigEndian(this Stream stream, out uint value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt24LittleEndian(this Stream stream, out uint value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt24LittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt32(this Stream stream, out int value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt32LittleEndian(out value);
            else
                return stream.TryReadInt32BigEndian(out value);
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt32BigEndian(this Stream stream, out int value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt32BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt32LittleEndian(this Stream stream, out int value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt32LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt32(this Stream stream, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt32LittleEndian(out value);
            else
                return stream.TryReadUInt32BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt32BigEndian(this Stream stream, out uint value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt32BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt32LittleEndian(this Stream stream, out uint value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt32LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDWORD(this Stream stream, out uint value)
            => stream.TryReadUInt32(out value);

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDWORDBigEndian(this Stream stream, out uint value)
            => stream.TryReadUInt32BigEndian(out value);

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDWORDLittleEndian(this Stream stream, out uint value)
            => stream.TryReadUInt32LittleEndian(out value);

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadSingle(this Stream stream, out float value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadSingle();
            return true;
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadSingleBigEndian(this Stream stream, out float value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadSingleBigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt48(this Stream stream, out long value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt48LittleEndian(out value);
            else
                return stream.TryReadInt48BigEndian(out value);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt48BigEndian(this Stream stream, out long value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt48LittleEndian(this Stream stream, out long value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt48LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt48(this Stream stream, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt48LittleEndian(out value);
            else
                return stream.TryReadUInt48BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt48BigEndian(this Stream stream, out ulong value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt48LittleEndian(this Stream stream, out ulong value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt48LittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt64(this Stream stream, out long value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt64LittleEndian(out value);
            else
                return stream.TryReadInt64BigEndian(out value);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt64BigEndian(this Stream stream, out long value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt64LittleEndian(this Stream stream, out long value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt64(this Stream stream, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt64LittleEndian(out value);
            else
                return stream.TryReadUInt64BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt64BigEndian(this Stream stream, out ulong value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt64BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt64LittleEndian(this Stream stream, out ulong value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt64LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadQWORD(this Stream stream, out ulong value)
            => stream.TryReadUInt64(out value);

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadQWORDBigEndian(this Stream stream, out ulong value)
            => stream.TryReadUInt64BigEndian(out value);

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadQWORDLittleEndian(this Stream stream, out ulong value)
            => stream.TryReadUInt64LittleEndian(out value);

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDouble(this Stream stream, out double value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadDouble();
            return true;
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDoubleBigEndian(this Stream stream, out double value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadDoubleBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDecimal(this Stream stream, out decimal value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadDecimal();
            return true;
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDecimalBigEndian(this Stream stream, out decimal value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadDecimalBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadGuid(this Stream stream, out Guid value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadGuid();
            return true;
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadGuidBigEndian(this Stream stream, out Guid value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadGuidBigEndian();
            return true;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt128(this Stream stream, out Int128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt128();
            return true;
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt128BigEndian(this Stream stream, out Int128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt128BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt128(this Stream stream, out UInt128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt128();
            return true;
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt128BigEndian(this Stream stream, out UInt128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt128BigEndian();
            return true;
        }
#endif

        #endregion
    }
}
