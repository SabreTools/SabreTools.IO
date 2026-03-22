using System;
using System.Text;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class ByteArrayExtensionsTests
    {
        #region AlignToBoundary

        [Fact]
        public void AlignToBoundary_Null_False()
        {
            byte[]? arr = null;
            int offset = 0;
            byte alignment = 4;

            bool actual = arr.AlignToBoundary(ref offset, alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_Empty_False()
        {
            byte[]? arr = [];
            int offset = 0;
            byte alignment = 4;

            bool actual = arr.AlignToBoundary(ref offset, alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_EOF_False()
        {
            byte[]? arr = [0x01, 0x02];
            int offset = 1;
            byte alignment = 4;

            bool actual = arr.AlignToBoundary(ref offset, alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_TooShort_False()
        {
            byte[]? arr = [0x01, 0x02];
            int offset = 1;
            byte alignment = 4;

            bool actual = arr.AlignToBoundary(ref offset, alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_CanAlign_True()
        {
            byte[]? arr = [0x01, 0x02, 0x03, 0x04, 0x05];
            int offset = 1;
            byte alignment = 4;

            bool actual = arr.AlignToBoundary(ref offset, alignment);
            Assert.True(actual);
        }

        #endregion

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
    }
}
