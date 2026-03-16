using System;
using System.Collections.Generic;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for numeric conversion
    /// </summary>
    public static class NumericExtensions
    {
        #region From Byte Array

        /// <summary>
        /// Convert a byte array to an Int16
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ToInt16BigEndian(this byte[] value)
            => value.ToInt16BigEndian(0);

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
        /// Convert a byte array to an Int16
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ToInt16LittleEndian(this byte[] value)
            => value.ToInt16LittleEndian(0);

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
        /// Convert a byte array to a UInt16
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ToUInt16BigEndian(this byte[] value)
            => value.ToUInt16BigEndian(0);

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
        /// Convert a byte array to a UInt16
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ToUInt16LittleEndian(this byte[] value)
            => value.ToUInt16LittleEndian(0);

        /// <summary>
        /// Convert a byte array at an offset to a UInt16
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ToUInt16LittleEndian(this byte[] value, int offset)
        {
            return (ushort)(value[offset + 0]
                         | (value[offset + 1] << 8));
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Convert a byte array to a Half
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ToHalfBigEndian(this byte[] value)
            => value.ToHalfBigEndian(0);

        /// <summary>
        /// Convert a byte array to a Half
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe Half ToHalfBigEndian(this byte[] value, int offset)
        {
            var output = new Half();
            byte* p = (byte*)&output;

            *p++ = value[1];
            *p = value[0];

            return output;
        }

        /// <summary>
        /// Convert a byte array to a Half
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Half ToHalfLittleEndian(this byte[] value)
            => value.ToHalfLittleEndian(0);

        /// <summary>
        /// Convert a byte array to a Half
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe Half ToHalfLittleEndian(this byte[] value, int offset)
        {
            var output = new Half();
            byte* p = (byte*)&output;

            *p++ = value[0];
            *p = value[1];

            return output;
        }
#endif

        /// <summary>
        /// Convert a byte array to an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ToInt24BigEndian(this byte[] value)
            => value.ToInt24BigEndian(0);

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
        /// Convert a byte array to an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ToInt24LittleEndian(this byte[] value)
            => value.ToInt24LittleEndian(0);

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
        /// Convert a byte array to a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ToUInt24BigEndian(this byte[] value)
            => value.ToUInt24BigEndian(0);

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
        /// Convert a byte array to a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ToUInt24LittleEndian(this byte[] value)
            => value.ToUInt24LittleEndian(0);

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
        /// Convert a byte array to an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ToInt32BigEndian(this byte[] value)
            => value.ToInt32BigEndian(0);

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
        /// Convert a byte array to an Int32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ToInt32LittleEndian(this byte[] value)
            => value.ToInt32LittleEndian(0);

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
        /// Convert a byte array to a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ToUInt32BigEndian(this byte[] value)
            => value.ToUInt32BigEndian(0);

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
        /// Convert a byte array to a UInt32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ToUInt32LittleEndian(this byte[] value)
            => value.ToUInt32LittleEndian(0);

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
        /// Convert a byte array to a Single
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ToSingleBigEndian(this byte[] value)
            => value.ToSingleBigEndian(0);

        /// <summary>
        /// Convert a byte array to a Single
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe float ToSingleBigEndian(this byte[] value, int offset)
        {
            var output = new float();
            byte* p = (byte*)&output;

            *p++ = value[3];
            *p++ = value[2];
            *p++ = value[1];
            *p = value[0];

            return output;
        }

        /// <summary>
        /// Convert a byte array to a Single
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static float ToSingleLittleEndian(this byte[] value)
            => value.ToSingleLittleEndian(0);

        /// <summary>
        /// Convert a byte array to a Single
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe float ToSingleLittleEndian(this byte[] value, int offset)
        {
            var output = new float();
            byte* p = (byte*)&output;

            *p++ = value[0];
            *p++ = value[1];
            *p++ = value[2];
            *p = value[3];

            return output;
        }

        /// <summary>
        /// Convert a byte array to an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ToInt48BigEndian(this byte[] value)
            => value.ToInt48BigEndian(0);

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
        /// Convert a byte array to an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ToInt48LittleEndian(this byte[] value)
            => value.ToInt48LittleEndian(0);

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
        /// Convert a byte array to a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ToUInt48BigEndian(this byte[] value)
            => value.ToUInt48BigEndian(0);

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
        /// Convert a byte array to a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ToUInt48LittleEndian(this byte[] value)
            => value.ToUInt48LittleEndian(0);

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
        /// Convert a byte array to an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ToInt64BigEndian(this byte[] value)
            => value.ToInt64BigEndian(0);

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
        /// Convert a byte array to an Int64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ToInt64LittleEndian(this byte[] value)
            => value.ToInt64LittleEndian(0);

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
        /// Convert a byte array to a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ToUInt64BigEndian(this byte[] value)
            => value.ToUInt64BigEndian(0);

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
        /// Convert a byte array to a UInt64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ToUInt64LittleEndian(this byte[] value)
            => value.ToUInt64LittleEndian(0);

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

        /// <summary>
        /// Convert a byte array to a Double
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ToDoubleBigEndian(this byte[] value)
            => value.ToDoubleBigEndian(0);

        /// <summary>
        /// Convert a byte array to a Double
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe double ToDoubleBigEndian(this byte[] value, int offset)
        {
            var output = new double();
            byte* p = (byte*)&output;

            *p++ = value[7];
            *p++ = value[6];
            *p++ = value[5];
            *p++ = value[4];
            *p++ = value[3];
            *p++ = value[2];
            *p++ = value[1];
            *p = value[0];

            return output;
        }

        /// <summary>
        /// Convert a byte array to a Double
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static double ToDoubleLittleEndian(this byte[] value)
            => value.ToDoubleLittleEndian(0);

        /// <summary>
        /// Convert a byte array to a Double
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe double ToDoubleLittleEndian(this byte[] value, int offset)
        {
            var output = new double();
            byte* p = (byte*)&output;

            *p++ = value[0];
            *p++ = value[1];
            *p++ = value[2];
            *p++ = value[3];
            *p++ = value[4];
            *p++ = value[5];
            *p++ = value[6];
            *p = value[7];

            return output;
        }

        /// <summary>
        /// Convert a byte array to a Guid
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ToGuidBigEndian(this byte[] value)
            => value.ToGuidBigEndian(0);

        /// <summary>
        /// Convert a byte array to a Guid
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ToGuidBigEndian(this byte[] value, int offset)
        {
            int a = value.ReadInt32BigEndian(ref offset);
            short b = value.ReadInt16BigEndian(ref offset);
            short c = value.ReadInt16BigEndian(ref offset);
            byte[] d = value.ReadBytes(ref offset, 8);

            return new Guid(a, b, c, d);
        }

        /// <summary>
        /// Convert a byte array to a Guid
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Guid ToGuidLittleEndian(this byte[] value)
            => value.ToGuidLittleEndian(0);

        /// <summary>
        /// Convert a byte array to a Guid
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Guid ToGuidLittleEndian(this byte[] value, int offset)
        {
            int a = value.ReadInt32LittleEndian(ref offset);
            short b = value.ReadInt16LittleEndian(ref offset);
            short c = value.ReadInt16LittleEndian(ref offset);
            byte[] d = value.ReadBytes(ref offset, 8);

            return new Guid(a, b, c, d);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert a byte array to an Int128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ToInt128BigEndian(this byte[] value)
            => value.ToInt128BigEndian(0);

        /// <summary>
        /// Convert a byte array at an offset to an Int128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ToInt128BigEndian(this byte[] value, int offset)
        {
            return value[offset + 15]
                | ((Int128)value[offset + 14] << 8)
                | ((Int128)value[offset + 13] << 16)
                | ((Int128)value[offset + 12] << 24)
                | ((Int128)value[offset + 11] << 32)
                | ((Int128)value[offset + 10] << 40)
                | ((Int128)value[offset + 9] << 48)
                | ((Int128)value[offset + 8] << 56)
                | ((Int128)value[offset + 7] << 64)
                | ((Int128)value[offset + 6] << 72)
                | ((Int128)value[offset + 5] << 80)
                | ((Int128)value[offset + 4] << 88)
                | ((Int128)value[offset + 3] << 96)
                | ((Int128)value[offset + 2] << 104)
                | ((Int128)value[offset + 1] << 112)
                | ((Int128)value[offset + 0] << 120);
        }

        /// <summary>
        /// Convert a byte array to an Int128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 ToInt128LittleEndian(this byte[] value)
            => value.ToInt128LittleEndian(0);

        /// <summary>
        /// Convert a byte array at an offset to an Int128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 ToInt128LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((Int128)value[offset + 1] << 8)
                | ((Int128)value[offset + 2] << 16)
                | ((Int128)value[offset + 3] << 24)
                | ((Int128)value[offset + 4] << 32)
                | ((Int128)value[offset + 5] << 40)
                | ((Int128)value[offset + 6] << 48)
                | ((Int128)value[offset + 7] << 56)
                | ((Int128)value[offset + 8] << 64)
                | ((Int128)value[offset + 9] << 72)
                | ((Int128)value[offset + 10] << 80)
                | ((Int128)value[offset + 11] << 88)
                | ((Int128)value[offset + 12] << 96)
                | ((Int128)value[offset + 13] << 104)
                | ((Int128)value[offset + 14] << 112)
                | ((Int128)value[offset + 15] << 120);
        }

        /// <summary>
        /// Convert a byte array to a UInt128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ToUInt128BigEndian(this byte[] value)
            => value.ToUInt128BigEndian(0);

        /// <summary>
        /// Convert a byte array at an offset to a UInt128
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ToUInt128BigEndian(this byte[] value, int offset)
        {
            return value[offset + 15]
                | ((UInt128)value[offset + 14] << 8)
                | ((UInt128)value[offset + 13] << 16)
                | ((UInt128)value[offset + 12] << 24)
                | ((UInt128)value[offset + 11] << 32)
                | ((UInt128)value[offset + 10] << 40)
                | ((UInt128)value[offset + 9] << 48)
                | ((UInt128)value[offset + 8] << 56)
                | ((UInt128)value[offset + 7] << 64)
                | ((UInt128)value[offset + 6] << 72)
                | ((UInt128)value[offset + 5] << 80)
                | ((UInt128)value[offset + 4] << 88)
                | ((UInt128)value[offset + 3] << 96)
                | ((UInt128)value[offset + 2] << 104)
                | ((UInt128)value[offset + 1] << 112)
                | ((UInt128)value[offset + 0] << 120);
        }

        /// <summary>
        /// Convert a byte array to a UInt128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 ToUInt128LittleEndian(this byte[] value)
            => value.ToUInt128LittleEndian(0);

        /// <summary>
        /// Convert a byte array at an offset to a UInt128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 ToUInt128LittleEndian(this byte[] value, int offset)
        {
            return value[offset + 0]
                | ((UInt128)value[offset + 1] << 8)
                | ((UInt128)value[offset + 2] << 16)
                | ((UInt128)value[offset + 3] << 24)
                | ((UInt128)value[offset + 4] << 32)
                | ((UInt128)value[offset + 5] << 40)
                | ((UInt128)value[offset + 6] << 48)
                | ((UInt128)value[offset + 7] << 56)
                | ((UInt128)value[offset + 8] << 64)
                | ((UInt128)value[offset + 9] << 72)
                | ((UInt128)value[offset + 10] << 80)
                | ((UInt128)value[offset + 11] << 88)
                | ((UInt128)value[offset + 12] << 96)
                | ((UInt128)value[offset + 13] << 104)
                | ((UInt128)value[offset + 14] << 112)
                | ((UInt128)value[offset + 15] << 120);
        }
#endif

        /// <summary>
        /// Convert a byte array to a Decimal
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ToDecimalBigEndian(this byte[] value)
            => value.ToDecimalBigEndian(0);

        /// <summary>
        /// Convert a byte array to a Decimal
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ToDecimalBigEndian(this byte[] value, int offset)
        {
            int flags = value.ToInt32BigEndian(offset + 0);
            int hi = value.ToInt32BigEndian(offset + 4);
            int mid = value.ToInt32BigEndian(offset + 8);
            int lo = value.ToInt32BigEndian(offset + 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Convert a byte array to a Decimal
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal ToDecimalLittleEndian(this byte[] value)
            => value.ToDecimalLittleEndian(0);

        /// <summary>
        /// Convert a byte array to a Decimal
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal ToDecimalLittleEndian(this byte[] value, int offset)
        {
            int lo = value.ToInt32LittleEndian(offset + 0);
            int mid = value.ToInt32LittleEndian(offset + 4);
            int hi = value.ToInt32LittleEndian(offset + 8);
            int flags = value.ToInt32LittleEndian(offset + 12);

            return new decimal([lo, mid, hi, flags]);
        }

        #endregion

        #region To Byte Array

        /// <summary>
        /// Convert an Int16 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesBigEndian(this short value)
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
        public static byte[] GetBytesLittleEndian(this short value)
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
        public static byte[] GetBytesBigEndian(this ushort value)
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
        public static byte[] GetBytesLittleEndian(this ushort value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
            ];

            return output;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Convert a Half to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe byte[] GetBytesBigEndian(this Half value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p);

            if (BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }

        /// <summary>
        /// Convert a Half to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe byte[] GetBytesLittleEndian(this Half value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p);

            if (!BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }
#endif

        /// <summary>
        /// Convert an Int24 encoded as an Int32 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesAsInt24BigEndian(this int value)
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
        public static byte[] GetBytesAsInt24LittleEndian(this int value)
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
        public static byte[] GetBytesAsUInt24BigEndian(this uint value)
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
        public static byte[] GetBytesAsUInt24LittleEndian(this uint value)
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
        public static byte[] GetBytesBigEndian(this int value)
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
        public static byte[] GetBytesLittleEndian(this int value)
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
        public static byte[] GetBytesBigEndian(this uint value)
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
        public static byte[] GetBytesLittleEndian(this uint value)
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
        /// Convert a Single to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe byte[] GetBytesBigEndian(this float value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p);

            if (BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }

        /// <summary>
        /// Convert a Single to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe byte[] GetBytesLittleEndian(this float value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p);

            if (!BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }

        /// <summary>
        /// Convert an Int48 encoded as an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesAsInt48BigEndian(this long value)
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
        public static byte[] GetBytesAsInt48LittleEndian(this long value)
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
        public static byte[] GetBytesAsUInt48BigEndian(this ulong value)
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
        public static byte[] GetBytesAsUInt48LittleEndian(this ulong value)
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
        public static byte[] GetBytesBigEndian(this long value)
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
        public static byte[] GetBytesLittleEndian(this long value)
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
        public static byte[] GetBytesBigEndian(this ulong value)
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
        public static byte[] GetBytesLittleEndian(this ulong value)
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
        /// Convert a Double to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static unsafe byte[] GetBytesBigEndian(this double value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p);

            if (BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }

        /// <summary>
        /// Convert a Double to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static unsafe byte[] GetBytesLittleEndian(this double value)
        {
            byte* p = (byte*)&value;

            List<byte> output = [];

            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p++);
            output.Add(*p);

            if (!BitConverter.IsLittleEndian)
                output.Reverse();

            return [.. output];
        }

        /// <summary>
        /// Convert a Guid to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesBigEndian(this Guid value)
        {
            byte[] bytes = value.ToByteArray();
            if (BitConverter.IsLittleEndian)
            {
                int offset = 0;

                int aInt = bytes.ReadInt32LittleEndian(ref offset);
                byte[] aBytes = aInt.GetBytesBigEndian();

                short bShort = bytes.ReadInt16LittleEndian(ref offset);
                byte[] bBytes = bShort.GetBytesBigEndian();

                short cShort = bytes.ReadInt16LittleEndian(ref offset);
                byte[] cBytes = cShort.GetBytesBigEndian();

                byte[] dBytes = bytes.ReadBytes(ref offset, 8);

                return [.. aBytes, .. bBytes, .. cBytes, .. dBytes];
            }
            else
            {
                return bytes;
            }
        }

        /// <summary>
        /// Convert a Decimal to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] GetBytesLittleEndian(this Guid value)
        {
            byte[] bytes = value.ToByteArray();
            if (BitConverter.IsLittleEndian)
            {
                return bytes;
            }
            else
            {
                int offset = 0;

                int aInt = bytes.ReadInt32BigEndian(ref offset);
                byte[] aBytes = aInt.GetBytesLittleEndian();

                short bShort = bytes.ReadInt16BigEndian(ref offset);
                byte[] bBytes = bShort.GetBytesLittleEndian();

                short cShort = bytes.ReadInt16BigEndian(ref offset);
                byte[] cBytes = cShort.GetBytesLittleEndian();

                byte[] dBytes = bytes.ReadBytes(ref offset, 8);

                return [.. aBytes, .. bBytes, .. cBytes, .. dBytes];
            }
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesBigEndian(this Int128 value)
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
        public static byte[] GetBytesLittleEndian(this Int128 value)
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
        public static byte[] GetBytesBigEndian(this UInt128 value)
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
        public static byte[] GetBytesLittleEndian(this UInt128 value)
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

        /// <summary>
        /// Convert a Decimal to a byte array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] GetBytesBigEndian(this decimal value)
        {
            int[] bits = decimal.GetBits(value);

            byte[] lo = bits[0].GetBytesBigEndian();
            byte[] mid = bits[1].GetBytesBigEndian();
            byte[] hi = bits[2].GetBytesBigEndian();
            byte[] flags = bits[3].GetBytesBigEndian();

            return [.. flags, .. hi, .. mid, .. lo];
        }

        /// <summary>
        /// Convert a Decimal to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static byte[] GetBytesLittleEndian(this decimal value)
        {
            int[] bits = decimal.GetBits(value);

            byte[] lo = bits[0].GetBytesLittleEndian();
            byte[] mid = bits[1].GetBytesLittleEndian();
            byte[] hi = bits[2].GetBytesLittleEndian();
            byte[] flags = bits[3].GetBytesLittleEndian();

            return [.. lo, .. mid, .. hi, .. flags];
        }

        #endregion
    }
}
