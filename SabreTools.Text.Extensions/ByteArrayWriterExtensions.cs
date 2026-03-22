using System;
using System.Text;
using SabreTools.Numerics.Extensions;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for byte arrays
    /// </summary>
    public static class ByteArrayWriterExtensions
    {
        #region Write Null Terminated

        /// <summary>
        /// Write a null-terminated string to the array
        /// </summary>
        public static bool WriteNullTerminatedString(this byte[] content, ref int offset, string? value, Encoding encoding)
        {
            // If the value is null
            if (value is null)
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

        #endregion

        #region Write Prefixed

        /// <summary>
        /// Write a byte-prefixed ASCII string to the byte array
        /// </summary>
        public static bool WritePrefixedAnsiString(this byte[] content, ref int offset, string? value)
        {
            // If the value is null
            if (value is null)
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
            if (value is null)
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
            if (value is null)
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
            if (value is null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.BigEndianUnicode.GetBytes(value);

            // Write the length as a ushort
            if (!content.Write(ref offset, (ushort)value.Length))
                return false;

            // Write the buffer
            return WriteFromBuffer(content, ref offset, buffer);
        }

        #endregion

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
