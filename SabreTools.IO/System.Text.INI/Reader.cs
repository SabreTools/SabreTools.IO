using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SabreTools.Text.INI
{
    public class Reader : IDisposable
    {
        #region Fields

        /// <summary>
        /// Get if at end of stream
        /// </summary>
        public bool EndOfStream => _reader.EndOfStream;

        /// <summary>
        /// Contents of the currently read line as a key value pair
        /// </summary>
        public KeyValuePair<string, string>? KeyValuePair { get; private set; } = null;

        /// <summary>
        /// Contents of the current line, unprocessed
        /// </summary>
        public string? CurrentLine { get; private set; } = string.Empty;

        /// <summary>
        /// Get the current line number
        /// </summary>
        public long LineNumber { get; private set; } = 0;

        /// <summary>
        /// Current row type
        /// </summary>
        public RowType RowType { get; private set; } = RowType.None;

        /// <summary>
        /// Current section being read
        /// </summary>
        public string? Section { get; private set; } = string.Empty;

        /// <summary>
        /// Validate that rows are in key=value format
        /// </summary>
        public bool ValidateRows { get; set; } = true;

        #endregion

        #region Private Properties

        /// <summary>
        /// Internal stream reader
        /// </summary>
        private readonly StreamReader _reader;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for reading from a file
        /// </summary>
        public Reader(string filename)
        {
            _reader = new StreamReader(filename);
        }

        /// <summary>
        /// Constructor for reading from a stream
        /// </summary>
        public Reader(Stream stream, Encoding encoding)
        {
            _reader = new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Constructor for reading from a stream reader
        /// </summary>
        public Reader(StreamReader streamReader)
        {
            _reader = streamReader;
        }

        #endregion

        /// <summary>
        /// Read the next line in the INI file
        /// </summary>
        public bool ReadNextLine()
        {
            if (_reader.BaseStream is null)
                return false;

            if (!_reader.BaseStream.CanRead || _reader.EndOfStream)
                return false;

            CurrentLine = _reader.ReadLine()?.Trim();
            LineNumber++;
            ProcessLine();
            return true;
        }

        /// <summary>
        /// Process the current line and extract out values
        /// </summary>
        /// <exception cref="InvalidDataException">
        /// Thrown if an invalid line is encountered during processing
        /// and <see cref="ValidateRows"/> is true.
        /// </exception>
        private void ProcessLine()
        {
            if (CurrentLine is null)
                return;

            // Comment
            if (CurrentLine.StartsWith(";"))
            {
                KeyValuePair = null;
                RowType = RowType.Comment;
            }

            // Section
            else if (CurrentLine.StartsWith("[") && CurrentLine.EndsWith("]"))
            {
                KeyValuePair = null;
                RowType = RowType.SectionHeader;
                Section = CurrentLine.TrimStart('[').TrimEnd(']');
            }

            // KeyValuePair
            else if (CurrentLine.Contains("="))
            {
                // Split the line by '=' for key-value pairs
                string[] data = CurrentLine.Split('=');

                // If the value field contains an '=', we need to put them back in
                string key = data[0].Trim();
                var valueArr = new string[data.Length - 1];
                Array.Copy(data, 1, valueArr, 0, valueArr.Length);
                string value = string.Join("=", valueArr).Trim();

                KeyValuePair = new KeyValuePair<string, string>(key, value);
                RowType = RowType.KeyValue;
            }

            // Empty
            else if (string.IsNullOrEmpty(CurrentLine))
            {
                KeyValuePair = null;
                CurrentLine = string.Empty;
                RowType = RowType.None;
            }

            // Invalid
            else
            {
                KeyValuePair = null;
                RowType = RowType.Invalid;

                if (ValidateRows)
                    throw new InvalidDataException($"Invalid INI row found, cannot continue: {CurrentLine}");
            }
        }

        #region IDisposable Implementation

        /// <summary>
        /// Dispose of the underlying reader
        /// </summary>
        public void Dispose()
        {
            _reader.Dispose();
        }

        #endregion
    }
}
