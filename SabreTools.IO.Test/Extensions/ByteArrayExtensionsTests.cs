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

        #region IsNumericArray

        [Fact]
        public void IsNumericArray_Empty_False()
        {
            byte[] arr = [];
            bool actual = arr.IsNumericArray();
            Assert.False(actual);
        }

        [Fact]
        public void IsNumericArray_NonNumeric_False()
        {
            byte[] arr = Encoding.ASCII.GetBytes("ABCDEF");
            bool actual = arr.IsNumericArray();
            Assert.False(actual);
        }

        [Fact]
        public void IsNumericArray_MixedNumeric_False()
        {
            byte[] arr = Encoding.ASCII.GetBytes("ABC123");
            bool actual = arr.IsNumericArray();
            Assert.False(actual);
        }

        [Fact]
        public void IsNumericArray_Numeric_True()
        {
            byte[] arr = Encoding.ASCII.GetBytes("0123456789");
            bool actual = arr.IsNumericArray();
            Assert.True(actual);
        }

        #endregion

        #region FindAllPositions

        [Fact]
        public void FindAllPositions_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            var positions = stack.FindAllPositions([0x01]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions(Array.Empty<byte>());
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02], start: -1);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], start: 2);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02], end: -2);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], end: 0);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], end: 2);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            int position = Assert.Single(positions);
            Assert.Equal(0, position);
        }

        [Fact]
        public void FindAllPositions_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            var positions = stack.FindAllPositions([0x01]);
            Assert.Equal(2, positions.Count);
        }

        #endregion

        #region FirstPosition

        [Fact]
        public void FirstPosition_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            int position = stack.FirstPosition([0x01]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition(Array.Empty<byte>());
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02], start: -1);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], start: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02], end: -2);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], end: 0);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], end: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(0, position);
        }

        [Fact]
        public void FirstPosition_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            int position = stack.FirstPosition([0x01]);
            Assert.Equal(0, position);
        }

        #endregion

        #region LastPosition

        [Fact]
        public void LastPosition_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            int position = stack.LastPosition([0x01]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition(Array.Empty<byte>());
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02], start: -1);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], start: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02], end: -2);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], end: 0);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], end: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(0, position);
        }

        [Fact]
        public void LastPosition_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            int position = stack.LastPosition([0x01]);
            Assert.Equal(1, position);
        }

        #endregion

        #region EqualsExactly

        [Fact]
        public void EqualsExactly_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.EqualsExactly([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EqualsExactly(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_ShorterNeedle_NoMatches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EqualsExactly([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void EqualsExactly_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.False(found);
        }

        #endregion

        #region StartsWith

        [Fact]
        public void StartsWith_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.StartsWith([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void StartsWith_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            bool found = stack.StartsWith([0x01]);
            Assert.True(found);
        }

        #endregion

        #region EndsWith

        [Fact]
        public void EndsWith_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.EndsWith([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EndsWith(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EndsWith([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void EndsWith_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.EndsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            bool found = stack.EndsWith([0x01]);
            Assert.True(found);
        }

        #endregion

        #region Add

        [Theory]
        [InlineData(new byte[0], 0, new byte[0])]
        [InlineData(new byte[0], 1234, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0xD2 }, 0, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0xD2 }, 1234, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0xA4 })]
        public void Add_NumericInput(byte[] self, uint add, byte[] expected)
        {
            byte[] actual = self.Add(add);

            Assert.Equal(expected.Length, actual.Length);
            if (actual.Length > 0)
                Assert.True(actual.EqualsExactly(expected));
        }

        [Theory]
        [InlineData(new byte[0], new byte[0], new byte[0])]
        [InlineData(new byte[0], new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[0], new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0x00, 0x00 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x00, 0x00 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0x09, 0xA4 })]
        [InlineData(new byte[] { 0xAB, 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0xAB, 0x09, 0xA4 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0xAB, 0x04, 0xD2 }, new byte[] { 0xAB, 0x09, 0xA4 })]
        public void Add_ArrayInput(byte[] self, byte[] add, byte[] expected)
        {
            byte[] actual = self.Add(add);

            Assert.Equal(expected.Length, actual.Length);
            if (actual.Length > 0)
                Assert.True(actual.EqualsExactly(expected));
        }

        #endregion

        #region RotateLeft

        [Theory]
        [InlineData(new byte[0], 0, new byte[0])]
        [InlineData(new byte[] { 0x01 }, 0, new byte[] { 0x01 })]
        [InlineData(new byte[] { 0x01 }, 1, new byte[] { 0x02 })]
        [InlineData(new byte[] { 0x80 }, 1, new byte[] { 0x01 })]
        [InlineData(new byte[] { 0x00, 0x01 }, 0, new byte[] { 0x00, 0x01 })]
        [InlineData(new byte[] { 0x00, 0x01 }, 1, new byte[] { 0x00, 0x02 })]
        [InlineData(new byte[] { 0x00, 0x80 }, 1, new byte[] { 0x01, 0x00 })]
        [InlineData(new byte[] { 0x80, 0x00 }, 1, new byte[] { 0x00, 0x01 })]
        public void RotateLeftTest(byte[] self, int numBits, byte[] expected)
        {
            byte[] actual = self.RotateLeft(numBits);

            Assert.Equal(expected.Length, actual.Length);
            if (actual.Length > 0)
                Assert.True(actual.EqualsExactly(expected));
        }

        #endregion

        #region Xor

        [Theory]
        [InlineData(new byte[0], new byte[0], new byte[0])]
        [InlineData(new byte[0], new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[0], new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0x00, 0x00 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x00, 0x00 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0x00, 0x00 })]
        [InlineData(new byte[] { 0xAB, 0x04, 0xD2 }, new byte[] { 0x04, 0xD2 }, new byte[] { 0xAB, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x04, 0xD2 }, new byte[] { 0xAB, 0x04, 0xD2 }, new byte[] { 0xAB, 0x00, 0x00 })]
        public void XorTest(byte[] self, byte[] add, byte[] expected)
        {
            byte[] actual = self.Xor(add);

            Assert.Equal(expected.Length, actual.Length);
            if (actual.Length > 0)
                Assert.True(actual.EqualsExactly(expected));
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
