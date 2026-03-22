using System.Linq;
using System.Text;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class ByteArrayReaderExtensionsTests
    {
        #region ReadNullTerminatedString

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

        #endregion

        #region ReadStringsFrom

        [Fact]
        public void ReadStringsFrom_Null_Null()
        {
            byte[]? arr = null;
            var actual = arr.ReadStringsFrom(3);
            Assert.Null(actual);
        }

        [Fact]
        public void ReadStringsFrom_Empty_Null()
        {
            byte[]? arr = [];
            var actual = arr.ReadStringsFrom(3);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2048)]
        public void ReadStringsFrom_InvalidLimit_Empty(int charLimit)
        {
            byte[]? arr = new byte[1024];
            var actual = arr.ReadStringsFrom(charLimit);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsFrom_NoValidStrings_Empty()
        {
            byte[]? arr = new byte[1024];
            var actual = arr.ReadStringsFrom(4);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsFrom_AsciiStrings_Filled()
        {
            byte[]? arr =
            [
                .. Encoding.ASCII.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = arr.ReadStringsFrom(4);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsFrom_Latin1Strings_Filled()
        {
            byte[]? arr =
            [
                .. Encoding.Latin1.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = arr.ReadStringsFrom(4);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsFrom_UTF16_Filled()
        {
            byte[]? arr =
            [
                .. Encoding.Unicode.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = arr.ReadStringsFrom(4);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsFrom_Mixed_Filled()
        {
            byte[]? arr =
            [
                .. Encoding.ASCII.GetBytes("TEST1"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO1"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA1"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("TEST2"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("TWO2"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("DATA2"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TEST3"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TWO3"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("DATA3"),
                .. new byte[] { 0x00 },
            ];
            var actual = arr.ReadStringsFrom(5);
            Assert.NotNull(actual);
            Assert.Equal(6, actual.Count);
        }

        /// <summary>
        /// This test is here mainly for performance testing
        /// and should not be enabled unless there are changes
        /// to the core reading methods that need comparison.
        /// </summary>
        // [Fact]
        // public void ReadStringsFrom_Mixed_MASSIVE()
        // {
        //     byte[]? arr =
        //     [
        //         .. Encoding.ASCII.GetBytes("TEST1"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.ASCII.GetBytes("TWO1"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.ASCII.GetBytes("DATA1"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.UTF8.GetBytes("TEST2"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.UTF8.GetBytes("TWO2"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.UTF8.GetBytes("DATA2"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.Unicode.GetBytes("TEST3"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.Unicode.GetBytes("TWO3"),
        //         .. new byte[] { 0x00 },
        //         .. Encoding.Unicode.GetBytes("DATA3"),
        //         .. new byte[] { 0x00 },
        //     ];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     arr = [.. arr, .. arr, .. arr, .. arr];
        //     // arr = [.. arr, .. arr, .. arr, .. arr];
        //     // arr = [.. arr, .. arr, .. arr, .. arr];

        //     var actual = arr.ReadStringsFrom(5);
        //     Assert.NotNull(actual);
        //     Assert.NotEmpty(actual);
        // }

        #endregion

        #region ReadStringsWithEncoding

        [Fact]
        public void ReadStringsWithEncoding_Null_Empty()
        {
            byte[]? bytes = null;
            var actual = bytes.ReadStringsWithEncoding(1, Encoding.ASCII);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_Empty_Empty()
        {
            byte[]? bytes = [];
            var actual = bytes.ReadStringsWithEncoding(1, Encoding.ASCII);
            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2048)]
        public void ReadStringsWithEncoding_InvalidLimit_Empty(int charLimit)
        {
            byte[]? bytes = new byte[1024];
            var actual = bytes.ReadStringsWithEncoding(charLimit, Encoding.ASCII);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_NoValidStrings_Empty()
        {
            byte[]? bytes = new byte[1024];
            var actual = bytes.ReadStringsWithEncoding(5, Encoding.ASCII);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_AsciiStrings_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.ASCII.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("ONE"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = bytes.ReadStringsWithEncoding(4, Encoding.ASCII);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsWithEncoding_InvalidAsciiChars_Empty()
        {
            byte[]? arr =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
                .. Enumerable.Range(0x80, 0x80).Select(i => (byte)i),
            ];
            var actual = arr.ReadStringsWithEncoding(1, Encoding.ASCII);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_Latin1_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.Latin1.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("ONE"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.Latin1.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = bytes.ReadStringsWithEncoding(4, Encoding.Latin1);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsWithEncoding_InvalidLatin1Chars_Empty()
        {
            byte[]? arr =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
                0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87,
                0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
                0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97,
                0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            ];
            var actual = arr.ReadStringsWithEncoding(1, Encoding.Latin1);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_UTF8_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.UTF8.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF8.GetBytes("ONE"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF8.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF8.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = bytes.ReadStringsWithEncoding(4, Encoding.UTF8);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsWithEncoding_InvalidUTF8Chars_Empty()
        {
            byte[]? arr =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
                .. Enumerable.Range(0x80, 0x42).Select(i => (byte)i),
                0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC,
                0xFD, 0xFE, 0xFF,
            ];
            var actual = arr.ReadStringsWithEncoding(1, Encoding.UTF8);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_UTF16_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.Unicode.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("ONE"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = bytes.ReadStringsWithEncoding(4, Encoding.Unicode);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsWithEncoding_InvalidUTF16Chars_Empty()
        {
            byte[]? arr =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            ];
            var actual = arr.ReadStringsWithEncoding(1, Encoding.Unicode);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsWithEncoding_UTF32_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.UTF32.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF32.GetBytes("ONE"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF32.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.UTF32.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            var actual = bytes.ReadStringsWithEncoding(4, Encoding.UTF32);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsWithEncoding_InvalidUTF32Chars_Empty()
        {
            byte[]? arr =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            ];
            var actual = arr.ReadStringsWithEncoding(1, Encoding.UTF32);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        #endregion

    }
}
