using System.Text;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class ByteArrayReaderExtensionsTests
    {
        [Fact]
        public void ReadNullTerminatedAnsiStringTest()
        {
            int offset = 0;
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.ASCII);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedLatin1StringTest()
        {
            int offset = 0;
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF8StringTest()
        {
            int offset = 0;
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.UTF8);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUnicodeStringTest()
        {
            int offset = 0;
            byte[] bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.Unicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedBigEndianUnicodeStringTest()
        {
            int offset = 0;
            byte[] bytes = [0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.BigEndianUnicode);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadNullTerminatedUTF32StringTest()
        {
            int offset = 0;
            byte[] bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.UTF32);
            Assert.Equal("ABC", actual);
        }
    }
}
