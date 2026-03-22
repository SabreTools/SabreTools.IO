using System;
using System.Text;

namespace SabreTools.Numerics.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayWriterExtensions
    {
        /// <summary>
        /// Write a UInt8 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, byte value)
            => WriteFromBuffer(content, ref offset, [value]);

        /// <summary>
        /// Write a UInt8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt8 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.Write(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt8[] and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, byte[] value)
            => WriteFromBuffer(content, ref offset, value);

        /// <summary>
        /// Write a UInt8[] and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(content, ref offset, value);
        }

        /// <summary>
        /// Write an Int8 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, sbyte value)
            => WriteFromBuffer(content, ref offset, [(byte)value]);

        /// <summary>
        /// Write a Int8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt8 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.Write(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Char and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Char with an Encoding and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, short value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt16 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, ushort value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt16 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, Half value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Half value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, Half value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }
#endif

        /// <summary>
        /// Write an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, Int24 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Int24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, Int24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, UInt24 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, UInt24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, UInt24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, int value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt32 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, uint value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt32 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, float value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, Int48 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Int48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, Int48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, UInt48 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, UInt48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, UInt48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, long value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt64 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt64 value)
        {
            bool actual = content.WriteLittleEndian(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, double value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, Guid value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }
#endif

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this byte[] content, ref int offset, decimal value)
        {
            if (BitConverter.IsLittleEndian)
                return content.WriteLittleEndian(ref offset, value);
            else
                return content.WriteBigEndian(ref offset, value);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, decimal value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this byte[] content, ref int offset, decimal value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an array of bytes to the byte array
        /// </summary>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown if <paramref name="offset"/> into <paramref name="content"/>
        /// would not accomodate <paramref name="value"/>.
        /// </exception>
        private static bool WriteFromBuffer(byte[] content, ref int offset, byte[] value)
        {
            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // If there are not enough bytes
            if (offset + value.Length > content.Length)
                throw new System.IO.EndOfStreamException(nameof(content));

            // Handle the general case, forcing a write of the correct length
            Array.Copy(value, 0, content, offset, value.Length);
            offset += value.Length;

            return true;
        }
    }
}
