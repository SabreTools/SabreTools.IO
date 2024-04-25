using System;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    /// <remarks>TODO: Add WriteDecimal methods</remarks>
    public static class ByteArrayWriterExtensions
    {
        /// <summary>
        /// Write a UInt8 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, byte value)
            => WriteFromBuffer(content, ref offset, [value]);

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
        public static bool Write(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            
            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);
            
            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            
            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);
            
            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }
#endif

        /// <summary>
        /// Write a null-terminated string to the array
        /// </summary>
        public static bool WriteNullTerminatedString(this byte[] content, ref int offset, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Add the null terminator and write
            value += "\0";
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a null-terminated ASCII string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedAnsiString(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.ASCII);

        /// <summary>
        /// Write a null-terminated Unicode string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedUnicodeString(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.Unicode);

        /// <summary>
        /// Write a byte-prefixed ASCII string to the byte array
        /// </summary>
        public static bool WritePrefixedAnsiString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.ASCII.GetBytes(value);

            // Write the length as a byte
            if (!content.Write(ref offset, (byte)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the byte array
        /// </summary>
        public static bool WritePrefixedUnicodeString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            // Write the length as a ushort
            if (!content.Write(ref offset, (ushort)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the byte array
        /// </summary>
        public static bool WriteQuotedString(this byte[] content, ref int offset, string? value)
            => content.WriteQuotedString(ref offset, value, Encoding.UTF8);

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the byte array
        /// </summary>
        public static bool WriteQuotedString(this byte[] content, ref int offset, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Write without the null terminator
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a <typeparamref name="T"/> to the byte array
        /// </summary>
        public static bool WriteType<T>(this byte[] content, ref int offset, T? value)
        {
            // Handle the null case
            if (value == null)
                return false;

            int typeSize = Marshal.SizeOf(typeof(T));

            var buffer = new byte[typeSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            handle.Free();

            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an array of bytes to the byte array
        /// </summary>
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