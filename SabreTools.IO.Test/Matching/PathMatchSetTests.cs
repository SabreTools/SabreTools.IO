using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test.Matching
{
    public class PathMatchSetTests
    {
        [Fact]
        public void InvalidNeedle_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new PathMatchSet(string.Empty, "name"));
            Assert.Throws<InvalidDataException>(() => new PathMatchSet(string.Empty, PathVersionMock, "name"));
        }

        [Fact]
        public void InvalidNeedles_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new PathMatchSet([], "name"));
            Assert.Throws<InvalidDataException>(() => new PathMatchSet([], PathVersionMock, "name"));
        }

        [Fact]
        public void GenericConstructor_NoDelegates()
        {
            var needles = new List<PathMatch> { "test" };
            var cms = new PathMatchSet(needles, "name");
            Assert.Null(cms.GetVersion);
        }

        [Fact]
        public void VersionConstructor_SingleDelegate()
        {
            var needles = new List<PathMatch> { "test" };
            var cms = new PathMatchSet(needles, PathVersionMock, "name");
            Assert.NotNull(cms.GetVersion);
        }

        #region Array

        [Fact]
        public void MatchesAll_NullArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll((string[]?)null);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_EmptyArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(Array.Empty<string>());
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_MatchingArray_Matches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(new string[] { "test" });
            string path = Assert.Single(actual);
            Assert.Equal("test", path);
        }

        [Fact]
        public void MatchesAll_MismatchedArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(new string[] { "not" });
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAny_NullArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny((string[]?)null);
            Assert.Null(actual);
        }

        [Fact]
        public void MatchesAny_EmptyArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(Array.Empty<string>());
            Assert.Null(actual);
        }

        [Fact]
        public void MatchesAny_MatchingArray_Matches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(new string[] { "test" });
            Assert.Equal("test", actual);
        }

        [Fact]
        public void MatchesAny_MismatchedArray_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(new string[] { "not" });
            Assert.Null(actual);
        }

        #endregion

        #region List

        [Fact]
        public void MatchesAll_NullList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll((List<string>?)null);
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_EmptyList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(new List<string>());
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAll_MatchingList_Matches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(new List<string> { "test" });
            string path = Assert.Single(actual);
            Assert.Equal("test", path);
        }

        [Fact]
        public void MatchesAll_MismatchedList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            var actual = cms.MatchesAll(new List<string> { "not" });
            Assert.Empty(actual);
        }

        [Fact]
        public void MatchesAny_NullList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny((List<string>?)null);
            Assert.Null(actual);
        }

        [Fact]
        public void MatchesAny_EmptyList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(new List<string>());
            Assert.Null(actual);
        }

        [Fact]
        public void MatchesAny_MatchingList_Matches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(new List<string> { "test" });
            Assert.Equal("test", actual);
        }

        [Fact]
        public void MatchesAny_MismatchedList_NoMatches()
        {
            var cms = new PathMatchSet("test", "name");
            string? actual = cms.MatchesAny(new List<string> { "not" });
            Assert.Null(actual);
        }

        #endregion

        #region Mock Delegates

        /// <inheritdoc cref="GetPathVersion"/>
        private static string? PathVersionMock(string path, List<string>? files) => null;

        #endregion
    }
}
