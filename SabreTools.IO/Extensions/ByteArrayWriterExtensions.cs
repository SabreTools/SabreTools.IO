using System;
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
    /// Extensions for byte arrays
    /// </summary>
    /// TODO: Handle proper negative values for Int24 and Int48
    public static class ByteArrayWriterExtensions
    {
        /// <summary>
        /// Write a UInt8 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, byte value)
            => WriteFromBuffer(content, ref offset, [value]);

        /// <summary>
        /// Write a UInt8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt8 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.Write(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt8[] and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, byte[] value)
            => WriteFromBuffer(content, ref offset, value);

        /// <summary>
        /// Write a UInt8[] and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, byte[] value)
        {
            Array.Reverse(value);
            return WriteFromBuffer(content, ref offset, value);
        }

        /// <summary>
        /// Write an Int8 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, sbyte value)
            => WriteFromBuffer(content, ref offset, [(byte)value]);

        /// <summary>
        /// Write a Int8 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt8 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.Write(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Char and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Char with an Encoding and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, char value, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes($"{value}");
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt16 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt16 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Write a Half and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, Half value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Half value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }
#endif

        /// <summary>
        /// Write an Int32 as an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write an Int32 as an Int24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsInt24BigEndian(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, reduced, 3);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write a UInt32 as a UInt24 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top byte</remarks>
        public static bool WriteAsUInt24BigEndian(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[3];
            Array.Copy(buffer, 1, reduced, 0, 3);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt32 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt32 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 as an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write an Int64 as an Int48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsInt48BigEndian(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, reduced, 6);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write a UInt64 as a UInt48 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        /// <remarks>Throws away top 2 bytes</remarks>
        public static bool WriteAsUInt48BigEndian(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);

            byte[] reduced = new byte[6];
            Array.Copy(buffer, 2, reduced, 0, 6);
            return WriteFromBuffer(content, ref offset, reduced);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothInt64 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in both-endian format</remarks>
        public static bool WriteBothEndian(this byte[] content, ref int offset, BothUInt64 value)
        {
            bool actual = content.Write(ref offset, value.LittleEndian);
            actual &= content.WriteBigEndian(ref offset, value.BigEndian);
            return actual;
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, decimal value)
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

            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, decimal value)
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
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Guid value)
        {
            byte[] buffer = value.ToByteArray();
            Array.Reverse(buffer);
            return WriteFromBuffer(content, ref offset, buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, Int128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        public static bool Write(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }

        /// <summary>
        /// Write a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Writes in big-endian format</remarks>
        public static bool WriteBigEndian(this byte[] content, ref int offset, UInt128 value)
        {
            byte[] buffer = ((BigInteger)value).ToByteArray();
            Array.Reverse(buffer);

            byte[] padded = new byte[16];
            Array.Copy(buffer, 0, padded, 16 - buffer.Length, buffer.Length);
            return WriteFromBuffer(content, ref offset, padded);
        }
#endif

        /// <summary>
        /// Write a null-terminated string to the array
        /// </summary>
        public static bool WriteNullTerminatedString(this byte[] content, ref int offset, string? value, Encoding encoding)
        {
            // If the value is null
            if (value == null)
                return false;

            // Add the null terminator and write
            value += "\0";
            byte[] buffer = encoding.GetBytes(value);
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a null-terminated ASCII string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedAnsiString(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.ASCII);

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a null-terminated Latin1 string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedLatin1String(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.Latin1);
#endif

        /// <summary>
        /// Write a null-terminated UTF-8 string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedUTF8String(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.UTF8);

        /// <summary>
        /// Write a null-terminated UTF-16 (Unicode) string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedUnicodeString(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.Unicode);

        /// <summary>
        /// Write a null-terminated UTF-16 (Unicode) string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedBigEndianUnicodeString(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.BigEndianUnicode);

        /// <summary>
        /// Write a null-terminated UTF-32 string to the byte array
        /// </summary>
        public static bool WriteNullTerminatedUTF32String(this byte[] content, ref int offset, string? value)
            => content.WriteNullTerminatedString(ref offset, value, Encoding.UTF32);

        /// <summary>
        /// Write a byte-prefixed ASCII string to the byte array
        /// </summary>
        public static bool WritePrefixedAnsiString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.ASCII.GetBytes(value);

            // Write the length as a byte
            if (!content.Write(ref offset, (byte)value.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Write a byte-prefixed Latin1 string to the byte array
        /// </summary>
        public static bool WritePrefixedLatin1String(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Latin1.GetBytes(value);

            // Write the length as a byte
            if (!content.Write(ref offset, (byte)value.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }
#endif

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the byte array
        /// </summary>
        public static bool WritePrefixedUnicodeString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            // Write the length as a ushort
            if (!content.Write(ref offset, (ushort)value.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

        /// <summary>
        /// Write a ushort-prefixed Unicode string to the byte array
        /// </summary>
        public static bool WritePrefixedBigEndianUnicodeString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value == null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.BigEndianUnicode.GetBytes(value);

            // Write the length as a ushort
            if (!content.Write(ref offset, (ushort)value.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

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
            if (value == null)
                return true;

            // Handle special struct cases
            if (type == typeof(Guid))
                return content.Write(ref offset, (Guid)value);
#if NET6_0_OR_GREATER
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
                if (value == null)
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
                if (value == null)
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
                        offset = currentOffset + fieldOffset?.Value ?? 0;
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
            if (marshalAsAttr == null)
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
        }

        /// <summary>
        /// Write an array of bytes to the byte array
        /// </summary>
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
