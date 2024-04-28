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
    /// Extensions for Streams
    /// </summary>
    /// TODO: Handle proper negative values for Int24 and Int48
    public static class StreamWriterExtensions
    {
        /// <summary>
        /// Write a UInt8
        /// </summary>
        public static bool Write(this Stream stream, byte value)
            => WriteFromBuffer(stream, [value]);

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
        public static bool Write(this Stream stream, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        public static bool Write(this Stream stream, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt16
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Write a Half
        /// </summary>
        public static bool Write(this Stream stream, Half value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Half
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Half value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }
#endif

        /// <summary>
        /// Write an Int32 as an Int24
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24(this Stream stream, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write an Int32 as an Int24
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24BigEndian(this Stream stream, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24(this Stream stream, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24BigEndian(this Stream stream, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        public static bool Write(this Stream stream, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        public static bool Write(this Stream stream, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt32
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        public static bool Write(this Stream stream, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Single
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int64 as an Int48
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48(this Stream stream, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write an Int64 as an Int48
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48BigEndian(this Stream stream, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48(this Stream stream, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48BigEndian(this Stream stream, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(stream, reduced);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        public static bool Write(this Stream stream, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write an Int64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        public static bool Write(this Stream stream, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a UInt64
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        public static bool Write(this Stream stream, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Double
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        public static bool Write(this Stream stream, decimal value)
        {
            int[] bits = decimal.GetBits(value);

            byte[] lo = BitConverter.GetBytes(bits[0]);
            byte[] mid = BitConverter.GetBytes(bits[1]);
            byte[] hi = BitConverter.GetBytes(bits[2]);
            byte[] flags = BitConverter.GetBytes(bits[3]);

            byte[] buffer = new byte[16];
            Array.Copy(lo, 0, buffer, 0, 4);
            Array.Copy(mid, 0, buffer, 4, 4);
            Array.Copy(hi, 0, buffer, 8, 4);
            Array.Copy(flags, 0, buffer, 12, 4);

            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, decimal value)
        {
            int[] bits = decimal.GetBits(value);

            byte[] lo = BitConverter.GetBytes(bits[0]);
            byte[] mid = BitConverter.GetBytes(bits[1]);
            byte[] hi = BitConverter.GetBytes(bits[2]);
            byte[] flags = BitConverter.GetBytes(bits[3]);

            byte[] buffer = new byte[16];
            Array.Copy(lo, 0, buffer, 0, 4);
            Array.Copy(mid, 0, buffer, 4, 4);
            Array.Copy(hi, 0, buffer, 8, 4);
            Array.Copy(flags, 0, buffer, 12, 4);

            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        public static bool Write(this Stream stream, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            Array.Reverse(buffer);
            return WriteFromBuffer(stream, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128
        /// </summary>
        public static bool Write(this Stream stream, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(stream, padded);
        }

        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(stream, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        public static bool Write(this Stream stream, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(stream, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this Stream stream, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(stream, padded);
        }
#endif

        /// <summary>
        /// Write a null-terminated string to the stream
        /// </summary>
        public static bool WriteNullTerminatedString(this Stream stream, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Add the null terminator and write
            value += "\0";
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a null-terminated ASCII string to the stream
        /// </summary>
        public static bool WriteNullTerminatedAnsiString(this Stream stream, string? value)
            => stream.WriteNullTerminatedString(value, Encoding.ASCII);

        /// <summary>
        /// Write a null-terminated Unicode string to the stream
        /// </summary>
        public static bool WriteNullTerminatedUnicodeString(this Stream stream, string? value)
            => stream.WriteNullTerminatedString(value, Encoding.Unicode);

        /// <summary>
        /// Write a byte-prefixed ASCII string to the stream
        /// </summary>
        public static bool WritePrefixedAnsiString(this Stream stream, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.ASCII.GetBytes(value);

            // Write the length as a byte
            if (!stream.Write((byte)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the stream
        /// </summary>
        public static bool WritePrefixedUnicodeString(this Stream stream, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            // Write the length as a ushort
            if (!stream.Write((ushort)buffer.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the stream
        /// </summary>
        public static bool WriteQuotedString(this Stream stream, string? value)
            => stream.WriteQuotedString(value, Encoding.UTF8);

        /// <summary>
        /// Write a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline to the stream
        /// </summary>
        public static bool WriteQuotedString(this Stream stream, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Write without the null terminator
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(stream, buffer);
        }

        /// <summary>
        /// Write a <typeparamref name="T"/> to the stream
        /// </summary>
        /// TODO: Fix writing as reading was fixed
        public static bool WriteType<T>(this Stream stream, T? value)
        {
            // Handle the null case
            if (value == null)
                return false;

            int typeSize = Marshal.SizeOf(typeof(T));

            var buffer = new byte[typeSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            handle.Free();

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