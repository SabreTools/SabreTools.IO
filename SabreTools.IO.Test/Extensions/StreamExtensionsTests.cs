using System;
using System.IO;
using System.Text;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class StreamExtensionsTests
    {
        #region AlignToBoundary

        [Fact]
        public void AlignToBoundary_Null_False()
        {
            Stream? stream = null;
            byte alignment = 4;
            bool actual = stream.AlignToBoundary(alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_Empty_False()
        {
            Stream? stream = new MemoryStream([]);
            byte alignment = 4;
            bool actual = stream.AlignToBoundary(alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_EOF_False()
        {
            Stream? stream = new MemoryStream([0x01, 0x02]);
            byte alignment = 4;

            stream.Position = 1;
            bool actual = stream.AlignToBoundary(alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_TooShort_False()
        {
            Stream? stream = new MemoryStream([0x01, 0x02]);
            byte alignment = 4;

            stream.Position = 1;
            bool actual = stream.AlignToBoundary(alignment);
            Assert.False(actual);
        }

        [Fact]
        public void AlignToBoundary_CanAlign_True()
        {
            Stream? stream = new MemoryStream([0x01, 0x02, 0x03, 0x04, 0x05]);
            byte alignment = 4;

            stream.Position = 1;
            bool actual = stream.AlignToBoundary(alignment);
            Assert.True(actual);
        }

        #endregion

        #region ReadFrom

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadFrom_Null_Null(bool retainPosition)
        {
            Stream? stream = null;
            byte[]? actual = stream.ReadFrom(0, 1, retainPosition);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadFrom_NonSeekable_Null(bool retainPosition)
        {
            Stream? stream = new NonSeekableStream();
            byte[]? actual = stream.ReadFrom(0, 1, retainPosition);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadFrom_Empty_Null(bool retainPosition)
        {
            Stream? stream = new MemoryStream([]);
            byte[]? actual = stream.ReadFrom(0, 1, retainPosition);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(2048, true)]
        [InlineData(-1, false)]
        [InlineData(2048, false)]
        public void ReadFrom_InvalidOffset_Null(long offset, bool retainPosition)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            byte[]? actual = stream.ReadFrom(offset, 1, retainPosition);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(2048, true)]
        [InlineData(-1, false)]
        [InlineData(2048, false)]
        public void ReadFrom_InvalidLength_Null(int length, bool retainPosition)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            byte[]? actual = stream.ReadFrom(0, length, retainPosition);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadFrom_Valid_Filled(bool retainPosition)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            byte[]? actual = stream.ReadFrom(0, 512, retainPosition);

            Assert.NotNull(actual);
            Assert.Equal(512, actual.Length);

            if (retainPosition)
                Assert.Equal(0, stream.Position);
            else
                Assert.Equal(512, stream.Position);
        }

        #endregion

        #region ReadStringsFrom

        [Fact]
        public void ReadStringsFrom_Null_Null()
        {
            Stream? stream = null;
            var actual = stream.ReadStringsFrom(0, 1, 3);
            Assert.Null(actual);
        }

        [Fact]
        public void ReadStringsFrom_NonSeekable_Null()
        {
            Stream? stream = new NonSeekableStream();
            var actual = stream.ReadStringsFrom(0, 1, 3);
            Assert.Null(actual);
        }

        [Fact]
        public void ReadStringsFrom_Empty_Null()
        {
            Stream? stream = new MemoryStream([]);
            var actual = stream.ReadStringsFrom(0, 1, 3);
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2048)]
        public void ReadStringsFrom_InvalidLimit_Empty(int charLimit)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            var actual = stream.ReadStringsFrom(0, 1024, charLimit);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsFrom_NoValidStrings_Empty()
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            var actual = stream.ReadStringsFrom(0, 1024, 4);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReadStringsFrom_AsciiStrings_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.ASCII.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            Stream? stream = new MemoryStream(bytes);
            var actual = stream.ReadStringsFrom(0, bytes.Length, 4);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsFrom_UTF16_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.Unicode.GetBytes("TEST"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TWO"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("DATA"),
                .. new byte[] { 0x00 },
            ];
            Stream? stream = new MemoryStream(bytes);
            var actual = stream.ReadStringsFrom(0, bytes.Length, 4);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void ReadStringsFrom_Mixed_Filled()
        {
            byte[]? bytes =
            [
                .. Encoding.ASCII.GetBytes("TEST1"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("TWO1"),
                .. new byte[] { 0x00 },
                .. Encoding.ASCII.GetBytes("DATA1"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TEST2"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("TWO2"),
                .. new byte[] { 0x00 },
                .. Encoding.Unicode.GetBytes("DATA2"),
                .. new byte[] { 0x00 },
            ];
            Stream? stream = new MemoryStream(bytes);
            var actual = stream.ReadStringsFrom(0, bytes.Length, 5);
            Assert.NotNull(actual);
            Assert.Equal(4, actual.Count);
        }

        #endregion

        #region SeekIfPossible

        [Fact]
        public void SeekIfPossible_NonSeekable_CurrentPosition()
        {
            var stream = new NonSeekableStream();
            long actual = stream.SeekIfPossible(0);
            Assert.Equal(8, actual);
        }

        [Fact]
        public void SeekIfPossible_NonPositionable_InvalidPosition()
        {
            var stream = new NonPositionableStream();
            long actual = stream.SeekIfPossible(0);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void SeekIfPossible_HiddenNonSeekable_InvalidPosition()
        {
            var stream = new HiddenNonSeekableStream();
            long actual = stream.SeekIfPossible(0);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void SeekIfPossible_NonNegative_ValidPosition()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, false, true);
            long actual = stream.SeekIfPossible(5);
            Assert.Equal(5, actual);
        }

        [Fact]
        public void SeekIfPossible_Negative_ValidPosition()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, false, true);
            long actual = stream.SeekIfPossible(-3);
            Assert.Equal(13, actual);
        }

        #endregion

        #region SegmentValid

        [Fact]
        public void SegmentValid_Null_False()
        {
            Stream? stream = null;
            bool actual = stream.SegmentValid(0, 1);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2048)]
        public void SegmentValid_InvalidOffset_False(long offset)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            bool actual = stream.SegmentValid(offset, 1);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2048)]
        public void SegmentValid_InvalidLength_False(int length)
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            bool actual = stream.SegmentValid(0, length);
            Assert.False(actual);
        }

        [Fact]
        public void SegmentValid_ValidSegment_True()
        {
            Stream? stream = new MemoryStream(new byte[1024]);
            bool actual = stream.SegmentValid(0, 1024);
            Assert.True(actual);
        }

        #endregion

        /// <summary>
        /// Represents a hidden non-seekable stream
        /// </summary>
        private class HiddenNonSeekableStream : Stream
        {
            public override bool CanRead => true;

            public override bool CanSeek => true;

            public override bool CanWrite => true;

            public override long Length => 16;

            public override long Position { get => 8; set => throw new NotSupportedException(); }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Represents a non-seekable stream
        /// </summary>
        private class NonSeekableStream : Stream
        {
            public override bool CanRead => true;

            public override bool CanSeek => false;

            public override bool CanWrite => true;

            public override long Length => 16;

            public override long Position { get => 8; set => throw new NotSupportedException(); }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Represents a non-seekable, non-positionable stream
        /// </summary>
        private class NonPositionableStream : Stream
        {
            public override bool CanRead => true;

            public override bool CanSeek => false;

            public override bool CanWrite => true;

            public override long Length => 16;

            public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
        }
    }
}
