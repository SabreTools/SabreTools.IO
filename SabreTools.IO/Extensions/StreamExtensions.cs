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
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    /// <remarks>TODO: Add WriteDecimal methods</remarks>
    public static class StreamExtensions
    {
        #region Read

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
        /// Seek to a specific point in the stream, if possible
        /// </summary>
        /// <param name="input">Input stream to try seeking on</param>
        /// <param name="offset">Optional offset to seek to</param>
        public static long SeekIfPossible(this Stream input, long offset = 0)
        {
            // If the stream is null, don't even try
            if (input == null)
                return -1;

            // If the input is not seekable, just return the current position
            if (!input.CanSeek)
            {
                try
                {
                    return input.Position;
                }
                catch
                {
                    return -1;
                }
            }
            // Attempt to seek to the offset
            try
            {
                if (offset < 0)
                    return input.Seek(offset, SeekOrigin.End);
                else if (offset >= 0)
                    return input.Seek(offset, SeekOrigin.Begin);

                return input.Position;
            }
            catch
            {
                return -1;
            }
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
    
        #endregion

        #region Write

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
            byte[] buffer = encoding.GetBytes([value]);
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
            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // Handle the general case, forcing a write of the correct length
            stream.Write(value, 0, value.Length);
            return true;
        }

        #endregion
    }
}
