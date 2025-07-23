using System.IO;
using System.Text;
using SabreTools.IO.Readers;
using SabreTools.IO.Writers;
using Xunit;

namespace SabreTools.IO.Test.ReadersWriters
{
    public class ClrMameProTests
    {
        [Fact]
        public void EndToEndTest()
        {
            string expected = "header (\n\tstandalone \"value\"\n)\n\n# Comment\n\ngame (\n\titem ( attr \"value\" )\n)";

            // Build and write the CMP file
            var stream = new MemoryStream();
            var writer = new ClrMameProWriter(stream, Encoding.UTF8);
            Assert.True(writer.Quotes);

            writer.WriteStartElement("header");
            writer.WriteRequiredStandalone("standalone", "value");
            writer.WriteOptionalStandalone("optstand", null);
            writer.WriteFullEndElement();

            writer.WriteString("\n\n# Comment\n");

            writer.WriteStartElement("game");
            writer.WriteStartElement("item");
            writer.WriteRequiredAttributeString("attr", "value");
            writer.WriteOptionalAttributeString("optional", null);
            writer.WriteEndElement();
            writer.WriteFullEndElement();

            writer.Flush();
            writer.Dispose();

            // Length includes UTF-8 BOM
            Assert.Equal(77, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);

            // Parse the CMP file
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new ClrMameProReader(stream, Encoding.UTF8);
            Assert.False(reader.DosCenter);
            Assert.True(reader.Quotes);

            while (!reader.EndOfStream)
            {
                bool hasNext = reader.ReadNextLine();
                Assert.True(hasNext);
                Assert.NotNull(reader.CurrentLine);
                Assert.True(reader.LineNumber >= 0);
            }

            reader.Dispose();
        }
    }
}
