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
    /// Extensions for BinaryReader
    /// </summary>
    public static class BinaryReaderExtensions
    {
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
    }
}
