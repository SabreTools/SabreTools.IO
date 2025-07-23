using System;
using System.IO;
using System.Text;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class IOExtensionsTests
    {
        #region Ensure

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("     ", "     ")] // TODO: This is a bad result
        [InlineData("dirname", "dirname")]
        [InlineData("\"dirname\"", "dirname")]
        public void EnsureTest(string? dir, string? expected)
        {
            // Handle test setup
            expected ??= PathTool.GetRuntimeDirectory();
            if (expected != null)
                expected = Path.GetFullPath(expected);

            string actual = dir.Ensure(create: false);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Get Encoding

        [Fact]
        public void GetEncoding_EmptyPath()
        {
            string path = "";
            Encoding expected = Encoding.Default;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_InvalidPath()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "INVALID");
            Encoding expected = Encoding.Default;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        // Disable warning about UTF7 usage
#pragma warning disable SYSLIB0001
        [Fact]
        public void GetEncoding_UTF7()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "utf7bom.txt");
            Encoding expected = Encoding.UTF7;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }
#pragma warning restore SYSLIB0001

        [Fact]
        public void GetEncoding_UTF8()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "utf8bom.txt");
            Encoding expected = Encoding.UTF8;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_Unicode()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "utf16lebom.txt");
            Encoding expected = Encoding.Unicode;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_BigEndianUnicode()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "utf16bebom.txt");
            Encoding expected = Encoding.BigEndianUnicode;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_UTF32()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "utf32bom.txt");
            Encoding expected = Encoding.UTF32;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_ASCII()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "ascii.txt");
            Encoding expected = Encoding.Default;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Get Normalized Extension

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("     ", null)]
        [InlineData("no-extension", null)]
        [InlineData("NO-EXTENSION", null)]
        [InlineData("no-extension.", null)]
        [InlineData("NO-EXTENSION.", null)]
        [InlineData("filename.ext", "ext")]
        [InlineData("FILENAME.EXT", "ext")]
        public void GetNormalizedExtensionTest(string? path, string? expected)
        {
            string? actual = path.GetNormalizedExtension();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Path

        [Fact]
        public void ListEmpty_NullDirectory()
        {
            string? dir = null;
            var empty = dir.ListEmpty();
            Assert.Null(empty);
        }

        [Fact]
        public void ListEmpty_InvalidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "INVALID");
            var empty = dir.ListEmpty();
            Assert.Null(empty);
        }

        [Fact]
        public void ListEmpty_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var empty = dir.ListEmpty();
            Assert.NotNull(empty);
            Assert.Empty(empty);
        }

        [Fact]
        public void SafeGetDirectories_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeGetDirectories();
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetDirectories_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeGetDirectories("*");
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetDirectories_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeGetDirectories("*", SearchOption.AllDirectories);
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeGetFiles();
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeGetFiles("*");
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeGetFiles("*", SearchOption.AllDirectories);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeGetFileSystemEntries();
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeGetFileSystemEntries("*");
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeGetFileSystemEntries("*", SearchOption.AllDirectories);
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeEnumerateDirectories();
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeEnumerateDirectories("*");
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var dirs = dir.SafeEnumerateDirectories("*", SearchOption.AllDirectories);
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeEnumerateFiles();
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeEnumerateFiles("*");
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeEnumerateFiles("*", SearchOption.AllDirectories);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeEnumerateFileSystemEntries();
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeEnumerateFileSystemEntries("*");
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var entries = dir.SafeEnumerateFileSystemEntries("*", SearchOption.AllDirectories);
            Assert.NotEmpty(entries);
        }

        #endregion
    }
}
