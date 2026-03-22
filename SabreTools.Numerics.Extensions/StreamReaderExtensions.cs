using System;
using System.IO;

namespace SabreTools.Numerics.Extensions
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
            return buffer.ToInt16BigEndian();
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return buffer.ToInt16LittleEndian();
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
            return buffer.ToUInt16BigEndian();
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return buffer.ToUInt16LittleEndian();
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

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadHalfLittleEndian();
            else
                return stream.ReadHalfBigEndian();
        }

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return buffer.ToHalfBigEndian();
        }

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Half ReadHalfLittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return buffer.ToHalfLittleEndian();
        }
#endif

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int24 ReadInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt24LittleEndian();
            else
                return stream.ReadInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int24 ReadInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return buffer.ToInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int24 ReadInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return buffer.ToInt24LittleEndian();
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt24 ReadUInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt24LittleEndian();
            else
                return stream.ReadUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt24 ReadUInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return buffer.ToUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt24 ReadUInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return buffer.ToUInt24LittleEndian();
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
            return buffer.ToInt32BigEndian();
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return buffer.ToInt32LittleEndian();
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
            return buffer.ToUInt32BigEndian();
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return buffer.ToUInt32LittleEndian();
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
            if (BitConverter.IsLittleEndian)
                return stream.ReadSingleLittleEndian();
            else
                return stream.ReadSingleBigEndian();
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return buffer.ToSingleBigEndian();
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static float ReadSingleLittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return buffer.ToSingleLittleEndian();
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int48 ReadInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt48LittleEndian();
            else
                return stream.ReadInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int48 ReadInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return buffer.ToInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int48 ReadInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return buffer.ToInt48LittleEndian();
        }

        /// <summary>
        /// Read a UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt48 ReadUInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt48LittleEndian();
            else
                return stream.ReadUInt48BigEndian();
        }

        /// <summary>
        /// Read a UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt48 ReadUInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return buffer.ToUInt48BigEndian();
        }

        /// <summary>
        /// Read an UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt48 ReadUInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return buffer.ToUInt48LittleEndian();
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
            return buffer.ToInt64BigEndian();
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return buffer.ToInt64LittleEndian();
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
            return buffer.ToUInt64BigEndian();
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return buffer.ToUInt64LittleEndian();
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
            if (BitConverter.IsLittleEndian)
                return stream.ReadDoubleLittleEndian();
            else
                return stream.ReadDoubleBigEndian();
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return buffer.ToDoubleBigEndian();
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static double ReadDoubleLittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return buffer.ToDoubleLittleEndian();
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid ReadGuid(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadGuidLittleEndian();
            else
                return stream.ReadGuidBigEndian();
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToGuidBigEndian();
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Guid ReadGuidLittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToGuidLittleEndian();
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt128LittleEndian();
            else
                return stream.ReadInt128BigEndian();
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToInt128BigEndian();
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 ReadInt128LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToInt128LittleEndian();
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt128LittleEndian();
            else
                return stream.ReadUInt128BigEndian();
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToUInt128BigEndian();
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 ReadUInt128LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToUInt128LittleEndian();
        }
#endif

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal ReadDecimal(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadDecimalLittleEndian();
            else
                return stream.ReadDecimalBigEndian();
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToDecimalBigEndian();
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal ReadDecimalLittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return buffer.ToDecimalLittleEndian();
        }

        /// <summary>
        /// Read a number of bytes from the stream to a buffer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is an invalid value.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// Thrown if the requested <paramref name="length"/> is greater
        /// than the read bytes from <paramref name="content"/>.
        /// length.
        /// </exception>
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
            stream.Seek(-1, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt8[] from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte[] PeekBytes(this Stream stream, int count)
        {
            byte[] value = stream.ReadBytes(count);
            stream.Seek(-count, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int8 from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static sbyte PeekSByte(this Stream stream)
        {
            sbyte value = stream.ReadSByte();
            stream.Seek(-1, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Char from the stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static char PeekChar(this Stream stream)
        {
            char value = stream.ReadChar();
            stream.Seek(-1, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
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
            stream.Seek(-2, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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

#if NET5_0_OR_GREATER
        /// <summary>
        /// Peek a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalf(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekHalfLittleEndian();
            else
                return stream.PeekHalfBigEndian();
        }

        /// <summary>
        /// Peek a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalfBigEndian(this Stream stream)
        {
            Half value = stream.ReadHalfBigEndian();
            stream.Seek(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Half from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalfLittleEndian(this Stream stream)
        {
            Half value = stream.ReadHalfLittleEndian();
            stream.Seek(-2, SeekOrigin.Current);
            return value;
        }
#endif

        /// <summary>
        /// Peek an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int24 PeekInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt24LittleEndian();
            else
                return stream.PeekInt24BigEndian();
        }

        /// <summary>
        /// Peek an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int24 PeekInt24BigEndian(this Stream stream)
        {
            Int24 value = stream.ReadInt24BigEndian();
            stream.Seek(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int24 PeekInt24LittleEndian(this Stream stream)
        {
            Int24 value = stream.ReadInt24LittleEndian();
            stream.Seek(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt24 PeekUInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt24LittleEndian();
            else
                return stream.PeekUInt24BigEndian();
        }

        /// <summary>
        /// Peek a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt24 PeekUInt24BigEndian(this Stream stream)
        {
            UInt24 value = stream.ReadUInt24BigEndian();
            stream.Seek(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt24 PeekUInt24LittleEndian(this Stream stream)
        {
            UInt24 value = stream.ReadUInt24LittleEndian();
            stream.Seek(-3, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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
            stream.Seek(-4, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            if (BitConverter.IsLittleEndian)
                return stream.PeekSingleLittleEndian();
            else
                return stream.PeekSingleBigEndian();
        }

        /// <summary>
        /// Peek a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingleBigEndian(this Stream stream)
        {
            float value = stream.ReadSingleBigEndian();
            stream.Seek(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Single from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingleLittleEndian(this Stream stream)
        {
            float value = stream.ReadSingleLittleEndian();
            stream.Seek(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int48 PeekInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt48LittleEndian();
            else
                return stream.PeekInt48BigEndian();
        }

        /// <summary>
        /// Peek an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int48 PeekInt48BigEndian(this Stream stream)
        {
            Int48 value = stream.ReadInt48BigEndian();
            stream.Seek(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int48 PeekInt48LittleEndian(this Stream stream)
        {
            Int48 value = stream.ReadInt48LittleEndian();
            stream.Seek(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt48 PeekUInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt48LittleEndian();
            else
                return stream.PeekUInt48BigEndian();
        }

        /// <summary>
        /// Peek an UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt48 PeekUInt48BigEndian(this Stream stream)
        {
            UInt48 value = stream.ReadUInt48BigEndian();
            stream.Seek(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt48 PeekUInt48LittleEndian(this Stream stream)
        {
            UInt48 value = stream.ReadUInt48LittleEndian();
            stream.Seek(-6, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            stream.Seek(-16, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            stream.Seek(-8, SeekOrigin.Current);
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
            stream.Seek(-16, SeekOrigin.Current);
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
            if (BitConverter.IsLittleEndian)
                return stream.PeekDoubleLittleEndian();
            else
                return stream.PeekDoubleBigEndian();
        }

        /// <summary>
        /// Peek a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDoubleBigEndian(this Stream stream)
        {
            double value = stream.ReadDoubleBigEndian();
            stream.Seek(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Double from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDoubleLittleEndian(this Stream stream)
        {
            double value = stream.ReadDoubleLittleEndian();
            stream.Seek(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuid(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekGuidLittleEndian();
            else
                return stream.PeekGuidBigEndian();
        }

        /// <summary>
        /// Peek a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuidBigEndian(this Stream stream)
        {
            Guid value = stream.ReadGuidBigEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuidLittleEndian(this Stream stream)
        {
            Guid value = stream.ReadGuidLittleEndian();
            stream.Seek(-16, SeekOrigin.Current);
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
            if (BitConverter.IsLittleEndian)
                return stream.PeekInt128LittleEndian();
            else
                return stream.PeekInt128BigEndian();
        }

        /// <summary>
        /// Peek an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128BigEndian(this Stream stream)
        {
            Int128 value = stream.ReadInt128BigEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128LittleEndian(this Stream stream)
        {
            Int128 value = stream.ReadInt128LittleEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekUInt128LittleEndian();
            else
                return stream.PeekUInt128BigEndian();
        }

        /// <summary>
        /// Peek a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128BigEndian(this Stream stream)
        {
            UInt128 value = stream.ReadUInt128BigEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128LittleEndian(this Stream stream)
        {
            UInt128 value = stream.ReadUInt128LittleEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }
#endif

        /// <summary>
        /// Peek a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimal(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.PeekDecimalLittleEndian();
            else
                return stream.PeekDecimalBigEndian();
        }

        /// <summary>
        /// Peek a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimalBigEndian(this Stream stream)
        {
            decimal value = stream.ReadDecimalBigEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimalLittleEndian(this Stream stream)
        {
            decimal value = stream.ReadDecimalLittleEndian();
            stream.Seek(-16, SeekOrigin.Current);
            return value;
        }

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
        /// Read a UInt8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadByteBothEndian(this Stream stream, out BothUInt8 value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default(byte);
                return false;
            }

            value = stream.ReadByteBothEndian();
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
        /// Read a Int8 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadSByteBothEndian(this Stream stream, out BothInt8 value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default(sbyte);
                return false;
            }

            value = stream.ReadSByteBothEndian();
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
        /// Read a Int16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt16BothEndian(this Stream stream, out BothInt16 value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default(short);
                return false;
            }

            value = stream.ReadInt16BothEndian();
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
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt16BothEndian(this Stream stream, out BothUInt16 value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default(ushort);
                return false;
            }

            value = stream.ReadUInt16BothEndian();
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

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadWORDBothEndian(this Stream stream, out BothUInt16 value)
            => stream.TryReadUInt16BothEndian(out value);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadHalf(this Stream stream, out Half value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadHalfLittleEndian(out value);
            else
                return stream.TryReadHalfBigEndian(out value);
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

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadHalfLittleEndian(this Stream stream, out Half value)
        {
            if (stream.Position > stream.Length - 2)
            {
                value = default;
                return false;
            }

            value = stream.ReadHalfLittleEndian();
            return true;
        }
#endif

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt24(this Stream stream, out Int24 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt24LittleEndian(out value);
            else
                return stream.TryReadInt24BigEndian(out value);
        }

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt24BigEndian(this Stream stream, out Int24 value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = new Int24();
                return false;
            }

            value = stream.ReadInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt24LittleEndian(this Stream stream, out Int24 value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = new Int24();
                return false;
            }

            value = stream.ReadInt24LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt24(this Stream stream, out UInt24 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt24LittleEndian(out value);
            else
                return stream.TryReadUInt24BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt24BigEndian(this Stream stream, out UInt24 value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = new UInt24();
                return false;
            }

            value = stream.ReadUInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt24LittleEndian(this Stream stream, out UInt24 value)
        {
            if (stream.Position > stream.Length - 3)
            {
                value = new UInt24();
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
        /// Read a Int32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt32BothEndian(this Stream stream, out BothInt32 value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default(int);
                return false;
            }

            value = stream.ReadInt32BothEndian();
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
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt32BothEndian(this Stream stream, out BothUInt32 value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default(uint);
                return false;
            }

            value = stream.ReadUInt32BothEndian();
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
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadDWORDBothEndian(this Stream stream, out BothUInt32 value)
            => stream.TryReadUInt32BothEndian(out value);

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadSingle(this Stream stream, out float value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadSingleLittleEndian(out value);
            else
                return stream.TryReadSingleBigEndian(out value);
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
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadSingleLittleEndian(this Stream stream, out float value)
        {
            if (stream.Position > stream.Length - 4)
            {
                value = default;
                return false;
            }

            value = stream.ReadSingleLittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt48(this Stream stream, out Int48 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt48LittleEndian(out value);
            else
                return stream.TryReadInt48BigEndian(out value);
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt48BigEndian(this Stream stream, out Int48 value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = new Int48();
                return false;
            }

            value = stream.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt48LittleEndian(this Stream stream, out Int48 value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = new Int48();
                return false;
            }

            value = stream.ReadInt48LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt48(this Stream stream, out UInt48 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt48LittleEndian(out value);
            else
                return stream.TryReadUInt48BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt48BigEndian(this Stream stream, out UInt48 value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = new UInt48();
                return false;
            }

            value = stream.ReadUInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an UInt48 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt48LittleEndian(this Stream stream, out UInt48 value)
        {
            if (stream.Position > stream.Length - 6)
            {
                value = new UInt48();
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

            value = stream.ReadInt64BigEndian();
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

            value = stream.ReadInt64BigEndian();
            return true;
        }

        /// <summary>
        /// Read a Int64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt64BothEndian(this Stream stream, out BothInt64 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default(long);
                return false;
            }

            value = stream.ReadInt64BothEndian();
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
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt64BothEndian(this Stream stream, out BothUInt64 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default(ulong);
                return false;
            }

            value = stream.ReadUInt64BothEndian();
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
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadQWORDBothEndian(this Stream stream, out BothUInt64 value)
            => stream.TryReadUInt64BothEndian(out value);

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDouble(this Stream stream, out double value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadDoubleLittleEndian(out value);
            else
                return stream.TryReadDoubleBigEndian(out value);
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
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDoubleLittleEndian(this Stream stream, out double value)
        {
            if (stream.Position > stream.Length - 8)
            {
                value = default;
                return false;
            }

            value = stream.ReadDoubleLittleEndian();
            return true;
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadGuid(this Stream stream, out Guid value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadGuidLittleEndian(out value);
            else
                return stream.TryReadGuidBigEndian(out value);
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

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadGuidLittleEndian(this Stream stream, out Guid value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadGuidLittleEndian();
            return true;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt128(this Stream stream, out Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadInt128LittleEndian(out value);
            else
                return stream.TryReadInt128BigEndian(out value);
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
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt128LittleEndian(this Stream stream, out Int128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadInt128LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt128(this Stream stream, out UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadUInt128LittleEndian(out value);
            else
                return stream.TryReadUInt128BigEndian(out value);
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

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt128LittleEndian(this Stream stream, out UInt128 value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadUInt128LittleEndian();
            return true;
        }
#endif

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDecimal(this Stream stream, out decimal value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.TryReadDecimalLittleEndian(out value);
            else
                return stream.TryReadDecimalBigEndian(out value);
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
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDecimalLittleEndian(this Stream stream, out decimal value)
        {
            if (stream.Position > stream.Length - 16)
            {
                value = default;
                return false;
            }

            value = stream.ReadDecimalLittleEndian();
            return true;
        }

        #endregion
    }
}
