using System;
using System.IO;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for BinaryWriter
    /// </summary>
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    /// <remarks>TODO: Add WriteDecimal methods</remarks>
    public static class BinaryWriterExtensions
    {
        #region Write

        /// <summary>
        /// Write a UInt8
        /// </summary>
        public static bool Write(this BinaryWriter writer, byte value)
            => WriteFromBuffer(writer, [value]);

        /// <summary>
        /// Write a UInt8[]
        /// </summary>
        public static bool Write(this BinaryWriter writer, byte[] value)
            => WriteFromBuffer(writer, value);

        /// <summary>
        /// Write a UInt8[]
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(writer, value);
        }

        /// <summary>
        /// Write an Int8
        /// </summary>
        public static bool Write(this BinaryWriter writer, sbyte value)
            => WriteFromBuffer(writer, [(byte)value]);

        /// <summary>
        /// Write a Char
        /// </summary>
        public static bool Write(this BinaryWriter writer, char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Char with an Encoding
        /// </summary>
        public static bool Write(this BinaryWriter writer, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        public static bool Write(this BinaryWriter writer, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        public static bool Write(this BinaryWriter writer, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        public static bool Write(this BinaryWriter writer, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        public static bool Write(this BinaryWriter writer, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        public static bool Write(this BinaryWriter writer, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        public static bool Write(this BinaryWriter writer, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        public static bool Write(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        public static bool Write(this BinaryWriter writer, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        public static bool Write(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128
        /// </summary>
        public static bool Write(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        public static bool Write(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }
#endif

        /// <summary>
        /// Write a null-terminated string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedString(this BinaryWriter writer, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Add the null terminator and write
            value += "\0";
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a null-terminated ASCII string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedAnsiString(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.ASCII);

        /// <summary>
        /// Write a null-terminated Unicode string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedUnicodeString(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.Unicode);

        /// <summary>
        /// Write a byte-prefixed ASCII string to the underlying stream
        /// </summary>
        public static bool WritePrefixedAnsiString(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.ASCII.GetBytes(value);

            // Write the length as a byte
            if (!Write(writer, (byte)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the underlying stream
        /// </summary>
        public static bool WritePrefixedUnicodeString(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            // Write the length as a ushort
            if (!Write(writer, (ushort)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the underlying stream
        /// </summary>
        public static bool WriteQuotedString(this BinaryWriter writer, string? value)
            => writer.WriteQuotedString(value, Encoding.UTF8);

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the underlying stream
        /// </summary>
        public static bool WriteQuotedString(this BinaryWriter writer, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Write without the null terminator
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a <typeparamref name="T"/> to the underlying stream
        /// </summary>
        public static bool WriteType<T>(this BinaryWriter writer, T? value)
        {
            // Handle the null case
            if (value == null)
                return false;

            int typeSize = Marshal.SizeOf(typeof(T));

            var buffer = new byte[typeSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            handle.Free();

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

        #endregion
    }
}