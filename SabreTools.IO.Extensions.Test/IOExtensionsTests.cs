using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class IOExtensionsTests
    {
        #region EnsureDirectory

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("     ", "     ")] // TODO: This is a bad result
        [InlineData("dirname", "dirname")]
        [InlineData("\"dirname\"", "dirname")]
        public void EnsureDirectoryTest(string? dir, string? expected)
        {
            // Handle test setup
            expected ??= PathTool.GetRuntimeDirectory();
            if (expected is not null)
                expected = Path.GetFullPath(expected);

            string actual = dir.EnsureDirectory(create: false);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetDirectoriesOnly

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
            var actual = IOExtensions.GetDirectoriesOnly(inputs, appendParent: true);
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
            var actual = IOExtensions.GetDirectoriesOnly(inputs, appendParent: false);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(string.Empty, first.ParentPath);
        }

        #endregion

        #region ListEmpty

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

        #endregion

        #region GetEncoding

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

        #region GetFilesOnly

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
            var actual = IOExtensions.GetFilesOnly(inputs, appendParent: true);
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
            var actual = IOExtensions.GetFilesOnly(inputs, appendParent: false);
            Assert.NotEmpty(actual);

            var first = actual[0];
            Assert.Equal(expectedCurrent, first.CurrentPath);
            Assert.Equal(string.Empty, first.ParentPath);
        }

        #endregion

        #region FileSize

        [Fact]
        public void FileSize_Null_Invalid()
        {
            string? filename = null;
            long actual = filename.GetFileSize();
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void FileSize_Empty_Invalid()
        {
            string? filename = string.Empty;
            long actual = filename.GetFileSize();
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void FileSize_Invalid_Invalid()
        {
            string? filename = "INVALID";
            long actual = filename.GetFileSize();
            Assert.Equal(-1, actual);
        }

        #endregion

        #region GetNormalizedExtension

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

        #region NormalizeFilePath

        [Theory]
        [InlineData(null, false, "")]
        [InlineData(null, true, "")]
        [InlineData("", false, "")]
        [InlineData("", true, "")]
        [InlineData("filename.bin", false, "filename.bin")]
        [InlineData("filename.bin", true, "filename.bin")]
        [InlineData("\"filename.bin\"", false, "filename.bin")]
        [InlineData("\"filename.bin\"", true, "filename.bin")]
        [InlineData("<filename.bin>", false, "filename.bin")]
        [InlineData("<filename.bin>", true, "filename.bin")]
        [InlineData("1.2.3.4..bin", false, "1.2.3.4..bin")]
        [InlineData("1.2.3.4..bin", true, "1.2.3.4..bin")]
        [InlineData("dir/filename.bin", false, "dir/filename.bin")]
        [InlineData("dir/filename.bin", true, "dir/filename.bin")]
        [InlineData(" dir / filename.bin", false, "dir/filename.bin")]
        [InlineData(" dir / filename.bin", true, "dir/filename.bin")]
        [InlineData("\0dir/\0filename.bin", false, "_dir/_filename.bin")]
        [InlineData("\0dir/\0filename.bin", true, "_dir/_filename.bin")]
        public void NormalizeOutputPathsTest(string? path, bool getFullPath, string expected)
        {
            // Modify expected to account for test data if necessary
            if (getFullPath && !string.IsNullOrEmpty(expected))
                expected = Path.GetFullPath(expected);

            string actual = path.NormalizeFilePath(getFullPath);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region SafeGetDirectories

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

        #endregion

        #region SafeGetFiles

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

        #endregion

        #region SafeGetFileSystemEntries

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

        #endregion

        #region SafeEnumerateDirectories

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

        #endregion

        #region SafeEnumerateFiles

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

        #endregion

        #region SafeEnumerateFileSystemEntries

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
