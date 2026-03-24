using System;
using System.IO;
using Xunit;

namespace SabreTools.IO.Test
{
    public class TransformTests
    {
        [Fact]
        public void SizeSplit_EmptyFileName_False()
        {
            string input = string.Empty;
            string outputDir = string.Empty;
            int size = 1;

            bool actual = Transform.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_InvalidFile_False()
        {
            string input = "INVALID";
            string outputDir = string.Empty;
            int size = 1;

            bool actual = Transform.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_InvalidSize_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = string.Empty;
            int size = 0;

            bool actual = Transform.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_Valid_True()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();
            int size = 16;

            bool actual = Transform.SizeSplit(input, outputDir, size);
            Assert.True(actual);

            Assert.Equal(2, Directory.GetFiles(outputDir).Length);

            string baseFilename = Path.GetFileName(input);
            string text = File.ReadAllText(Path.Combine(outputDir, $"{baseFilename}.0"));
            Assert.Equal("This doesn't mat", text);
            text = File.ReadAllText(Path.Combine(outputDir, $"{baseFilename}.1"));
            Assert.Equal("ch anything", text);

            File.Delete($"{baseFilename}.0");
            File.Delete($"{baseFilename}.1");
        }
    }
}
