using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.Numerics.Extensions;
using SabreTools.Text.Extensions;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for BinaryWriter
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Write a <typeparamref name="T"/> to the underlying stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are written by value, not by reference
        /// - Complex objects are written by value, not by reference
        /// - Enumeration values are written by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are serialized BEFORE fields in the child
        /// </remarks>
        public static bool WriteType<T>(this BinaryWriter writer, T? value)
            => writer.WriteType(value, typeof(T));

        /// <summary>
        /// Write a <typeparamref name="T"/> to the underlying stream
        /// </summary>
        /// <remarks>
        /// This method is different than standard marshalling in a few notable ways:
        /// - Strings are written by value, not by reference
        /// - Complex objects are written by value, not by reference
        /// - Enumeration values are written by the underlying value type
        /// - Arrays of the above are handled sequentially as above
        /// - Inherited fields from parents are serialized BEFORE fields in the child
        /// </remarks>
        public static bool WriteType(this BinaryWriter writer, object? value, Type type)
        {
            // Null values cannot be written
            if (value is null)
                return true;

            // Handle special struct cases
            if (type == typeof(Guid))
                return writer.Write((Guid)value);
#if NET5_0_OR_GREATER
            else if (type == typeof(Half))
            {
                writer.Write((Half)value);
                return true;
            }
#endif
#if NET7_0_OR_GREATER
            else if (type == typeof(Int128))
                return writer.Write((Int128)value);
            else if (type == typeof(UInt128))
                return writer.Write((UInt128)value);
#endif

            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return WriteComplexType(writer, value, type);
            else if (type.IsValueType && type.IsEnum)
                return WriteNormalType(writer, value, Enum.GetUnderlyingType(type));
            else
                return WriteNormalType(writer, value, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static bool WriteNormalType(BinaryWriter writer, object? value, Type type)
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

                return WriteFromBuffer(writer, buffer);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static bool WriteComplexType(BinaryWriter writer, object? value, Type type)
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
                long currentOffset = writer.BaseStream.Position;

                // Generate the fields by parent first
                var fields = MarshalHelpers.GetFields(type);

                // Loop through the fields and set them
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = MarshalHelpers.GetAttribute<FieldOffsetAttribute>(fi);
                        writer.BaseStream.Seek(currentOffset + (fieldOffset?.Value ?? 0), SeekOrigin.Begin);
                    }

                    if (!GetField(writer, encoding, fields, value, fi))
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
        private static bool GetField(BinaryWriter writer, Encoding encoding, FieldInfo[] fields, object instance, FieldInfo fi)
        {
            if (fi.FieldType.IsAssignableFrom(typeof(string)))
            {
                return WriteStringType(writer, encoding, instance, fi);
            }
            else if (fi.FieldType.IsArray)
            {
                return WriteArrayType(writer, fields, instance, fi);
            }
            else
            {
                var value = fi.GetValue(instance);
                return writer.WriteType(value, fi.FieldType);
            }
        }

        /// <summary>
        /// Write an array type field from an object
        /// </summary>
        private static bool WriteArrayType(BinaryWriter writer, FieldInfo[] fields, object instance, FieldInfo fi)
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
                if (!WriteType(writer, value, elementType))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Write a string type field from an object
        /// </summary>
        private static bool WriteStringType(BinaryWriter writer, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);
            if (fi.GetValue(instance) is not string fieldValue)
                return true;

#pragma warning disable IDE0010
            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    return writer.WritePrefixedAnsiString(fieldValue);

                case UnmanagedType.BStr:
                case UnmanagedType.TBStr: // Technically distinct; returns char[] instead
                    return writer.WritePrefixedUnicodeString(fieldValue);

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr!.SizeConst;
                    byte[] byvalBytes = encoding.GetBytes(fieldValue);
                    byte[] byvalSizedBytes = new byte[byvalLength];
                    Array.Copy(byvalBytes, byvalSizedBytes, Math.Min(byvalBytes.Length, byvalSizedBytes.Length));
                    writer.Write(byvalSizedBytes);
                    return true;

                case UnmanagedType.LPStr:
                case UnmanagedType.LPTStr: // Technically distinct; possibly not null-terminated
                case null:
                    return writer.WriteNullTerminatedAnsiString(fieldValue);

#if NET472_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                case UnmanagedType.LPUTF8Str:
                    return writer.WriteNullTerminatedUTF8String(fieldValue);
#endif

                case UnmanagedType.LPWStr:
                    return writer.WriteNullTerminatedUnicodeString(fieldValue);

                // No other string types are recognized
                default:
                    return false;
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Write an array of bytes to the underlying stream
        /// </summary>
        private static bool WriteFromBuffer(BinaryWriter writer, byte[] value)
        {
            // If the stream is not writable
            if (!writer.BaseStream.CanWrite)
                return false;

            // Handle the 0-byte case
            if (value.Length == 0)
                return true;

            // Handle the general case, forcing a write of the correct length
            writer.Write(value, 0, value.Length);
            return true;
        }
    }
}
