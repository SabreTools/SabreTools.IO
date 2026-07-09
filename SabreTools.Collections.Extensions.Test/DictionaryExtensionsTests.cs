using System.Collections.Generic;
using Xunit;

namespace SabreTools.Collections.Extensions.Test
{
    public class DictionaryExtensionsTests
    {
        #region MergeWith (String)

        [Fact]
        public void MergeWithString_EmptySource_EmptyOther_Empty()
        {
            Dictionary<string, List<string>> source = [];
            Dictionary<string, List<string>> other = [];

            source.MergeWith(other);
            Assert.Empty(source);
        }

        [Fact]
        public void MergeWithString_EmptySource_EmptyKeyOther_Empty()
        {
            Dictionary<string, List<string>> source = [];
            Dictionary<string, List<string>> other = [];
            other.Add("key", []);

            source.MergeWith(other);
            Assert.Empty(source);
        }

        [Fact]
        public void MergeWithString_EmptySource_FilledOther_Filled()
        {
            Dictionary<string, List<string>> source = [];
            Dictionary<string, List<string>> other = [];
            other.Add("key", ["value"]);

            source.MergeWith(other);
            string key = Assert.Single(source.Keys);
            Assert.Equal("key", key);
            List<string> actual = source[key];
            string value = Assert.Single(actual);
            Assert.Equal("value", value);
        }

        [Fact]
        public void MergeWithString_FilledSource_EmptyOther_Filled()
        {
            Dictionary<string, List<string>> source = [];
            source.Add("key", ["value"]);
            Dictionary<string, List<string>> other = [];

            source.MergeWith(other);
            string key = Assert.Single(source.Keys);
            Assert.Equal("key", key);
            List<string> actual = source[key];
            string value = Assert.Single(actual);
            Assert.Equal("value", value);
        }

        [Fact]
        public void MergeWithString_FilledSource_FilledOther_Filled()
        {
            Dictionary<string, List<string>> source = [];
            source.Add("key1", ["value1"]);
            Dictionary<string, List<string>> other = [];
            other.Add("key2", ["value2"]);

            source.MergeWith(other);
            Assert.Equal(2, source.Keys.Count);

            Assert.Contains("key1", source.Keys);
            List<string> actualKey1 = source["key1"];
            string value1 = Assert.Single(actualKey1);
            Assert.Equal("value1", value1);

            Assert.Contains("key2", source.Keys);
            List<string> actualKey2 = source["key2"];
            string value2 = Assert.Single(actualKey2);
            Assert.Equal("value2", value2);
        }

        #endregion

        #region MergeWith (Generic)

        [Fact]
        public void MergeWithGeneric_EmptySource_EmptyOther_Empty()
        {
            Dictionary<string, List<int>> source = [];
            Dictionary<string, List<int>> other = [];

            source.MergeWith(other);
            Assert.Empty(source);
        }

        [Fact]
        public void MergeWithGeneric_EmptySource_EmptyKeyOther_Empty()
        {
            Dictionary<string, List<int>> source = [];
            Dictionary<string, List<int>> other = [];
            other.Add("key", []);

            source.MergeWith(other);
            Assert.Empty(source);
        }

        [Fact]
        public void MergeWithGeneric_EmptySource_FilledOther_Filled()
        {
            Dictionary<string, List<int>> source = [];
            Dictionary<string, List<int>> other = [];
            other.Add("key", [1]);

            source.MergeWith(other);
            string key = Assert.Single(source.Keys);
            Assert.Equal("key", key);
            List<int> actual = source[key];
            int value = Assert.Single(actual);
            Assert.Equal(1, value);
        }

        [Fact]
        public void MergeWithGeneric_FilledSource_EmptyOther_Filled()
        {
            Dictionary<string, List<int>> source = [];
            source.Add("key", [1]);
            Dictionary<string, List<int>> other = [];

            source.MergeWith(other);
            string key = Assert.Single(source.Keys);
            Assert.Equal("key", key);
            List<int> actual = source[key];
            int value = Assert.Single(actual);
            Assert.Equal(1, value);
        }

        [Fact]
        public void MergeWithGeneric_FilledSource_FilledOther_Filled()
        {
            Dictionary<string, List<int>> source = [];
            source.Add("key1", [1]);
            Dictionary<string, List<int>> other = [];
            other.Add("key2", [2]);

            source.MergeWith(other);
            Assert.Equal(2, source.Keys.Count);

            Assert.Contains("key1", source.Keys);
            List<int> actualKey1 = source["key1"];
            int value1 = Assert.Single(actualKey1);
            Assert.Equal(1, value1);

            Assert.Contains("key2", source.Keys);
            List<int> actualKey2 = source["key2"];
            int value2 = Assert.Single(actualKey2);
            Assert.Equal(2, value2);
        }

        #endregion

    }
}
