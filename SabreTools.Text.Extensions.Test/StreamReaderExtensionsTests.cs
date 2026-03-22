using System.IO;
using System.Text;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class StreamReaderExtensionsTests
    {
        [Fact]
        public void ReadNullTerminatedAnsiStringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.ASCII);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedLatin1StringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF8StringTest()
        {
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.UTF8);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUnicodeStringTest()
        {
            byte[] bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.Unicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedBigEndianUnicodeStringTest()
        {
            byte[] bytes = [0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.BigEndianUnicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF32StringTest()
        {
            byte[] bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.UTF32);
            Assert.Equal("ABC", actual);
        }
    }
}
