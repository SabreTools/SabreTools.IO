using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test.Matching
{
    /// <remarks>
    /// All other test cases are covered by <see cref="PathMatchTests"/>
    /// </remarks>
    public class FilePathMatchTests
    {
        [Fact]
        public void ConstructorFormatsNeedle()
        {
            string needle = "test";
            string expected = $"{Path.DirectorySeparatorChar}{needle}";

            var fpm = new FilePathMatch(needle);
            Assert.Equal(expected, fpm.Needle);
        }
    }
}
