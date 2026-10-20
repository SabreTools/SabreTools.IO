using System;
using System.IO;
using SabreTools.IO.Compression.MSZIP;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Compression.Test
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

        [Fact]
        public void DecompressorShortDistanceCodeAtEndTest()
        {
            // One dynamic deflate block with 778 bytes of 'A'. The last match has a
            // 1-bit distance code, followed by a 2-bit end of block code and 1 bit
            // of padding, fewer than the 6 bits of the longest distance code
            byte[] block =
            [
                0x43, 0x4B, 0xED, 0xC6, 0x81, 0x01, 0xC0, 0x30, 0x0C, 0xC3, 0xA0,
                0xDB, 0xF4, 0xFF, 0x53, 0xD2, 0x21, 0x42, 0x4E, 0x37, 0xB6, 0x45,
            ];

            var output = new MemoryStream();
            var decompressor = Decompressor.Create();
            decompressor.CopyTo(block, output);

            byte[] data = output.ToArray();
            for (int i = 0; i < 778; i++)
            {
                Assert.Equal(0x41, data[i]);
            }
        }

        [Fact]
        public void DecompressorShortLiteralCodeAtEndTest()
        {
            // One dynamic deflate block with 518 bytes of 'A'. The last literal has a
            // 1-bit code, followed by a 2-bit end of block code and 5 bits of padding,
            // fewer than the 9 bits of the longest literal/length code
            byte[] block =
            [
                0x43, 0x4B, 0xED, 0xC1, 0x01, 0x82, 0xE4, 0x30, 0x0C, 0xC3, 0xB0, 0xB7,
                0xE1, 0xFF, 0x9F, 0xA2, 0x9C, 0xCE, 0xDE, 0x2B, 0x0E, 0xF0, 0xDF, 0x7F,
                0x02,
            ];

            var output = new MemoryStream();
            var decompressor = Decompressor.Create();
            decompressor.CopyTo(block, output);

            byte[] data = output.ToArray();
            for (int i = 0; i < 518; i++)
            {
                Assert.Equal(0x41, data[i]);
            }
        }
    }
}
