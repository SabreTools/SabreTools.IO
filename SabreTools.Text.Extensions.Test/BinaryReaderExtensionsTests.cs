using System.IO;
using System.Text;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class BinaryReaderExtensionsTests
    {
        #region ReadNullTerminatedString

        [Fact]
        public void ReadNullTerminatedAnsiStringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.ASCII);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedLatin1StringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF8StringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.UTF8);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUnicodeStringTest()
        {
            byte[] bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.Unicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedBigEndianUnicodeStringTest()
        {
            byte[] bytes = [0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.BigEndianUnicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF32StringTest()
        {
            byte[] bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.UTF32);
            Assert.Equal("ABC", actual);
        }

        #endregion

        #region ReadPrefixedString

        [Fact]
        public void ReadPrefixedAnsiStringTest()
        {
            byte[] bytes = [0x03, 0x41, 0x42, 0x43];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadPrefixedAnsiString();
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadPrefixedLatin1StringTest()
        {
            byte[] bytes = [0x03, 0x41, 0x42, 0x43];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadPrefixedLatin1String();
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadPrefixedUnicodeStringTest()
        {
            byte[] bytes = [0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadPrefixedUnicodeString();
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadPrefixedBigEndianUnicodeStringTest()
        {
            byte[] bytes = [0x00, 0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadPrefixedBigEndianUnicodeString();
            Assert.Equal("ABC", actual);
        }

        #endregion
    }
}
