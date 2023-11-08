using System;
using System.IO;
using System.Text;

namespace SabreTools.IO.Writers
{
    public class IniWriter : IDisposable
    {
        /// <summary>
        /// Internal stream writer for outputting
        /// </summary>
        private readonly StreamWriter? sw;

        /// <summary>
        /// Constructor for writing to a file
        /// </summary>
        public IniWriter(string filename)
        {
            sw = new StreamWriter(filename);
        }

        /// <summary>
        /// Consturctor for writing to a stream
        /// </summary>
        public IniWriter(Stream stream, Encoding encoding)
        {
            sw = new StreamWriter(stream, encoding);
        }

        /// <summary>
        /// Write a section tag
        /// </summary>
        public void WriteSection(string? value)
        {
            if (sw?.BaseStream == null)
                return;
            
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Section tag cannot be null or empty", nameof(value));

            sw.WriteLine($"[{value!.TrimStart('[').TrimEnd(']')}]");
        }

        /// <summary>
        /// Write a key value pair
        /// </summary>
        public void WriteKeyValuePair(string key, string? value)
        {
            if (sw?.BaseStream == null)
                return;
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            value ??= string.Empty;
            sw.WriteLine($"{key}={value}");
        }

        /// <summary>
        /// Write a comment
        /// </summary>
        public void WriteComment(string? value)
        {
            if (sw?.BaseStream == null)
                return;
            
            value ??= string.Empty;
            sw.WriteLine($";{value}");
        }

        /// <summary>
        /// Write a generic string
        /// </summary>
        public void WriteString(string? value)
        {
            if (sw?.BaseStream == null)
                return;
            
            value ??= string.Empty;
            sw.Write(value);
        }

        /// <summary>
        /// Write a newline
        /// </summary>
        public void WriteLine()
        {
            if (sw?.BaseStream == null)
                return;
            
            sw.WriteLine();
        }

        /// <summary>
        /// Flush the underlying writer
        /// </summary>
        public void Flush()
        {
            sw?.Flush();
        }

        /// <summary>
        /// Dispose of the underlying writer
        /// </summary>
        public void Dispose()
        {
            sw?.Dispose();
        }
    }
}
