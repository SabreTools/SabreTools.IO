using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test
{
    public class MatchUtilTests
    {
        #region Array

        [Fact]
        public void ArrayGetAllMatches_NullStack_NoMatches()
        {
            byte[]? stack = null;
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void ArrayGetAllMatches_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void ArrayGetAllMatches_EmptyMatchSets_NoMatches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets = [];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void ArrayGetAllMatches_Matching_Matches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[] { 0x01 }, "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            string setName = Assert.Single(matches);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void ArrayGetAllMatches_PartialMatchingAny_Matches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets, any: true);
            string setName = Assert.Single(matches);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void ArrayGetAllMatches_PartialMatchingAll_NoMatches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets, any: false);
            Assert.Empty(matches);
        }

        [Fact]
        public void ArrayGetFirstMatch_NullStack_NoMatches()
        {
            byte[]? stack = null;
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void ArrayGetFirstMatch_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void ArrayGetFirstMatch_EmptyMatchSets_NoMatches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets = [];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void ArrayGetFirstMatch_Matching_Matches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[] { 0x01 }, "name")];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void ArrayGetFirstMatch_PartialMatchingAny_Matches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets, any: true);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void ArrayGetFirstMatch_PartialMatchingAll_NoMatches()
        {
            byte[] stack = [0x01];
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets, any: false);
            Assert.Null(setName);
        }

        [Fact]
        public void ExactSizeArrayMatch()
        {
            byte[] source = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07];
            byte?[] check = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07];
            string expected = "match";

            var matchers = new List<ContentMatchSet>
            {
                new(check, expected),
            };

            string? actual = MatchUtil.GetFirstMatch("testfile", source, matchers, any: false);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Stream

        [Fact]
        public void StreamGetAllMatches_NullStack_NoMatches()
        {
            Stream? stack = null;
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void StreamGetAllMatches_EmptyStack_NoMatches()
        {
            Stream stack = new MemoryStream();
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void StreamGetAllMatches_EmptyMatchSets_NoMatches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets = [];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            Assert.Empty(matches);
        }

        [Fact]
        public void StreamGetAllMatches_Matching_Matches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[] { 0x01 }, "name")];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets);
            string setName = Assert.Single(matches);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void StreamGetAllMatches_PartialMatchingAny_Matches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets, any: true);
            string setName = Assert.Single(matches);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void StreamGetAllMatches_PartialMatchingAll_NoMatches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            var matches = MatchUtil.GetAllMatches("file", stack, matchSets, any: false);
            Assert.Empty(matches);
        }

        [Fact]
        public void StreamGetFirstMatch_NullStack_NoMatches()
        {
            Stream? stack = null;
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void StreamGetFirstMatch_EmptyStack_NoMatches()
        {
            Stream stack = new MemoryStream();
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[1], "name")];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void StreamGetFirstMatch_EmptyMatchSets_NoMatches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets = [];
            string? match = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Null(match);
        }

        [Fact]
        public void StreamGetFirstMatch_Matching_Matches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets = [new ContentMatchSet(new byte[] { 0x01 }, "name")];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void StreamGetFirstMatch_PartialMatchingAny_Matches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets, any: true);
            Assert.Equal("name", setName);
        }

        [Fact]
        public void StreamGetFirstMatch_PartialMatchingAll_NoMatches()
        {
            Stream stack = new MemoryStream([0x01]);
            List<ContentMatchSet> matchSets =
            [
                new ContentMatchSet([
                    new byte[] { 0x00 },
                    new ContentMatch([0x01]),
                ], "name")
            ];
            string? setName = MatchUtil.GetFirstMatch("file", stack, matchSets, any: false);
            Assert.Null(setName);
        }

        [Fact]
        public void ExactSizeStreamMatch()
        {
            byte[] source = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07];
            var stream = new MemoryStream(source);

            byte?[] check = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07];
            string expected = "match";

            var matchers = new List<ContentMatchSet>
            {
                new(check, expected),
            };

            string? actual = MatchUtil.GetFirstMatch("testfile", stream, matchers, any: false);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Path

        #endregion
    }
}
