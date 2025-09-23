using System;
using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test.Matching
{
    public class ContentMatchTests
    {
        [Fact]
        public void InvalidNeedle_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new ContentMatch(Array.Empty<byte>()));
            Assert.Throws<InvalidDataException>(() => new ContentMatch(Array.Empty<byte?>()));
        }

        [Fact]
        public void InvalidStart_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentMatch(new byte[1], start: -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentMatch(new byte?[1], start: -1));
        }

        [Fact]
        public void InvalidEnd_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentMatch(new byte[1], end: -2));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentMatch(new byte?[1], end: -2));
        }

        [Fact]
        public void ImplicitOperatorArray_Success()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = (ContentMatch)needle;
            Assert.NotNull(cm);
        }

        [Fact]
        public void ImplicitOperatorNullableArray_Success()
        {
            byte?[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = (ContentMatch)needle;
            Assert.NotNull(cm);
        }

        #region Byte Array

        [Fact]
        public void NullArray_NoMatch()
        {
            var cm = new ContentMatch(new byte?[1]);
            int actual = cm.Match((byte[]?)null);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EmptyArray_NoMatch()
        {
            var cm = new ContentMatch(new byte?[1]);
            int actual = cm.Match([]);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void LargerNeedleArray_NoMatch()
        {
            var cm = new ContentMatch(new byte?[2]);
            int actual = cm.Match(new byte[1]);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EqualLengthMatchingArray_Match()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(needle);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualLengthMatchingArrayReverse_Match()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(needle, reverse: true);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualLengthMismatchedArray_NoMatch()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new byte[4]);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EqualLengthMismatchedArrayReverse_NoMatch()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new byte[4], reverse: true);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void InequalLengthMatchingArray_Match()
        {
            byte[] stack = [0x01, 0x02, 0x03, 0x04];
            byte[] needle = [0x02, 0x03];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void InequalLengthMatchingArrayReverse_Match()
        {
            byte[] stack = [0x01, 0x02, 0x03, 0x04];
            byte[] needle = [0x02, 0x03];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack, reverse: true);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void InequalLengthMismatchedArray_NoMatch()
        {
            byte[] stack = [0x01, 0x02, 0x03, 0x04];
            byte[] needle = [0x02, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void InequalLengthMismatchedArrayReverse_NoMatch()
        {
            byte[] stack = [0x01, 0x02, 0x03, 0x04];
            byte[] needle = [0x02, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack, reverse: true);
            Assert.Equal(-1, actual);
        }

        #endregion

        #region Stream

        [Fact]
        public void NullStream_NoMatch()
        {
            var cm = new ContentMatch(new byte?[1]);
            int actual = cm.Match((Stream?)null);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EmptyStream_NoMatch()
        {
            var cm = new ContentMatch(new byte?[1]);
            int actual = cm.Match(new MemoryStream());
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void LargerNeedleStream_NoMatch()
        {
            var cm = new ContentMatch(new byte?[2]);
            int actual = cm.Match(new MemoryStream(new byte[1]));
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EqualLengthMatchingStream_Match()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new MemoryStream(needle));
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualLengthMatchingStreamReverse_Match()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new MemoryStream(needle), reverse: true);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualLengthMismatchedStream_NoMatch()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new MemoryStream(new byte[4]));
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void EqualLengthMismatchedStreamReverse_NoMatch()
        {
            byte[] needle = [0x01, 0x02, 0x03, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(new MemoryStream(new byte[4]), reverse: true);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void InequalLengthMatchingStream_Match()
        {
            Stream stack = new MemoryStream([0x01, 0x02, 0x03, 0x04]);
            byte[] needle = [0x02, 0x03];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void InequalLengthMatchingStreamReverse_Match()
        {
            Stream stack = new MemoryStream([0x01, 0x02, 0x03, 0x04]);
            byte[] needle = [0x02, 0x03];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack, reverse: true);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void InequalLengthMismatchedStream_NoMatch()
        {
            Stream stack = new MemoryStream([0x01, 0x02, 0x03, 0x04]);
            byte[] needle = [0x02, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void InequalLengthMismatchedStreamReverse_NoMatch()
        {
            Stream stack = new MemoryStream([0x01, 0x02, 0x03, 0x04]);
            byte[] needle = [0x02, 0x04];
            var cm = new ContentMatch(needle);

            int actual = cm.Match(stack, reverse: true);
            Assert.Equal(-1, actual);
        }

        #endregion
    }
}
