using System;
using System.IO;
using System.Text;
using SabreTools.IO.Compression.BZip2;
using Xunit;

namespace SabreTools.IO.Test.Compression
{
    public class BZip2Tests
    {
        [Fact]
        public void BZip2InputStreamTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "test-archive.bz2");
            Stream input = File.OpenRead(path);
            byte[] output = new byte[1024];

            var bzip = new BZip2InputStream(input);
            int actual = bzip.Read(output, 0, output.Length);
            bzip.Close();

            Assert.Equal(125, actual);
            string str = Encoding.UTF8.GetString(output, 0, 125);
            Assert.Equal("This is just a file that has a known set of hashes to make sure that everything with hashing is still working as anticipated.", str);
        }

        [Fact]
        public void BZip2OutputStreamTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-compress.bin");
            byte[] input = File.ReadAllBytes(path);
            var output = new MemoryStream();

            var bzip = new BZip2OutputStream(output, leaveOpen: true);
            bzip.Write(input, 0, input.Length);
            bzip.Close();

            Assert.Equal(122, output.Length);
        }
    }
}
