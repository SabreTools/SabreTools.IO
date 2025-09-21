using System;
using System.Linq;
using System.Text;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class ByteArrayExtensionsTests
    {
        #region IsNullOrEmpty

        [Fact]
        public void IsNullOrEmpty_Null_True()
        {
            byte[]? arr = null;
            bool actual = arr.IsNullOrEmpty();
            Assert.True(actual);
        }

        [Fact]
        public void IsNullOrEmpty_Empty_True()
        {
            byte[]? arr = [];
            bool actual = arr.IsNullOrEmpty();
            Assert.True(actual);
        }

        [Fact]
        public void IsNullOrEmpty_NonEmpty_False()
        {
            byte[]? arr = [0x01];
            bool actual = arr.IsNullOrEmpty();
            Assert.False(actual);
        }

        #endregion

        #region ToHexString

        [Fact]
        public void ToHexString_Null()
        {
            byte[]? arr = null;
            string? actual = arr.ToHexString();
            Assert.Null(actual);
        }

        [Fact]
        public void ToHexString_Valid()
        {
            byte[]? arr = [0x01, 0x02, 0x03, 0x04];
            string expected = "01020304";

            string? actual = arr.ToHexString();
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region FromHexString

        [Fact]
        public void FromHexString_Null()
        {
            string? str = null;
            byte[]? actual = str.FromHexString();
            Assert.Null(actual);
        }

        [Fact]
        public void FromHexString_Valid()
        {
            string str = "01020304";
            byte[]? expected = [0x01, 0x02, 0x03, 0x04];

            byte[]? actual = str.FromHexString();
            Assert.NotNull(actual);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void FromHexString_Invalid()
        {
            string str = "0102030G";
            byte[]? actual = str.FromHexString();
            Assert.Null(actual);
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
                .. Encoding.ASCII.GetBytes("TEST2"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO2"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA2"),
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

        #endregion
    }
}
