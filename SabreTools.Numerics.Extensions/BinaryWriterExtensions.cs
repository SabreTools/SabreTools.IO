using System;
using System.IO;
using System.Text;

namespace SabreTools.Numerics.Extensions
{
    /// <summary>
    /// Extensions for BinaryWriter
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <inheritdoc cref="BinaryWriter.Write(byte)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt8 value)
        {
            writer.Write(value.LittleEndian);
            writer.Write(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(sbyte)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt8 value)
        {
            writer.Write(value.LittleEndian);
            writer.Write(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(byte[])"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(writer, value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(char)"/>
        public static bool Write(this BinaryWriter writer, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, short value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, short value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt16 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ushort value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, ushort value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt16 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a Half to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, Half value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(Half)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Half value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(Half)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, Half value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }
#endif

        /// <summary>
        /// Write an Int24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, Int24 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Int24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, Int24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, UInt24 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, UInt24 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, UInt24 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, int value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, int value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt32 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, uint value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, uint value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt32 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(float)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, float value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(float)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, float value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, Int48 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Int48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, Int48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, UInt48 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, UInt48 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, UInt48 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, long value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, long value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt64 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt64 value)
        {
            writer.WriteLittleEndian(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(double)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, double value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(double)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, double value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, Guid value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write an Int128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in machine native format</remarks>
        public static bool Write(this BinaryWriter writer, UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return writer.WriteLittleEndian(value);
            else
                return writer.WriteBigEndian(value);
        }

        /// <summary>
        /// Write a UInt128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt128 to the underlying stream
        /// </summary>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }
#endif

        /// <inheritdoc cref="BinaryWriter.Write(decimal)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, decimal value)
        {
            byte[] buffer = value.GetBytesBigEndian();
            return WriteFromBuffer(writer, buffer);
        }

         /// <inheritdoc cref="BinaryWriter.Write(decimal)"/>
        /// <remarks>Writes in little-endian format</remarks>
        public static bool WriteLittleEndian(this BinaryWriter writer, decimal value)
        {
            byte[] buffer = value.GetBytesLittleEndian();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an array of bytes to the underlying stream
        /// </summary>
        private static bool WriteFromBuffer(BinaryWriter writer, byte[] value)
        {
            // If the stream is not writable
            if (!writer.BaseStream.CanWrite)
                return false;

            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // Handle the general case, forcing a write of the correct length
            writer.Write(value, 0, value.Length);
            return true;
        }
    }
}
