using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class ByteArrayWriterExtensionsTests
    {
        [Fact]
        public void WriteNullTerminatedAnsiStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = buffer.WriteNullTerminatedAnsiString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUTF8StringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = buffer.WriteNullTerminatedUTF8String(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUnicodeStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[8];
            byte[] expected = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];

            bool write = buffer.WriteNullTerminatedUnicodeString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUTF32StringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[16];
            byte[] expected = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];

            bool write = buffer.WriteNullTerminatedUTF32String(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WritePrefixedAnsiStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x03, 0x41, 0x42, 0x43];

            bool write = buffer.WritePrefixedAnsiString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WritePrefixedUnicodeStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[8];
            byte[] expected = [0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00];

            bool write = buffer.WritePrefixedUnicodeString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
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
