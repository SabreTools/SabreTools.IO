using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.Numerics.Extensions;
using SabreTools.Text.Extensions;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayReaderExtensions
    {
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
