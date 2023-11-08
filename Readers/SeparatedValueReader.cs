using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SabreTools.IO.Readers
{
    public class SeparatedValueReader : IDisposable
    {
        /// <summary>
        /// Internal stream reader for inputting
        /// </summary>
        private readonly StreamReader? sr;

        /// <summary>
        /// Internal value to say how many fields should be written
        /// </summary>
        private int fields = -1;

        /// <summary>
        /// Get if at end of stream
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                return sr?.EndOfStream ?? true;
            }
        }

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

        /// <summary>
        /// Constructor for reading from a file
        /// </summary>
        public SeparatedValueReader(string filename)
        {
            sr = new StreamReader(filename);
        }

        /// <summary>
        /// Constructor for reading from a stream
        /// </summary>
        public SeparatedValueReader(Stream stream, Encoding encoding)
        {
            sr = new StreamReader(stream, encoding);
        }

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
            if (sr?.BaseStream == null)
                return false;

            if (!sr.BaseStream.CanRead || sr.EndOfStream)
                return false;

            string? fullLine = sr.ReadLine();
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
                foreach (Match match in lineSplitRegex.Matches(fullLine))
                {
                    string curr = match.Value;
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
                Line = fullLine.Split(Separator).Select(f => f.Trim()).ToList();
            }

            // If we don't have a header yet and are expecting one, read this as the header
            if (Header && HeaderValues == null)
            {
                HeaderValues = Line;
                fields = HeaderValues.Count;
            }

            // If we're verifying field counts and the numbers are off, error out
            if (VerifyFieldCount && fields != -1 && Line.Count != fields)
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

        /// <summary>
        /// Dispose of the underlying reader
        /// </summary>
        public void Dispose()
        {
            sr?.Dispose();
        }
    }
}
