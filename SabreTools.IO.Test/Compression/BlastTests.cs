using System;
using System.IO;
using System.Text;
using SabreTools.IO.Compression.Blast;
using Xunit;

namespace SabreTools.IO.Test.Compression
{
    public class BlastTests
    {
        [Fact]
        public void DecompressorTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "test-archive.pk");
            byte[] input = File.ReadAllBytes(path);
            MemoryStream output = new MemoryStream();

            var decompressor = Decompressor.Create();
            decompressor.CopyTo(input, output);

            Assert.Equal(13, output.Length);
            byte[] bytes = output.ToArray();
            string str = Encoding.ASCII.GetString(bytes);
            Assert.Equal("AIAIAIAIAIAIA", str);
        }
    }
}