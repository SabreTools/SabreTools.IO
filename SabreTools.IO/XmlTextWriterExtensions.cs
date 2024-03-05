using System;
using System.Xml;

namespace SabreTools.IO
{
    /// <summary>
    /// Additional methods for XmlTextWriter
    /// </summary>
    public static class XmlTextWriterExtensions
    {
        /// <summary>
        /// Write an attribute, forcing empty if null
        /// </summary>
        /// <param name="writer">XmlTextWriter to write out with</param>
        /// <param name="localName">Name of the element</param>
        /// <param name="value">Value to write in the element</param>
        /// <param name="throwOnError">Indicates if an error should be thrown on a missing required value</param>
        public static void WriteRequiredAttributeString(this XmlTextWriter writer, string localName, string value, bool throwOnError = false)
        {
            // Throw an exception if we are configured to
            if (value == null && throwOnError)
                throw new ArgumentNullException(nameof(value));

            writer.WriteAttributeString(localName, value ?? string.Empty);
        }

        /// <summary>
        /// Force writing separate open and start tags, even for empty elements
        /// </summary>
        /// <param name="writer">XmlTextWriter to write out with</param>
        /// <param name="localName">Name of the element</param>
        /// <param name="value">Value to write in the element</param>
        /// <param name="throwOnError">Indicates if an error should be thrown on a missing required value</param>
        public static void WriteRequiredElementString(this XmlTextWriter writer, string localName, string value, bool throwOnError = false)
        {
            // Throw an exception if we are configured to
            if (value == null && throwOnError)
                throw new ArgumentNullException(nameof(value));

            writer.WriteStartElement(localName);
            if (value == null)
                writer.WriteRaw(string.Empty);
            else
                writer.WriteString(value);
            writer.WriteFullEndElement();
        }

        /// <summary>
        /// Write an attribute, if the value is not null or empty
        /// </summary>
        /// <param name="writer">XmlTextWriter to write out with</param>
        /// <param name="localName">Name of the attribute</param>
        /// <param name="value">Value to write in the attribute</param>
        public static void WriteOptionalAttributeString(this XmlTextWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteAttributeString(localName, value);
        }

        /// <summary>
        /// Write an element, if the value is not null or empty
        /// </summary>
        /// <param name="writer">XmlTextWriter to write out with</param>
        /// <param name="localName">Name of the element</param>
        /// <param name="value">Value to write in the element</param>
        public static void WriteOptionalElementString(this XmlTextWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteElementString(localName, value);
        }
    }
}
