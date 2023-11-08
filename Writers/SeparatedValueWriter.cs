using System;
using System.IO;
using System.Text;

namespace SabreTools.IO.Writers
{
    public class SeparatedValueWriter : IDisposable
    {
        /// <summary>
        /// Internal stream writer for outputting
        /// </summary>
        private readonly StreamWriter sw;

        /// <summary>
        /// Internal value if we've written a header before
        /// </summary>
        private bool header = false;

        /// <summary>
        /// Internal value if we've written our first line before
        /// </summary>
        private bool firstRow = false;

        /// <summary>
        /// Internal value to say how many fields should be written
        /// </summary>
        private int fields = -1;

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

        /// <summary>
        /// Constructor for writing to a file
        /// </summary>
        public SeparatedValueWriter(string filename)
        {
            sw = new StreamWriter(filename);
        }

        /// <summary>
        /// Consturctor for writing to a stream
        /// </summary>
        public SeparatedValueWriter(Stream stream, Encoding encoding)
        {
            sw = new StreamWriter(stream, encoding);
        }

        /// <summary>
        /// Write a header row
        /// </summary>
        public void WriteHeader(string?[] headers)
        {
            // If we haven't written anything out, we can write headers
            if (!header && !firstRow)
                WriteValues(headers);

            header = true;
        }

        /// <summary>
        /// Write a value row
        /// </summary>
        public void WriteValues(object?[] values, bool newline = true)
        {
            // If the writer can't be used, we error
            if (sw == null || !sw.BaseStream.CanWrite)
                throw new ArgumentException(nameof(sw));

            // If the separator character is invalid, we error
            if (Separator == default(char))
                throw new ArgumentException(nameof(Separator));

            // If we have the first row, set the bool and the field count
            if (!firstRow)
            {
                firstRow = true;
                if (VerifyFieldCount && fields == -1)
                    fields = values.Length;
            }

            // Get the number of fields to write out
            int fieldCount = values.Length;
            if (VerifyFieldCount)
                fieldCount = Math.Min(fieldCount, fields);

            // Iterate over the fields, writing out each
            bool firstField = true;
            for (int i = 0; i < fieldCount; i++)
            {
                var value = values[i];

                if (!firstField)
                    sw.Write(Separator);

                if (Quotes)
                    sw.Write("\"");
                sw.Write(value?.ToString() ?? string.Empty);
                if (Quotes)
                    sw.Write("\"");

                firstField = false;
            }

            // If we need to pad out the number of fields, add empties
            if (VerifyFieldCount && values.Length < fields)
            {
                for (int i = 0; i < fields - values.Length; i++)
                {
                    sw.Write(Separator);

                    if (Quotes)
                        sw.Write("\"\"");
                }
            }

            // Add a newline, if needed
            if (newline)
                sw.WriteLine();
        }

        /// <summary>
        /// Write a generic string
        /// </summary>
        public void WriteString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            sw.Write(value);
        }

        /// <summary>
        /// Write a newline
        /// </summary>
        public void WriteLine()
        {
            sw.WriteLine();
        }

        /// <summary>
        /// Flush the underlying writer
        /// </summary>
        public void Flush()
        {
            sw.Flush();
        }

        /// <summary>
        /// Dispose of the underlying writer
        /// </summary>
        public void Dispose()
        {
            sw.Dispose();
        }
    }
}
