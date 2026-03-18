using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Extensions;
using Xunit;

#pragma warning disable IDE0230 // Use UTF-8 string literal
namespace SabreTools.IO.Test
{
    public class TransformTests
    {
        #region Concatenate

        [Fact]
        public void Concatenate_EmptyList_False()
        {
            List<string> paths = [];
            string output = string.Empty;
            bool actual = Transform.Concatenate(paths, output);
            Assert.False(actual);
        }

        [Fact]
        public void Concatenate_InvalidOutput_False()
        {
            List<string> paths = ["a"];
            string output = string.Empty;
            bool actual = Transform.Concatenate(paths, output);
            Assert.False(actual);
        }

        [Fact]
        public void Concatenate_FilledList_True()
        {
            List<string> paths = [
                Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-compress.bin"),
            ];
            string output = Guid.NewGuid().ToString();
            bool actual = Transform.Concatenate(paths, output);
            Assert.True(actual);

            string text = File.ReadAllText(output);
            Assert.Equal("This doesn't match anythingThis is just a file that has a known set of hashes to make sure that everything with hashing is still working as anticipated.", text);

            File.Delete(output);
        }

        #endregion

        #region Interleave

        [Fact]
        public void Interleave_EvenNotExists_False()
        {
            string even = "NOT A REAL PATH";
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Interleave(even, odd, output, 1);
            Assert.False(actual);
        }

        [Fact]
        public void Interleave_OddNotExists_False()
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = "NOT A REAL PATH";
            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Interleave(even, odd, output, 1);
            Assert.False(actual);
        }

        [Fact]
        public void Interleave_InvalidValue_False()
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Interleave(even, odd, output, -1);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, "TThhiiss  ddooeessnn''tt  mmaattcchh  aannyytthhiinngg")]
        [InlineData(2, "ThThisis d doeoesnsn't't m matatchch a anynyththiningg")]
        [InlineData(4, "ThisThis doe doesn'tsn't mat match ach anythnythinging")]
        [InlineData(8, "This doeThis doesn't matsn't match anythch anythinging")]
        public void Interleave_SameLength_True(int blockSize, string expected)
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Interleave(even, odd, output, blockSize);
            Assert.True(actual);

            string text = File.ReadAllText(output);
            Assert.Equal(expected, text);

            File.Delete(output);
        }

        [Fact]
        public void Interleave_DifferentLength_True()
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-compress.bin");

            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Interleave(even, odd, output, 1);
            Assert.True(actual);

            string text = File.ReadAllText(output);
            Assert.Equal("TThhiiss  diose sjnu'stt  maa tfcihl ea ntyhtahti nhgas a known set of hashes to make sure that everything with hashing is still working as anticipated.", text);

            File.Delete(output);
        }

        #endregion

        #region BlockSplit

        [Fact]
        public void BlockSplit_EmptyFileName_False()
        {
            string input = string.Empty;
            string outputDir = string.Empty;
            bool actual = Transform.BlockSplit(input, outputDir, 1);
            Assert.False(actual);
        }

        [Fact]
        public void BlockSplit_InvalidFile_False()
        {
            string input = "INVALID";
            string outputDir = string.Empty;
            bool actual = Transform.BlockSplit(input, outputDir, 1);
            Assert.False(actual);
        }

        [Fact]
        public void BlockSplit_InvalidValue_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();

            bool actual = Transform.BlockSplit(input, outputDir, -1);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, "Ti os' ac ntig", "hsdentmthayhn")]
        [InlineData(2, "Th dsn mchnyin", "isoe'tat athg")]
        [InlineData(4, "Thissn'tch aing", " doe matnyth")]
        [InlineData(8, "This doech anyth", "sn't mating")]
        public void BlockSplit_ValidFile_True(int blockSize, string expectedEven, string expectedOdd)
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string outputDir = Guid.NewGuid().ToString();

            bool actual = Transform.BlockSplit(input, outputDir, blockSize);
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

        #endregion

        #region Swap

        [Fact]
        public void Swap_EmptyFileName_False()
        {
            string input = string.Empty;
            string output = string.Empty;
            bool actual = Transform.Swap(input, output, SwapOperation.Byteswap);
            Assert.False(actual);
        }

        [Fact]
        public void Swap_InvalidFile_False()
        {
            string input = "INVALID";
            string output = string.Empty;
            bool actual = Transform.Swap(input, output, SwapOperation.Byteswap);
            Assert.False(actual);
        }

        [Fact]
        public void Swap_InvalidType_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Transform.Swap(input, output, (SwapOperation)int.MaxValue);
            Assert.False(actual);
        }

        [Fact]
        public void Swap_Valid_True()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            // Bitswap
            bool actual = Transform.Swap(input, output, SwapOperation.Bitswap);
            Assert.True(actual);
            byte[] actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x2A, 0x16, 0x96, 0xCE, 0x04, 0x26, 0xF6, 0xA6, 0xCE, 0x76, 0xE4, 0x2E, 0x04, 0xB6, 0x86, 0x2E, 0xC6, 0x16, 0x04, 0x86, 0x76, 0x9E, 0x2E, 0x16, 0x96, 0x76, 0xE6 }.EqualsExactly(actualBytes));

            // Byteswap
            actual = Transform.Swap(input, output, SwapOperation.Byteswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x68, 0x54, 0x73, 0x69, 0x64, 0x20, 0x65, 0x6F, 0x6E, 0x73, 0x74, 0x27, 0x6D, 0x20, 0x74, 0x61, 0x68, 0x63, 0x61, 0x20, 0x79, 0x6E, 0x68, 0x74, 0x6E, 0x69, 0x67 }.EqualsExactly(actualBytes));

            // Wordswap
            actual = Transform.Swap(input, output, SwapOperation.Wordswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x69, 0x73, 0x54, 0x68, 0x6F, 0x65, 0x20, 0x64, 0x27, 0x74, 0x73, 0x6E, 0x61, 0x74, 0x20, 0x6D, 0x20, 0x61, 0x63, 0x68, 0x74, 0x68, 0x6E, 0x79, 0x69, 0x6E, 0x67 }.EqualsExactly(actualBytes));

            // WordByteswap
            actual = Transform.Swap(input, output, SwapOperation.WordByteswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x73, 0x69, 0x68, 0x54, 0x65, 0x6F, 0x64, 0x20, 0x74, 0x27, 0x6E, 0x73, 0x74, 0x61, 0x6D, 0x20, 0x61, 0x20, 0x68, 0x63, 0x68, 0x74, 0x79, 0x6E, 0x69, 0x6E, 0x67 }.EqualsExactly(actualBytes));

            File.Delete(output);
        }

        #endregion
    }
}
