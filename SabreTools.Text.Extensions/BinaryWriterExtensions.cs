using System.IO;
using System.Text;

namespace SabreTools.Text.Extensions
{
    /// <summary>
    /// Extensions for BinaryWriter
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Write a null-terminated string to the underlying stream
        /// </summary>
        public static bool WriteNullTerminatedString(this BinaryWriter writer, string? value, Encoding encoding)
        {
            // If the value is null
            if (value is null)
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
            if (value is null)
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
            if (value is null)
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
            if (value is null)
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
            if (value is null)
                return false;

            // Get the buffer
            byte[] buffer = Encoding.BigEndianUnicode.GetBytes(value);

            // Write the length as a ushort
            writer.Write((ushort)value.Length);

            // Write the buffer
            return WriteFromBuffer(writer, buffer);
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
