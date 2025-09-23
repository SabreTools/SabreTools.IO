using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Matching;
using Xunit;

namespace SabreTools.IO.Test.Matching
{
    public class PathMatchTests
    {
        [Fact]
        public void InvalidNeedle_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(() => new PathMatch(string.Empty));
        }

        [Fact]
        public void ImplicitOperatorArray_Success()
        {
            string needle = "test";
            var pm = (PathMatch)needle;
            Assert.NotNull(pm);
        }

        #region Array

        [Fact]
        public void NullArray_NoMatch()
        {
            var pm = new PathMatch("test");
            string? actual = pm.Match((string[]?)null);
            Assert.Null(actual);
        }

        [Fact]
        public void EmptyArray_NoMatch()
        {
            var pm = new PathMatch("test");
            string? actual = pm.Match(Array.Empty<string>());
            Assert.Null(actual);
        }

        [Fact]
        public void SingleItemArrayMatching_Match()
        {
            string needle = "test";
            string[] stack = [needle];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void SingleItemArrayMismatched_NoMatch()
        {
            string needle = "test";
            string[] stack = ["not"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        [Fact]
        public void MultiItemArrayMatching_Match()
        {
            string needle = "test";
            string[] stack = ["not", needle, "far"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void MultiItemArrayMismatched_NoMatch()
        {
            string needle = "test";
            string[] stack = ["not", "too", "far"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        #endregion

        #region List

        [Fact]
        public void NullList_NoMatch()
        {
            var pm = new PathMatch("test");
            string? actual = pm.Match((List<string>?)null);
            Assert.Null(actual);
        }

        [Fact]
        public void EmptyList_NoMatch()
        {
            var pm = new PathMatch("test");
            string? actual = pm.Match(new List<string>());
            Assert.Null(actual);
        }

        [Fact]
        public void SingleItemListMatching_Match()
        {
            string needle = "test";
            List<string> stack = [needle];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void SingleItemListMismatched_NoMatch()
        {
            string needle = "test";
            List<string> stack = ["not"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        [Fact]
        public void MultiItemListMatching_Match()
        {
            string needle = "test";
            List<string> stack = ["not", needle, "far"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void MultiItemListMismatched_NoMatch()
        {
            string needle = "test";
            List<string> stack = ["not", "too", "far"];
            var pm = new PathMatch(needle);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        #endregion

        #region Match Case

        [Fact]
        public void MatchCaseEqual_Match()
        {
            string needle = "test";
            List<string> stack = [needle];
            var pm = new PathMatch(needle, matchCase: true);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void NoMatchCaseEqual_Match()
        {
            string needle = "test";
            List<string> stack = [needle];
            var pm = new PathMatch(needle, matchCase: false);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void MatchCaseInequal_NoMatch()
        {
            string needle = "test";
            List<string> stack = [needle.ToUpperInvariant()];
            var pm = new PathMatch(needle, matchCase: true);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        [Fact]
        public void NoMatchCaseInequal_Match()
        {
            string needle = "test";
            List<string> stack = [needle.ToUpperInvariant()];
            var pm = new PathMatch(needle, matchCase: false);

            string? actual = pm.Match(stack);
            Assert.Equal(needle.ToUpperInvariant(), actual);
        }

        [Fact]
        public void MatchCaseContains_Match()
        {
            string needle = "test";
            List<string> stack = [$"prefix_{needle}_postfix"];
            var pm = new PathMatch(needle, matchCase: true);

            string? actual = pm.Match(stack);
            Assert.Equal($"prefix_{needle}_postfix", actual);
        }

        [Fact]
        public void NoMatchCaseContains_Match()
        {
            string needle = "test";
            List<string> stack = [$"prefix_{needle}_postfix"];
            var pm = new PathMatch(needle, matchCase: false);

            string? actual = pm.Match(stack);
            Assert.Equal($"prefix_{needle}_postfix", actual);
        }

        #endregion

        #region Use Ends With

        [Fact]
        public void EndsWithEqual_Match()
        {
            string needle = "test";
            List<string> stack = [needle];
            var pm = new PathMatch(needle, useEndsWith: true);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void NoEndsWithEqual_Match()
        {
            string needle = "test";
            List<string> stack = [needle];
            var pm = new PathMatch(needle, useEndsWith: false);

            string? actual = pm.Match(stack);
            Assert.Equal(needle, actual);
        }

        [Fact]
        public void EndsWithInequal_Match()
        {
            string needle = "test";
            List<string> stack = [needle.ToUpperInvariant()];
            var pm = new PathMatch(needle, useEndsWith: true);

            string? actual = pm.Match(stack);
            Assert.Equal(needle.ToUpperInvariant(), actual);
        }

        [Fact]
        public void NoEndsWithInequal_Match()
        {
            string needle = "test";
            List<string> stack = [needle.ToUpperInvariant()];
            var pm = new PathMatch(needle, useEndsWith: false);

            string? actual = pm.Match(stack);
            Assert.Equal(needle.ToUpperInvariant(), actual);
        }

        [Fact]
        public void EndsWithContains_NoMatch()
        {
            string needle = "test";
            List<string> stack = [$"prefix_{needle}_postfix"];
            var pm = new PathMatch(needle, useEndsWith: true);

            string? actual = pm.Match(stack);
            Assert.Null(actual);
        }

        [Fact]
        public void NoEndsWithContains_Match()
        {
            string needle = "test";
            List<string> stack = [$"prefix_{needle}_postfix"];
            var pm = new PathMatch(needle, useEndsWith: false);

            string? actual = pm.Match(stack);
            Assert.Equal($"prefix_{needle}_postfix", actual);
        }

        #endregion
    }
}
