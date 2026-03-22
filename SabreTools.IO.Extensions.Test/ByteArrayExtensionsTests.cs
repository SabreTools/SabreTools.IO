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
    }
}
