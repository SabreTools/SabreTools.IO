﻿using System;
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
    /// Extensions for BinaryReader
    /// </summary>
    /// TODO: Handle proper negative values for Int24 and Int48
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
            byte[] buffer = reader.ReadBytes(count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <inheritdoc cref="BinaryReader.ReadChars(int)"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static char[] ReadCharsBigEndian(this BinaryReader reader, int count)
        {
            char[] buffer = reader.ReadChars(count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32
        /// </summary>
        public static int ReadInt24(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32
        /// </summary>
        public static uint ReadUInt24(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadSingle"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64
        /// </summary>
        public static long ReadInt48(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64
        /// </summary>
        public static ulong ReadUInt48(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadDouble"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <inheritdoc cref="BinaryReader.ReadDecimal"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            Array.Reverse(buffer);

            int i1 = BitConverter.ToInt32(buffer, 0);
            int i2 = BitConverter.ToInt32(buffer, 4);
            int i3 = BitConverter.ToInt32(buffer, 8);
            int i4 = BitConverter.ToInt32(buffer, 12);

            return new decimal([i1, i2, i3, i4]);
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

        // TODO: Determine if the reverse reads are doing what are expected
#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the underlying stream
        /// </summary>
        public static Int128 ReadInt128(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the underlying stream
        /// </summary>
        public static UInt128 ReadUInt128(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            Array.Reverse(buffer);
            return (UInt128)new BigInteger(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.Equals(Encoding.ASCII))
                return reader.ReadNullTerminatedAnsiString();
            else if (encoding.Equals(Encoding.Unicode))
                return reader.ReadNullTerminatedUnicodeString();

            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            List<byte> buffer = [];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte ch = reader.ReadByte();
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            List<byte> buffer = [];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte ch = reader.ReadByte();
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return Encoding.ASCII.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated Unicode string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            List<byte> buffer = [];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte[] ch = reader.ReadBytes(2);
                buffer.AddRange(ch);
                if (ch[0] == '\0' && ch[1] == '\0')
                    break;
            }

            return Encoding.Unicode.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a byte-prefixed ASCII string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedAnsiString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte size = reader.ReadByte();
            if (reader.BaseStream.Position + size >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            ushort size = reader.ReadUInt16();
            if (reader.BaseStream.Position + size >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this BinaryReader reader)
            => reader.ReadQuotedString(Encoding.Default);

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the stream
        /// </summary>
        public static string? ReadQuotedString(this BinaryReader reader, Encoding encoding)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            var bytes = new List<byte>();
            bool openQuote = false;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                // Read the byte value
                byte b = reader.ReadByte();

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
        /// Read a <typeparamref name="T"/> from the underlying stream
        /// </summary>
        public static T? ReadType<T>(this BinaryReader reader)
        {
            int typeSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = reader.ReadBytes(typeSize);

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var data = (T?)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return data;
        }
    }
}
