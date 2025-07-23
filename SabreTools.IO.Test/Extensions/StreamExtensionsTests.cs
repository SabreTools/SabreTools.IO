using System;
using System.IO;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class StreamExtensionsTests
    {
        #region Align to Boundary

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

        #region Seek If Possible

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
