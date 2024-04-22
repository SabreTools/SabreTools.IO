using System;
using System.IO;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Big endian reading overloads for BinaryReader
    /// </summary>
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    /// <remarks>TODO: Add U/Int128 methods</remarks>
    public static class BinaryReaderExtensions
    {
        /// <inheritdoc cref="BinaryReader.Read(byte[], int, int)"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadBigEndian(this BinaryReader reader, byte[] buffer, int index, int count)
        {
            int retval = reader.Read(buffer, index, count);
            Array.Reverse(buffer);
            return retval;
        }

        /// <inheritdoc cref="BinaryReader.Read(char[], int, int)"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadBigEndian(this BinaryReader reader, char[] buffer, int index, int count)
        {
            int retval = reader.Read(buffer, index, count);
            Array.Reverse(buffer);
            return retval;
        }

        /// <inheritdoc cref="BinaryReader.ReadBytes(int)"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ReadBytesBigEndian(this BinaryReader reader, int count)
        {
            byte[] retval = reader.ReadBytes(count);
            Array.Reverse(retval);
            return retval;
        }

        /// <inheritdoc cref="BinaryReader.ReadChars(int)"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static char[] ReadCharsBigEndian(this BinaryReader reader, int count)
        {
            char[] retval = reader.ReadChars(count);
            Array.Reverse(retval);
            return retval;
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(2);
            Array.Reverse(retval);
            return BitConverter.ToInt16(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(2);
            Array.Reverse(retval);
            return BitConverter.ToUInt16(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(4);
            Array.Reverse(retval);
            return BitConverter.ToInt32(retval, 0);
        }

        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(4);
            Array.Reverse(retval);
            return BitConverter.ToUInt32(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadSingle"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(4);
            Array.Reverse(retval);
            return BitConverter.ToSingle(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(8);
            Array.Reverse(retval);
            return BitConverter.ToInt64(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(8);
            Array.Reverse(retval);
            return BitConverter.ToUInt64(retval, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadDouble"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(8);
            Array.Reverse(retval);
            return BitConverter.ToDouble(retval, 0);
        }

        /// <summary>
        /// Read a Guid from the underlying stream
        /// </summary>
        public static Guid ReadGuid(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid from the underlying stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

        /// <inheritdoc cref="BinaryReader.ReadDecimal"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this BinaryReader reader)
        {
            byte[] retval = reader.ReadBytes(16);
            Array.Reverse(retval);

            int i1 = BitConverter.ToInt32(retval, 0);
            int i2 = BitConverter.ToInt32(retval, 4);
            int i3 = BitConverter.ToInt32(retval, 8);
            int i4 = BitConverter.ToInt32(retval, 12);

            return new decimal([i1, i2, i3, i4]);
        }
    }
}
