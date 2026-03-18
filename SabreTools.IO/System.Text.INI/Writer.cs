using System;
using System.IO;
using System.Text;

namespace SabreTools.Text.INI
{
    public class Writer : IDisposable
    {
        #region Private Properties

        /// <summary>
        /// Internal stream writer
        /// </summary>
        private readonly StreamWriter _writer;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for writing to a file
        /// </summary>
        public Writer(string filename)
        {
            _writer = new StreamWriter(filename);
        }

        /// <summary>
        /// Consturctor for writing to a stream
        /// </summary>
        public Writer(Stream stream, Encoding encoding)
        {
#if NET20 || NET35 || NET40
            _writer = new StreamWriter(stream, encoding);
#else
            _writer = new StreamWriter(stream, encoding, 1024, leaveOpen: true);
#endif
        }

        /// <summary>
        /// Constructor for writing to a stream writer
        /// </summary>
        public Writer(StreamWriter streamWriter)
        {
            _writer = streamWriter;
        }

        #endregion

        /// <summary>
        /// Write a section tag
        /// </summary>
        /// <param name="value">Value to use as the section tag</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="value"/> is null or empty.
        /// </exception>
        public void WriteSection(string? value)
        {
            if (_writer.BaseStream is null)
                return;

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Section tag cannot be null or empty", nameof(value));

            _writer.WriteLine($"[{value!.TrimStart('[').TrimEnd(']')}]");
        }

        /// <summary>
        /// Write a key value pair
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="key"/> is null or empty.
        /// </exception>
        public void WriteKeyValuePair(string key, string? value)
        {
            if (_writer.BaseStream is null)
                return;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("Key cannot be null or empty", nameof(key));

            value ??= string.Empty;
            _writer.WriteLine($"{key}={value}");
        }

        /// <summary>
        /// Write a comment
        /// </summary>
        public void WriteComment(string? value)
        {
            if (_writer.BaseStream is null)
                return;

            value ??= string.Empty;
            _writer.WriteLine($";{value}");
        }

        /// <summary>
        /// Write a generic string
        /// </summary>
        public void WriteString(string? value)
        {
            if (_writer.BaseStream is null)
                return;

            value ??= string.Empty;
            _writer.Write(value);
        }

        /// <summary>
        /// Write a newline
        /// </summary>
        public void WriteLine()
        {
            if (_writer.BaseStream is null)
                return;

            _writer.WriteLine();
        }

        /// <summary>
        /// Flush the underlying writer
        /// </summary>
        public void Flush()
        {
            _writer.Flush();
        }

        #region IDisposable Implementation

        /// <summary>
        /// Dispose of the underlying writer
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }

        #endregion
    }
}
