﻿using System;
using System.Collections.Generic;
using System.IO;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for Streams
    /// </summary>
    public static class StreamReaderExtensions
    {
        /// <summary>
        /// Read a UInt8 from the stream
        /// </summary>
        public static byte ReadByteValue(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8[] from the stream
        /// </summary>
        public static byte[] ReadBytes(this Stream stream, int count)
            => ReadExactlyToBuffer(stream, count);

        /// <summary>
        /// Read an Int8 from the stream
        /// </summary>
        public static sbyte ReadSByte(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char from the stream
        /// </summary>
        public static char ReadChar(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static short ReadInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt16LittleEndian();
            else
                return stream.ReadInt16BigEndian();
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (short)(buffer[1]
                        | (buffer[0] << 8));
        }

        /// <summary>
        /// Read an Int16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (short)(buffer[0]
                        | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadUInt16(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt16LittleEndian();
            else
                return stream.ReadUInt16BigEndian();
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (ushort)(buffer[1]
                         | (buffer[0] << 8));
        }

        /// <summary>
        /// Read a UInt16 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return (ushort)(buffer[0]
                         | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ushort ReadWORD(this Stream stream)
            => stream.ReadUInt16();

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadWORDBigEndian(this Stream stream)
            => stream.ReadUInt16BigEndian();

        /// <summary>
        /// Read a WORD (2-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadWORDLittleEndian(this Stream stream)
            => stream.ReadUInt16LittleEndian();

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            return BitConverter.ToHalf(buffer, 0);
        }

        /// <summary>
        /// Read a Half from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 2);
            Array.Reverse(buffer);
            return BitConverter.ToHalf(buffer, 0);
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt24LittleEndian();
            else
                return stream.ReadInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (int)(buffer[2]
                      | (buffer[1] << 8)
                      | (buffer[0] << 16));
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt24(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt24LittleEndian();
            else
                return stream.ReadUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (uint)(buffer[2]
                       | (buffer[1] << 8)
                       | (buffer[0] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt24LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 3);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16));
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt32LittleEndian();
            else
                return stream.ReadInt32BigEndian();
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (int)(buffer[3]
                      | (buffer[2] << 8)
                      | (buffer[1] << 16)
                      | (buffer[0] << 24));
        }

        /// <summary>
        /// Read an Int32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16)
                      | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt32(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt32LittleEndian();
            else
                return stream.ReadUInt32BigEndian();
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (uint)(buffer[3]
                       | (buffer[2] << 8)
                       | (buffer[1] << 16)
                       | (buffer[0] << 24));
        }

        /// <summary>
        /// Read a UInt32 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16)
                       | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadDWORD(this Stream stream)
            => stream.ReadUInt32();

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadDWORDBigEndian(this Stream stream)
            => stream.ReadUInt32BigEndian();

        /// <summary>
        /// Read a DWORD (4-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadDWORDLittleEndian(this Stream stream)
            => stream.ReadUInt32LittleEndian();

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static float ReadSingle(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt48LittleEndian();
            else
                return stream.ReadInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((long)buffer[5] << 0)
                 | ((long)buffer[4] << 8)
                 | ((long)buffer[3] << 16)
                 | ((long)buffer[2] << 24)
                 | ((long)buffer[1] << 32)
                 | ((long)buffer[0] << 40);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt48(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt48LittleEndian();
            else
                return stream.ReadUInt48BigEndian();
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((ulong)buffer[5] << 0)
                 | ((ulong)buffer[4] << 8)
                 | ((ulong)buffer[3] << 16)
                 | ((ulong)buffer[2] << 24)
                 | ((ulong)buffer[1] << 32)
                 | ((ulong)buffer[0] << 40);
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt48LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 6);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadInt64LittleEndian();
            else
                return stream.ReadInt64BigEndian();
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return ((long)buffer[7] << 0)
                 | ((long)buffer[6] << 8)
                 | ((long)buffer[5] << 16)
                 | ((long)buffer[4] << 24)
                 | ((long)buffer[3] << 32)
                 | ((long)buffer[2] << 40)
                 | ((long)buffer[1] << 48)
                 | ((long)buffer[0] << 56);
        }

        /// <summary>
        /// Read an Int64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40)
                 | ((long)buffer[6] << 48)
                 | ((long)buffer[7] << 56);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt64(this Stream stream)
        {
            if (BitConverter.IsLittleEndian)
                return stream.ReadUInt64LittleEndian();
            else
                return stream.ReadUInt64BigEndian();
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return ((ulong)buffer[7] << 0)
                 | ((ulong)buffer[6] << 8)
                 | ((ulong)buffer[5] << 16)
                 | ((ulong)buffer[4] << 24)
                 | ((ulong)buffer[3] << 32)
                 | ((ulong)buffer[2] << 40)
                 | ((ulong)buffer[1] << 48)
                 | ((ulong)buffer[0] << 56);
        }

        /// <summary>
        /// Read a UInt64 from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40)
                 | ((ulong)buffer[6] << 48)
                 | ((ulong)buffer[7] << 56);
        }

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadQWORD(this Stream stream)
            => stream.ReadUInt64();

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadQWORDBigEndian(this Stream stream)
            => stream.ReadUInt64BigEndian();

        /// <summary>
        /// Read a QWORD (8-byte) from the stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadQWORDLittleEndian(this Stream stream)
            => stream.ReadUInt64LittleEndian();

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static double ReadDouble(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static decimal ReadDecimal(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Decimal from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Guid ReadGuid(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 from the stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this Stream stream)
        {
            byte[] buffer = ReadExactlyToBuffer(stream, 16);
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
            else if (encoding.Equals(Encoding.UTF8))
                return stream.ReadNullTerminatedUTF8String();
            else if (encoding.Equals(Encoding.Unicode))
                return stream.ReadNullTerminatedUnicodeString();
            else if (encoding.Equals(Encoding.UTF32))
                return stream.ReadNullTerminatedUTF32String();

            if (stream.Position >= stream.Length)
                return null;

            List<byte> buffer = [];
            while (stream.Position < stream.Length)
            {
                byte ch = stream.ReadByteValue();
                if (ch == '\0')
                    break;

                buffer.Add(ch);
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

            byte[] buffer = ReadUntilNull1Byte(stream);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-8 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(stream);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(stream);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the stream
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this Stream stream)
        {
            if (stream.Position >= stream.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(stream);
            return Encoding.UTF32.GetString(buffer);
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
            if (stream.Position + (size * 2) >= stream.Length)
                return null;

            byte[] buffer = stream.ReadBytes(size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a <typeparamref name="T"/> from the stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static T? ReadType<T>(this Stream stream)
            => (T?)stream.ReadType(typeof(T));

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static object? ReadType(this Stream stream, Type type)
        {
            // Handle special struct cases
            if (type == typeof(Guid))
                return stream.ReadGuid();
#if NET6_0_OR_GREATER
            else if (type == typeof(Half))
                return stream.ReadHalf();
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return stream.ReadInt128();
            else if (type == typeof(UInt128))
                return stream.ReadUInt128();
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(stream, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(stream, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(stream, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadNormalType(Stream stream, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = ReadExactlyToBuffer(stream, typeSize);

                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var data = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
                handle.Free();

                return data;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadComplexType(Stream stream, Type type)
        {
            try
            {
                // Try to create an instance of the type
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                    return null;

                // Get the layout information
                var layoutAttr = MarshalHelpers.GetAttribute<StructLayoutAttribute>(type);
                LayoutKind layoutKind = MarshalHelpers.DetermineLayoutKind(layoutAttr, type);
                Encoding encoding = MarshalHelpers.DetermineEncoding(layoutAttr);

                // Cache the current offset
                long currentOffset = stream.Position;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        stream.Seek(currentOffset + fieldOffset?.Value ?? 0, SeekOrigin.Begin);
                    }

                    SetField(stream, encoding, fields, instance, fi);
                }

                return instance;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set a single field on an object
        /// </summary>
        private static void SetField(Stream stream, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                var value = ReadStringType(stream, encoding, instance, fi);
                fi.SetValue(instance, value);
            }
            else if (fi.FieldType.IsArray)
            {
                var value = ReadArrayType(stream, fields, instance, fi);
                if (value.GetType() == fi.FieldType)
                    fi.SetValue(instance, value);
                else
                    fi.SetValue(instance, Convert.ChangeType(value, fi.FieldType));
            }
            else
            {
                var value = stream.ReadType(fi.FieldType);
                fi.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Read an array type field for an object
        /// </summary>
        private static Array ReadArrayType(Stream stream, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);
            if (marshalAsAttr == null)
                return new object[0];

            // Get the number of elements expected
            int elementCount = MarshalHelpers.GetArrayElementCount(marshalAsAttr, fields, instance);
            if (elementCount < 0)
                return new object[0];

            // Get the item type for the array
            Type elementType = fi.FieldType.GetElementType() ?? typeof(object);

            // Loop through and build the array
            Array arr = Array.CreateInstance(elementType, elementCount);
            for (int i = 0; i < elementCount; i++)
            {
                var value = ReadType(stream, elementType);
                if (value != null && elementType.IsEnum)
                    arr.SetValue(Enum.ToObject(elementType, value), i);
                else
                    arr.SetValue(value, i);
            }

            // Return the built array
            return arr;
        }

        /// <summary>
        /// Read a string type field for an object
        /// </summary>
        private static string? ReadStringType(Stream stream, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);

            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return stream.ReadPrefixedAnsiString();

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return stream.ReadPrefixedUnicodeString();

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = stream.ReadBytes(byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return stream.ReadNullTerminatedAnsiString();

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return stream.ReadNullTerminatedUTF8String();
#endif

                case UnmanagedType.LPWStr:
                    return stream.ReadNullTerminatedUnicodeString();

                // No other string types are recognized
                default:
                    return null;
            }
        }

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                byte next = stream.ReadByteValue();
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                ushort next = stream.ReadUInt16();
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(Stream stream)
        {
            var bytes = new List<byte>();
            while (stream.Position < stream.Length)
            {
                uint next = stream.ReadUInt32();
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read a number of bytes from the stream to a buffer
        /// </summary>
        private static byte[] ReadExactlyToBuffer(Stream stream, int length)
        {
            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value, {length} requested");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            int read = stream.Read(buffer, 0, length);
            if (read < length)
                throw new EndOfStreamException($"Requested to read {length} bytes from {nameof(stream)}, {read} returned");

            return buffer;
        }
    }
}
