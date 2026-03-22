using System;

namespace SabreTools.Numerics.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
        #region Exact Read

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
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt8 ReadByteBothEndian(this byte[] content, ref int offset)
        {
            byte le = content.ReadByte(ref offset);
            byte be = content.ReadByte(ref offset);
            return new BothUInt8(le, be);
        }

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static byte[] ReadBytes(this byte[] content, ref int offset, int count)
            => ReadExactlyToBuffer(content, ref offset, count);

        /// <summary>
        /// Read an Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Int8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt8 ReadSByteBothEndian(this byte[] content, ref int offset)
        {
            sbyte le = content.ReadSByte(ref offset);
            sbyte be = content.ReadSByte(ref offset);
            return new BothInt8(le, be);
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
            return buffer.ToInt16BigEndian();
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return buffer.ToInt16LittleEndian();
        }

        /// <summary>
        /// Read a Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt16 ReadInt16BothEndian(this byte[] content, ref int offset)
        {
            short le = content.ReadInt16LittleEndian(ref offset);
            short be = content.ReadInt16BigEndian(ref offset);
            return new BothInt16(le, be);
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
            return buffer.ToUInt16BigEndian();
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return buffer.ToUInt16LittleEndian();
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadUInt16BothEndian(this byte[] content, ref int offset)
        {
            ushort le = content.ReadUInt16LittleEndian(ref offset);
            ushort be = content.ReadUInt16BigEndian(ref offset);
            return new BothUInt16(le, be);
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

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadWORDBothEndian(this byte[] content, ref int offset)
            => content.ReadUInt16BothEndian(ref offset);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadHalfLittleEndian(ref offset);
            else
                return content.ReadHalfBigEndian(ref offset);
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return buffer.ToHalfBigEndian();
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Half ReadHalfLittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 2);
            return buffer.ToHalfLittleEndian();
        }
#endif

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int24 ReadInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt24LittleEndian(ref offset);
            else
                return content.ReadInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int24 ReadInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return buffer.ToInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int24 ReadInt24LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return buffer.ToInt24LittleEndian();
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt24 ReadUInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt24LittleEndian(ref offset);
            else
                return content.ReadUInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt24 ReadUInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return buffer.ToUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt24 ReadUInt24LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 3);
            return buffer.ToUInt24LittleEndian();
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
            return buffer.ToInt32BigEndian();
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return buffer.ToInt32LittleEndian();
        }

        /// <summary>
        /// Read a Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt32 ReadInt32BothEndian(this byte[] content, ref int offset)
        {
            int le = content.ReadInt32LittleEndian(ref offset);
            int be = content.ReadInt32BigEndian(ref offset);
            return new BothInt32(le, be);
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
            return buffer.ToUInt32BigEndian();
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return buffer.ToUInt32LittleEndian();
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadUInt32BothEndian(this byte[] content, ref int offset)
        {
            uint le = content.ReadUInt32LittleEndian(ref offset);
            uint be = content.ReadUInt32BigEndian(ref offset);
            return new BothUInt32(le, be);
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
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadDWORDBothEndian(this byte[] content, ref int offset)
            => content.ReadUInt32BothEndian(ref offset);

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static float ReadSingle(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadSingleLittleEndian(ref offset);
            else
                return content.ReadSingleBigEndian(ref offset);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return buffer.ToSingleBigEndian();
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static float ReadSingleLittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 4);
            return buffer.ToSingleLittleEndian();
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int48 ReadInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt48LittleEndian(ref offset);
            else
                return content.ReadInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int48 ReadInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return buffer.ToInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int48 ReadInt48LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return buffer.ToInt48LittleEndian();
        }

        /// <summary>
        /// Read a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt48 ReadUInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt48LittleEndian(ref offset);
            else
                return content.ReadUInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Read an UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt48 ReadUInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return buffer.ToUInt48BigEndian();
        }

        /// <summary>
        /// Read an UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt48 ReadUInt48LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 6);
            return buffer.ToUInt48LittleEndian();
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
            return buffer.ToInt64BigEndian();
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return buffer.ToInt64LittleEndian();
        }

        /// <summary>
        /// Read a Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt64 ReadInt64BothEndian(this byte[] content, ref int offset)
        {
            long le = content.ReadInt64LittleEndian(ref offset);
            long be = content.ReadInt64BigEndian(ref offset);
            return new BothInt64(le, be);
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
            return buffer.ToUInt64BigEndian();
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return buffer.ToUInt64LittleEndian();
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadUInt64BothEndian(this byte[] content, ref int offset)
        {
            ulong le = content.ReadUInt64LittleEndian(ref offset);
            ulong be = content.ReadUInt64BigEndian(ref offset);
            return new BothUInt64(le, be);
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
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadQWORDBothEndian(this byte[] content, ref int offset)
            => content.ReadUInt64BothEndian(ref offset);

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static double ReadDouble(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadDoubleLittleEndian(ref offset);
            else
                return content.ReadDoubleBigEndian(ref offset);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return buffer.ToDoubleBigEndian();
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static double ReadDoubleLittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 8);
            return buffer.ToDoubleLittleEndian();
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid ReadGuid(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadGuidLittleEndian(ref offset);
            else
                return content.ReadGuidBigEndian(ref offset);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToGuidBigEndian();
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Guid ReadGuidLittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToGuidLittleEndian();
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadInt128LittleEndian(ref offset);
            else
                return content.ReadInt128BigEndian(ref offset);
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToInt128BigEndian();
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 ReadInt128LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToInt128LittleEndian();
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadUInt128LittleEndian(ref offset);
            else
                return content.ReadUInt128BigEndian(ref offset);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToUInt128BigEndian();
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 ReadUInt128LittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToUInt128LittleEndian();
        }
#endif

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal ReadDecimal(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.ReadDecimalLittleEndian(ref offset);
            else
                return content.ReadDecimalBigEndian(ref offset);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToDecimalBigEndian();
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal ReadDecimalLittleEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadExactlyToBuffer(content, ref offset, 16);
            return buffer.ToDecimalLittleEndian();
        }

        /// <summary>
        /// Read a number of bytes from the byte array to a buffer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="offset"/> or <paramref name="length"/>
        /// is an invalid value.
        /// </exception>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown if the requested <paramref name="offset"/> and
        /// <paramref name="length"/> is greater than <paramref name="content"/>
        /// length.
        /// </exception>
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

        #endregion

        #region Peek Read

        /// <summary>
        /// Peek a UInt8 without incrementing the pointer to an array
        /// </summary>
        public static byte PeekByte(this byte[] content, ref int offset)
        {
            byte value = content.ReadByte(ref offset);
            offset -= 1;
            return value;
        }

        /// <summary>
        /// Peek a UInt8 without incrementing the pointer to an array
        /// </summary>
        public static byte PeekByteValue(this byte[] content, ref int offset)
            => content.PeekByte(ref offset);

        /// <summary>
        /// Peek a UInt8 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt8 PeekByteBothEndian(this byte[] content, ref int offset)
        {
            BothUInt8 value = content.ReadByteBothEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a UInt8[] without incrementing the pointer to an array
        /// </summary>
        public static byte[] PeekBytes(this byte[] content, ref int offset, int count)
        {
            byte[] value = content.ReadBytes(ref offset, count);
            offset -= count;
            return value;
        }

        /// <summary>
        /// Peek an Int8 without incrementing the pointer to an array
        /// </summary>
        public static sbyte PeekSByte(this byte[] content, ref int offset)
        {
            sbyte value = content.ReadSByte(ref offset);
            offset -= 1;
            return value;
        }

        /// <summary>
        /// Peek a Int8 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt8 PeekSByteBothEndian(this byte[] content, ref int offset)
        {
            BothInt8 value = content.ReadSByteBothEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a Char without incrementing the pointer to an array
        /// </summary>
        public static char PeekChar(this byte[] content, ref int offset)
        {
            char value = content.ReadChar(ref offset);
            offset -= 1;
            return value;
        }

        /// <summary>
        /// Peek an Int16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static short PeekInt16(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt16LittleEndian(ref offset);
            else
                return content.PeekInt16BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short PeekInt16BigEndian(this byte[] content, ref int offset)
        {
            short value = content.ReadInt16BigEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek an Int16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short PeekInt16LittleEndian(this byte[] content, ref int offset)
        {
            short value = content.ReadInt16LittleEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a Int16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt16 PeekInt16BothEndian(this byte[] content, ref int offset)
        {
            BothInt16 value = content.ReadInt16BothEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a UInt16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort PeekUInt16(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt16LittleEndian(ref offset);
            else
                return content.PeekUInt16BigEndian(ref offset);
        }

        /// <summary>
        /// Peek a UInt16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort PeekUInt16BigEndian(this byte[] content, ref int offset)
        {
            ushort value = content.ReadUInt16BigEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a UInt16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort PeekUInt16LittleEndian(this byte[] content, ref int offset)
        {
            ushort value = content.ReadUInt16LittleEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a UInt16 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 PeekUInt16BothEndian(this byte[] content, ref int offset)
        {
            BothUInt16 value = content.ReadUInt16BothEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a WORD (2-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort PeekWORD(this byte[] content, ref int offset)
            => content.PeekUInt16(ref offset);

        /// <summary>
        /// Peek a WORD (2-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort PeekWORDBigEndian(this byte[] content, ref int offset)
            => content.PeekUInt16BigEndian(ref offset);

        /// <summary>
        /// Peek a WORD (2-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort PeekWORDLittleEndian(this byte[] content, ref int offset)
            => content.PeekUInt16LittleEndian(ref offset);

        /// <summary>
        /// Peek a WORD (2-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 PeekWORDBothEndian(this byte[] content, ref int offset)
            => content.PeekUInt16BothEndian(ref offset);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Peek a Half without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half PeekHalf(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekHalfLittleEndian(ref offset);
            else
                return content.PeekHalfBigEndian(ref offset);
        }

        /// <summary>
        /// Peek a Half without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half PeekHalfBigEndian(this byte[] content, ref int offset)
        {
            Half value = content.ReadHalfBigEndian(ref offset);
            offset -= 2;
            return value;
        }

        /// <summary>
        /// Peek a Half without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Half PeekHalfLittleEndian(this byte[] content, ref int offset)
        {
            Half value = content.ReadHalfLittleEndian(ref offset);
            offset -= 2;
            return value;
        }
#endif

        /// <summary>
        /// Peek an Int24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int24 PeekInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt24LittleEndian(ref offset);
            else
                return content.PeekInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int24 PeekInt24BigEndian(this byte[] content, ref int offset)
        {
            Int24 value = content.ReadInt24BigEndian(ref offset);
            offset -= 3;
            return value;
        }

        /// <summary>
        /// Peek an Int24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int24 PeekInt24LittleEndian(this byte[] content, ref int offset)
        {
            Int24 value = content.ReadInt24LittleEndian(ref offset);
            offset -= 3;
            return value;
        }

        /// <summary>
        /// Peek a UInt24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt24 PeekUInt24(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt24LittleEndian(ref offset);
            else
                return content.PeekUInt24BigEndian(ref offset);
        }

        /// <summary>
        /// Peek a UInt24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt24 PeekUInt24BigEndian(this byte[] content, ref int offset)
        {
            UInt24 value = content.ReadUInt24BigEndian(ref offset);
            offset -= 3;
            return value;
        }

        /// <summary>
        /// Peek a UInt24 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt24 PeekUInt24LittleEndian(this byte[] content, ref int offset)
        {
            UInt24 value = content.ReadUInt24LittleEndian(ref offset);
            offset -= 3;
            return value;
        }

        /// <summary>
        /// Peek an Int32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int PeekInt32(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt32LittleEndian(ref offset);
            else
                return content.PeekInt32BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int PeekInt32BigEndian(this byte[] content, ref int offset)
        {
            int value = content.ReadInt32BigEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek an Int32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int PeekInt32LittleEndian(this byte[] content, ref int offset)
        {
            int value = content.ReadInt32LittleEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a Int32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt32 PeekInt32BothEndian(this byte[] content, ref int offset)
        {
            BothInt32 value = content.ReadInt32BothEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a UInt32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint PeekUInt32(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt32LittleEndian(ref offset);
            else
                return content.PeekUInt32BigEndian(ref offset);
        }

        /// <summary>
        /// Peek a UInt32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint PeekUInt32BigEndian(this byte[] content, ref int offset)
        {
            uint value = content.ReadUInt32BigEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a UInt32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint PeekUInt32LittleEndian(this byte[] content, ref int offset)
        {
            uint value = content.ReadUInt32LittleEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a UInt32 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 PeekUInt32BothEndian(this byte[] content, ref int offset)
        {
            BothUInt32 value = content.ReadUInt32BothEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a DWORD (4-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint PeekDWORD(this byte[] content, ref int offset)
            => content.PeekUInt32(ref offset);

        /// <summary>
        /// Peek a DWORD (4-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint PeekDWORDBigEndian(this byte[] content, ref int offset)
            => content.PeekUInt32BigEndian(ref offset);

        /// <summary>
        /// Peek a DWORD (4-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint PeekDWORDLittleEndian(this byte[] content, ref int offset)
            => content.PeekUInt32LittleEndian(ref offset);

        /// <summary>
        /// Peek a DWORD (4-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 PeekDWORDBothEndian(this byte[] content, ref int offset)
            => content.PeekUInt32BothEndian(ref offset);

        /// <summary>
        /// Peek a Single without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static float PeekSingle(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekSingleLittleEndian(ref offset);
            else
                return content.PeekSingleBigEndian(ref offset);
        }

        /// <summary>
        /// Peek a Single without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float PeekSingleBigEndian(this byte[] content, ref int offset)
        {
            float value = content.ReadSingleBigEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek a Single without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static float PeekSingleLittleEndian(this byte[] content, ref int offset)
        {
            float value = content.ReadSingleLittleEndian(ref offset);
            offset -= 4;
            return value;
        }

        /// <summary>
        /// Peek an Int48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int48 PeekInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt48LittleEndian(ref offset);
            else
                return content.PeekInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int48 PeekInt48BigEndian(this byte[] content, ref int offset)
        {
            Int48 value = content.ReadInt48BigEndian(ref offset);
            offset -= 6;
            return value;
        }

        /// <summary>
        /// Peek an Int48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int48 PeekInt48LittleEndian(this byte[] content, ref int offset)
        {
            Int48 value = content.ReadInt48LittleEndian(ref offset);
            offset -= 6;
            return value;
        }

        /// <summary>
        /// Peek a UInt48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt48 PeekUInt48(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt48LittleEndian(ref offset);
            else
                return content.PeekUInt48BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an UInt48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt48 PeekUInt48BigEndian(this byte[] content, ref int offset)
        {
            UInt48 value = content.ReadUInt48BigEndian(ref offset);
            offset -= 6;
            return value;
        }

        /// <summary>
        /// Peek an UInt48 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt48 PeekUInt48LittleEndian(this byte[] content, ref int offset)
        {
            UInt48 value = content.ReadUInt48LittleEndian(ref offset);
            offset -= 6;
            return value;
        }

        /// <summary>
        /// Peek an Int64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long PeekInt64(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt64LittleEndian(ref offset);
            else
                return content.PeekInt64BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long PeekInt64BigEndian(this byte[] content, ref int offset)
        {
            long value = content.ReadInt64BigEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek an Int64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long PeekInt64LittleEndian(this byte[] content, ref int offset)
        {
            long value = content.ReadInt64LittleEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a Int64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt64 PeekInt64BothEndian(this byte[] content, ref int offset)
        {
            BothInt64 value = content.ReadInt64BothEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a UInt64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong PeekUInt64(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt64LittleEndian(ref offset);
            else
                return content.PeekUInt64BigEndian(ref offset);
        }

        /// <summary>
        /// Peek a UInt64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong PeekUInt64BigEndian(this byte[] content, ref int offset)
        {
            ulong value = content.ReadUInt64BigEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a UInt64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong PeekUInt64LittleEndian(this byte[] content, ref int offset)
        {
            ulong value = content.ReadUInt64LittleEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a UInt64 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 PeekUInt64BothEndian(this byte[] content, ref int offset)
        {
            BothUInt64 value = content.ReadUInt64BothEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a QWORD (8-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong PeekQWORD(this byte[] content, ref int offset)
            => content.PeekUInt64(ref offset);

        /// <summary>
        /// Peek a QWORD (8-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong PeekQWORDBigEndian(this byte[] content, ref int offset)
            => content.PeekUInt64BigEndian(ref offset);

        /// <summary>
        /// Peek a QWORD (8-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong PeekQWORDLittleEndian(this byte[] content, ref int offset)
            => content.PeekUInt64LittleEndian(ref offset);

        /// <summary>
        /// Peek a QWORD (8-byte) without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 PeekQWORDBothEndian(this byte[] content, ref int offset)
            => content.PeekUInt64BothEndian(ref offset);

        /// <summary>
        /// Peek a Double without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static double PeekDouble(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekDoubleLittleEndian(ref offset);
            else
                return content.PeekDoubleBigEndian(ref offset);
        }

        /// <summary>
        /// Peek a Double without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double PeekDoubleBigEndian(this byte[] content, ref int offset)
        {
            double value = content.ReadDoubleBigEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a Double without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static double PeekDoubleLittleEndian(this byte[] content, ref int offset)
        {
            double value = content.ReadDoubleLittleEndian(ref offset);
            offset -= 8;
            return value;
        }

        /// <summary>
        /// Peek a Guid without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid PeekGuid(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekGuidLittleEndian(ref offset);
            else
                return content.PeekGuidBigEndian(ref offset);
        }

        /// <summary>
        /// Peek a Guid without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid PeekGuidBigEndian(this byte[] content, ref int offset)
        {
            Guid value = content.ReadGuidBigEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a Guid without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid PeekGuidLittleEndian(this byte[] content, ref int offset)
        {
            Guid value = content.ReadGuidLittleEndian(ref offset);
            offset -= 16;
            return value;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Peek an Int128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 PeekInt128(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekInt128LittleEndian(ref offset);
            else
                return content.PeekInt128BigEndian(ref offset);
        }

        /// <summary>
        /// Peek an Int128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 PeekInt128BigEndian(this byte[] content, ref int offset)
        {
            Int128 value = content.ReadInt128BigEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek an Int128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 PeekInt128LittleEndian(this byte[] content, ref int offset)
        {
            Int128 value = content.ReadInt128LittleEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a UInt128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 PeekUInt128(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekUInt128LittleEndian(ref offset);
            else
                return content.PeekUInt128BigEndian(ref offset);
        }

        /// <summary>
        /// Peek a UInt128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 PeekUInt128BigEndian(this byte[] content, ref int offset)
        {
            UInt128 value = content.ReadUInt128BigEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a UInt128 without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 PeekUInt128LittleEndian(this byte[] content, ref int offset)
        {
            UInt128 value = content.ReadUInt128LittleEndian(ref offset);
            offset -= 16;
            return value;
        }
#endif

        /// <summary>
        /// Peek a Decimal without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal PeekDecimal(this byte[] content, ref int offset)
        {
            if (BitConverter.IsLittleEndian)
                return content.PeekDecimalLittleEndian(ref offset);
            else
                return content.PeekDecimalBigEndian(ref offset);
        }

        /// <summary>
        /// Peek a Decimal without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal PeekDecimalBigEndian(this byte[] content, ref int offset)
        {
            decimal value = content.ReadDecimalBigEndian(ref offset);
            offset -= 16;
            return value;
        }

        /// <summary>
        /// Peek a Decimal without incrementing the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal PeekDecimalLittleEndian(this byte[] content, ref int offset)
        {
            decimal value = content.ReadDecimalLittleEndian(ref offset);
            offset -= 16;
            return value;
        }

        #endregion

        #region Try Read

        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static bool TryReadByte(this byte[] content, ref int offset, out byte value)
        {
            if (offset > content.Length - 1)
            {
                value = default;
                return false;
            }

            value = content.ReadByte(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static bool TryReadByteValue(this byte[] content, ref int offset, out byte value)
        {
            if (offset > content.Length - 1)
            {
                value = default;
                return false;
            }

            value = content.ReadByteValue(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadByteBothEndian(this byte[] content, ref int offset, out BothUInt8 value)
        {
            if (offset > content.Length - 2)
            {
                value = default(byte);
                return false;
            }

            value = content.ReadByteBothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static bool TryReadBytes(this byte[] content, ref int offset, int count, out byte[] value)
        {
            if (offset > content.Length - count)
            {
                value = [];
                return false;
            }

            value = content.ReadBytes(ref offset, count);
            return true;
        }

        /// <summary>
        /// Read an Int8 and increment the pointer to an array
        /// </summary>
        public static bool TryReadSByte(this byte[] content, ref int offset, out sbyte value)
        {
            if (offset > content.Length - 1)
            {
                value = default;
                return false;
            }

            value = content.ReadSByte(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Int8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadSByteBothEndian(this byte[] content, ref int offset, out BothInt8 value)
        {
            if (offset > content.Length - 2)
            {
                value = default(sbyte);
                return false;
            }

            value = content.ReadSByteBothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static bool TryReadChar(this byte[] content, ref int offset, out char value)
        {
            if (offset > content.Length - 1)
            {
                value = default;
                return false;
            }

            value = content.ReadChar(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt16(this byte[] content, ref int offset, out short value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt16LittleEndian(ref offset, out value);
            else
                return content.TryReadInt16BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt16BigEndian(this byte[] content, ref int offset, out short value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadInt16BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt16LittleEndian(this byte[] content, ref int offset, out short value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadInt16LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt16BothEndian(this byte[] content, ref int offset, out BothInt16 value)
        {
            if (offset > content.Length - 4)
            {
                value = default(short);
                return false;
            }

            value = content.ReadInt16BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt16(this byte[] content, ref int offset, out ushort value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt16LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt16BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt16BigEndian(this byte[] content, ref int offset, out ushort value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt16BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt16LittleEndian(this byte[] content, ref int offset, out ushort value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt16LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt16BothEndian(this byte[] content, ref int offset, out BothUInt16 value)
        {
            if (offset > content.Length - 4)
            {
                value = default(ushort);
                return false;
            }

            value = content.ReadUInt16BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadWORD(this byte[] content, ref int offset, out ushort value)
            => content.TryReadUInt16(ref offset, out value);

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadWORDBigEndian(this byte[] content, ref int offset, out ushort value)
            => content.TryReadUInt16BigEndian(ref offset, out value);

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadWORDLittleEndian(this byte[] content, ref int offset, out ushort value)
            => content.TryReadUInt16LittleEndian(ref offset, out value);

        /// <summary>
        /// Read a WORD (2-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadWORDBothEndian(this byte[] content, ref int offset, out BothUInt16 value)
            => content.TryReadUInt16BothEndian(ref offset, out value);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadHalf(this byte[] content, ref int offset, out Half value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadHalfLittleEndian(ref offset, out value);
            else
                return content.TryReadHalfBigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadHalfBigEndian(this byte[] content, ref int offset, out Half value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadHalfBigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadHalfLittleEndian(this byte[] content, ref int offset, out Half value)
        {
            if (offset > content.Length - 2)
            {
                value = default;
                return false;
            }

            value = content.ReadHalfLittleEndian(ref offset);
            return true;
        }
#endif

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt24(this byte[] content, ref int offset, out Int24 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt24LittleEndian(ref offset, out value);
            else
                return content.TryReadInt24BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt24BigEndian(this byte[] content, ref int offset, out Int24 value)
        {
            if (offset > content.Length - 3)
            {
                value = new Int24();
                return false;
            }

            value = content.ReadInt24BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt24LittleEndian(this byte[] content, ref int offset, out Int24 value)
        {
            if (offset > content.Length - 3)
            {
                value = new Int24();
                return false;
            }

            value = content.ReadInt24LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt24(this byte[] content, ref int offset, out UInt24 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt24LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt24BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt24BigEndian(this byte[] content, ref int offset, out UInt24 value)
        {
            if (offset > content.Length - 3)
            {
                value = new UInt24();
                return false;
            }

            value = content.ReadUInt24BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt24LittleEndian(this byte[] content, ref int offset, out UInt24 value)
        {
            if (offset > content.Length - 3)
            {
                value = new UInt24();
                return false;
            }

            value = content.ReadUInt24LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt32(this byte[] content, ref int offset, out int value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt32LittleEndian(ref offset, out value);
            else
                return content.TryReadInt32BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt32BigEndian(this byte[] content, ref int offset, out int value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadInt32BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt32LittleEndian(this byte[] content, ref int offset, out int value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadInt32LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt32BothEndian(this byte[] content, ref int offset, out BothInt32 value)
        {
            if (offset > content.Length - 8)
            {
                value = default(int);
                return false;
            }

            value = content.ReadInt32BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt32(this byte[] content, ref int offset, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt32LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt32BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt32BigEndian(this byte[] content, ref int offset, out uint value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt32BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt32LittleEndian(this byte[] content, ref int offset, out uint value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt32LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt32BothEndian(this byte[] content, ref int offset, out BothUInt32 value)
        {
            if (offset > content.Length - 8)
            {
                value = default(uint);
                return false;
            }

            value = content.ReadUInt32BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDWORD(this byte[] content, ref int offset, out uint value)
            => content.TryReadUInt32(ref offset, out value);

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDWORDBigEndian(this byte[] content, ref int offset, out uint value)
            => content.TryReadUInt32BigEndian(ref offset, out value);

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDWORDLittleEndian(this byte[] content, ref int offset, out uint value)
            => content.TryReadUInt32LittleEndian(ref offset, out value);

        /// <summary>
        /// Read a DWORD (4-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadDWORDBothEndian(this byte[] content, ref int offset, out BothUInt32 value)
            => content.TryReadUInt32BothEndian(ref offset, out value);

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadSingle(this byte[] content, ref int offset, out float value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadSingleLittleEndian(ref offset, out value);
            else
                return content.TryReadSingleBigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadSingleBigEndian(this byte[] content, ref int offset, out float value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadSingleBigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadSingleLittleEndian(this byte[] content, ref int offset, out float value)
        {
            if (offset > content.Length - 4)
            {
                value = default;
                return false;
            }

            value = content.ReadSingleLittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt48(this byte[] content, ref int offset, out Int48 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt48LittleEndian(ref offset, out value);
            else
                return content.TryReadInt48BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt48BigEndian(this byte[] content, ref int offset, out Int48 value)
        {
            if (offset > content.Length - 6)
            {
                value = new Int48();
                return false;
            }

            value = content.ReadInt48BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt48LittleEndian(this byte[] content, ref int offset, out Int48 value)
        {
            if (offset > content.Length - 6)
            {
                value = new Int48();
                return false;
            }

            value = content.ReadInt48LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt48(this byte[] content, ref int offset, out UInt48 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt48LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt48BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt48BigEndian(this byte[] content, ref int offset, out UInt48 value)
        {
            if (offset > content.Length - 6)
            {
                value = new UInt48();
                return false;
            }

            value = content.ReadUInt48BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt48LittleEndian(this byte[] content, ref int offset, out UInt48 value)
        {
            if (offset > content.Length - 6)
            {
                value = new UInt48();
                return false;
            }

            value = content.ReadUInt48LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt64(this byte[] content, ref int offset, out long value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt64LittleEndian(ref offset, out value);
            else
                return content.TryReadInt64BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt64BigEndian(this byte[] content, ref int offset, out long value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadInt64BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt64LittleEndian(this byte[] content, ref int offset, out long value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadInt64BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt64BothEndian(this byte[] content, ref int offset, out BothInt64 value)
        {
            if (offset > content.Length - 16)
            {
                value = default(long);
                return false;
            }

            value = content.ReadInt64BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt64(this byte[] content, ref int offset, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt64LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt64BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt64BigEndian(this byte[] content, ref int offset, out ulong value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt64BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt64LittleEndian(this byte[] content, ref int offset, out ulong value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt64LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt64BothEndian(this byte[] content, ref int offset, out BothUInt64 value)
        {
            if (offset > content.Length - 16)
            {
                value = default(ulong);
                return false;
            }

            value = content.ReadUInt64BothEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadQWORD(this byte[] content, ref int offset, out ulong value)
            => content.TryReadUInt64(ref offset, out value);

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadQWORDBigEndian(this byte[] content, ref int offset, out ulong value)
            => content.TryReadUInt64BigEndian(ref offset, out value);

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadQWORDLittleEndian(this byte[] content, ref int offset, out ulong value)
            => content.TryReadUInt64LittleEndian(ref offset, out value);

        /// <summary>
        /// Read a QWORD (8-byte) and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadQWORDBothEndian(this byte[] content, ref int offset, out BothUInt64 value)
            => content.TryReadUInt64BothEndian(ref offset, out value);

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDouble(this byte[] content, ref int offset, out double value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadDoubleLittleEndian(ref offset, out value);
            else
                return content.TryReadDoubleBigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDoubleBigEndian(this byte[] content, ref int offset, out double value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadDoubleBigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDoubleLittleEndian(this byte[] content, ref int offset, out double value)
        {
            if (offset > content.Length - 8)
            {
                value = default;
                return false;
            }

            value = content.ReadDoubleLittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadGuid(this byte[] content, ref int offset, out Guid value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadGuidLittleEndian(ref offset, out value);
            else
                return content.TryReadGuidBigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadGuidBigEndian(this byte[] content, ref int offset, out Guid value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadGuidBigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadGuidLittleEndian(this byte[] content, ref int offset, out Guid value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadGuidLittleEndian(ref offset);
            return true;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt128(this byte[] content, ref int offset, out Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadInt128LittleEndian(ref offset, out value);
            else
                return content.TryReadInt128BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt128BigEndian(this byte[] content, ref int offset, out Int128 value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadInt128BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt128LittleEndian(this byte[] content, ref int offset, out Int128 value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadInt128LittleEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt128(this byte[] content, ref int offset, out UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadUInt128LittleEndian(ref offset, out value);
            else
                return content.TryReadUInt128BigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt128BigEndian(this byte[] content, ref int offset, out UInt128 value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt128BigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt128LittleEndian(this byte[] content, ref int offset, out UInt128 value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadUInt128LittleEndian(ref offset);
            return true;
        }
#endif

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDecimal(this byte[] content, ref int offset, out decimal value)
        {
            if (BitConverter.IsLittleEndian)
                return content.TryReadDecimalLittleEndian(ref offset, out value);
            else
                return content.TryReadDecimalBigEndian(ref offset, out value);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDecimalBigEndian(this byte[] content, ref int offset, out decimal value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadDecimalBigEndian(ref offset);
            return true;
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDecimalLittleEndian(this byte[] content, ref int offset, out decimal value)
        {
            if (offset > content.Length - 16)
            {
                value = default;
                return false;
            }

            value = content.ReadDecimalLittleEndian(ref offset);
            return true;
        }

        #endregion
    }
}
