using System;
using System.IO;
using System.Text;

namespace SabreTools.Numerics.Extensions
{
    /// <summary>
    /// Extensions for Streams
    /// </summary>
    public static class StreamWriterExtensions
    {
        /// <summary>
        /// Write a UInt8
        /// </summary>
        public static bool Write(this Stream stream, byte value)
            => WriteFromBuffer(stream, [value]);

        /// <summary>
        /// Write a UInt8
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothUInt8 value)
        {
            bool actual = stream.Write(value.LittleEndian);
            actual &= stream.Write(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt8[]
        /// </summary>
        public static bool Write(this Stream stream, byte[] value)
            => WriteFromBuffer(stream, value);

        /// <summary>
        /// Write a UInt8[]
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(stream, value);
        }

        /// <summary>
        /// Write an Int8
        /// </summary>
        public static bool Write(this Stream stream, sbyte value)
            => WriteFromBuffer(stream, [(byte)value]);

        /// <summary>
        /// Write a Int8
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothInt8 value)
        {
            bool actual = stream.Write(value.LittleEndian);
            actual &= stream.Write(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Char
        /// </summary>
        public static bool Write(this Stream stream, char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Char with an Encoding
        /// </summary>
        public static bool Write(this Stream stream, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, short value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, short value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, short value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Int16
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothInt16 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, ushort value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, ushort value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, ushort value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothUInt16 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a Half
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, Half value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Half
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Half value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Half
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, Half value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }
#endif

        /// <summary>
        /// Write an Int24
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, Int24 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int24
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Int24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int24
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, Int24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt24
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, UInt24 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt24
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, UInt24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt24
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, UInt24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, int value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, int value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, int value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Int32
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothInt32 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, uint value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, uint value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, uint value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothUInt32 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, float value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, float value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        public static bool WriteLittleEndian(this Stream stream, float value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int48
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, Int48 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int48
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Int48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int48
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, Int48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt48
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, UInt48 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt48
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, UInt48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt48
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, UInt48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, long value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, long value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, long value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Int64
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothInt64 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, ulong value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, ulong value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this Stream stream, BothUInt64 value)
        {
            bool actual = stream.WriteLittleEndian(value.LittleEndian);
            actual &= stream.WriteBigEndian(value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, double value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, double value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, double value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, Guid value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Guid value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, Guid value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Int128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, Int128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, UInt128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, UInt128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }
#endif

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this Stream stream, decimal value)
        {
            if (BitConverter.IsLittleEndian)
                return stream.WriteLittleEndian(value);
            else
                return stream.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, decimal value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this Stream stream, decimal value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an array of bytes to the stream
        /// </summary>
        private static bool WriteFromBuffer(Stream stream, byte[] value)
        {
            // If the stream is not writable
            if (!stream.CanWrite)
                return false;

            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // Handle the general case, forcing a write of the correct length
            stream.Write(value, 0, value.Length);
            return true;
        }
    }
}
