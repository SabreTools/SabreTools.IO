using System.IO;
using Xunit;

namespace SabreTools.IO.Test.Streams
{
    public class BufferedStreamTests
    {
        #region ReadNextByte

        [Fact]
        public void ReadNextByte_Empty_Null()
        {
            var source = new MemoryStream();
            var stream = new IO.Streams.BufferedStream(source);
            byte? actual = stream.ReadNextByte();
            Assert.Null(actual);
        }

        [Fact]
        public void ReadNextByte_Filled_ValidPosition_Byte()
        {
            var source = new MemoryStream(new byte[1024]);
            var stream = new IO.Streams.BufferedStream(source);
            byte? actual = stream.ReadNextByte();
            Assert.Equal((byte)0x00, actual);
        }

        [Fact]
        public void ReadNextByte_Filled_InvalidPosition_Null()
        {
            var source = new MemoryStream(new byte[1024]);
            source.Seek(0, SeekOrigin.End);
            var stream = new IO.Streams.BufferedStream(source);
            byte? actual = stream.ReadNextByte();
            Assert.Null(actual);
        }

        #endregion
    }
}