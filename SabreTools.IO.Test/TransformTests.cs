using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

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
    }
}
