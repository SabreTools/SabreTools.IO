using System;
using System.IO;
using SabreTools.IO.Streams;
using Xunit;

namespace SabreTools.IO.Test.Streams
{
    public class ViewStreamTests
    {
        #region Constructor

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1024, 0, 1024, 1024)]
        [InlineData(1024, 256, 512, 512)]
        public void Constructor_Array(int size, long offset, long length, long expectedLength)
        {
            byte[] data = new byte[size];
            var stream = new ViewStream(data, offset, length);
            Assert.Equal(expectedLength, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Theory]
        [InlineData(0, -1, 0)]
        [InlineData(0, 2048, 0)]
        [InlineData(1024, -1, 1024)]
        [InlineData(1024, 2048, 1024)]
        [InlineData(1024, -1, 512)]
        [InlineData(1024, 2048, 512)]
        public void Constructor_Array_InvalidOffset(int size, long offset, long length)
        {
            byte[] data = new byte[size];
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ViewStream(data, offset, length));
        }

        [Theory]
        [InlineData(0, 0, -1)]
        [InlineData(0, 0, 2048)]
        [InlineData(1024, 0, -1)]
        [InlineData(1024, 0, 2048)]
        [InlineData(1024, 256, -1)]
        [InlineData(1024, 256, 2048)]
        public void Constructor_Array_InvalidLength(int size, long offset, long length)
        {
            byte[] data = new byte[size];
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ViewStream(data, offset, length));
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1024, 0, 1024, 1024)]
        [InlineData(1024, 256, 512, 512)]
        public void Constructor_Stream(int size, long offset, long length, long expectedLength)
        {
            Stream data = new MemoryStream(new byte[size]);
            var stream = new ViewStream(data, offset, length);
            Assert.Equal(expectedLength, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Theory]
        [InlineData(0, -1, 0)]
        [InlineData(0, 2048, 0)]
        [InlineData(1024, -1, 1024)]
        [InlineData(1024, 2048, 1024)]
        [InlineData(1024, -1, 512)]
        [InlineData(1024, 2048, 512)]
        public void Constructor_Stream_InvalidOffset(int size, long offset, long length)
        {
            Stream data = new MemoryStream(new byte[size]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ViewStream(data, offset, length));
        }

        [Theory]
        [InlineData(0, 0, -1)]
        [InlineData(0, 0, 2048)]
        [InlineData(1024, 0, -1)]
        [InlineData(1024, 0, 2048)]
        [InlineData(1024, 256, -1)]
        [InlineData(1024, 256, 2048)]
        public void Constructor_Stream_InvalidLength(int size, long offset, long length)
        {
            Stream data = new MemoryStream(new byte[size]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ViewStream(data, offset, length));
        }

        #endregion

        #region Position

        [Theory]
        [InlineData(0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 256, 0)]
        [InlineData(0, 0, 0, 2048, 0)]
        [InlineData(1024, 0, 1024, -1, 0)]
        [InlineData(1024, 0, 1024, 0, 0)]
        [InlineData(1024, 0, 1024, 256, 256)]
        [InlineData(1024, 0, 1024, 2048, 1023)]
        [InlineData(1024, 256, 512, -1, 0)]
        [InlineData(1024, 256, 512, 0, 0)]
        [InlineData(1024, 256, 512, 256, 256)]
        [InlineData(1024, 256, 512, 2048, 511)]
        public void Position_Array(int size, long offset, long length, long position, long expectedPosition)
        {
            byte[] data = new byte[size];
            var stream = new ViewStream(data, offset, length);
            stream.Position = position;
            Assert.Equal(expectedPosition, stream.Position);
        }

        [Theory]
        [InlineData(0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 256, 0)]
        [InlineData(0, 0, 0, 2048, 0)]
        [InlineData(1024, 0, 1024, -1, 0)]
        [InlineData(1024, 0, 1024, 0, 0)]
        [InlineData(1024, 0, 1024, 256, 256)]
        [InlineData(1024, 0, 1024, 2048, 1023)]
        [InlineData(1024, 256, 512, -1, 0)]
        [InlineData(1024, 256, 512, 0, 0)]
        [InlineData(1024, 256, 512, 256, 256)]
        [InlineData(1024, 256, 512, 2048, 511)]
        public void Position_Stream(int size, long offset, long length, long position, long expectedPosition)
        {
            Stream data = new MemoryStream(new byte[size]);
            var stream = new ViewStream(data, offset, length);
            stream.Position = position;
            Assert.Equal(expectedPosition, stream.Position);
        }

        #endregion

        #region SegmentValid

        [Theory]
        [InlineData(0, 0, 0, -1, 0, false)]
        [InlineData(0, 0, 0, 2048, 0, false)]
        [InlineData(0, 0, 0, 0, 0, true)]
        [InlineData(0, 0, 0, 0, -1, false)]
        [InlineData(0, 0, 0, 0, 2048, false)]
        [InlineData(1024, 0, 1024, -1, 0, false)]
        [InlineData(1024, 0, 1024, 2048, 0, false)]
        [InlineData(1024, 0, 1024, 0, 0, true)]
        [InlineData(1024, 0, 1024, 256, 0, true)]
        [InlineData(1024, 0, 1024, 256, 256, true)]
        [InlineData(1024, 0, 1024, 0, -1, false)]
        [InlineData(1024, 0, 1024, 0, 2048, false)]
        [InlineData(1024, 256, 512, -1, 0, false)]
        [InlineData(1024, 256, 512, 2048, 0, false)]
        [InlineData(1024, 256, 512, 0, 0, true)]
        [InlineData(1024, 256, 512, 256, 0, true)]
        [InlineData(1024, 256, 512, 256, 256, true)]
        [InlineData(1024, 256, 512, 0, -1, false)]
        [InlineData(1024, 256, 512, 0, 2048, false)]
        public void SegmentValid_Array(int size, long offset, long length, int segmentStart, int segmentLength, bool expected)
        {
            byte[] data = new byte[size];
            var stream = new ViewStream(data, offset, length);
            bool actual = stream.SegmentValid(segmentStart, segmentLength);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, 0, -1, 0, false)]
        [InlineData(0, 0, 0, 2048, 0, false)]
        [InlineData(0, 0, 0, 0, 0, true)]
        [InlineData(0, 0, 0, 0, -1, false)]
        [InlineData(0, 0, 0, 0, 2048, false)]
        [InlineData(1024, 0, 1024, -1, 0, false)]
        [InlineData(1024, 0, 1024, 2048, 0, false)]
        [InlineData(1024, 0, 1024, 0, 0, true)]
        [InlineData(1024, 0, 1024, 256, 0, true)]
        [InlineData(1024, 0, 1024, 256, 256, true)]
        [InlineData(1024, 0, 1024, 0, -1, false)]
        [InlineData(1024, 0, 1024, 0, 2048, false)]
        [InlineData(1024, 256, 512, -1, 0, false)]
        [InlineData(1024, 256, 512, 2048, 0, false)]
        [InlineData(1024, 256, 512, 0, 0, true)]
        [InlineData(1024, 256, 512, 256, 0, true)]
        [InlineData(1024, 256, 512, 256, 256, true)]
        [InlineData(1024, 256, 512, 0, -1, false)]
        [InlineData(1024, 256, 512, 0, 2048, false)]
        public void SegmentValid_Stream(int size, long offset, long length, int segmentStart, int segmentLength, bool expected)
        {
            Stream data = new MemoryStream(new byte[size]);
            var stream = new ViewStream(data, offset, length);
            bool actual = stream.SegmentValid(segmentStart, segmentLength);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Read

        [Theory]
        [InlineData(0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 2048, 0)]
        [InlineData(1024, 0, 1024, -1, 0)]
        [InlineData(1024, 0, 1024, 0, 0)]
        [InlineData(1024, 0, 1024, 256, 256)]
        [InlineData(1024, 0, 1024, 1024, 1024)]
        [InlineData(1024, 0, 1024, 2048, 0)]
        [InlineData(1024, 256, 512, -1, 0)]
        [InlineData(1024, 256, 512, 0, 0)]
        [InlineData(1024, 256, 512, 256, 256)]
        [InlineData(1024, 256, 512, 512, 512)]
        [InlineData(1024, 256, 512, 2048, 0)]
        public void Read_Array(int size, long offset, long length, int count, int expectedRead)
        {
            byte[] data = new byte[size];
            var stream = new ViewStream(data, offset, length);

            byte[] buffer = new byte[1024];
            int actual = stream.Read(buffer, 0, count);
            Assert.Equal(expectedRead, actual);
        }

        [Theory]
        [InlineData(0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 2048, 0)]
        [InlineData(1024, 0, 1024, -1, 0)]
        [InlineData(1024, 0, 1024, 0, 0)]
        [InlineData(1024, 0, 1024, 256, 256)]
        [InlineData(1024, 0, 1024, 1024, 1024)]
        [InlineData(1024, 0, 1024, 2048, 0)]
        [InlineData(1024, 256, 512, -1, 0)]
        [InlineData(1024, 256, 512, 0, 0)]
        [InlineData(1024, 256, 512, 256, 256)]
        [InlineData(1024, 256, 512, 512, 512)]
        [InlineData(1024, 256, 512, 2048, 0)]
        public void Read_Stream(int size, long offset, long length, int count, int expectedRead)
        {
            Stream data = new MemoryStream(new byte[size]);
            var stream = new ViewStream(data, offset, length);

            byte[] buffer = new byte[1024];
            int actual = stream.Read(buffer, 0, count);
            Assert.Equal(expectedRead, actual);
        }

        #endregion

        #region Seek

        [Theory]
        [InlineData(0, 0, 0, -1, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, -1, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, -1, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.Begin, 0)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.End, 1022)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.Begin, 0)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.Begin, 256)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.Current, 256)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.Begin, 1023)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.Current, 1023)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.Begin, 0)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.End, 510)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.Current, 0)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.Begin, 0)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.Current, 0)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.Begin, 256)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.Current, 256)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.Begin, 511)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.Current, 511)]
        public void Seek_Array(int size, long offset, long length, long position, SeekOrigin seekOrigin, long expectedPosition)
        {
            byte[] data = new byte[size];
            var stream = new ViewStream(data, offset, length);
            stream.Seek(position, seekOrigin);
            Assert.Equal(expectedPosition, stream.Position);
        }

        [Theory]
        [InlineData(0, 0, 0, -1, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, -1, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, -1, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 0, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 256, SeekOrigin.Current, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.Begin, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.End, 0)]
        [InlineData(0, 0, 0, 2048, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.Begin, 0)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.End, 1022)]
        [InlineData(1024, 0, 1024, -1, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.Begin, 0)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 0, SeekOrigin.Current, 0)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.Begin, 256)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 256, SeekOrigin.Current, 256)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.Begin, 1023)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.End, 1023)]
        [InlineData(1024, 0, 1024, 2048, SeekOrigin.Current, 1023)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.Begin, 0)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.End, 510)]
        [InlineData(1024, 256, 512, -1, SeekOrigin.Current, 0)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.Begin, 0)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 0, SeekOrigin.Current, 0)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.Begin, 256)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 256, SeekOrigin.Current, 256)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.Begin, 511)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.End, 511)]
        [InlineData(1024, 256, 512, 2048, SeekOrigin.Current, 511)]
        public void Seek_Stream(int size, long offset, long length, long position, SeekOrigin seekOrigin, long expectedPosition)
        {
            Stream data = new MemoryStream(new byte[size]);
            var stream = new ViewStream(data, offset, length);
            stream.Seek(position, seekOrigin);
            Assert.Equal(expectedPosition, stream.Position);
        }

        #endregion

        #region Unimplemented

        [Fact]
        public void SetLength_Array_Throws()
        {
            byte[] data = new byte[1024];
            var stream = new ViewStream(data, 0, 1024);
            Assert.Throws<NotImplementedException>(() => stream.SetLength(0));
        }

        [Fact]
        public void SetLength_Stream_Throws()
        {
            Stream data = new MemoryStream(new byte[1024]);
            var stream = new ViewStream(data, 0, 1024);
            Assert.Throws<NotImplementedException>(() => stream.SetLength(0));
        }

        [Fact]
        public void Write_Array_Throws()
        {
            byte[] data = new byte[1024];
            var stream = new ViewStream(data, 0, 1024);
            Assert.Throws<NotImplementedException>(() => stream.Write([], 0, 0));
        }

        [Fact]
        public void Write_Stream_Throws()
        {
            Stream data = new MemoryStream(new byte[1024]);
            var stream = new ViewStream(data, 0, 1024);
            Assert.Throws<NotImplementedException>(() => stream.Write([], 0, 0));
        }

        #endregion
    }
}