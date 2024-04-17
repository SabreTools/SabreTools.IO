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

            stream = new ReadOnlyBitStream(new MemoryStream(new byte[16]));
            Assert.Equal(16, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void ReadSingleBitTest()
        {
            byte[] data = [0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte? bit = stream.ReadBit();
            Assert.NotNull(bit);
            Assert.Equal((byte)0b00000001, bit);
            Assert.Equal(1, stream.Position);
        }

        [Fact]
        public void ReadBitsLSBTest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? bits = stream.ReadBitsLSB(4);
            Assert.NotNull(bits);
            Assert.Equal((byte)0b00000101, bits);
            Assert.Equal(1, stream.Position);
        }

        [Fact]
        public void ReadBitsMSBTest()
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? bits = stream.ReadBitsMSB(4);
            Assert.NotNull(bits);
            Assert.Equal((byte)0b00001010, bits);
            Assert.Equal(1, stream.Position);
        }
    }
}