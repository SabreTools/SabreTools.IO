using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.Numerics;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for BinaryReader
    /// </summary>
    public static class BinaryReaderExtensions
    {
        #region Exact Read

        /// <inheritdoc cref="BinaryReader.ReadByte"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt8 ReadByteBothEndian(this BinaryReader reader)
        {
            byte le = reader.ReadByte();
            byte be = reader.ReadByte();
            return new BothUInt8(le, be);
        }

        /// <inheritdoc cref="BinaryReader.ReadSByte"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt8 ReadSByteBothEndian(this BinaryReader reader)
        {
            sbyte le = reader.ReadSByte();
            sbyte be = reader.ReadSByte();
            return new BothInt8(le, be);
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToInt16BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static short ReadInt16LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToInt16LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt16"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt16 ReadInt16BothEndian(this BinaryReader reader)
        {
            short le = reader.ReadInt16LittleEndian();
            short be = reader.ReadInt16BigEndian();
            return new BothInt16(le, be);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToUInt16BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static ushort ReadUInt16LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToUInt16LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt16"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadUInt16BothEndian(this BinaryReader reader)
        {
            ushort le = reader.ReadUInt16LittleEndian();
            ushort be = reader.ReadUInt16BigEndian();
            return new BothUInt16(le, be);
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

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt16 ReadWORDBothEndian(this BinaryReader reader)
            => reader.ReadUInt16BothEndian();

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half from the underlying stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Half ReadHalf(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadHalfLittleEndian();
            else
                return reader.ReadHalfBigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadHalf"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToHalfBigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadHalf"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static Half ReadHalfLittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(2);
            return buffer.ToHalfLittleEndian();
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
            return buffer.ToInt24BigEndian();
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt24LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return buffer.ToInt24LittleEndian();
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
            return buffer.ToUInt24BigEndian();
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt24LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(3);
            return buffer.ToUInt24LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToInt32BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static int ReadInt32LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToInt32LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt32"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt32 ReadInt32BothEndian(this BinaryReader reader)
        {
            int le = reader.ReadInt32LittleEndian();
            int be = reader.ReadInt32BigEndian();
            return new BothInt32(le, be);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt32"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToUInt32BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt32"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ReadUInt32LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToUInt32LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt32"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadUInt32BothEndian(this BinaryReader reader)
        {
            uint le = reader.ReadUInt32LittleEndian();
            uint be = reader.ReadUInt32BigEndian();
            return new BothUInt32(le, be);
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

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt32 ReadDWORDBothEndian(this BinaryReader reader)
            => reader.ReadUInt32BothEndian();

        /// <inheritdoc cref="BinaryReader.ReadSingle"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToSingleBigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadSingle"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static float ReadSingleLittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(4);
            return buffer.ToSingleLittleEndian();
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
            return buffer.ToInt48BigEndian();
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt48LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return buffer.ToInt48LittleEndian();
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
            return buffer.ToUInt48BigEndian();
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt48LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(6);
            return buffer.ToUInt48LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToInt64BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static long ReadInt64LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToInt64LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadInt64"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothInt64 ReadInt64BothEndian(this BinaryReader reader)
        {
            long le = reader.ReadInt64LittleEndian();
            long be = reader.ReadInt64BigEndian();
            return new BothInt64(le, be);
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToUInt64BigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ReadUInt64LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToUInt64LittleEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadUInt64"/>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadUInt64BothEndian(this BinaryReader reader)
        {
            ulong le = reader.ReadUInt64LittleEndian();
            ulong be = reader.ReadUInt64BigEndian();
            return new BothUInt64(le, be);
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

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static BothUInt64 ReadQWORDBothEndian(this BinaryReader reader)
            => reader.ReadUInt64BothEndian();

        /// <inheritdoc cref="BinaryReader.ReadDouble"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToDoubleBigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadDouble"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static double ReadDoubleLittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(8);
            return buffer.ToDoubleLittleEndian();
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

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static Int128 ReadInt128(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadInt128LittleEndian();
            else
                return reader.ReadInt128BigEndian();
        }

        /// <summary>
        /// Read an Int128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToInt128BigEndian();
        }

        /// <summary>
        /// Read an Int128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static Int128 ReadInt128LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToInt128LittleEndian();
        }

        /// <summary>
        /// Read a UInt128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static UInt128 ReadUInt128(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.ReadUInt128LittleEndian();
            else
                return reader.ReadUInt128BigEndian();
        }

        /// <summary>
        /// Read a UInt128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToUInt128BigEndian();
        }

        /// <summary>
        /// Read a UInt128 from the underlying stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static UInt128 ReadUInt128LittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToUInt128LittleEndian();
        }
#endif

        /// <inheritdoc cref="BinaryReader.ReadDecimal"/>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToDecimalBigEndian();
        }

        /// <inheritdoc cref="BinaryReader.ReadDecimal"/>
        /// <remarks>Reads in little-endian format</remarks>
        public static decimal ReadDecimalLittleEndian(this BinaryReader reader)
        {
            byte[] buffer = reader.ReadBytes(16);
            return buffer.ToDecimalLittleEndian();
        }

        /// <summary>
        /// Read a null-terminated string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return reader.ReadNullTerminatedAnsiString();
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return reader.ReadNullTerminatedLatin1String();
#endif
            else if (encoding.CodePage == Encoding.UTF8.CodePage)
                return reader.ReadNullTerminatedUTF8String();
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return reader.ReadNullTerminatedUnicodeString();
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return reader.ReadNullTerminatedBigEndianUnicodeString();
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
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

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a null-terminated Latin1 string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedLatin1String(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(reader);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

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
        /// Read a null-terminated UTF-16 (Unicode) string from the underlying stream
        /// </summary>
        public static string? ReadNullTerminatedBigEndianUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(reader);
            return Encoding.BigEndianUnicode.GetString(buffer);
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
        /// Read a ushort-prefixed Unicode string from the underlying stream
        /// </summary>
        public static string? ReadPrefixedBigEndianUnicodeString(this BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            ushort size = reader.ReadUInt16();
            if (reader.BaseStream.Position + (size * 2) >= reader.BaseStream.Length)
                return null;

            byte[] buffer = reader.ReadBytes(size * 2);
            return Encoding.BigEndianUnicode.GetString(buffer);
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
#if NET5_0_OR_GREATER
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
                if (instance is null)
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
                        reader.BaseStream.Seek(currentOffset + (fieldOffset?.Value ?? 0), SeekOrigin.Begin);
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
            if (marshalAsAttr is null)
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
                if (value is not null && elementType.IsEnum)
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
            if (fi is null)
                return null;

            // Get all MarshalAs attributes for the field, if possible
            var attributes = fi.GetCustomAttributes(typeof(MarshalAsAttribute), true);
            if (attributes.Length == 0)
                return null;

            // Use the first found attribute
            var marshalAsAttr = attributes[0] as MarshalAsAttribute;
#pragma warning disable IDE0010
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
#pragma warning restore IDE0010
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

        #endregion

        #region Peek Read

        /// <summary>
        /// Peek a UInt8 from the base stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte PeekByte(this BinaryReader reader)
        {
            byte value = reader.ReadByte();
            reader.BaseStream.SeekIfPossible(-1, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt8 from the base stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte PeekByteValue(this BinaryReader reader)
            => reader.PeekByte();

        /// <summary>
        /// Peek a UInt8 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt8 PeekByteBothEndian(this BinaryReader reader)
        {
            BothUInt8 value = reader.ReadByteBothEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt8[] from the base stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static byte[] PeekBytes(this BinaryReader reader, int count)
        {
            byte[] value = reader.ReadBytes(count);
            reader.BaseStream.SeekIfPossible(-count, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int8 from the base stream
        /// </summary>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static sbyte PeekSByte(this BinaryReader reader)
        {
            sbyte value = reader.ReadSByte();
            reader.BaseStream.SeekIfPossible(-1, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int8 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt8 PeekSByteBothEndian(this BinaryReader reader)
        {
            BothInt8 value = reader.ReadSByteBothEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt16LittleEndian();
            else
                return reader.PeekInt16BigEndian();
        }

        /// <summary>
        /// Peek an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16BigEndian(this BinaryReader reader)
        {
            short value = reader.ReadInt16BigEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static short PeekInt16LittleEndian(this BinaryReader reader)
        {
            short value = reader.ReadInt16LittleEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt16 PeekInt16BothEndian(this BinaryReader reader)
        {
            BothInt16 value = reader.ReadInt16BothEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt16LittleEndian();
            else
                return reader.PeekUInt16BigEndian();
        }

        /// <summary>
        /// Peek a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16BigEndian(this BinaryReader reader)
        {
            ushort value = reader.ReadUInt16BigEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekUInt16LittleEndian(this BinaryReader reader)
        {
            ushort value = reader.ReadUInt16LittleEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt16 PeekUInt16BothEndian(this BinaryReader reader)
        {
            BothUInt16 value = reader.ReadUInt16BothEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORD(this BinaryReader reader)
            => reader.PeekUInt16();

        /// <summary>
        /// Peek a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORDBigEndian(this BinaryReader reader)
            => reader.PeekUInt16BigEndian();

        /// <summary>
        /// Peek a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ushort PeekWORDLittleEndian(this BinaryReader reader)
            => reader.PeekUInt16LittleEndian();

        /// <summary>
        /// Peek a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt16 PeekWORDBothEndian(this BinaryReader reader)
            => reader.PeekUInt16BothEndian();

#if NET5_0_OR_GREATER
        /// <summary>
        /// Peek a Half from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalf(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekHalfLittleEndian();
            else
                return reader.PeekHalfBigEndian();
        }

        /// <summary>
        /// Peek a Half from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalfBigEndian(this BinaryReader reader)
        {
            Half value = reader.ReadHalfBigEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Half from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Half PeekHalfLittleEndian(this BinaryReader reader)
        {
            Half value = reader.ReadHalfLittleEndian();
            reader.BaseStream.SeekIfPossible(-2, SeekOrigin.Current);
            return value;
        }
#endif

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt24LittleEndian();
            else
                return reader.PeekInt24BigEndian();
        }

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24BigEndian(this BinaryReader reader)
        {
            int value = reader.ReadInt24BigEndian();
            reader.BaseStream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt24LittleEndian(this BinaryReader reader)
        {
            int value = reader.ReadInt24LittleEndian();
            reader.BaseStream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt24LittleEndian();
            else
                return reader.PeekUInt24BigEndian();
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24BigEndian(this BinaryReader reader)
        {
            uint value = reader.ReadUInt24BigEndian();
            reader.BaseStream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt24LittleEndian(this BinaryReader reader)
        {
            uint value = reader.ReadUInt24LittleEndian();
            reader.BaseStream.SeekIfPossible(-3, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt32LittleEndian();
            else
                return reader.PeekInt32BigEndian();
        }

        /// <summary>
        /// Peek an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32BigEndian(this BinaryReader reader)
        {
            int value = reader.ReadInt32BigEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static int PeekInt32LittleEndian(this BinaryReader reader)
        {
            int value = reader.ReadInt32LittleEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt32 PeekInt32BothEndian(this BinaryReader reader)
        {
            BothInt32 value = reader.ReadInt32BothEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt32LittleEndian();
            else
                return reader.PeekUInt32BigEndian();
        }

        /// <summary>
        /// Peek a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32BigEndian(this BinaryReader reader)
        {
            uint value = reader.ReadUInt32BigEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekUInt32LittleEndian(this BinaryReader reader)
        {
            uint value = reader.ReadUInt32LittleEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt32 PeekUInt32BothEndian(this BinaryReader reader)
        {
            BothUInt32 value = reader.ReadUInt32BothEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORD(this BinaryReader reader)
            => reader.PeekUInt32();

        /// <summary>
        /// Peek a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORDBigEndian(this BinaryReader reader)
            => reader.PeekUInt32BigEndian();

        /// <summary>
        /// Peek a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static uint PeekDWORDLittleEndian(this BinaryReader reader)
            => reader.PeekUInt32LittleEndian();

        /// <summary>
        /// Peek a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt32 PeekDWORDBothEndian(this BinaryReader reader)
            => reader.PeekUInt32BothEndian();

        /// <summary>
        /// Peek a Single from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingle(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekSingleLittleEndian();
            else
                return reader.PeekSingleBigEndian();
        }

        /// <summary>
        /// Peek a Single from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingleBigEndian(this BinaryReader reader)
        {
            float value = reader.ReadSingleBigEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Single from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static float PeekSingleLittleEndian(this BinaryReader reader)
        {
            float value = reader.ReadSingleLittleEndian();
            reader.BaseStream.SeekIfPossible(-4, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt48LittleEndian();
            else
                return reader.PeekInt48BigEndian();
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48BigEndian(this BinaryReader reader)
        {
            long value = reader.ReadInt48BigEndian();
            reader.BaseStream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt48LittleEndian(this BinaryReader reader)
        {
            long value = reader.ReadInt48LittleEndian();
            reader.BaseStream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt48 encoded as a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt48LittleEndian();
            else
                return reader.PeekUInt48BigEndian();
        }

        /// <summary>
        /// Peek an UInt48 encoded as an UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48BigEndian(this BinaryReader reader)
        {
            ulong value = reader.ReadUInt48BigEndian();
            reader.BaseStream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an UInt48 encoded as an UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt48LittleEndian(this BinaryReader reader)
        {
            ulong value = reader.ReadUInt48LittleEndian();
            reader.BaseStream.SeekIfPossible(-6, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt64LittleEndian();
            else
                return reader.PeekInt64BigEndian();
        }

        /// <summary>
        /// Peek an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64BigEndian(this BinaryReader reader)
        {
            long value = reader.ReadInt64BigEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static long PeekInt64LittleEndian(this BinaryReader reader)
        {
            long value = reader.ReadInt64LittleEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothInt64 PeekInt64BothEndian(this BinaryReader reader)
        {
            BothInt64 value = reader.ReadInt64BothEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt64LittleEndian();
            else
                return reader.PeekUInt64BigEndian();
        }

        /// <summary>
        /// Peek a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64BigEndian(this BinaryReader reader)
        {
            ulong value = reader.ReadUInt64BigEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekUInt64LittleEndian(this BinaryReader reader)
        {
            ulong value = reader.ReadUInt64LittleEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt64 PeekUInt64BothEndian(this BinaryReader reader)
        {
            BothUInt64 value = reader.ReadUInt64BothEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORD(this BinaryReader reader)
            => reader.PeekUInt64();

        /// <summary>
        /// Peek a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORDBigEndian(this BinaryReader reader)
            => reader.PeekUInt64BigEndian();

        /// <summary>
        /// Peek a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static ulong PeekQWORDLittleEndian(this BinaryReader reader)
            => reader.PeekUInt64LittleEndian();

        /// <summary>
        /// Peek a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static BothUInt64 PeekQWORDBothEndian(this BinaryReader reader)
            => reader.PeekUInt64BothEndian();

        /// <summary>
        /// Peek a Double from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDouble(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekDoubleLittleEndian();
            else
                return reader.PeekDoubleBigEndian();
        }

        /// <summary>
        /// Peek a Double from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDoubleBigEndian(this BinaryReader reader)
        {
            double value = reader.ReadDoubleBigEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Double from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static double PeekDoubleLittleEndian(this BinaryReader reader)
        {
            double value = reader.ReadDoubleLittleEndian();
            reader.BaseStream.SeekIfPossible(-8, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuid(this BinaryReader reader)
        {
            Guid value = reader.ReadGuid();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Guid from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Guid PeekGuidBigEndian(this BinaryReader reader)
        {
            Guid value = reader.ReadGuidBigEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Peek an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekInt128LittleEndian();
            else
                return reader.PeekInt128BigEndian();
        }

        /// <summary>
        /// Peek an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128BigEndian(this BinaryReader reader)
        {
            Int128 value = reader.ReadInt128BigEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static Int128 PeekInt128LittleEndian(this BinaryReader reader)
        {
            Int128 value = reader.ReadInt128LittleEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekUInt128LittleEndian();
            else
                return reader.PeekUInt128BigEndian();
        }

        /// <summary>
        /// Peek a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128BigEndian(this BinaryReader reader)
        {
            UInt128 value = reader.ReadUInt128BigEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static UInt128 PeekUInt128LittleEndian(this BinaryReader reader)
        {
            UInt128 value = reader.ReadUInt128LittleEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }
#endif

        /// <summary>
        /// Peek a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimal(this BinaryReader reader)
        {
            if (BitConverter.IsLittleEndian)
                return reader.PeekDecimalLittleEndian();
            else
                return reader.PeekDecimalBigEndian();
        }

        /// <summary>
        /// Peek a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimalBigEndian(this BinaryReader reader)
        {
            decimal value = reader.ReadDecimalBigEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        /// <summary>
        /// Peek a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        /// <remarks>Only works properly on seekable streams</remarks>
        public static decimal PeekDecimalLittleEndian(this BinaryReader reader)
        {
            decimal value = reader.ReadDecimalLittleEndian();
            reader.BaseStream.SeekIfPossible(-16, SeekOrigin.Current);
            return value;
        }

        #endregion

        #region Try Read

        /// <summary>
        /// Read a UInt8 from the base stream
        /// </summary>
        public static bool TryReadByteValue(this BinaryReader reader, out byte value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 1)
            {
                value = default;
                return false;
            }

            value = reader.ReadByte();
            return true;
        }

        /// <summary>
        /// Read a UInt8 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadByteBothEndian(this BinaryReader reader, out BothUInt8 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default(byte);
                return false;
            }

            value = reader.ReadByteBothEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt8[] from the base stream
        /// </summary>
        public static bool TryReadBytes(this BinaryReader reader, int count, out byte[] value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - count)
            {
                value = [];
                return false;
            }

            value = reader.ReadBytes(count);
            return true;
        }

        /// <summary>
        /// Read an Int8 from the base stream
        /// </summary>
        public static bool TryReadSByte(this BinaryReader reader, out sbyte value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 1)
            {
                value = default;
                return false;
            }

            value = reader.ReadSByte();
            return true;
        }

        /// <summary>
        /// Read a Int8 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadSByteBothEndian(this BinaryReader reader, out BothInt8 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default(sbyte);
                return false;
            }

            value = reader.ReadSByteBothEndian();
            return true;
        }

        /// <summary>
        /// Read a Char from the base stream
        /// </summary>
        public static bool TryReadChar(this BinaryReader reader, out char value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 1)
            {
                value = default;
                return false;
            }

            value = reader.ReadChar();
            return true;
        }

        /// <summary>
        /// Read an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt16(this BinaryReader reader, out short value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt16LittleEndian(out value);
            else
                return reader.TryReadInt16BigEndian(out value);
        }

        /// <summary>
        /// Read an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt16BigEndian(this BinaryReader reader, out short value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt16BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt16LittleEndian(this BinaryReader reader, out short value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt16LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a Int16 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt16BothEndian(this BinaryReader reader, out BothInt16 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default(short);
                return false;
            }

            value = reader.ReadInt16BothEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt16(this BinaryReader reader, out ushort value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt16LittleEndian(out value);
            else
                return reader.TryReadUInt16BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt16BigEndian(this BinaryReader reader, out ushort value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt16BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt16LittleEndian(this BinaryReader reader, out ushort value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt16LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt16 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt16BothEndian(this BinaryReader reader, out BothUInt16 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default(ushort);
                return false;
            }

            value = reader.ReadUInt16BothEndian();
            return true;
        }

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadWORD(this BinaryReader reader, out ushort value)
            => reader.TryReadUInt16(out value);

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadWORDBigEndian(this BinaryReader reader, out ushort value)
            => reader.TryReadUInt16BigEndian(out value);

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadWORDLittleEndian(this BinaryReader reader, out ushort value)
            => reader.TryReadUInt16LittleEndian(out value);

        /// <summary>
        /// Read a WORD (2-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadWORDBothEndian(this BinaryReader reader, out BothUInt16 value)
            => reader.TryReadUInt16BothEndian(out value);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a Half from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadHalf(this BinaryReader reader, out Half value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadHalfLittleEndian(out value);
            else
                return reader.TryReadHalfBigEndian(out value);
        }

        /// <summary>
        /// Read a Half from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadHalfBigEndian(this BinaryReader reader, out Half value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadHalfBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Half from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadHalfLittleEndian(this BinaryReader reader, out Half value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 2)
            {
                value = default;
                return false;
            }

            value = reader.ReadHalfLittleEndian();
            return true;
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt24(this BinaryReader reader, out int value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt24LittleEndian(out value);
            else
                return reader.TryReadInt24BigEndian(out value);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt24BigEndian(this BinaryReader reader, out int value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 3)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt24LittleEndian(this BinaryReader reader, out int value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 3)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt24LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt24(this BinaryReader reader, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt24LittleEndian(out value);
            else
                return reader.TryReadUInt24BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt24BigEndian(this BinaryReader reader, out uint value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 3)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt24BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt24LittleEndian(this BinaryReader reader, out uint value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 3)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt24LittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt32(this BinaryReader reader, out int value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt32LittleEndian(out value);
            else
                return reader.TryReadInt32BigEndian(out value);
        }

        /// <summary>
        /// Read an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt32BigEndian(this BinaryReader reader, out int value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt32BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt32LittleEndian(this BinaryReader reader, out int value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt32LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a Int32 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt32BothEndian(this BinaryReader reader, out BothInt32 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default(int);
                return false;
            }

            value = reader.ReadInt32BothEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt32(this BinaryReader reader, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt32LittleEndian(out value);
            else
                return reader.TryReadUInt32BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt32BigEndian(this BinaryReader reader, out uint value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt32BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt32LittleEndian(this BinaryReader reader, out uint value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt32LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt32 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt32BothEndian(this BinaryReader reader, out BothUInt32 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default(uint);
                return false;
            }

            value = reader.ReadUInt32BothEndian();
            return true;
        }

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDWORD(this BinaryReader reader, out uint value)
            => reader.TryReadUInt32(out value);

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDWORDBigEndian(this BinaryReader reader, out uint value)
            => reader.TryReadUInt32BigEndian(out value);

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDWORDLittleEndian(this BinaryReader reader, out uint value)
            => reader.TryReadUInt32LittleEndian(out value);

        /// <summary>
        /// Read a DWORD (4-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadDWORDBothEndian(this BinaryReader reader, out BothUInt32 value)
            => reader.TryReadUInt32BothEndian(out value);

        /// <summary>
        /// Read a Single from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadSingle(this BinaryReader reader, out float value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadSingleLittleEndian(out value);
            else
                return reader.TryReadSingleBigEndian(out value);
        }

        /// <summary>
        /// Read a Single from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadSingleBigEndian(this BinaryReader reader, out float value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadSingleBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Single from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadSingleLittleEndian(this BinaryReader reader, out float value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 4)
            {
                value = default;
                return false;
            }

            value = reader.ReadSingleLittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt48(this BinaryReader reader, out long value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt48LittleEndian(out value);
            else
                return reader.TryReadInt48BigEndian(out value);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt48BigEndian(this BinaryReader reader, out long value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 6)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt48LittleEndian(this BinaryReader reader, out long value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 6)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt48LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt48(this BinaryReader reader, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt48LittleEndian(out value);
            else
                return reader.TryReadUInt48BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt48BigEndian(this BinaryReader reader, out ulong value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 6)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an UInt48 encoded as an UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt48LittleEndian(this BinaryReader reader, out ulong value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 6)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt48LittleEndian();
            return true;
        }

        /// <summary>
        /// Read an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt64(this BinaryReader reader, out long value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt64LittleEndian(out value);
            else
                return reader.TryReadInt64BigEndian(out value);
        }

        /// <summary>
        /// Read an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt64BigEndian(this BinaryReader reader, out long value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt64LittleEndian(this BinaryReader reader, out long value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt48BigEndian();
            return true;
        }

        /// <summary>
        /// Read a Int64 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadInt64BothEndian(this BinaryReader reader, out BothInt64 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default(long);
                return false;
            }

            value = reader.ReadInt64BothEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt64(this BinaryReader reader, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt64LittleEndian(out value);
            else
                return reader.TryReadUInt64BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt64BigEndian(this BinaryReader reader, out ulong value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt64BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt64LittleEndian(this BinaryReader reader, out ulong value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt64LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt64 from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadUInt64BothEndian(this BinaryReader reader, out BothUInt64 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default(ulong);
                return false;
            }

            value = reader.ReadUInt64BothEndian();
            return true;
        }

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadQWORD(this BinaryReader reader, out ulong value)
            => reader.TryReadUInt64(out value);

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadQWORDBigEndian(this BinaryReader reader, out ulong value)
            => reader.TryReadUInt64BigEndian(out value);

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadQWORDLittleEndian(this BinaryReader reader, out ulong value)
            => reader.TryReadUInt64LittleEndian(out value);

        /// <summary>
        /// Read a QWORD (8-byte) from the base stream
        /// </summary>
        /// <remarks>Reads in both-endian format</remarks>
        public static bool TryReadQWORDBothEndian(this BinaryReader reader, out BothUInt64 value)
            => reader.TryReadUInt64BothEndian(out value);

        /// <summary>
        /// Read a Double from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDouble(this BinaryReader reader, out double value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadDoubleLittleEndian(out value);
            else
                return reader.TryReadDoubleBigEndian(out value);
        }

        /// <summary>
        /// Read a Double from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDoubleBigEndian(this BinaryReader reader, out double value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadDoubleBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Double from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDoubleLittleEndian(this BinaryReader reader, out double value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 8)
            {
                value = default;
                return false;
            }

            value = reader.ReadDoubleLittleEndian();
            return true;
        }

        /// <summary>
        /// Read a Guid from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadGuid(this BinaryReader reader, out Guid value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadGuid();
            return true;
        }

        /// <summary>
        /// Read a Guid from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadGuidBigEndian(this BinaryReader reader, out Guid value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadGuidBigEndian();
            return true;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadInt128(this BinaryReader reader, out Int128 value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadInt128LittleEndian(out value);
            else
                return reader.TryReadInt128BigEndian(out value);
        }

        /// <summary>
        /// Read an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadInt128BigEndian(this BinaryReader reader, out Int128 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt128BigEndian();
            return true;
        }

        /// <summary>
        /// Read an Int128 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadInt128LittleEndian(this BinaryReader reader, out Int128 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadInt128LittleEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadUInt128(this BinaryReader reader, out UInt128 value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadUInt128LittleEndian(out value);
            else
                return reader.TryReadUInt128BigEndian(out value);
        }

        /// <summary>
        /// Read a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadUInt128BigEndian(this BinaryReader reader, out UInt128 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt128BigEndian();
            return true;
        }

        /// <summary>
        /// Read a UInt128 from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadUInt128LittleEndian(this BinaryReader reader, out UInt128 value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadUInt128LittleEndian();
            return true;
        }
#endif

        /// <summary>
        /// Read a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in machine native format</remarks>
        public static bool TryReadDecimal(this BinaryReader reader, out decimal value)
        {
            if (BitConverter.IsLittleEndian)
                return reader.TryReadDecimalLittleEndian(out value);
            else
                return reader.TryReadDecimalBigEndian(out value);
        }

        /// <summary>
        /// Read a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static bool TryReadDecimalBigEndian(this BinaryReader reader, out decimal value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadDecimalBigEndian();
            return true;
        }

        /// <summary>
        /// Read a Decimal from the base stream
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static bool TryReadDecimalLittleEndian(this BinaryReader reader, out decimal value)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length - 16)
            {
                value = default;
                return false;
            }

            value = reader.ReadDecimalLittleEndian();
            return true;
        }

        #endregion
    }
}
