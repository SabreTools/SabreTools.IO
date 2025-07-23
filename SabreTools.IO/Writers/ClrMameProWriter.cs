using System;
using System.IO;
using System.Text;

namespace SabreTools.IO.Writers
{
    /// <summary>
    /// ClrMamePro writer patterned heavily off of XmlTextWriter
    /// </summary>
    /// <see cref="https://referencesource.microsoft.com/#System.Xml/System/Xml/Core/XmlTextWriter.cs"/>
    public class ClrMameProWriter : IDisposable
    {
        #region Private Enums and Structs

        /// <summary>
        /// State machine state for use in the table
        /// </summary>
        private enum State
        {
            Start,
            Prolog,
            Element,
            Attribute,
            Content,
            AttrOnly,
            Epilog,
            Error,
            Closed,
        }

        /// <summary>
        /// Potential token types
        /// </summary>
        private enum Token
        {
            None,
            Standalone,
            StartElement,
            EndElement,
            LongEndElement,
            StartAttribute,
            EndAttribute,
            Content,
        }

        /// <summary>
        /// Tag information for the stack
        /// </summary>
        private record struct TagInfo(string? Name, bool Mixed)
        {
            public void Init()
            {
                Name = null;
                Mixed = false;
            }
        }

        #endregion

        #region Constants

        /// <summary>
        /// State table for determining the state machine
        /// </summary>
        private readonly State[] StateTable = [
            //                         State.Start      State.Prolog     State.Element    State.Attribute  State.Content   State.AttrOnly   State.Epilog
            //
            /* Token.None           */ State.Prolog,    State.Prolog,    State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.Standalone     */ State.Prolog,    State.Prolog,    State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.StartElement   */ State.Element,   State.Element,   State.Element,   State.Element,   State.Element,  State.Error,     State.Element,
            /* Token.EndElement     */ State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.LongEndElement */ State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.StartAttribute */ State.AttrOnly,  State.Error,     State.Attribute, State.Attribute, State.Error,    State.Error,     State.Error,
            /* Token.EndAttribute   */ State.Error,     State.Error,     State.Error,     State.Element,   State.Error,    State.Epilog,    State.Error,
            /* Token.Content        */ State.Content,   State.Content,   State.Content,   State.Attribute, State.Content,  State.Attribute, State.Epilog,
        ];

        #endregion

        #region Fields

        /// <summary>
        /// Get if quotes should surround attribute values
        /// </summary>
        public bool Quotes { get; set; }

        #endregion

        #region Private Properties

        /// <summary>
        /// Internal stream writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// Stack for tracking current node
        /// </summary>
        private TagInfo[] _stack;

        /// <summary>
        /// Pointer to current top element in the stack
        /// </summary>
        private int _topPtr;

        /// <summary>
        /// Current state in the machine
        /// </summary>
        private State _currentState;

        /// <summary>
        /// Last seen token
        /// </summary>
        private Token _lastToken;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for writing to a file
        /// </summary>
        public ClrMameProWriter(string filename)
        {
            _writer = new StreamWriter(filename);
            Quotes = true;

            // Element stack
            _stack = new TagInfo[10];
            _topPtr = 0;
            _stack[_topPtr].Init();
        }

        /// <summary>
        /// Constructor for writing to a stream
        /// </summary>
        public ClrMameProWriter(Stream stream, Encoding encoding)
        {
#if NET20 || NET35 || NET40
            _writer = new StreamWriter(stream, encoding);
#else
            _writer = new StreamWriter(stream, encoding, 1024, leaveOpen: true);
#endif
            Quotes = true;

            // Element stack
            _stack = new TagInfo[10];
            _topPtr = 0;
            _stack[_topPtr].Init();
        }

        /// <summary>
        /// Constructor for writing to a stream writer
        /// </summary>
        public ClrMameProWriter(StreamWriter streamWriter)
        {
            _writer = streamWriter;
            Quotes = true;

            // Element stack
            _stack = new TagInfo[10];
            _topPtr = 0;
            _stack[_topPtr].Init();
        }

        #endregion

        /// <summary>
        /// Write the start of an element node
        /// </summary>
        public void WriteStartElement(string name)
        {
            try
            {
                // If we're writing quotes, don't write out quote characters internally
                if (Quotes)
                    name = name.Replace("\"", "''");

                AutoComplete(Token.StartElement);
                PushStack();
                _stack[_topPtr].Name = name;
                _writer.Write(name);
                _writer.Write(" (");
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Write the end of an element node
        /// </summary>
        public void WriteEndElement()
        {
            InternalWriteEndElement(false);
        }

        /// <summary>
        /// Write the end of a mixed element node
        /// </summary>
        public void WriteFullEndElement()
        {
            InternalWriteEndElement(true);
        }

        /// <summary>
        /// Write the start of an attribute node
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        public void WriteStartAttribute(string name, bool? quoteOverride = null)
        {
            try
            {
                // If we're writing quotes, don't write out quote characters internally
                if ((quoteOverride == null && Quotes) || (quoteOverride == true))
                    name = name.Replace("\"", "''");

                AutoComplete(Token.StartAttribute);
                _writer.Write(name);
                _writer.Write(" ");
                if ((quoteOverride == null && Quotes) || (quoteOverride == true))
                    _writer.Write("\"");
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Write the end of an attribute node
        /// </summary>
        public void WriteEndAttribute(bool? quoteOverride = null)
        {
            try
            {
                AutoComplete(Token.EndAttribute, quoteOverride);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Write a complete attribute with content
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        public void WriteAttributeString(string name, string? value, bool? quoteOverride = null)
        {
            WriteStartAttribute(name, quoteOverride);
            WriteString(value);
            WriteEndAttribute(quoteOverride);
        }

        /// <summary>
        /// Ensure writing writing null values as empty strings
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        /// <param name="throwOnError">Indicates if an error should be thrown on a missing required value</param>
        public void WriteRequiredAttributeString(string name, string? value, bool? quoteOverride = null, bool throwOnError = false)
        {
            // Throw an exception if we are configured to
            if (value == null && throwOnError)
                throw new ArgumentNullException(nameof(value));

            WriteAttributeString(name, value ?? string.Empty, quoteOverride);
        }

        /// <summary>
        /// Write an attribute, if the value is not null or empty
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        public void WriteOptionalAttributeString(string name, string? value, bool? quoteOverride = null)
        {
            if (!string.IsNullOrEmpty(value))
                WriteAttributeString(name, value, quoteOverride);
        }

        /// <summary>
        /// Write a standalone attribute
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        public void WriteStandalone(string name, string? value, bool? quoteOverride = null)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Name cannot be null or empty", nameof(name));

                // If we're writing quotes, don't write out quote characters internally
                if ((quoteOverride == null && Quotes)
                    || (quoteOverride == true))
                {
                    name = name.Replace("\"", "''");
                    value = value?.Replace("\"", "''");
                }

                AutoComplete(Token.Standalone);
                _writer.Write(name);
                _writer.Write(" ");
                if ((quoteOverride == null && Quotes)
                    || (quoteOverride == true))
                {
                    _writer.Write("\"");
                }
                _writer.Write(value);
                if ((quoteOverride == null && Quotes)
                    || (quoteOverride == true))
                {
                    _writer.Write("\"");
                }
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Ensure writing writing null values as empty strings
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        /// <param name="throwOnError">Indicates if an error should be thrown on a missing required value</param>
        public void WriteRequiredStandalone(string name, string? value, bool? quoteOverride = null, bool throwOnError = false)
        {
            // Throw an exception if we are configured to
            if (value == null && throwOnError)
                throw new ArgumentNullException(nameof(value));

            WriteStandalone(name, value ?? string.Empty, quoteOverride);
        }

        /// <summary>
        /// Write an standalone, if the value is not null or empty
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
        public void WriteOptionalStandalone(string name, string? value, bool? quoteOverride = null)
        {
            if (!string.IsNullOrEmpty(value))
                WriteStandalone(name, value, quoteOverride);
        }

        /// <summary>
        /// Write a string content value
        /// </summary>
        public void WriteString(string? value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    AutoComplete(Token.Content);

                    // If we're writing quotes, don't write out quote characters internally
                    if (Quotes)
                        value = value!.Replace("\"", "''");

                    _writer.Write(value);
                }
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Close the writer
        /// </summary>
        public void Close()
        {
            try
            {
                AutoCompleteAll();
            }
            catch
            {
                // Don't fail at this step
            }
            finally
            {
                _currentState = State.Closed;
                _writer.Close();
            }
        }

        /// <summary>
        /// Flush the base TextWriter
        /// </summary>
        public void Flush()
        {
            _writer.Flush();
        }

        /// <summary>
        /// Prepare for the next token to be written
        /// </summary>
        private void AutoComplete(Token token, bool? quoteOverride = null)
        {
            // Handle the error cases
            if (_currentState == State.Closed)
                throw new InvalidOperationException();
            else if (_currentState == State.Error)
                throw new InvalidOperationException();

            State newState = StateTable[(int)token * 7 + (int)_currentState];
            if (newState == State.Error)
                throw new InvalidOperationException();

            // TODO: Figure out how to get attributes on their own lines ONLY if an element contains both attributes and elements
            switch (token)
            {
                case Token.StartElement:
                case Token.Standalone:
                    if (_currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote(quoteOverride);
                        WriteEndStartTag(false);
                    }
                    else if (_currentState == State.Element)
                    {
                        WriteEndStartTag(false);
                    }

                    if (_currentState != State.Start)
                        Indent(false);

                    break;

                case Token.EndElement:
                case Token.LongEndElement:
                    if (_currentState == State.Attribute)
                        WriteEndAttributeQuote(quoteOverride);

                    if (_currentState == State.Content)
                        token = Token.LongEndElement;
                    else
                        WriteEndStartTag(token == Token.EndElement);

                    break;

                case Token.StartAttribute:
                    if (_currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote(quoteOverride);
                        _writer.Write(' ');
                    }
                    else if (_currentState == State.Element)
                    {
                        _writer.Write(' ');
                    }

                    break;

                case Token.EndAttribute:
                    WriteEndAttributeQuote(quoteOverride);
                    break;

                case Token.Content:
                    if (_currentState == State.Element && _lastToken != Token.Content)
                        WriteEndStartTag(false);

                    if (newState == State.Content)
                        _stack[_topPtr].Mixed = true;

                    break;

                default:
                    throw new InvalidOperationException();
            }

            _currentState = newState;
            _lastToken = token;
        }

        /// <summary>
        /// Autocomplete all open element nodes
        /// </summary>
        private void AutoCompleteAll()
        {
            while (_topPtr > 0)
            {
                WriteEndElement();
            }
        }

        /// <summary>
        /// Internal helper to write the end of an element
        /// </summary>
        private void InternalWriteEndElement(bool longFormat)
        {
            try
            {
                if (_topPtr <= 0)
                    throw new InvalidOperationException();

                AutoComplete(longFormat ? Token.LongEndElement : Token.EndElement);
                if (this._lastToken == Token.LongEndElement)
                {
                    Indent(true);
                    _writer.Write(')');
                }

                _topPtr--;
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Internal helper to write the end of a tag
        /// </summary>
        private void WriteEndStartTag(bool empty)
        {
            if (empty)
                _writer.Write(" )");
        }

        /// <summary>
        /// Internal helper to write the end of an attribute
        /// </summary>
        private void WriteEndAttributeQuote(bool? quoteOverride = null)
        {
            if ((quoteOverride == null && Quotes) || (quoteOverride == true))
                _writer.Write("\"");
        }

        /// <summary>
        /// Internal helper to indent a node, if necessary
        /// </summary>
        private void Indent(bool beforeEndElement)
        {
            if (_topPtr == 0)
            {
                _writer.WriteLine();
            }
            else if (!_stack[_topPtr].Mixed)
            {
                _writer.WriteLine();
                int i = beforeEndElement ? _topPtr - 1 : _topPtr;
                for (; i > 0; i--)
                {
                    _writer.Write('\t');
                }
            }
        }

        /// <summary>
        /// Move up one element in the stack
        /// </summary>
        private void PushStack()
        {
            if (_topPtr == _stack.Length - 1)
            {
                TagInfo[] na = new TagInfo[_stack.Length + 10];
                if (_topPtr > 0) Array.Copy(_stack, na, _topPtr + 1);
                _stack = na;
            }

            _topPtr++; // Move up stack
            _stack[_topPtr].Init();
        }

        #region IDisposable Implementation

        /// <summary>
        /// Close and dispose
        /// </summary>
        public void Dispose()
        {
            Close();
            _writer.Dispose();
        }

        #endregion
    }
}
