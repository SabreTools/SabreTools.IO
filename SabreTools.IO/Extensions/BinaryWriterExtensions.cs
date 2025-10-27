using System;
using System.IO;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SabreTools.Numerics;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for BinaryWriter
    /// </summary>
    /// TODO: Handle proper negative values for Int24 and Int48
    public static class BinaryWriterExtensions
    {
        /// <inheritdoc cref="BinaryWriter.Write(byte)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt8 value)
        {
            writer.Write(value.LittleEndian);
            writer.Write(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(sbyte)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt8 value)
        {
            writer.Write(value.LittleEndian);
            writer.Write(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(byte[])"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(writer, value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(char)"/>
        public static bool Write(this BinaryWriter writer, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt16 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt16 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <inheritdoc cref="BinaryWriter.Write(Half)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Half value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }
#endif

        /// <summary>
        /// Write an Int32 as an Int24 to the underlying stream
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24(this BinaryWriter writer, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write an Int32 as an Int24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24BigEndian(this BinaryWriter writer, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24 to the underlying stream
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24(this BinaryWriter writer, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24BigEndian(this BinaryWriter writer, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(writer, reduced);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt32 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt32 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(float)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write an Int64 as an Int48 to the underlying stream
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48(this BinaryWriter writer, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write an Int64 as an Int48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48BigEndian(this BinaryWriter writer, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48 to the underlying stream
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(writer, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48 to the underlying stream
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48BigEndian(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(writer, reduced);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothInt64 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this BinaryWriter writer, BothUInt64 value)
        {
            writer.Write(value.LittleEndian);
            writer.WriteBigEndian(value.BigEndian);
            return true;
        }

        /// <inheritdoc cref="BinaryWriter.Write(double)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(decimal)"/>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, decimal value)
        {
            int[] bits = decimal.GetBits(value);

            byte[] lo = BitConverter.GetBytes(bits[0]);
            byte[] mid = BitConverter.GetBytes(bits[1]);
            byte[] hi = BitConverter.GetBytes(bits[2]);
            byte[] flags = BitConverter.GetBytes(bits[3]);

            byte[] buffer = new byte[16];
            Array.Copy(lo, 0, buffer, 0, 4);
            Array.Copy(mid, 0, buffer, 4, 4);
            Array.Copy(hi, 0, buffer, 8, 4);
            Array.Copy(flags, 0, buffer, 12, 4);

            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        public static bool Write(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a Guid
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            Array.Reverse(buffer);
            return WriteFromBuffer(writer, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128
        /// </summary>
        public static bool Write(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write an Int128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        public static bool Write(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }

        /// <summary>
        /// Write a UInt128
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this BinaryWriter writer, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(writer, padded);
        }
#endif

        /// <summary>
        /// Write a null-terminated string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedString(this BinaryWriter writer, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Add the null terminator and write
            value += "\0";
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a null-terminated ASCII string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedAnsiString(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.ASCII);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a null-terminated Latin1 string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedLatin1String(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.Latin1);
#endif

        /// <summary>
        /// Write a null-terminated UTF-8 string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedUTF8String(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.UTF8);

        /// <summary>
        /// Write a null-terminated UTF-16 (Unicode) string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedUnicodeString(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.Unicode);

        /// <summary>
        /// Write a null-terminated UTF-16 (Unicode) string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedBigEndianUnicodeString(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.BigEndianUnicode);

        /// <summary>
        /// Write a null-terminated UTF-32 string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedUTF32String(this BinaryWriter writer, string? value)
            => writer.WriteNullTerminatedString(value, Encoding.UTF32);

        /// <summary>
        /// Write a byte-prefixed ASCII string to the underlying stream
        /// </summary>
        public static bool WritePrefixedAnsiString(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.ASCII.GetBytes(value);

            // Write the length as a byte
            writer.Write((byte)value.Length);

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a byte-prefixed Latin1 string to the underlying stream
        /// </summary>
        public static bool WritePrefixedLatin1String(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Latin1.GetBytes(value);

            // Write the length as a byte
            writer.Write((byte)value.Length);

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }
#endif

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the underlying stream
        /// </summary>
        public static bool WritePrefixedUnicodeString(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            // Write the length as a ushort
            writer.Write((ushort)value.Length);

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the underlying stream
        /// </summary>
        public static bool WritePrefixedBigEndianUnicodeString(this BinaryWriter writer, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.BigEndianUnicode.GetBytes(value);

            // Write the length as a ushort
            writer.Write((ushort)value.Length);

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
        }

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
            if (value == null)
                return true;

            // Handle special struct cases
            if (type == typeof(Guid))
                return writer.Write((Guid)value);
#if NET6_0_OR_GREATER
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
                if (value == null)
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
                if (value == null)
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
                        writer.BaseStream.Seek(currentOffset + fieldOffset?.Value ?? 0, SeekOrigin.Begin);
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
            if (marshalAsAttr == null)
                return false;

            // Get the array
            Array? arr = fi.GetValue(instance) as Array;
            if (arr == null)
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
            string? fieldValue = fi.GetValue(instance) as string;
            if (fieldValue == null)
                return true;

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
