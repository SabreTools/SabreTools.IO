using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class StringExtensionsTests
    {
        #region OptionalContains

        [Theory]
        [InlineData(null, "ANY", false)]
        [InlineData("", "ANY", false)]
        [InlineData("ANY", "ANY", true)]
        [InlineData("ANYTHING", "ANY", true)]
        [InlineData("THING", "ANY", false)]
        [InlineData("THINGANY", "ANY", true)]
        public void OptionalContainsTest(string? haystack, string needle, bool expected)
        {
            bool actual = haystack.OptionalContains(needle);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region OptionalEndsWith

        [Theory]
        [InlineData(null, "ANY", false)]
        [InlineData("", "ANY", false)]
        [InlineData("ANY", "ANY", true)]
        [InlineData("ANYTHING", "ANY", false)]
        [InlineData("THING", "ANY", false)]
        [InlineData("THINGANY", "ANY", true)]
        public void OptionalEndsWithTest(string? haystack, string needle, bool expected)
        {
            bool actual = haystack.OptionalEndsWith(needle);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region OptionalEquals

        [Theory]
        [InlineData(null, "ANY", false)]
        [InlineData("", "ANY", false)]
        [InlineData("ANY", "ANY", true)]
        [InlineData("ANYTHING", "ANY", false)]
        [InlineData("THING", "ANY", false)]
        [InlineData("THINGANY", "ANY", false)]
        public void OptionalEqualsTest(string? haystack, string needle, bool expected)
        {
            bool actual = haystack.OptionalEquals(needle);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region OptionalStartsWith

        [Theory]
        [InlineData(null, "ANY", false)]
        [InlineData("", "ANY", false)]
        [InlineData("ANY", "ANY", true)]
        [InlineData("ANYTHING", "ANY", true)]
        [InlineData("THING", "ANY", false)]
        [InlineData("THINGANY", "ANY", false)]
        public void OptionalStartsWithTest(string? haystack, string needle, bool expected)
        {
            bool actual = haystack.OptionalStartsWith(needle);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
