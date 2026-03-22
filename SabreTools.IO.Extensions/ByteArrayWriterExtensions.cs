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
    public static class ByteArrayWriterExtensions
    {
        /// <summary>
        /// Write a <typeparamref name="T"/> to the byte array
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are written by value, not by reference
        /// - Complex objects are written by value, not by reference
        /// - Enumeration values are written by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are serialized BEFORE fields in the child
        /// </remarks>
        public static bool WriteType<T>(this byte[] content, ref int offset, T? value)
            => content.WriteType(ref offset, value, typeof(T));

        /// <summary>
        /// Write a <typeparamref name="T"/> to the byte array
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are written by value, not by reference
        /// - Complex objects are written by value, not by reference
        /// - Enumeration values are written by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are serialized BEFORE fields in the child
        /// </remarks>
        public static bool WriteType(this byte[] content, ref int offset, object? value, Type type)
        {
            // Null values cannot be written
            if (value is null)
                return true;

            // Handle special struct cases
            if (type == typeof(Guid))
                return content.Write(ref offset, (Guid)value);
#if NET5_0_OR_GREATER
            else if (type == typeof(Half))
                return content.Write(ref offset, (Half)value);
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return content.Write(ref offset, (Int128)value);
            else if (type == typeof(UInt128))
                return content.Write(ref offset, (UInt128)value);
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return WriteComplexType(content, ref offset, value, type);
            else if (type.IsValueType && type.IsEnum)
                return WriteNormalType(content, ref offset, value, Enum.GetUnderlyingType(type));
            else
                return WriteNormalType(content, ref offset, value, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static bool WriteNormalType(byte[] content, ref int offset, object? value, Type type)
        {
            try
            {
                // Null values cannot be written
                if (value is null)
                    return true;

                int typeSize = Marshal.SizeOf(type);
                if (value.GetType() != type)
                    value = Convert.ChangeType(value, type);

                var buffer = new byte[typeSize];
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
                handle.Free();

                return WriteFromBuffer(content, ref offset, buffer);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static bool WriteComplexType(byte[] content, ref int offset, object? value, Type type)
        {
            try
            {
                // Null values cannot be written
                if (value is null)
                    return true;

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

                    if (!GetField(content, ref offset, encoding, fields, value, fi))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Write a single field from an object
        /// </summary>
        private static bool GetField(byte[] content, ref int offset, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                return WriteStringType(content, ref offset, encoding, instance, fi);
            }
            else if (fi.FieldType.IsArray)
            {
                return WriteArrayType(content, ref offset, fields, instance, fi);
            }
            else
            {
                var value = fi.GetValue(instance);
                return content.WriteType(ref offset, value, fi.FieldType);
            }
        }

        /// <summary>
        /// Write an array type field from an object
        /// </summary>
        private static bool WriteArrayType(byte[] content, ref int offset, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);
            if (marshalAsAttr is null)
                return false;

            // Get the array
            if (fi.GetValue(instance) is not Array arr)
                return false;

            // Get the number of elements expected
            int elementCount = MarshalHelpers.GetArrayElementCount(marshalAsAttr, fields, instance);
            if (elementCount < 0)
                return false;

            // Get the item type for the array
            Type elementType = fi.FieldType.GetElementType() ?? typeof(object);

            // Loop through and write the array
            for (int i = 0; i < elementCount; i++)
            {
                var value = arr.GetValue(i);
                if (!WriteType(content, ref offset, value, elementType))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Write a string type field from an object
        /// </summary>
        private static bool WriteStringType(byte[] content, ref int offset, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);
            if (fi.GetValue(instance) is not string fieldValue)
                return true;

#pragma warning disable IDE0010
            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return content.WritePrefixedAnsiString(ref offset, fieldValue);

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return content.WritePrefixedUnicodeString(ref offset, fieldValue);

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = encoding.GetBytes(fieldValue);
                    byte[] byvalSizedBytes = new byte[byvalLength];
                    Array.Copy(byvalBytes, byvalSizedBytes, Math.Min(byvalBytes.Length, byvalSizedBytes.Length));
                    return content.Write(ref offset, byvalSizedBytes);

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return content.WriteNullTerminatedAnsiString(ref offset, fieldValue);

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return content.WriteNullTerminatedUTF8String(ref offset, fieldValue);
#endif

                case UnmanagedType.LPWStr:
                    return content.WriteNullTerminatedUnicodeString(ref offset, fieldValue);

                // No other string types are recognized
                default:
                    return false;
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Write an array of bytes to the byte array
        /// </summary>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown if <paramref name="offset"/> into <paramref name="content"/>
        /// would not accomodate <paramref name="value"/>.
        /// </exception>
        private static bool WriteFromBuffer(byte[] content, ref int offset, byte[] value)
        {
            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // If there are not enough bytes
            if (offset + value.Length > content.Length)
                throw new System.IO.EndOfStreamException(nameof(content));

            // Handle the general case, forcing a write of the correct length
            Array.Copy(value, 0, content, offset, value.Length);
            offset += value.Length;

            return true;
        }
    }
}
