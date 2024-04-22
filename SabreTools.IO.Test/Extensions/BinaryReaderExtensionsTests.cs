using System;
using System.IO;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class BinaryReaderExtensionsTests
    {
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void ReadInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32BigEndian();
            Assert.Equal(0x00010203, read);
        }

        [Fact]
        public void ReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = br.ReadSingleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
        }

        [Fact]
        public void ReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = br.ReadDoubleBigEndian();
            Assert.Equal(expected, read);
        }

        // TODO: Add byte[], char[] tests
        // TODO: Add string reading tests
    }
}