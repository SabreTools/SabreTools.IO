using System.IO;
using System.Text;
using SabreTools.IO.Readers;
using SabreTools.IO.Writers;
using Xunit;

namespace SabreTools.IO.Test.ReadersWriters
{
    public class SeparatedValueTests
    {
        [Fact]
        public void EndToEndTest()
        {
            string expected = "\"col1\",\"col2\",\"col3\"\n\"value1\",\"value2\",\"value3\"\n\"value4\",\"value5\",\"value6\"\n";

            // Build and write the CSV
            var stream = new MemoryStream();
            var writer = new SeparatedValueWriter(stream, Encoding.UTF8);
            Assert.True(writer.Quotes);
            Assert.Equal(',', writer.Separator);
            Assert.True(writer.VerifyFieldCount);

            writer.WriteHeader(["col1", "col2", "col3"]);
            writer.WriteValues(["value1", "value2", "value3"]);
            writer.WriteString("\"value4\",\"value5\",\"value6\"\n");

            writer.Flush();
            writer.Dispose();

            // Length includes UTF-8 BOM
            Assert.Equal(78, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);

            // Parse the CSV
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new SeparatedValueReader(stream, Encoding.UTF8);
            Assert.True(reader.Header);
            Assert.True(reader.Quotes);
            Assert.Equal(',', reader.Separator);
            Assert.True(reader.VerifyFieldCount);

            while (!reader.EndOfStream)
            {
                bool hasNext = reader.ReadNextLine();
                Assert.True(hasNext);
                Assert.NotNull(reader.CurrentLine);
                Assert.True(reader.LineNumber >= 0);

                if (reader.LineNumber > 0)
                {
                    Assert.NotNull(reader.GetValue(0));
                    Assert.NotNull(reader.GetValue("col2"));
                }
            }

            reader.Dispose();
        }
    }
}
