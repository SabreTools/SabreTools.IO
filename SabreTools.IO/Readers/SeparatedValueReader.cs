using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SabreTools.IO.Readers
{
    public class SeparatedValueReader : IDisposable
    {
        #region Fields

        /// <summary>
        /// Get if at end of stream
        /// </summary>
        public bool EndOfStream => _reader.EndOfStream;

        /// <summary>
        /// Contents of the current line, unprocessed
        /// </summary>
        public string? CurrentLine { get; private set; } = string.Empty;

        /// <summary>
        /// Get the current line number
        /// </summary>
        public long LineNumber { get; private set; } = 0;

        /// <summary>
        /// Assume the first row is a header
        /// </summary>
        public bool Header { get; set; } = true;

        /// <summary>
        /// Header row values
        /// </summary>
        public List<string>? HeaderValues { get; set; } = null;

        /// <summary>
        /// Get the current line values
        /// </summary>
        public List<string>? Line { get; private set; } = null;

        /// <summary>
        /// Assume that values are wrapped in quotes
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
        /// Internal stream reader
        /// </summary>
        private readonly StreamReader _reader;

        /// <summary>
        /// How many fields should be written
        /// </summary>
        private int _fieldCount = -1;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for reading from a file
        /// </summary>
        public SeparatedValueReader(string filename)
        {
            _reader = new StreamReader(filename);
        }

        /// <summary>
        /// Constructor for reading from a stream
        /// </summary>
        public SeparatedValueReader(Stream stream, Encoding encoding)
        {
            _reader = new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Constructor for reading from a stream reader
        /// </summary>
        public SeparatedValueReader(StreamReader streamReader)
        {
            _reader = streamReader;
        }

        #endregion

        /// <summary>
        /// Read the header line
        /// </summary>
        public bool ReadHeader()
        {
            if (!Header)
                throw new InvalidOperationException("No header line expected");

            if (HeaderValues != null)
                throw new InvalidOperationException("No more than 1 header row in a file allowed");

            return ReadNextLine();
        }

        /// <summary>
        /// Read the next line in the separated value file
        /// </summary>
        public bool ReadNextLine()
        {
            if (_reader.BaseStream == null)
                return false;

            if (!_reader.BaseStream.CanRead || _reader.EndOfStream)
                return false;

            string? fullLine = _reader.ReadLine();
            CurrentLine = fullLine;
            LineNumber++;

            if (fullLine == null)
                return false;

            // If we have quotes, we need to split specially
            if (Quotes)
            {
                // https://stackoverflow.com/questions/3776458/split-a-comma-separated-string-with-both-quoted-and-unquoted-strings
                var lineSplitRegex = new Regex($"(?:^|{Separator})(\"(?:[^\"]+|\"\")*\"|[^{Separator}]*)");
                var temp = new List<string>();
                foreach (Match? match in lineSplitRegex.Matches(fullLine))
                {
                    string? curr = match?.Value;
                    if (curr == null)
                        continue;
                    if (curr.Length == 0)
                        temp.Add("");

                    // Trim separator, whitespace, quotes, inter-quote whitespace
                    curr = curr.TrimStart(Separator).Trim().Trim('\"').Trim();
                    temp.Add(curr);
                }

                Line = temp;
            }

            // Otherwise, just split on the delimiter
            else
            {
                var lineArr = fullLine.Split(Separator);
                lineArr = Array.ConvertAll(lineArr, f => f.Trim());
                Line = [.. lineArr];
            }

            // If we don't have a header yet and are expecting one, read this as the header
            if (Header && HeaderValues == null)
            {
                HeaderValues = Line;
                _fieldCount = HeaderValues.Count;
            }

            // If we're verifying field counts and the numbers are off, error out
            if (VerifyFieldCount && _fieldCount != -1 && Line.Count != _fieldCount)
                throw new InvalidDataException($"Invalid row found, cannot continue: {fullLine}");

            return true;
        }

        /// <summary>
        /// Get the value for the current line for the current key
        /// </summary>
        public string? GetValue(string key)
        {
            // No header means no key-based indexing
            if (!Header)
                throw new ArgumentException("No header expected so no keys can be used");

            // If we don't have the key, return null
            if (HeaderValues == null)
                throw new ArgumentException($"Current line doesn't have key {key}");
            if (!HeaderValues.Contains(key))
                return null;

            int index = HeaderValues.IndexOf(key);
            if (Line == null)
                throw new ArgumentException($"Current line doesn't have index {index}");
            if (Line.Count < index)
                throw new ArgumentException($"Current line doesn't have index {index}");

            return Line[index];
        }

        /// <summary>
        /// Get the value for the current line for the current index
        /// </summary>
        public string GetValue(int index)
        {
            if (Line == null)
                throw new ArgumentException($"Current line doesn't have index {index}");
            if (Line.Count < index)
                throw new ArgumentException($"Current line doesn't have index {index}");

            return Line[index];
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
