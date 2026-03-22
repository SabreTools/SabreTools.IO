using System.Text;
using SabreTools.Matching;
using Xunit;

namespace SabreTools.Numerics.Extensions.Test
{
    public class ByteArrayExtensionsTests
    {
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
