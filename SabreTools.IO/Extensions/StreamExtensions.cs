using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for Streams
    /// </summary>
    /// <remarks>TODO: Add U/Int24 and U/Int48 methods</remarks>
    public static class StreamExtensions
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
        /// Read a Int8 from the stream
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
        /// Read a Int16 from the stream
        /// </summary>
        public static short ReadInt16(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a Int16 from the stream in big-endian format
        /// </summary>
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
        /// Read a UInt16 from the stream in big-endian format
        /// </summary>
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
        /// Read an Int32 from the stream in big-endian format
        /// </summary>
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
        /// Read a UInt32 from the stream in big-endian format
        /// </summary>
        public static uint ReadUInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 from the stream
        /// </summary>
        public static long ReadInt64(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Int64 from the stream in big-endian format
        /// </summary>
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
        /// Read a UInt64 from the stream in big-endian format
        /// </summary>
        public static ulong ReadUInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
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
        /// Read a Guid from the stream in big-endian format
        /// </summary>
        public static Guid ReadGuidBigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read a Int128 from the stream
        /// </summary>
        public static Int128 ReadInt128(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            return new Int128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a Int128 from the stream in big-endian format
        /// </summary>
        public static Int128 ReadInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new Int128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        public static UInt128 ReadUInt128(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            return new UInt128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }

        /// <summary>
        /// Read a UInt128 from the stream in big-endian format
        /// </summary>
        public static UInt128 ReadUInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new UInt128(BitConverter.ToUInt64(buffer, 0), BitConverter.ToUInt64(buffer, 8));
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadString(this Stream stream)
            => stream.ReadString(Encoding.Default);

        /// <summary>
        /// Read a null-terminated string from the stream
        /// </summary>
        public static string? ReadString(this Stream stream, Encoding encoding)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes("\0");
            int charWidth = nullTerminator.Length;

            var tempBuffer = new List<byte>();

            byte[] buffer = new byte[charWidth];
            while (stream.Position < stream.Length && stream.Read(buffer, 0, charWidth) != 0 && !buffer.SequenceEqual(nullTerminator))
            {
                tempBuffer.AddRange(buffer);
            }

            return encoding.GetString([.. tempBuffer]);
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
        /// Read a number of bytes from the current Stream to a buffer
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
