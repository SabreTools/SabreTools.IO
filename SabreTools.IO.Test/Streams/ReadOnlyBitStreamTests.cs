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
        public void ReadSingleBitTest()
        {
            byte[] data = [0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            byte? bit = stream.ReadBit();

            Assert.NotNull(bit);
            Assert.Equal((byte)0b00000001, bit);
            Assert.Equal(1, stream.Position);
        }

        [Theory]
        [InlineData(4, 0b00000101, 1)]
        [InlineData(9, 0b10101010_1, 2)]
        public void ReadBitsLETest(int bits, uint expected, int position)
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? actual = stream.ReadBitsLE(bits);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(position, stream.Position);
        }

        [Theory]
        [InlineData(4, 0b00001010, 1)]
        [InlineData(9, 0b10101010_1, 2)]
        public void ReadBitsBETest(int bits, uint expected, int position)
        {
            byte[] data = [0b01010101, 0b01010101, 0b01010101, 0b01010101];
            var stream = new ReadOnlyBitStream(new MemoryStream(data));
            uint? actual = stream.ReadBitsBE(bits);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(position, stream.Position);
        }
    }
}