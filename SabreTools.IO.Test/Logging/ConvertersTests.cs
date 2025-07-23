using SabreTools.IO.Logging;
using Xunit;

namespace SabreTools.IO.Test.Logging
{
    public class ConvertersTests
    {
        [Theory]
        [InlineData(null, LogLevel.VERBOSE)]
        [InlineData("", LogLevel.VERBOSE)]
        [InlineData("INVALID", LogLevel.VERBOSE)]
        [InlineData("verbose", LogLevel.VERBOSE)]
        [InlineData("VERBOSE", LogLevel.VERBOSE)]
        [InlineData("user", LogLevel.USER)]
        [InlineData("USER", LogLevel.USER)]
        [InlineData("warning", LogLevel.WARNING)]
        [InlineData("WARNING", LogLevel.WARNING)]
        [InlineData("error", LogLevel.ERROR)]
        [InlineData("ERROR", LogLevel.ERROR)]
        public void AsLogLevelTest(string? level, LogLevel expected)
        {
            LogLevel actual = level.AsLogLevel();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(LogLevel.VERBOSE, "VERBOSE")]
        [InlineData(LogLevel.USER, "USER")]
        [InlineData(LogLevel.WARNING, "WARNING")]
        [InlineData(LogLevel.ERROR, "ERROR")]
        [InlineData((LogLevel)99, null)]
        public void FromLogLevelTest(LogLevel level, string? expected)
        {
            string? actual = level.FromLogLevel();
            Assert.Equal(expected, actual);
        }
    }
}
