using System.IO;
using System.Linq;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class ReadOnlyBitStreamExtensionsTests
    {
        [Fact]
        public void ReadByteTest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            byte expected = 0b01010101;

            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte? actual = stream.ReadByte();
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadUInt16Test()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            ushort expected = 0b0101010101010101;

            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            ushort? actual = stream.ReadUInt16();
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadUInt32Test()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            uint expected = 0b01010101010101010101010101010101;

            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? actual = stream.ReadUInt32();
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadUInt64Test()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];

            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            ulong? actual = stream.ReadUInt64();
            Assert.Null(actual);
        }

        [Fact]
        public void ReadBytesTest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];

            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte[]? actual = stream.ReadBytes(4);
            Assert.NotNull(actual);
            Assert.True(data.SequenceEqual(actual));
        }
    }
}
