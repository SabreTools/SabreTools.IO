using System.IO;
using System.Text;
using SabreTools.IO.Readers;
using SabreTools.IO.Writers;
using Xunit;

namespace SabreTools.IO.Test.ReadersWriters
{
    public class IniTests
    {
        [Fact]
        public void EndToEndTest()
        {
            string expected = "[section1]\nkey1=value1\nkey2=value2\n\n;comment\n;string\n";

            // Build and write the INI
            var stream = new MemoryStream();
            var writer = new IniWriter(stream, Encoding.UTF8);

            writer.WriteSection("section1");
            writer.WriteKeyValuePair("key1", "value1");
            writer.WriteKeyValuePair("key2", "value2");
            writer.WriteLine();
            writer.WriteComment("comment");
            writer.WriteString(";string\n");

            writer.Flush();
            writer.Dispose();

            // Length includes UTF-8 BOM
            Assert.Equal(56, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);

            // Parse the INI
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new IniReader(stream, Encoding.UTF8);

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