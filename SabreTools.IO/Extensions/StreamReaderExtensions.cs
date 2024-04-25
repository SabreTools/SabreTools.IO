using System;
using System.Collections.Generic;
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
    public static class StreamReaderExtensions
    {
        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        public static byte ReadByteValue(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8[] from the stream
        /// </summary>
        public static byte[] ReadBytes(this Stream stream, int count)
            => ReadToBuffer(stream, count);

        /// <summary>
        /// Read an Int8 from the stream
        /// </summary>
        public static sbyte ReadSByte(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char from the stream
        /// </summary>
        public static char ReadChar(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        public static short ReadInt16(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 2);
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        public static ushort ReadUInt16(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 2);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32
        /// </summary>
        public static int ReadInt24(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32
        /// </summary>
        public static uint ReadUInt24(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        public static int ReadInt32(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        public static uint ReadUInt32(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        public static float ReadSingle(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64
        /// </summary>
        public static long ReadInt48(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64
        /// </summary>
        public static ulong ReadUInt48(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        public static long ReadInt64(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        public static ulong ReadUInt64(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        public static double ReadDouble(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        public static decimal ReadDecimal(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);

            int i1 = BitConverter.ToInt32(buffer, 0);
            int i2 = BitConverter.ToInt32(buffer, 4);
            int i3 = BitConverter.ToInt32(buffer, 8);
            int i4 = BitConverter.ToInt32(buffer, 12);

            return new decimal([i1, i2, i3, i4]);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);

            int i1 = BitConverter.ToInt32(buffer, 0);
            int i2 = BitConverter.ToInt32(buffer, 4);
            int i3 = BitConverter.ToInt32(buffer, 8);
            int i4 = BitConverter.ToInt32(buffer, 12);

            return new decimal([i1, i2, i3, i4]);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        public static Guid ReadGuid(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        public static Int128 ReadInt128(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        public static UInt128 ReadUInt128(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return (UInt128)new BigInteger(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadNullTerminatedString(this Stream stream, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.Equals(Encoding.ASCII))
                return stream.ReadNullTerminatedAnsiString();
            else if (encoding.Equals(Encoding.Unicode))
                return stream.ReadNullTerminatedUnicodeString();

            if (stream.Position >= stream.Length)
                return null;

            List<byte> buffer = [];
            while (stream.Position < stream.Length)
            {
                byte ch = stream.ReadByteValue();
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the stream
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            List<byte> buffer = [];
            while (stream.Position < stream.Length)
            {
                byte ch = stream.ReadByteValue();
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return Encoding.ASCII.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated Unicode string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            List<byte> buffer = [];
            while (stream.Position < stream.Length)
            {
                byte[] ch = stream.ReadBytes(2);
                buffer.AddRange(ch);
                if (ch[0] == '\0' && ch[1] == '\0')
                    break;
            }

            return Encoding.Unicode.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a byte-prefixed ASCII string from the stream
        /// </summary>
        public static string? ReadPrefixedAnsiString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte size = stream.ReadByteValue();
            if (stream.Position + size >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the stream
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            ushort size = stream.ReadUInt16();
            if (stream.Position + size >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this Stream stream)
            => stream.ReadQuotedString(Encoding.Default);

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this Stream stream, Encoding encoding)
        {
            if (stream.Position >= stream.Length)
                return null;

            var bytes = new List<byte>();
            bool openQuote = false;
            while (stream.Position < stream.Length)
            {
                // Read the byte value
                byte b = stream.ReadByteValue();

                // If we have a quote, flip the flag
                if (b == (byte)'"')
                    openQuote = !openQuote;

                // If we have a newline not in a quoted string, exit the loop
                else if (b == (byte)'\n' && !openQuote)
                    break;

                // Add the byte to the set
                bytes.Add(b);
            }

            var line = encoding.GetString([.. bytes]);
            return line.TrimEnd();
        }

        /// <summary>
        /// Read a <typeparamref name="T"/> from the stream
        /// </summary>
        public static T? ReadType<T>(this Stream stream)
        {
            int typeSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = ReadToBuffer(stream, typeSize);

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var data = (T?)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return data;
        }

        /// <summary>
        /// Read a number of bytes from the stream to a buffer
        /// </summary>
        private static byte[] ReadToBuffer(Stream stream, int length)
        {
            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            int read = stream.Read(buffer, 0, length);
            if (read < length)
                throw new EndOfStreamException(nameof(stream));

            return buffer;
        }
    }
}
