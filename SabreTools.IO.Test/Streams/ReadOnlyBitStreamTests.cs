using System.IO;
using SabreTools.IO.Streams;
using Xunit;

namespace SabreTools.IO.Test.Streams
{
    public class ReadOnlyBitStreamTests
    {
        [Fact]
        public void DefaultConstructorTest()
        {
            var stream = new ReadOnlyBitStream(new MemoryStream());
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);

            stream = new ReadOnlyBitStream(new MemoryStream(new byte[16], 0, 16, true, true));
            Assert.Equal(16, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void ReadSingleBitLETest()
        {
            byte[] data = [0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte? bit = stream.ReadBitLE();
            Assert.NotNull(bit);
            Assert.Equal((byte)0b00000000, bit);
            Assert.Equal(1, stream.Position);
        }

        [Fact]
        public void ReadSingleBitBETest()
        {
            byte[] data = [0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte? bit = stream.ReadBitBE();
            Assert.NotNull(bit);
            Assert.Equal((byte)0b00000001, bit);
            Assert.Equal(1, stream.Position);
        }

        [Fact]
        public void ReadBitsLETest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? bits = stream.ReadBitsLE(4);
            Assert.NotNull(bits);
            Assert.Equal((byte)0b00001010, bits); // Transcribed to big-endian
            Assert.Equal(1, stream.Position);
        }

        [Fact]
        public void ReadBitsBETest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? bits = stream.ReadBitsBE(4);
            Assert.NotNull(bits);
            Assert.Equal((byte)0b00001010, bits);
            Assert.Equal(1, stream.Position);
        }
    }
}