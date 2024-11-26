using System.IO;
using System.Text;
using Xunit;

namespace SabreTools.IO.Test
{
    public class IniFileTests
    {
        [Fact]
        public void EndToEndTest()
        {
            string expected = "[section1]\nkey1=value1\nkey2=value2\n";

            // Build the INI
            var iniFile = new IniFile();
            iniFile.AddOrUpdate("section1.key1", "value1");
            iniFile["section1.key2"] = "value2";
            iniFile["section2.key3"] = "REMOVEME";
            bool removed = iniFile.Remove("section2.key3");

            Assert.True(removed);
            Assert.Equal("value1", iniFile["section1.key1"]);
            Assert.Equal("value2", iniFile["section1.key2"]);

            // Write the INI
            var stream = new MemoryStream();
            bool write = iniFile.Write(stream);

            // Length includes UTF-8 BOM
            Assert.True(write);
            Assert.Equal(38, stream.Length);
            string actual = Encoding.UTF8.GetString(stream.ToArray(), 3, (int)stream.Length - 3);
            Assert.Equal(expected, actual);

            // Parse the INI
            stream.Seek(0, SeekOrigin.Begin);
            var secondIni = new IniFile(stream);
            Assert.Equal("value1", secondIni["section1.key1"]);
            Assert.Equal("value2", secondIni["section1.key2"]);
        }

        [Fact]
        public void RemoveInvalidKeyTest()
        {
            var iniFile = new IniFile();
            bool removed = iniFile.Remove("invalid.key");
            Assert.False(removed);
        }

        [Fact]
        public void ReadEmptyStreamTest()
        {
            var stream = new MemoryStream();
            var iniFile = new IniFile(stream);
            Assert.Empty(iniFile);
        }

        [Fact]
        public void WriteEmptyIniFileTest()
        {
            var iniFile = new IniFile();
            var stream = new MemoryStream();
            bool write = iniFile.Write(stream);
            Assert.False(write);
        }
    }
}