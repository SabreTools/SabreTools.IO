using System;
using System.IO;
using SabreTools.IO.Extensions;
using SabreTools.IO.Transform;
using Xunit;

namespace SabreTools.IO.Test.Transform
{
    public class SwapTests
    {
        #region Process

        [Fact]
        public void Process_EmptyFileName_False()
        {
            string input = string.Empty;
            string output = string.Empty;
            bool actual = Swap.Process(input, output, Operation.Byteswap);
            Assert.False(actual);
        }

        [Fact]
        public void Process_InvalidFile_False()
        {
            string input = "INVALID";
            string output = string.Empty;
            bool actual = Swap.Process(input, output, Operation.Byteswap);
            Assert.False(actual);
        }

        [Fact]
        public void Process_InvalidType_False()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            bool actual = Swap.Process(input, output, (Operation)int.MaxValue);
            Assert.False(actual);
        }

        [Fact]
        public void Process_Valid_True()
        {
            string input = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            string output = Guid.NewGuid().ToString();

            // Bitswap
            bool actual = Swap.Process(input, output, Operation.Bitswap);
            Assert.True(actual);
            byte[] actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x2A, 0x16, 0x96, 0xCE, 0x04, 0x26, 0xF6, 0xA6, 0xCE, 0x76, 0xE4, 0x2E, 0x04, 0xB6, 0x86, 0x2E, 0xC6, 0x16, 0x04, 0x86, 0x76, 0x9E, 0x2E, 0x16, 0x96, 0x76, 0xE6 }.EqualsExactly(actualBytes));

            // Byteswap
            actual = Swap.Process(input, output, Operation.Byteswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x68, 0x54, 0x73, 0x69, 0x64, 0x20, 0x65, 0x6F, 0x6E, 0x73, 0x74, 0x27, 0x6D, 0x20, 0x74, 0x61, 0x68, 0x63, 0x61, 0x20, 0x79, 0x6E, 0x68, 0x74, 0x6E, 0x69, 0x67 }.EqualsExactly(actualBytes));

            // Wordswap
            actual = Swap.Process(input, output, Operation.Wordswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x69, 0x73, 0x54, 0x68, 0x6F, 0x65, 0x20, 0x64, 0x27, 0x74, 0x73, 0x6E, 0x61, 0x74, 0x20, 0x6D, 0x20, 0x61, 0x63, 0x68, 0x74, 0x68, 0x6E, 0x79, 0x69, 0x6E, 0x67 }.EqualsExactly(actualBytes));

            // WordByteswap
            actual = Swap.Process(input, output, Operation.WordByteswap);
            Assert.True(actual);
            actualBytes = File.ReadAllBytes(output);
            Assert.True(new byte[] { 0x73, 0x69, 0x68, 0x54, 0x65, 0x6F, 0x64, 0x20, 0x74, 0x27, 0x6E, 0x73, 0x74, 0x61, 0x6D, 0x20, 0x61, 0x20, 0x68, 0x63, 0x68, 0x74, 0x79, 0x6E, 0x69, 0x6E, 0x67 }.EqualsExactly(actualBytes));

            File.Delete(output);
        }

        #endregion
    }
}
