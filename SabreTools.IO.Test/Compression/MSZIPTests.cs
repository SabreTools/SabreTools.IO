using System;
using System.IO;
using SabreTools.IO.Compression.MSZIP;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Compression
{
    public class MSZIPTests
    {
        [Fact]
        public void DecompressorTest()
        {
            // Testing Note: This is a fake file that has multiple blocks
            // sequentially. In real cabinet files, these are embedded in
            // CFDATA blocks.
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "test-archive.msz");
            byte[] inputBytes = File.ReadAllBytes(path);
            var input = new MemoryStream(inputBytes);
            var output = new MemoryStream();

            var decompressor = Decompressor.Create();
            input.SeekIfPossible(0x0000);
            decompressor.CopyTo(input, output);
            input.SeekIfPossible(0x3969);
            decompressor.CopyTo(input, output);

            Assert.Equal(65536, output.Length);
        }
    }
}
