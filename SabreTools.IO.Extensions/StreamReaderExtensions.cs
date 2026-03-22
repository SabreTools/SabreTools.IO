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
    /// Extensions for Streams
    /// </summary>
    public static class StreamReaderExtensions
    {
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
#if NET5_0_OR_GREATER
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
                if (instance is null)
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
                        stream.Seek(currentOffset + (fieldOffset?.Value ?? 0), SeekOrigin.Begin);
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
                var value = ReadType(stream, elementType);
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
        private static string? ReadStringType(Stream stream, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = MarshalHelpers.GetAttribute<MarshalAsAttribute>(fi);

#pragma warning disable IDE0010
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
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Read a number of bytes from the stream to a buffer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is an invalid value.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// Thrown if the requested <paramref name="length"/> is greater
        /// than the read bytes from <paramref name="content"/>.
        /// length.
        /// </exception>
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
