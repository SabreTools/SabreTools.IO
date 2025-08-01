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
    /// Extensions for BinaryReader
    /// </summary>
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
            return (short)(buffer[1]
                        | (buffer[0] << 8));
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return (short)(buffer[0]
                        | (buffer[1] << 8));
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return (ushort)(buffer[1]
                         | (buffer[0] << 8));
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return (ushort)(buffer[0]
                         | (buffer[1] << 8));
        }

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        public static ushort ReadWORD(this BinaryReader reader)
            => reader.ReadUInt16();

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadWORDBigEndian(this BinaryReader reader)
            => reader.ReadUInt16BigEndian();

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadWORDLittleEndian(this BinaryReader reader)
            => reader.ReadUInt16LittleEndian();

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <inheritdoc cref="BinaryReader.ReadHalf"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            Array.Reverse(buffer);
            return BitConverter.ToHalf(buffer, 0);
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static int ReadInt24(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadInt24LittleEndian();
            else
                return reader.ReadInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return (int)(buffer[2]
                       | (buffer[1] << 8)
                       | (buffer[0] << 16));
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt24LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static uint ReadUInt24(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadUInt24LittleEndian();
            else
                return reader.ReadUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return (uint)(buffer[2]
                       | (buffer[1] << 8)
                       | (buffer[0] << 16));
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt24LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16));
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return (int)(buffer[3]
                      | (buffer[2] << 8)
                      | (buffer[1] << 16)
                      | (buffer[0] << 24));
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return (int)(buffer[0]
                      | (buffer[1] << 8)
                      | (buffer[2] << 16)
                      | (buffer[3] << 24));
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return (uint)(buffer[3]
                       | (buffer[2] << 8)
                       | (buffer[1] << 16)
                       | (buffer[0] << 24));
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt32"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return (uint)(buffer[0]
                       | (buffer[1] << 8)
                       | (buffer[2] << 16)
                       | (buffer[3] << 24));
        }

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        public static uint ReadDWORD(this BinaryReader reader)
            => reader.ReadUInt32();

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadDWORDBigEndian(this BinaryReader reader)
            => reader.ReadUInt32BigEndian();

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadDWORDLittleEndian(this BinaryReader reader)
            => reader.ReadUInt32LittleEndian();

        /// <inheritdoc cref="BinaryReader.ReadSingle"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static long ReadInt48(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadInt48LittleEndian();
            else
                return reader.ReadInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return ((long)buffer[5] << 0)
                 | ((long)buffer[4] << 8)
                 | ((long)buffer[3] << 16)
                 | ((long)buffer[2] << 24)
                 | ((long)buffer[1] << 32)
                 | ((long)buffer[0] << 40);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt48LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadUInt48(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadUInt48LittleEndian();
            else
                return reader.ReadUInt48BigEndian();
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return ((ulong)buffer[5] << 0)
                 | ((ulong)buffer[4] << 8)
                 | ((ulong)buffer[3] << 16)
                 | ((ulong)buffer[2] << 24)
                 | ((ulong)buffer[1] << 32)
                 | ((ulong)buffer[0] << 40);
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt48LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return ((ulong)buffer[0] << 0)
                 | ((ulong)buffer[1] << 8)
                 | ((ulong)buffer[2] << 16)
                 | ((ulong)buffer[3] << 24)
                 | ((ulong)buffer[4] << 32)
                 | ((ulong)buffer[5] << 40);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return ((long)buffer[7] << 0)
                 | ((long)buffer[6] << 8)
                 | ((long)buffer[5] << 16)
                 | ((long)buffer[4] << 24)
                 | ((long)buffer[3] << 32)
                 | ((long)buffer[2] << 40)
                 | ((long)buffer[1] << 48)
                 | ((long)buffer[0] << 56);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt64LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return ((long)buffer[0] << 0)
                 | ((long)buffer[1] << 8)
                 | ((long)buffer[2] << 16)
                 | ((long)buffer[3] << 24)
                 | ((long)buffer[4] << 32)
                 | ((long)buffer[5] << 40)
                 | ((long)buffer[6] << 48)
                 | ((long)buffer[7] << 56);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return ((ulong)buffer[7] << 0)
                 | ((ulong)buffer[6] << 8)
                 | ((ulong)buffer[5] << 16)
                 | ((ulong)buffer[4] << 24)
                 | ((ulong)buffer[3] << 32)
                 | ((ulong)buffer[2] << 40)
                 | ((ulong)buffer[1] << 48)
                 | ((ulong)buffer[0] << 56);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
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
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static ulong ReadQWORD(this BinaryReader reader)
            => reader.ReadUInt64();

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadQWORDBigEndian(this BinaryReader reader)
            => reader.ReadUInt64BigEndian();

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadQWORDLittleEndian(this BinaryReader reader)
            => reader.ReadUInt64LittleEndian();

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

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
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
            else if (encoding.Equals(Encoding.UTF8))
                return reader.ReadNullTerminatedUTF8String();
            else if (encoding.Equals(Encoding.Unicode))
                return reader.ReadNullTerminatedUnicodeString();
            else if (encoding.Equals(Encoding.UTF32))
                return reader.ReadNullTerminatedUTF32String();

            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            List<byte> buffer = [];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte ch = reader.ReadByte();
                if (ch == '\0')
                    break;

                buffer.Add(ch);
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

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-8 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(reader);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(reader);
            return Encoding.UTF32.GetString(buffer);
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
            if (reader.BaseStream.Position + (size * 2) >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a <typeparamref name="T"/> from the underlying stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static T? ReadType<T>(this BinaryReader reader)
            => (T?)reader.ReadType(typeof(T));

        /// <summary>
        /// Read a <paramref name="type"/> from the underlying stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static object? ReadType(this BinaryReader reader, Type type)
        {
            // Handle special struct cases
            if (type == typeof(Guid))
                return reader.ReadGuid();
#if NET6_0_OR_GREATER
            else if (type == typeof(Half))
                return reader.ReadHalf();
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return reader.ReadInt128();
            else if (type == typeof(UInt128))
                return reader.ReadUInt128();
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(reader, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(reader, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(reader, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the underlying stream
        /// </summary>
        private static object? ReadNormalType(BinaryReader reader, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = reader.ReadBytes(typeSize); ;

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
        /// Read a <paramref name="type"/> from the underlying stream
        /// </summary>
        private static object? ReadComplexType(BinaryReader reader, Type type)
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
                long currentOffset = reader.BaseStream.Position;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        reader.BaseStream.Seek(currentOffset + fieldOffset?.Value ?? 0, SeekOrigin.Begin);
                    }

                    SetField(reader, encoding, fields, instance, fi);
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
        private static void SetField(BinaryReader reader, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                var value = ReadStringType(reader, encoding, fi);
                fi.SetValue(instance, value);
            }
            else if (fi.FieldType.IsArray)
            {
                var value = ReadArrayType(reader, fields, instance, fi);
                if (value.GetType() == fi.FieldType)
                    fi.SetValue(instance, value);
                else
                    fi.SetValue(instance, Convert.ChangeType(value, fi.FieldType));
            }
            else
            {
                var value = reader.ReadType(fi.FieldType);
                fi.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Read an array type field for an object
        /// </summary>
        private static Array ReadArrayType(BinaryReader reader, FieldInfo[] fields, object instance, FieldInfo fi)
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
                var value = ReadType(reader, elementType);
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
        private static string? ReadStringType(BinaryReader reader, Encoding encoding, FieldInfo? fi)
        {
            // If the FieldInfo is null
            if (fi == null)
                return null;

            // Get all MarshalAs attributes for the field, if possible
            var attributes = fi.GetCustomAttributes(typeof(MarshalAsAttribute), true);
            if (attributes.Length == 0)
                return null;

            // Use the first found attribute
            var marshalAsAttr = attributes[0] as MarshalAsAttribute;
            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return reader.ReadPrefixedAnsiString();

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return reader.ReadPrefixedUnicodeString();

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = reader.ReadBytes(byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return reader.ReadNullTerminatedAnsiString();

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return reader.ReadNullTerminatedUTF8String();
#endif

                case UnmanagedType.LPWStr:
                    return reader.ReadNullTerminatedUnicodeString();

                // No other string types are recognized
                default:
                    return null;
            }
        }

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte next = reader.ReadByte();
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                ushort next = reader.ReadUInt16();
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(BinaryReader reader)
        {
            var bytes = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                uint next = reader.ReadUInt32();
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }
    }
}
