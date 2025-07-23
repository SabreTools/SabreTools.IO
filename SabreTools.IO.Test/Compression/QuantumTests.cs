using System;
using System.IO;
using SabreTools.IO.Compression.Quantum;
using Xunit;

namespace SabreTools.IO.Test.Compression
{
    public class QuantumTests
    {
        // This test is disabled for the forseeable future. The current Quantum
        // processing code only handles standalone Quantum archives in theory.
        // There is additional work that needs to be done to support MS-CAB padding
        // before this test will pass properly.

        //[Fact]
        public void DecompressorTest()
        {
            // Testing Note: This is a fake file that has been taken
            // from the CFDATA block of a cabinet file.
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "test-archive.qtm");
            byte[] inputBytes = File.ReadAllBytes(path);

            var decompressor = Decompressor.Create(inputBytes, 19);
            byte[] output = decompressor.Process();

            Assert.Equal(38470, output.Length);
        }
    }
}