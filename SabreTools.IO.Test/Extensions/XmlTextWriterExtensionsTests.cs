using System;
using System.IO;
using System.Text;
using System.Xml;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class XmlTextWriterExtensionsTests
    {
        [Fact]
        public void WriteRequiredAttributeString_NullInputThrow_Throws()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            Assert.Throws<ArgumentNullException>(()
                => writer.WriteRequiredAttributeString("attr", null, throwOnError: true));
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(52, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRequiredAttributeString_NullInputNoThrow_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element attr=\"\" />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            writer.WriteRequiredAttributeString("attr", null, throwOnError: false);
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(60, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRequiredAttributeString_ValidInput_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element attr=\"val\" />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            writer.WriteRequiredAttributeString("attr", "val", throwOnError: false);
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(63, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRequiredElementString_NullInputThrow_Throws()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            Assert.Throws<ArgumentNullException>(()
                => writer.WriteRequiredElementString("element", null, throwOnError: true));
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(41, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRequiredElementString_NullInputNoThrow_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element></element>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteRequiredElementString("element", null, throwOnError: false);
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(60, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteRequiredElementString_ValidInput_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element>val</element>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteRequiredElementString("element", "val", throwOnError: false);
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(63, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }
    
        [Fact]
        public void WriteOptionalAttributeString_NullInput_NoWrite()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            writer.WriteOptionalAttributeString("attr", null);
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(52, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteOptionalAttributeString_EmptyInput_NoWrite()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            writer.WriteOptionalAttributeString("attr", string.Empty);
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(52, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteOptionalAttributeString_ValidInput_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element attr=\"val\" />";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("element");
            writer.WriteOptionalAttributeString("attr", "val");
            writer.WriteEndElement();
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(63, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }
    
        [Fact]
        public void WriteOptionalElementString_NullInput_NoWrite()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteOptionalElementString("element", null);
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(41, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteOptionalElementString_EmptyInput_NoWrite()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteOptionalElementString("element", string.Empty);
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(41, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteOptionalElementString_ValidInput_Writes()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><element>val</element>";

            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteOptionalElementString("element", "val");
            writer.Flush();

            // Length includes UTF-8 BOM
            Assert.Equal(63, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);
        }
    }
}