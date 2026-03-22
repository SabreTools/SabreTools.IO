using System.Text;
using SabreTools.Matching;
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
