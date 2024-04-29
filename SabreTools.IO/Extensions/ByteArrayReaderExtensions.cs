using System;
using System.Collections.Generic;
using System.Linq;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    /// TODO: Handle proper negative values for Int24 and Int48
    public static class ByteArrayReaderExtensions
    {
        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static byte ReadByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return buffer[0];
        }

        /// <summary>
        /// Read a UInt8 and increment the pointer to an array
        /// </summary>
        public static byte ReadByteValue(this byte[] content, ref int offset)
            => content.ReadByte(ref offset);

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        public static byte[] ReadBytes(this byte[] content, ref int offset, int count)
            => ReadToBuffer(content, ref offset, count);

        /// <summary>
        /// Read a UInt8[] and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static byte[] ReadBytesBigEndian(this byte[] content, ref int offset, int count)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>
        /// Read an Int8 and increment the pointer to an array
        /// </summary>
        public static sbyte ReadSByte(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Read a Char and increment the pointer to an array
        /// </summary>
        public static char ReadChar(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 1);
            return (char)buffer[0];
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        public static short ReadInt16(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read an Int16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static short ReadInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        public static ushort ReadUInt16(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read a UInt16 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ushort ReadUInt16BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        // Half was introduced in net5.0 but doesn't have a BitConverter implementation until net6.0
#if NET6_0_OR_GREATER
        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        public static Half ReadHalf(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            return BitConverter.ToHalf(buffer, 0);
        }

        /// <summary>
        /// Read a Half and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Half ReadHalfBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 2);
            Array.Reverse(buffer);
            return BitConverter.ToHalf(buffer, 0);
        }
#endif

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt24(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int24 encoded as an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt24(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read a UInt24 encoded as a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt24BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 3);
            Array.Reverse(buffer);

            byte[] padded = new byte[4];
            Array.Copy(buffer, padded, 3);
            return BitConverter.ToUInt32(padded, 0);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        public static int ReadInt32(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read an Int32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static int ReadInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        public static uint ReadUInt32(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a UInt32 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static uint ReadUInt32BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        public static float ReadSingle(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read a Single and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static float ReadSingleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 4);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 and increment the pointer to an array
        /// </summary>
        public static long ReadInt48(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read an Int48 encoded as an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt48(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 6);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <summary>
        /// Read a UInt48 encoded as a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt48BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 6);
            Array.Reverse(buffer);

            byte[] padded = new byte[8];
            Array.Copy(buffer, padded, 6);
            return BitConverter.ToUInt64(padded, 0);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        public static long ReadInt64(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read an Int64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static long ReadInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        public static ulong ReadUInt64(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a UInt64 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static ulong ReadUInt64BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        public static double ReadDouble(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Double and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static double ReadDoubleBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 8);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        public static decimal ReadDecimal(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Decimal and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static decimal ReadDecimalBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);

            int lo = BitConverter.ToInt32(buffer, 0);
            int mid = BitConverter.ToInt32(buffer, 4);
            int hi = BitConverter.ToInt32(buffer, 8);
            int flags = BitConverter.ToInt32(buffer, 12);

            return new decimal([lo, mid, hi, flags]);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        public static Guid ReadGuid(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return new Guid(buffer);
        }

        /// <summary>
        /// Read a Guid and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Guid ReadGuidBigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return new Guid(buffer);
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        public static Int128 ReadInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read an Int128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static Int128 ReadInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return (Int128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        public static UInt128 ReadUInt128(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            return (UInt128)new BigInteger(buffer);
        }

        /// <summary>
        /// Read a UInt128 and increment the pointer to an array
        /// </summary>
        /// <remarks>Reads in big-endian format</remarks>
        public static UInt128 ReadUInt128BigEndian(this byte[] content, ref int offset)
        {
            byte[] buffer = ReadToBuffer(content, ref offset, 16);
            Array.Reverse(buffer);
            return (UInt128)new BigInteger(buffer);
        }
#endif

        /// <summary>
        /// Read a null-terminated string from the array
        /// </summary>
        public static string? ReadNullTerminatedString(this byte[] content, ref int offset, Encoding encoding)
        {
            // Short-circuit to explicit implementations
            if (encoding.Equals(Encoding.ASCII))
                return content.ReadNullTerminatedAnsiString(ref offset);
            else if (encoding.Equals(Encoding.Unicode))
                return content.ReadNullTerminatedUnicodeString(ref offset);

            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                buffer.Add(ch);
                if (ch == '\0')
                    break;
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

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte ch = content.ReadByteValue(ref offset);
                buffer.Add(ch);
                if (ch == '\0')
                    break;
            }

            return Encoding.ASCII.GetString([.. buffer]);
        }

        /// <summary>
        /// Read a null-terminated Unicode string from the byte array
        /// </summary>
        public static string? ReadNullTerminatedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            List<byte> buffer = [];
            while (offset < content.Length)
            {
                byte[] ch = content.ReadBytes(ref offset, 2);
                buffer.AddRange(ch);
                if (ch[0] == '\0' && ch[1] == '\0')
                    break;
            }

            return Encoding.Unicode.GetString([.. buffer]);
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

        /// <summary>
        /// Read a ushort-prefixed Unicode string from the byte array
        /// </summary>
        public static string? ReadPrefixedUnicodeString(this byte[] content, ref int offset)
        {
            if (offset >= content.Length)
                return null;

            ushort size = content.ReadUInt16(ref offset);
            if (offset + size >= content.Length)
                return null;

            byte[] buffer = content.ReadBytes(ref offset, size);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the byte array
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset)
            => content.ReadQuotedString(ref offset, Encoding.Default);

        /// <summary>
        /// Read a string that is terminated by a newline but contains a quoted portion that
        /// may also contain a newline from the byte array
        /// </summary>
        public static string? ReadQuotedString(this byte[] content, ref int offset, Encoding encoding)
        {
            if (offset >= content.Length)
                return null;

            byte[] nullTerminator = encoding.GetBytes("\0");
            int charWidth = nullTerminator.Length;

            var keyChars = new List<char>();
            bool openQuote = false;
            while (offset < content.Length)
            {
                char c = encoding.GetChars(content, offset, charWidth)[0];
                keyChars.Add(c);
                offset += charWidth;

                // If we have a quote, flip the flag
                if (c == '"')
                    openQuote = !openQuote;

                // If we have a newline not in a quoted string, exit the loop
                else if (c == (byte)'\n' && !openQuote)
                    break;
            }

            return new string([.. keyChars]).TrimEnd();
        }

        /// <summary>
        /// Read a <typeparamref name="T"/> from the stream
        /// </summary>
        public static T? ReadType<T>(this byte[] content, ref int offset)
            => (T?)content.ReadType(ref offset, typeof(T));

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        public static object? ReadType(this byte[] content, ref int offset, Type type)
        {
            if (type.IsClass || (type.IsValueType && !type.IsEnum && !type.IsPrimitive))
                return ReadComplexType(content, ref offset, type);
            else if (type.IsValueType && type.IsEnum)
                return ReadNormalType(content, ref offset, Enum.GetUnderlyingType(type));
            else
                return ReadNormalType(content, ref offset, type);
        }

        /// <summary>
        /// Read a <paramref name="type"/> from the stream
        /// </summary>
        private static object? ReadNormalType(byte[] content, ref int offset, Type type)
        {
            try
            {
                int typeSize = Marshal.SizeOf(type);
                byte[] buffer = ReadToBuffer(content, ref offset, typeSize);

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
        private static object? ReadComplexType(this byte[] content, ref int offset, Type type)
        {
            try
            {
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                    return null;

                // Get the layout attribute
                var layoutAttr = type.GetCustomAttributes(typeof(StructLayoutAttribute), true).FirstOrDefault() as StructLayoutAttribute;

                // Get the layout type
                LayoutKind layoutKind = LayoutKind.Auto;
                if (layoutAttr != null)
                    layoutKind = layoutAttr.Value;
                else if (type.IsAutoLayout)
                    layoutKind = LayoutKind.Auto;
                else if (type.IsExplicitLayout)
                    layoutKind = LayoutKind.Explicit;
                else if (type.IsLayoutSequential)
                    layoutKind = LayoutKind.Sequential;

                // Get the encoding to use
                Encoding encoding = layoutAttr?.CharSet switch
                {
                    CharSet.None => Encoding.ASCII,
                    CharSet.Ansi => Encoding.ASCII,
                    CharSet.Unicode => Encoding.Unicode,
                    CharSet.Auto => Encoding.ASCII, // UTF-8 on Unix
                    _ => Encoding.ASCII,
                };

                // Cache the current offset
                int currentOffset = offset;

                // Loop through the fields and set them
                var fields = type.GetFields();
                foreach (var fi in fields)
                {
                    // If we have an explicit layout, move accordingly
                    if (layoutKind == LayoutKind.Explicit)
                    {
                        var fieldOffset = fi.GetCustomAttributes(typeof(FieldOffsetAttribute), true).FirstOrDefault() as FieldOffsetAttribute;
                        offset = currentOffset + fieldOffset?.Value ?? 0;
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
            var marshalAsAttr = fi.GetCustomAttributes(typeof(MarshalAsAttribute), true).FirstOrDefault() as MarshalAsAttribute;
            if (marshalAsAttr == null)
                return new object[0];

            // Get the number of elements expected
            int elementCount = -1;
            if (marshalAsAttr.Value == UnmanagedType.ByValArray)
            {
                elementCount = marshalAsAttr.SizeConst;
            }
            else if (marshalAsAttr.Value == UnmanagedType.LPArray)
            {
                elementCount = marshalAsAttr.SizeConst;
                if (marshalAsAttr.SizeParamIndex >= 0)
                    elementCount = GetSizeFromField(marshalAsAttr, fields, instance);
            }

            // Get the item type for the array
            Type elementType = fi.FieldType.GetElementType() ?? typeof(object);

            // Create an array of the proper length
            Array arr = Array.CreateInstance(elementType, elementCount);

            // Loop through and build the array
            for (int i = 0; i < elementCount; i++)
            {
                var value = ReadType(content, ref offset, elementType);
                arr.SetValue(value, i);
            }

            // Return the built array
            return arr;
        }

        /// <summary>
        /// Get the expected LPArray size
        /// </summary>
        private static int GetSizeFromField(MarshalAsAttribute marshalAsAttr, FieldInfo[] fields, object instance)
        {
            // If the index is invalid
            if (marshalAsAttr.SizeParamIndex < 0)
                return -1;

            // Get the size field
            var sizeField = fields[marshalAsAttr.SizeParamIndex];
            if (sizeField == null)
                return -1;

            // Cast based on the field type
            return sizeField.GetValue(instance) switch
            {
                sbyte val => val,
                byte val => val,
                short val => val,
                ushort val => val,
                int val => val,
                uint val => (int)val,
                long val => (int)val,
                ulong val => (int)val,
                _ => -1,
            };
        }

        /// <summary>
        /// Read a string type field for an object
        /// </summary>
        private static string? ReadStringType(byte[] content, ref int offset, Encoding encoding, object instance, FieldInfo fi)
        {
            var marshalAsAttr = fi.GetCustomAttributes(typeof(MarshalAsAttribute), true).FirstOrDefault() as MarshalAsAttribute;

            switch (marshalAsAttr?.Value)
            {
                case UnmanagedType.AnsiBStr:
                    byte ansiLength = content.ReadByteValue(ref offset);
                    byte[] ansiBytes = content.ReadBytes(ref offset, ansiLength);
                    return Encoding.ASCII.GetString(ansiBytes);

                case UnmanagedType.BStr:
                    ushort bstrLength = content.ReadUInt16(ref offset);
                    byte[] bstrBytes = content.ReadBytes(ref offset, bstrLength * 2);
                    return Encoding.Unicode.GetString(bstrBytes);

                case UnmanagedType.ByValTStr:
                    int byvalLength = marshalAsAttr.SizeConst;
                    byte[] byvalBytes = content.ReadBytes(ref offset, byvalLength);
                    return encoding.GetString(byvalBytes);

                case UnmanagedType.LPStr:
                case null:
                    var lpstrBytes = new List<byte>();
                    while (true)
                    {
                        byte next = content.ReadByteValue(ref offset);
                        if (next == 0x00)
                            break;

                        lpstrBytes.Add(next);

                        if (offset >= content.Length)
                            break;
                    }

                    return Encoding.ASCII.GetString([.. lpstrBytes]);

                case UnmanagedType.LPWStr:
                    var lpwstrBytes = new List<byte>();
                    while (true)
                    {
                        ushort next = content.ReadUInt16(ref offset);

                        if (next == 0x0000)
                            break;
                        lpwstrBytes.AddRange(BitConverter.GetBytes(next));

                        if (offset >= content.Length)
                            break;
                    }

                    return Encoding.Unicode.GetString([.. lpwstrBytes]);

                // No support required yet
                case UnmanagedType.LPTStr:
#if NET472_OR_GREATER || NETCOREAPP
                case UnmanagedType.LPUTF8Str:
#endif
                case UnmanagedType.TBStr:
                default:
                    return null;
            }
        }

        /// <summary>
        /// Read a number of bytes from the byte array to a buffer
        /// </summary>
        private static byte[] ReadToBuffer(byte[] content, ref int offset, int length)
        {
            // If we have an invalid length
            if (length < 0)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be 0 or a positive value");

            // Handle the 0-byte case
            if (length == 0)
                return [];

            // If there are not enough bytes
            if (offset + length > content.Length)
                throw new System.IO.EndOfStreamException($"Requested to read {nameof(length)} bytes from {nameof(content)}, {content.Length - offset} returned");

            // Handle the general case, forcing a read of the correct length
            byte[] buffer = new byte[length];
            Array.Copy(content, offset, buffer, 0, length);
            offset += length;

            return buffer;
        }
    }
}