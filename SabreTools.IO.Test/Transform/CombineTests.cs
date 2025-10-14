using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Transform;
using Xunit;

namespace SabreTools.IO.Test.Transform
{
    public class CombineTests
    {
        #region Concatenate

        [Fact]
        public void Concatenate_EmptyList_False()
        {
            List<string> paths = [];
            string output = string.Empty;
            bool actual = Combine.Concatenate(paths, output);
            Assert.False(actual);
        }

        [Fact]
        public void Concatenate_InvalidOutput_False()
        {
            List<string> paths = ["a"];
            string output = string.Empty;
            bool actual = Combine.Concatenate(paths, output);
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
            bool actual = Combine.Concatenate(paths, output);
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

            bool actual = Combine.Interleave(even, odd, output, BlockSize.Byte);
            Assert.False(actual);
        }

        [Fact]
        public void Interleave_OddNotExists_False()
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = "NOT A REAL PATH";
            string output = Guid.NewGuid().ToString();

            bool actual = Combine.Interleave(even, odd, output, BlockSize.Byte);
            Assert.False(actual);
        }

        [Fact]
        public void Interleave_InvalidType_False()
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Combine.Interleave(even, odd, output, (BlockSize)int.MaxValue);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(BlockSize.Byte, "TThhiiss  ddooeessnn''tt  mmaattcchh  aannyytthhiinngg")]
        [InlineData(BlockSize.Word, "ThThisis d doeoesnsn't't m matatchch a anynyththiningg")]
        [InlineData(BlockSize.Dword, "ThisThis doe doesn'tsn't mat match ach anythnythinging")]
        [InlineData(BlockSize.Qword, "This doeThis doesn't matsn't match anythch anythinging")]
        public void Interleave_SameLength_True(BlockSize type, string expected)
        {
            string even = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string odd = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Combine.Interleave(even, odd, output, type);
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

            bool actual = Combine.Interleave(even, odd, output, BlockSize.Byte);
            Assert.True(actual);

            string text = File.ReadAllText(output);
            Assert.Equal("TThhiiss  diose sjnu'stt  maa tfcihl ea ntyhtahti nhgas a known set of hashes to make sure that everything with hashing is still working as anticipated.", text);

            File.Delete(output);
        }

        #endregion
    }
}
