using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test.Matching
{
    public class ContentMatchSetTests
    {
        [Fact]
        public void InvalidNeedle_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet(Array.Empty<byte>(), "name"));
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet(Array.Empty<byte>(), ArrayVersionMock, "name"));
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet(Array.Empty<byte>(), StreamVersionMock, "name"));
        }

        [Fact]
        public void InvalidNeedles_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet([], "name"));
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet([], ArrayVersionMock, "name"));
            Assert.Throws<InvalidDataException>(() => new ContentMatchSet([], StreamVersionMock, "name"));
        }

        [Fact]
        public void GenericConstructor_NoDelegates()
        {
            var needles = new List<ContentMatch> { new byte[] { 0x01, 0x02, 0x03, 0x04 } };
            var cms = new ContentMatchSet(needles, "name");
            Assert.Null(cms.GetArrayVersion);
            Assert.Null(cms.GetStreamVersion);
        }

        [Fact]
        public void ArrayConstructor_SingleDelegate()
        {
            var needles = new List<ContentMatch> { new byte[] { 0x01, 0x02, 0x03, 0x04 } };
            var cms = new ContentMatchSet(needles, ArrayVersionMock, "name");
            Assert.NotNull(cms.GetArrayVersion);
            Assert.Null(cms.GetStreamVersion);
        }

        [Fact]
        public void StreamConstructor_SingleDelegate()
        {
            var needles = new List<ContentMatch> { new byte[] { 0x01, 0x02, 0x03, 0x04 } };
            var cms = new ContentMatchSet(needles, StreamVersionMock, "name");
            Assert.Null(cms.GetArrayVersion);
            Assert.NotNull(cms.GetStreamVersion);
        }

        #region Array

        [Fact]
        public void MatchesAll_NullArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll((byte[]?)null);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_EmptyArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll([]);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_MatchingArray_Matches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll([0x01, 0x02, 0x03, 0x04]);
            int position = Assert.Single(actual);
            Assert.Equal(0, position);
        }

        [Fact]
        public void MatchesAll_MismatchedArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll([0x01, 0x03]);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAny_NullArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny((byte[]?)null);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void MatchesAny_EmptyArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny([]);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void MatchesAny_MatchingArray_Matches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny([0x01, 0x02, 0x03, 0x04]);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void MatchesAny_MismatchedArray_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny([0x01, 0x03]);
            Assert.Equal(-1, actual);
        }

        #endregion

        #region Stream

        [Fact]
        public void MatchesAll_NullStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll((Stream?)null);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_EmptyStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll(new MemoryStream());
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_MatchingStream_Matches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll(new MemoryStream([0x01, 0x02, 0x03, 0x04]));
            int position = Assert.Single(actual);
            Assert.Equal(0, position);
        }

        [Fact]
        public void MatchesAll_MismatchedStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            var actual = cms.MatchesAll([0x01, 0x03]);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAny_NullStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny((Stream?)null);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void MatchesAny_EmptyStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny(new MemoryStream());
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void MatchesAny_MatchingStream_Matches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny(new MemoryStream([0x01, 0x02, 0x03, 0x04]));
            Assert.Equal(0, actual);
        }

        [Fact]
        public void MatchesAny_MismatchedStream_NoMatches()
        {
            var cms = new ContentMatchSet(new byte[] { 0x01, 0x02, 0x03, 0x04 }, "name");
            int actual = cms.MatchesAny([0x01, 0x03]);
            Assert.Equal(-1, actual);
        }

        #endregion

        #region Mock Delegates

        /// <inheritdoc cref="GetArrayVersion"/>
        private static string? ArrayVersionMock(string path, byte[]? content, List<int> positions) => null;

        /// <inheritdoc cref="GetStreamVersion"/>
        private static string? StreamVersionMock(string path, Stream? content, List<int> positions) => null;

        #endregion
    }
}
