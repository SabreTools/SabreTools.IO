using System;
using System.IO;
using SabreTools.IO.Transform;
using Xunit;

namespace SabreTools.IO.Test.Transform
{
    public class SplitTests
    {
        #region BlockSplit

        [Fact]
        public void BlockSplit_EmptyFileName_False()
        {
            string input = string.Empty;
            string outputDir = string.Empty;
            bool actual = Split.BlockSplit(input, outputDir, BlockSize.Byte);
            Assert.False(actual);
        }

        [Fact]
        public void BlockSplit_InvalidFile_False()
        {
            string input = "INVALID";
            string outputDir = string.Empty;
            bool actual = Split.BlockSplit(input, outputDir, BlockSize.Byte);
            Assert.False(actual);
        }

        [Fact]
        public void BlockSplit_InvalidType_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();

            bool actual = Split.BlockSplit(input, outputDir, (BlockSize)int.MaxValue);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(BlockSize.Byte, "Ti os' ac ntig", "hsdentmthayhn")]
        [InlineData(BlockSize.Word, "Th dsn mchnyin", "isoe'tat athg")]
        [InlineData(BlockSize.Dword, "Thissn'tch aing", " doe matnyth")]
        [InlineData(BlockSize.Qword, "This doech anyth", "sn't mating")]
        public void BlockSplit_ValidFile_True(BlockSize type, string expectedEven, string expectedOdd)
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();

            bool actual = Split.BlockSplit(input, outputDir, type);
            Assert.True(actual);

            string baseFilename = Path.GetFileName(input);
            string text = File.ReadAllText(Path.Combine(outputDir, $"{baseFilename}.even"));
            Assert.Equal(expectedEven, text);
            text = File.ReadAllText(Path.Combine(outputDir, $"{baseFilename}.odd"));
            Assert.Equal(expectedOdd, text);

            File.Delete($"{baseFilename}.even");
            File.Delete($"{baseFilename}.odd");
        }

        #endregion

        #region SizeSplit

        [Fact]
        public void SizeSplit_EmptyFileName_False()
        {
            string input = string.Empty;
            string outputDir = string.Empty;
            int size = 1;

            bool actual = Split.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_InvalidFile_False()
        {
            string input = "INVALID";
            string outputDir = string.Empty;
            int size = 1;

            bool actual = Split.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_InvalidSize_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = string.Empty;
            int size = 0;

            bool actual = Split.SizeSplit(input, outputDir, size);
            Assert.False(actual);
        }

        [Fact]
        public void SizeSplit_Valid_True()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();
            int size = 16;

            bool actual = Split.SizeSplit(input, outputDir, size);
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

        #endregion
    }
}
