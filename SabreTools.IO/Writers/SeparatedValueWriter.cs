using System;
using System.IO;
using System.Text;

namespace SabreTools.IO.Writers
{
    public class SeparatedValueWriter : IDisposable
    {
        #region Fields

        /// <summary>
        /// Set if values should be wrapped in quotes
        /// </summary>
        public bool Quotes { get; set; } = true;

        /// <summary>
        /// Set what character should be used as a separator
        /// </summary>
        public char Separator { get; set; } = ',';

        /// <summary>
        /// Set if field count should be verified from the first row
        /// </summary>
        public bool VerifyFieldCount { get; set; } = true;

        #endregion

        #region Private Properties

        /// <summary>
        /// Internal stream writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// Internal value if we've written a header before
        /// </summary>
        private bool _header = false;

        /// <summary>
        /// Internal value if we've written our first line before
        /// </summary>
        private bool _firstRow = false;

        /// <summary>
        /// Internal value to say how many fields should be written
        /// </summary>
        private int _fields = -1;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for writing to a file
        /// </summary>
        public SeparatedValueWriter(string filename)
        {
            _writer = new StreamWriter(filename);
        }

        /// <summary>
        /// Consturctor for writing to a stream
        /// </summary>
        public SeparatedValueWriter(Stream stream, Encoding encoding)
        {
#if NET20 || NET35 || NET40
            _writer = new StreamWriter(stream, encoding);
#else
            _writer = new StreamWriter(stream, encoding, 1024, leaveOpen: true);
#endif
        }

        /// <summary>
        /// Consturctor for writing to a stream writer
        /// </summary>
        public SeparatedValueWriter(StreamWriter streamWriter)
        {
            _writer = streamWriter;
        }

        #endregion

        /// <summary>
        /// Write a header row
        /// </summary>
        public void WriteHeader(string?[] headers)
        {
            // If we haven't written anything out, we can write headers
            if (!_header && !_firstRow)
                WriteValues(headers);

            _header = true;
        }

        /// <summary>
        /// Write a value row
        /// </summary>
        public void WriteValues(object?[] values, bool newline = true)
        {
            // If the writer can't be used, we error
            if (_writer == null || !_writer.BaseStream.CanWrite)
                throw new ArgumentException(nameof(_writer));

            // If the separator character is invalid, we error
            if (Separator == default(char))
                throw new ArgumentException(nameof(Separator));

            // If we have the first row, set the bool and the field count
            if (!_firstRow)
            {
                _firstRow = true;
                if (VerifyFieldCount && _fields == -1)
                    _fields = values.Length;
            }

            // Get the number of fields to write out
            int fieldCount = values.Length;
            if (VerifyFieldCount)
                fieldCount = Math.Min(fieldCount, _fields);

            // Iterate over the fields, writing out each
            bool firstField = true;
            for (int i = 0; i < fieldCount; i++)
            {
                var value = values[i];

                if (!firstField)
                    _writer.Write(Separator);

                if (Quotes)
                    _writer.Write("\"");
                _writer.Write(value?.ToString() ?? string.Empty);
                if (Quotes)
                    _writer.Write("\"");

                firstField = false;
            }

            // If we need to pad out the number of fields, add empties
            if (VerifyFieldCount && values.Length < _fields)
            {
                for (int i = 0; i < _fields - values.Length; i++)
                {
                    _writer.Write(Separator);

                    if (Quotes)
                        _writer.Write("\"\"");
                }
            }

            // Add a newline, if needed
            if (newline)
                _writer.WriteLine();
        }

        /// <summary>
        /// Write a generic string
        /// </summary>
        public void WriteString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            _writer.Write(value);
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
