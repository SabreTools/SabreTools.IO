using System.IO;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class StreamWriterExtensionsTests
    {
        [Fact]
        public void WriteNullTerminatedAnsiStringTest()
        {
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = stream.WriteNullTerminatedAnsiString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUTF8StringTest()
        {
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = stream.WriteNullTerminatedUTF8String("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUnicodeStringTest()
        {
            var stream = new MemoryStream(new byte[8], 0, 8, true, true);
            byte[] expected = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];

            bool write = stream.WriteNullTerminatedUnicodeString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUTF32StringTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];

            bool write = stream.WriteNullTerminatedUTF32String("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WritePrefixedAnsiStringTest()
        {
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            byte[] expected = [0x03, 0x41, 0x42, 0x43];

            bool write = stream.WritePrefixedAnsiString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WritePrefixedUnicodeStringTest()
        {
            var stream = new MemoryStream(new byte[8], 0, 8, true, true);
            byte[] expected = [0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00];

            bool write = stream.WritePrefixedUnicodeString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        /// <summary>
        /// Validate that a set of actual bytes matches the expected bytes
        /// </summary>
        private static void ValidateBytes(byte[] expected, byte[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
