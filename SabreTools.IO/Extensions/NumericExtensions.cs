namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for numeric conversion
    /// </summary>
    /// TODO: Add fractional types (Half, Single, Double, Decimal)
    /// TODO: Add GUID
    public static class NumericExtensions
    {
        #region From Byte Array

        /// <summary>
        /// Convert a byte array at an offset to an Int16
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ToInt16BigEndian(this byte[] value, int offset)
        {
            return (short)(value[offset + 1]
                        | (value[offset + 0] << 8));
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int16
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ToInt16LittleEndian(this byte[] value, int offset)
        {
            return (short)(value[offset + 0]
                        | (value[offset + 1] << 8));
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt16
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ToUInt16BigEndian(this byte[] value, int offset)
        {
            return (ushort)(value[offset + 1]
                         | (value[offset + 0] << 8));
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt16
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ToUInt16LittleEndian(this byte[] value, int offset)
        {
            return (ushort)(value[offset + 0]
                         | (value[offset + 1] << 8));
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ToInt24BigEndian(this byte[] value, int offset)
        {
            return value[offset + 2]
                | (value[offset + 1] << 8)
                | (value[offset + 0] << 16);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ToInt24LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | (value[offset + 1] << 8)
                | (value[offset + 2] << 16);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ToUInt24BigEndian(this byte[] value, int offset)
        {
            return (uint)(value[offset + 2]
                       | (value[offset + 1] << 8)
                       | (value[offset + 0] << 16));
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ToUInt24LittleEndian(this byte[] value, int offset)
        {
            return (uint)(value[offset + 0]
                        | (value[offset + 1] << 8)
                        | (value[offset + 2] << 16));
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ToInt32BigEndian(this byte[] value, int offset)
        {
            return value[offset + 3]
                | (value[offset + 2] << 8)
                | (value[offset + 1] << 16)
                | (value[offset + 0] << 24);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ToInt32LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | (value[offset + 1] << 8)
                | (value[offset + 2] << 16)
                | (value[offset + 3] << 24);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ToUInt32BigEndian(this byte[] value, int offset)
        {
            return (uint)(value[offset + 3]
                       | (value[offset + 2] << 8)
                       | (value[offset + 1] << 16)
                       | (value[offset + 0] << 24));
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ToUInt32LittleEndian(this byte[] value, int offset)
        {
            return (uint)(value[offset + 0]
                       | (value[offset + 1] << 8)
                       | (value[offset + 2] << 16)
                       | (value[offset + 3] << 24));
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ToInt48BigEndian(this byte[] value, int offset)
        {
            return value[offset + 5]
                | ((long)value[offset + 4] << 8)
                | ((long)value[offset + 3] << 16)
                | ((long)value[offset + 2] << 24)
                | ((long)value[offset + 1] << 32)
                | ((long)value[offset + 0] << 40);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ToInt48LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((long)value[offset + 1] << 8)
                | ((long)value[offset + 2] << 16)
                | ((long)value[offset + 3] << 24)
                | ((long)value[offset + 4] << 32)
                | ((long)value[offset + 5] << 40);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ToUInt48BigEndian(this byte[] value, int offset)
        {
            return value[offset + 5]
                | ((ulong)value[offset + 4] << 8)
                | ((ulong)value[offset + 3] << 16)
                | ((ulong)value[offset + 2] << 24)
                | ((ulong)value[offset + 1] << 32)
                | ((ulong)value[offset + 0] << 40);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ToUInt48LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((ulong)value[offset + 1] << 8)
                | ((ulong)value[offset + 2] << 16)
                | ((ulong)value[offset + 3] << 24)
                | ((ulong)value[offset + 4] << 32)
                | ((ulong)value[offset + 5] << 40);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ToInt64BigEndian(this byte[] value, int offset)
        {
            return value[offset + 7]
                | ((long)value[offset + 6] << 8)
                | ((long)value[offset + 5] << 16)
                | ((long)value[offset + 4] << 24)
                | ((long)value[offset + 3] << 32)
                | ((long)value[offset + 2] << 40)
                | ((long)value[offset + 1] << 48)
                | ((long)value[offset + 0] << 56);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ToInt64LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((long)value[offset + 1] << 8)
                | ((long)value[offset + 2] << 16)
                | ((long)value[offset + 3] << 24)
                | ((long)value[offset + 4] << 32)
                | ((long)value[offset + 5] << 40)
                | ((long)value[offset + 6] << 48)
                | ((long)value[offset + 7] << 56);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ToUInt64BigEndian(this byte[] value, int offset)
        {
            return value[offset + 7]
                | ((ulong)value[offset + 6] << 8)
                | ((ulong)value[offset + 5] << 16)
                | ((ulong)value[offset + 4] << 24)
                | ((ulong)value[offset + 3] << 32)
                | ((ulong)value[offset + 2] << 40)
                | ((ulong)value[offset + 1] << 48)
                | ((ulong)value[offset + 0] << 56);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ToUInt64LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((ulong)value[offset + 1] << 8)
                | ((ulong)value[offset + 2] << 16)
                | ((ulong)value[offset + 3] << 24)
                | ((ulong)value[offset + 4] << 32)
                | ((ulong)value[offset + 5] << 40)
                | ((ulong)value[offset + 6] << 48)
                | ((ulong)value[offset + 7] << 56);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert a byte array at an offset to an Int128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static System.Int128 ToInt128BigEndian(this byte[] value, int offset)
        {
            return value[offset + 15]
                | ((System.Int128)value[offset + 14] << 8)
                | ((System.Int128)value[offset + 13] << 16)
                | ((System.Int128)value[offset + 12] << 24)
                | ((System.Int128)value[offset + 11] << 32)
                | ((System.Int128)value[offset + 10] << 40)
                | ((System.Int128)value[offset + 9] << 48)
                | ((System.Int128)value[offset + 8] << 56)
                | ((System.Int128)value[offset + 7] << 64)
                | ((System.Int128)value[offset + 6] << 72)
                | ((System.Int128)value[offset + 5] << 80)
                | ((System.Int128)value[offset + 4] << 88)
                | ((System.Int128)value[offset + 3] << 96)
                | ((System.Int128)value[offset + 2] << 104)
                | ((System.Int128)value[offset + 1] << 112)
                | ((System.Int128)value[offset + 0] << 120);
        }

        /// <summary>
        /// Convert a byte array at an offset to an Int128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static System.Int128 ToInt128LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((System.Int128)value[offset + 1] << 8)
                | ((System.Int128)value[offset + 2] << 16)
                | ((System.Int128)value[offset + 3] << 24)
                | ((System.Int128)value[offset + 4] << 32)
                | ((System.Int128)value[offset + 5] << 40)
                | ((System.Int128)value[offset + 6] << 48)
                | ((System.Int128)value[offset + 7] << 56)
                | ((System.Int128)value[offset + 8] << 64)
                | ((System.Int128)value[offset + 9] << 72)
                | ((System.Int128)value[offset + 10] << 80)
                | ((System.Int128)value[offset + 11] << 88)
                | ((System.Int128)value[offset + 12] << 96)
                | ((System.Int128)value[offset + 13] << 104)
                | ((System.Int128)value[offset + 14] << 112)
                | ((System.Int128)value[offset + 15] << 120);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static System.UInt128 ToUInt128BigEndian(this byte[] value, int offset)
        {
            return value[offset + 15]
                | ((System.UInt128)value[offset + 14] << 8)
                | ((System.UInt128)value[offset + 13] << 16)
                | ((System.UInt128)value[offset + 12] << 24)
                | ((System.UInt128)value[offset + 11] << 32)
                | ((System.UInt128)value[offset + 10] << 40)
                | ((System.UInt128)value[offset + 9] << 48)
                | ((System.UInt128)value[offset + 8] << 56)
                | ((System.UInt128)value[offset + 7] << 64)
                | ((System.UInt128)value[offset + 6] << 72)
                | ((System.UInt128)value[offset + 5] << 80)
                | ((System.UInt128)value[offset + 4] << 88)
                | ((System.UInt128)value[offset + 3] << 96)
                | ((System.UInt128)value[offset + 2] << 104)
                | ((System.UInt128)value[offset + 1] << 112)
                | ((System.UInt128)value[offset + 0] << 120);
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static System.UInt128 ToUInt128LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((System.UInt128)value[offset + 1] << 8)
                | ((System.UInt128)value[offset + 2] << 16)
                | ((System.UInt128)value[offset + 3] << 24)
                | ((System.UInt128)value[offset + 4] << 32)
                | ((System.UInt128)value[offset + 5] << 40)
                | ((System.UInt128)value[offset + 6] << 48)
                | ((System.UInt128)value[offset + 7] << 56)
                | ((System.UInt128)value[offset + 8] << 64)
                | ((System.UInt128)value[offset + 9] << 72)
                | ((System.UInt128)value[offset + 10] << 80)
                | ((System.UInt128)value[offset + 11] << 88)
                | ((System.UInt128)value[offset + 12] << 96)
                | ((System.UInt128)value[offset + 13] << 104)
                | ((System.UInt128)value[offset + 14] << 112)
                | ((System.UInt128)value[offset + 15] << 120);
        }
#endif

        #endregion

        #region To Byte Array

        /// <summary>
        /// Convert an Int16 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this short value)
        {
            byte[] output =
            [
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int16 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this short value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt16 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this ushort value)
        {
            byte[] output =
            [
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt16 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this ushort value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int24 encoded as an Int32 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayAsInt24BigEndian(this int value)
        {
            byte[] output =
            [
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int24 encoded as an Int32 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayAsInt24LittleEndian(this int value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt24 encoded as a UInt32 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayAsUInt24BigEndian(this uint value)
        {
            byte[] output =
            [
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt24 encoded as a UInt32 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayAsUInt24LittleEndian(this uint value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int32 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this int value)
        {
            byte[] output =
            [
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int32 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this int value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt32 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this uint value)
        {
            byte[] output =
            [
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt32 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this uint value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int48 encoded as an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayAsInt48BigEndian(this long value)
        {
            byte[] output =
            [
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int48 encoded as an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayAsInt48LittleEndian(this long value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Converta UInt48 encoded as a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayAsUInt48BigEndian(this ulong value)
        {
            byte[] output =
            [
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt48 encoded as a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayAsUInt48LittleEndian(this ulong value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this long value)
        {
            byte[] output =
            [
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this long value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this ulong value)
        {
            byte[] output =
            [
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this ulong value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
            ];

            return output;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this System.Int128 value)
        {
            byte[] output =
            [
                (byte)((value >> 120) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this System.Int128 value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 120) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ToByteArrayBigEndian(this System.UInt128 value)
        {
            byte[] output =
            [
                (byte)((value >> 120) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] ToByteArrayLittleEndian(this System.UInt128 value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 120) & 0xFF),
            ];

            return output;
        }
#endif

#endregion
    }
}
