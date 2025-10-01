using SabreTools.Text.Compare;
using Xunit;

namespace SabreTools.IO.Test.Compare
{
    public class NaturalComparerUtilTests
    {
        [Fact]
        public void CompareNumeric_BothNull_Equal()
        {
            int actual = NaturalComparerUtil.ComparePaths(null, null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CompareNumeric_SingleNull_Ordered()
        {
            int actual = NaturalComparerUtil.ComparePaths(null, "notnull");
            Assert.Equal(-1, actual);

            actual = NaturalComparerUtil.ComparePaths("notnull", null);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void CompareNumeric_BothEqual_Equal()
        {
            int actual = NaturalComparerUtil.ComparePaths("notnull", "notnull");
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CompareNumeric_BothEqualWithPath_Equal()
        {
            int actual = NaturalComparerUtil.ComparePaths("notnull/file.ext", "notnull/file.ext");
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CompareNumeric_BothEqualWithAltPath_Equal()
        {
            int actual = NaturalComparerUtil.ComparePaths("notnull/file.ext", "notnull\\file.ext");
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CompareNumeric_NumericNonDecimalString_Ordered()
        {
            int actual = NaturalComparerUtil.ComparePaths("100", "10");
            Assert.Equal(1, actual);

            actual = NaturalComparerUtil.ComparePaths("10", "100");
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void CompareNumeric_NumericDecimalString_Ordered()
        {
            int actual = NaturalComparerUtil.ComparePaths("100.100", "100.10");
            Assert.Equal(1, actual);

            actual = NaturalComparerUtil.ComparePaths("100.10", "100.100");
            Assert.Equal(-1, actual);
        }
    }
}
