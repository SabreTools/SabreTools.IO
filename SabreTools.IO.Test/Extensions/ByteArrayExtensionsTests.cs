using System;
using System.Linq;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class ByteArrayExtensionsTests
    {
        #region Is Null or Empty

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

        #region To Hex String

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

        #region From Hex String

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
    }
}
