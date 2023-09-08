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
#if NET48
        private readonly StreamWriter sw;
#else
        private readonly StreamWriter? sw;
#endif

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
#if NET48
        public void WriteSection(string value)
#else
        public void WriteSection(string? value)
#endif
        {
            if (sw?.BaseStream == null)
                return;
            
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Section tag cannot be null or empty", nameof(value));

            sw.WriteLine($"[{value.TrimStart('[').TrimEnd(']')}]");
        }

        /// <summary>
        /// Write a key value pair
        /// </summary>
#if NET48
        public void WriteKeyValuePair(string key, string value)
#else
        public void WriteKeyValuePair(string key, string? value)
#endif
        {
            if (sw?.BaseStream == null)
                return;
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

#if NET48
            value = value != null ? value : string.Empty;
#else
            value ??= string.Empty;
#endif
            sw.WriteLine($"{key}={value}");
        }

        /// <summary>
        /// Write a comment
        /// </summary>
#if NET48
        public void WriteComment(string value)
#else
        public void WriteComment(string? value)
#endif
        {
            if (sw?.BaseStream == null)
                return;
            
#if NET48
            value = value != null ? value : string.Empty;
#else
            value ??= string.Empty;
#endif
            sw.WriteLine($";{value}");
        }

        /// <summary>
        /// Write a generic string
        /// </summary>
#if NET48
        public void WriteString(string value)
#else
        public void WriteString(string? value)
#endif
        {
            if (sw?.BaseStream == null)
                return;
            
#if NET48
            value = value != null ? value : string.Empty;
#else
            value ??= string.Empty;
#endif
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
