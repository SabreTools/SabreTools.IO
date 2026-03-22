using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.Numerics.Extensions;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
        /// <summary>
        /// Read a null-terminated string from the array
        /// </summary>
        public static string? ReadNullTerminatedString(this byte[] content, ref int offset, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.CodePage == Encoding.ASCII.CodePage)
                return content.ReadNullTerminatedAnsiString(ref offset);
#if NET5_0_OR_GREATER
            else if (encoding.CodePage == Encoding.Latin1.CodePage)
                return content.ReadNullTerminatedAnsiString(ref offset);
#endif
            else if (encoding.CodePage == Encoding.UTF8.CodePage)
                return content.ReadNullTerminatedUTF8String(ref offset);
            else if (encoding.CodePage == Encoding.Unicode.CodePage)
                return content.ReadNullTerminatedUnicodeString(ref offset);
            else if (encoding.CodePage == Encoding.BigEndianUnicode.CodePage)
                return content.ReadNullTerminatedBigEndianUnicodeString(ref offset);
            else if (encoding.CodePage == Encoding.UTF32.CodePage)
                return content.ReadNullTerminatedUTF32String(ref offset);

            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                if (ch == '\0')
                    break;

                buffer.Add(ch);
            }

            return encoding.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedAnsiString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a null-terminated Latin1 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedLatin1String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated UTF-8 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUTF8String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull1Byte(content, ref offset);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(content, ref offset);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-16 (Unicode) string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedBigEndianUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull2Byte(content, ref offset);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Read a null-terminated UTF-32 string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUTF32String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte[] buffer = ReadUntilNull4Byte(content, ref offset);
            return Encoding.UTF32.GetString(buffer);
        }

        /// <summary>
        /// Read a byte-prefixed ASCII string from the byte array
        /// </summary>
        public static string? ReadPrefixedAnsiString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte size = content.ReadByteValue(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.ASCII.GetString(buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Read a byte-prefixed Latin1 string from the byte array
        /// </summary>
        public static string? ReadPrefixedLatin1String(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            byte size = content.ReadByteValue(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.Latin1.GetString(buffer);
        }
#endif

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + (size * 2) >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size * 2);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedBigEndianUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + (size * 2) >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size * 2);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Read a <typeparamref name="T"/> and increment the pointer to an array
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static T? ReadType<T>(this byte[] content, ref int offset)
            => (T?)content.ReadType(ref offset, typeof(T));

        /// <summary>
        /// Read a <paramref name="type"/> and increment the pointer to an array
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are read by value, not by reference
        /// - Complex objects are read by value, not by reference
        /// - Enumeration values are read by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are deserialized BEFORE fields in the child
        /// </remarks>
        public static object? ReadType(this byte[] content, ref int offset, Type type)
        {
            // Handle special struct cases
            if (type == typeof(Guid))
                return content.ReadGuid(ref offset);
#if NET5_0_OR_GREATER
            else if (type == typeof(Half))
                return content.ReadHalf(ref offset);
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return content.ReadInt128(ref offset);
            else if (type == typeof(UInt128))
                return content.ReadUInt128(ref offset);
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(content, ref offset, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(content, ref offset, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(content, ref offset, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> and increment the pointer to an array
        /// </summary>
        private static object? ReadNormalType(byte[] content, ref int offset, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = ReadExactlyToBuffer(content, ref offset, typeSize);

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
        /// Read a <paramref name="type"/> and increment the pointer to an array
        /// </summary>
        private static object? ReadComplexType(byte[] content, ref int offset, Type type)
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
                int currentOffset = offset;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        offset = currentOffset + (fieldOffset?.Value ?? 0);
                    }

                    SetField(content, ref offset, encoding, fields, instance, fi);
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
        private static void SetField(byte[] content, ref int offset, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                var value = ReadStringType(content, ref offset, encoding, instance, fi);
                fi.SetValue(instance, value);
            }
            else if (fi.FieldType.IsArray)
            {
                var value = ReadArrayType(content, ref offset, fields, instance, fi);
                if (value.GetType() == fi.FieldType)
                    fi.SetValue(instance, value);
                else
                    fi.SetValue(instance, Convert.ChangeType(value, fi.FieldType));
            }
            else
            {
                var value = content.ReadType(ref offset, fi.FieldType);
                fi.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Read an array type field for an object
        /// </summary>
        private static Array ReadArrayType(byte[] content, ref int offset, FieldInfo[] fields, object instance, FieldInfo fi)
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
                var value = ReadType(content, ref offset, elementType);
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
        private static string? ReadStringType(byte[] content, ref int offset, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);

#pragma warning disable IDE0010
            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return content.ReadPrefixedAnsiString(ref offset);

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return content.ReadPrefixedUnicodeString(ref offset);

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = content.ReadBytes(ref offset, byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return content.ReadNullTerminatedAnsiString(ref offset);

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return content.ReadNullTerminatedUTF8String(ref offset);
#endif

                case UnmanagedType.LPWStr:
                    return content.ReadNullTerminatedUnicodeString(ref offset);

                // No other string types are recognized
                default:
                    return null;
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Read bytes until a 1-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull1Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                byte next = content.ReadByte(ref offset);
                if (next == 0x00)
                    break;

                bytes.Add(next);
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 2-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull2Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                ushort next = content.ReadUInt16(ref offset);
                if (next == 0x0000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read bytes until a 4-byte null terminator is found
        /// </summary>
        private static byte[] ReadUntilNull4Byte(byte[] content, ref int offset)
        {
            var bytes = new List<byte>();
            while (offset < content.Length)
            {
                uint next = content.ReadUInt32(ref offset);
                if (next == 0x00000000)
                    break;

                bytes.AddRange(BitConverter.GetBytes(next));
            }

            return [.. bytes];
        }

        /// <summary>
        /// Read a number of bytes from the byte array to a buffer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="offset"/> or <paramref name="length"/>
        /// is an invalid value.
        /// </exception>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown if the requested <paramref name="offset"/> and
        /// <paramref name="length"/> is greater than <paramref name="content"/>
        /// length.
        /// </exception>
        private static byte[] ReadExactlyToBuffer(byte[] content, ref int offset, int length)
        {
            // If we have an invalid offset
            if (offset < 0 || offset >= content.Length)
                throw new ArgumentOutOfRangeException($"{nameof(offset)} must be between 0 and {content.Length}, {offset} provided");

            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value, {length} requested");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // If there are not enough bytes
            if (offset + length > content.Length)
                throw new System.IO.EndOfStreamException($"Requested to read {length} bytes from {nameof(content)}, {content.Length - offset} returned");

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            Array.Copy(content, offset, buffer, 0, length);
            offset += length;

            return buffer;
        }
    }
}
