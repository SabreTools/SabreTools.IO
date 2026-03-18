using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SabreTools.IO.Test
{
    public class PathToolTests
    {
        [Fact]
        public void GetDirectoriesOnly_NoAppendParent()
        {
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData");
            string expectedCurrent = Path.Combine(expectedParent, "Subdirectory");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Subdir*"),
            ];
            var actual = PathTool.GetDirectoriesOnly(inputs, appendParent: true);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(expectedParent, first.ParentPath);
        }

        [Fact]
        public void GetDirectoriesOnly_AppendParent()
        {
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData");
            string expectedCurrent = Path.Combine(expectedParent, "Subdirectory");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Subdir*"),
            ];
            var actual = PathTool.GetDirectoriesOnly(inputs, appendParent: false);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(string.Empty, first.ParentPath);
        }

        [Fact]
        public void GetFilesOnly_NoAppendParent()
        {
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData");
            string expectedCurrent = Path.Combine(expectedParent, "ascii.txt");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Subdir*"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "utf8bom.txt"),
            ];
            var actual = PathTool.GetFilesOnly(inputs, appendParent: true);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(expectedParent, first.ParentPath);
        }

        [Fact]
        public void GetFilesOnly_AppendParent()
        {
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData");
            string expectedCurrent = Path.Combine(expectedParent, "ascii.txt");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Subdir*"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "utf8bom.txt"),
            ];
            var actual = PathTool.GetFilesOnly(inputs, appendParent: false);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(string.Empty, first.ParentPath);
        }
    }
}
