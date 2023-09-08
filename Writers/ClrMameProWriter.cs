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
#if NET48
        private class TagInfo
        {
            public string Name { get; set; }
            
            public bool Mixed { get; set; }

            public void Init()
            {
                Name = null;
                Mixed = false;
            }
        }
#else
        private record struct TagInfo(string? Name, bool Mixed)
        {
            public void Init()
            {
                Name = null;
                Mixed = false;
            }
        }
#endif

        /// <summary>
        /// Internal stream writer
        /// </summary>
        private readonly StreamWriter sw;

        /// <summary>
        /// Stack for tracking current node
        /// </summary>
        private TagInfo[] stack;

        /// <summary>
        /// Pointer to current top element in the stack
        /// </summary>
        private int top;

        /// <summary>
        /// State table for determining the state machine
        /// </summary>
        private readonly State[] stateTable = {
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
        };

        /// <summary>
        /// Current state in the machine
        /// </summary>
        private State currentState;

        /// <summary>
        /// Last seen token
        /// </summary>
        private Token lastToken;

        /// <summary>
        /// Get if quotes should surround attribute values
        /// </summary>
        public bool Quotes { get; set; }

        /// <summary>
        /// Constructor for opening a write from a file
        /// </summary>
        public ClrMameProWriter(string filename)
        {
            sw = new StreamWriter(filename);
            Quotes = true;

            // Element stack
            stack = new TagInfo[10];
            top = 0;
            stack[top].Init();
        }

        /// <summary>
        /// Constructor for opening a write from a stream and encoding
        /// </summary>
        public ClrMameProWriter(Stream stream, Encoding encoding)
        {
            sw = new StreamWriter(stream, encoding);
            Quotes = true;

            // Element stack
            stack = new TagInfo[10];
            top = 0;
            stack[top].Init();
        }

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
                stack[top].Name = name;
                sw.Write(name);
                sw.Write(" (");
            }
            catch
            {
                currentState = State.Error;
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
        /// Write a complete element with content
        /// </summary>
#if NET48
        public void WriteElementString(string name, string value)
#else
        public void WriteElementString(string name, string? value)
#endif
        {
            WriteStartElement(name);
            WriteString(value);
            WriteEndElement();
        }

        /// <summary>
        /// Ensure writing writing null values as empty strings
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="value">Value to write in the element</param>
        /// <param name="throwOnError">Indicates if an error should be thrown on a missing required value</param>
#if NET48
        public void WriteRequiredElementString(string name, string value, bool throwOnError = false)
#else
        public void WriteRequiredElementString(string name, string? value, bool throwOnError = false)
#endif
        {
            // Throw an exception if we are configured to
            if (value == null && throwOnError)
                throw new ArgumentNullException(nameof(value));

            WriteElementString(name, value ?? string.Empty);
        }

        /// <summary>
        /// Write an element, if the value is not null or empty
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="value">Value to write in the element</param>
#if NET48
        public void WriteOptionalElementString(string name, string value)
#else
        public void WriteOptionalElementString(string name, string? value)
#endif
        {
            if (!string.IsNullOrEmpty(value))
                WriteElementString(name, value);
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
                sw.Write(name);
                sw.Write(" ");
                if ((quoteOverride == null && Quotes) || (quoteOverride == true))
                    sw.Write("\"");
            }
            catch
            {
                currentState = State.Error;
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
                currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Write a complete attribute with content
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        /// <param name="quoteOverride">Non-null to overwrite the writer setting, null otherwise</param>
#if NET48
        public void WriteAttributeString(string name, string value, bool? quoteOverride = null)
#else
        public void WriteAttributeString(string name, string? value, bool? quoteOverride = null)
#endif
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
#if NET48
        public void WriteRequiredAttributeString(string name, string value, bool? quoteOverride = null, bool throwOnError = false)
#else
        public void WriteRequiredAttributeString(string name, string? value, bool? quoteOverride = null, bool throwOnError = false)
#endif
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
#if NET48
        public void WriteOptionalAttributeString(string name, string value, bool? quoteOverride = null)
#else
        public void WriteOptionalAttributeString(string name, string? value, bool? quoteOverride = null)
#endif
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
#if NET48
        public void WriteStandalone(string name, string value, bool? quoteOverride = null)
#else
        public void WriteStandalone(string name, string? value, bool? quoteOverride = null)
#endif
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
                sw.Write(name);
                sw.Write(" ");
                if ((quoteOverride == null && Quotes)
                    || (quoteOverride == true))
                {
                    sw.Write("\"");
                }
                sw.Write(value);
                if ((quoteOverride == null && Quotes)
                    || (quoteOverride == true))
                {
                    sw.Write("\"");
                }
            }
            catch
            {
                currentState = State.Error;
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
#if NET48
        public void WriteRequiredStandalone(string name, string value, bool? quoteOverride = null, bool throwOnError = false)
#else
        public void WriteRequiredStandalone(string name, string? value, bool? quoteOverride = null, bool throwOnError = false)
#endif
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
#if NET48
        public void WriteOptionalStandalone(string name, string value, bool? quoteOverride = null)
#else
        public void WriteOptionalStandalone(string name, string? value, bool? quoteOverride = null)
#endif
        {
            if (!string.IsNullOrEmpty(value))
                WriteStandalone(name, value, quoteOverride);
        }

        /// <summary>
        /// Write a string content value
        /// </summary>
#if NET48
        public void WriteString(string value)
#else
        public void WriteString(string? value)
#endif
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    AutoComplete(Token.Content);

                    // If we're writing quotes, don't write out quote characters internally
                    if (Quotes)
                        value = value.Replace("\"", "''");

                    sw.Write(value);
                }
            }
            catch
            {
                currentState = State.Error;
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
                currentState = State.Closed;
                sw.Close();
            }
        }

        /// <summary>
        /// Close and dispose
        /// </summary>
        public void Dispose()
        {
            Close();
            sw.Dispose();
        }

        /// <summary>
        /// Flush the base TextWriter
        /// </summary>
        public void Flush()
        {
            sw.Flush();
        }

        /// <summary>
        /// Prepare for the next token to be written
        /// </summary>
        private void AutoComplete(Token token, bool? quoteOverride = null)
        {
            // Handle the error cases
            if (currentState == State.Closed)
                throw new InvalidOperationException();
            else if (currentState == State.Error)
                throw new InvalidOperationException();

            State newState = stateTable[(int)token * 7 + (int)currentState];
            if (newState == State.Error)
                throw new InvalidOperationException();

            // TODO: Figure out how to get attributes on their own lines ONLY if an element contains both attributes and elements
            switch (token)
            {
                case Token.StartElement:
                case Token.Standalone:
                    if (currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote(quoteOverride);
                        WriteEndStartTag(false);
                    }
                    else if (currentState == State.Element)
                    {
                        WriteEndStartTag(false);
                    }

                    if (currentState != State.Start)
                        Indent(false);

                    break;

                case Token.EndElement:
                case Token.LongEndElement:
                    if (currentState == State.Attribute)
                        WriteEndAttributeQuote(quoteOverride);

                    if (currentState == State.Content)
                        token = Token.LongEndElement;
                    else
                        WriteEndStartTag(token == Token.EndElement);

                    break;

                case Token.StartAttribute:
                    if (currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote(quoteOverride);
                        sw.Write(' ');
                    }
                    else if (currentState == State.Element)
                    {
                        sw.Write(' ');
                    }

                    break;

                case Token.EndAttribute:
                    WriteEndAttributeQuote(quoteOverride);
                    break;

                case Token.Content:
                    if (currentState == State.Element && lastToken != Token.Content)
                        WriteEndStartTag(false);

                    if (newState == State.Content)
                        stack[top].Mixed = true;

                    break;

                default:
                    throw new InvalidOperationException();
            }

            currentState = newState;
            lastToken = token;
        }

        /// <summary>
        /// Autocomplete all open element nodes
        /// </summary>
        private void AutoCompleteAll()
        {
            while (top > 0)
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
                if (top <= 0)
                    throw new InvalidOperationException();

                AutoComplete(longFormat ? Token.LongEndElement : Token.EndElement);
                if (this.lastToken == Token.LongEndElement)
                {
                    Indent(true);
                    sw.Write(')');
                }

                top--;
            }
            catch
            {
                currentState = State.Error;
                throw;
            }
        }

        /// <summary>
        /// Internal helper to write the end of a tag
        /// </summary>
        private void WriteEndStartTag(bool empty)
        {
            if (empty)
                sw.Write(" )");
        }

        /// <summary>
        /// Internal helper to write the end of an attribute
        /// </summary>
        private void WriteEndAttributeQuote(bool? quoteOverride = null)
        {
            if ((quoteOverride == null && Quotes) || (quoteOverride == true))
                sw.Write("\"");
        }

        /// <summary>
        /// Internal helper to indent a node, if necessary
        /// </summary>
        private void Indent(bool beforeEndElement)
        {
            if (top == 0)
            {
                sw.WriteLine();
            }
            else if (!stack[top].Mixed)
            {
                sw.WriteLine();
                int i = beforeEndElement ? top - 1 : top;
                for (; i > 0; i--)
                {
                    sw.Write('\t');
                }
            }
        }

        /// <summary>
        /// Move up one element in the stack
        /// </summary>
        private void PushStack()
        {
            if (top == stack.Length - 1)
            {
                TagInfo[] na = new TagInfo[stack.Length + 10];
                if (top > 0) Array.Copy(stack, na, top + 1);
                stack = na;
            }

            top++; // Move up stack
            stack[top].Init();
        }
    }
}
