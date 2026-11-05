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
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            string expectedCurrent = Path.Combine(expectedParent, "Subdirectory");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "Subdir*"),
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
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            string expectedCurrent = Path.Combine(expectedParent, "Subdirectory");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "Subdir*"),
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
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "INVALID");
            var empty = dir.ListEmpty();
            Assert.Null(empty);
        }

        [Fact]
        public void ListEmpty_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var empty = dir.ListEmpty();
            Assert.NotNull(empty);
            Assert.Empty(empty);
        }

        #endregion

        #region Concatenate

        [Fact]
        public void Concatenate_EmptyList_False()
        {
            List<string> paths = [];
            string output = string.Empty;
            bool actual = IOExtensions.Concatenate(paths, output);
            Assert.False(actual);
        }

        [Fact]
        public void Concatenate_InvalidOutput_False()
        {
            List<string> paths = ["a"];
            string output = string.Empty;
            bool actual = IOExtensions.Concatenate(paths, output);
            Assert.False(actual);
        }

        [Fact]
        public void Concatenate_FilledList_True()
        {
            List<string> paths = [
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "ascii.txt"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "file-to-compress.bin"),
            ];
            string output = Guid.NewGuid().ToString();
            bool actual = IOExtensions.Concatenate(paths, output);
            Assert.True(actual);

            string text = File.ReadAllText(output);
            Assert.Equal("This doesn't match anythingThis is just a file that has a known set of hashes to make sure that everything with hashing is still working as anticipated.", text);

            File.Delete(output);
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
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "INVALID");
            Encoding expected = Encoding.Default;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        // Disable warning about UTF7 usage
#pragma warning disable SYSLIB0001
        [Fact]
        public void GetEncoding_UTF7()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf7bom.txt");
            Encoding expected = Encoding.UTF7;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }
#pragma warning restore SYSLIB0001

        [Fact]
        public void GetEncoding_UTF8()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf8bom.txt");
            Encoding expected = Encoding.UTF8;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_Unicode()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf16lebom.txt");
            Encoding expected = Encoding.Unicode;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_BigEndianUnicode()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf16bebom.txt");
            Encoding expected = Encoding.BigEndianUnicode;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_UTF32()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf32bom.txt");
            Encoding expected = Encoding.UTF32;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_ASCII()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "ascii.txt");
            Encoding expected = Encoding.Default;

            var actual = path.GetEncoding();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetFilesOnly

        [Fact]
        public void GetFilesOnly_NoAppendParent()
        {
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            string expectedCurrent = Path.Combine(expectedParent, "ascii.txt");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "Subdir*"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf8bom.txt"),
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
            string expectedParent = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            string expectedCurrent = Path.Combine(expectedParent, "ascii.txt");

            List<string> inputs =
            [
                string.Empty,
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "Subdir*"),
                Path.Combine(Environment.CurrentDirectory, "TestData", "Path", "utf8bom.txt"),
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

        #region ResolvePath

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ResolvePath_NullOrEmpty_ReturnsNull(string? path)
        {
            string? actual = path.ResolvePath();
            Assert.Null(actual);
        }

        // TODO: Add tests for home path without modifying the runner's home path

        [Fact]
        public void ResolvePath_AbsolutePath_Exists_ReturnsFullPath()
        {
            string filename = Guid.NewGuid().ToString();
            string path = Path.Combine(Environment.CurrentDirectory, filename);

            File.WriteAllBytes(path, []);
            try
            {
                string? actual = filename.ResolvePath();
                Assert.Equal(path, actual);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void ResolvePath_AbsolutePath_Missing_ReturnsNull()
        {
            string filename = Guid.NewGuid().ToString();
            string path = Path.Combine(Environment.CurrentDirectory, filename);

            string? actual = path.ResolvePath();
            Assert.Null(actual);
        }

        [Fact]
        public void ResolvePath_RelativePath_Exists_ReturnsFullPath()
        {
            string filename = Guid.NewGuid().ToString();
            string subDir = "RELATIVE";
            string path = Path.Combine(subDir, filename);
            string expected = Path.Combine(Environment.CurrentDirectory, path);

            Directory.CreateDirectory(subDir);
            File.WriteAllBytes(path, []);
            try
            {
                string? actual = path.ResolvePath();
                Assert.Equal(expected, actual);
            }
            finally
            {
                Directory.Delete(subDir, recursive: true);
            }
        }

        [Fact]
        public void ResolvePath_RelativePath_Missing_ReturnsNull()
        {
            string filename = Guid.NewGuid().ToString();
            string subDir = "INVALID";
            string path = Path.Combine(subDir, filename);

            string? actual = path.ResolvePath();
            Assert.Null(actual);
        }

        [Fact]
        public void ResolvePath_BareName_RuntimeDirectory_ReturnsFullPath()
        {
            string filename = Guid.NewGuid().ToString();
            string path = Path.Combine(AppContext.BaseDirectory, filename);

            File.WriteAllBytes(path, []);
            try
            {
                string? actual = filename.ResolvePath();
                Assert.Equal(path, actual);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void ResolvePath_BareName_Path_ReturnsFullPath()
        {
            string filename = Guid.NewGuid().ToString();
            string subDir = Path.GetFullPath("TEMP");
            string path = Path.Combine(subDir, filename);
            string expected = Path.Combine(Environment.CurrentDirectory, path);

            Directory.CreateDirectory(subDir);
            File.WriteAllBytes(path, []);

            // Prepend the temp directory to PATH and check it exists
            // TODO: See if there's a way around changing the actual environment variables
            string? originalPath = Environment.GetEnvironmentVariable("PATH");
            try
            {
                Environment.SetEnvironmentVariable("PATH", subDir + Path.PathSeparator + (originalPath ?? string.Empty));
                string? actual = filename.ResolvePath();
                Assert.Equal(expected, actual);
            }
            finally
            {
                Environment.SetEnvironmentVariable("PATH", originalPath);
                Directory.Delete(subDir, recursive: true);
            }
        }

        [Fact]
        public void ResolvePath_BareName_Missing_ReturnsNull()
        {
            string filename = Guid.NewGuid().ToString();

            string? actual = filename.ResolvePath();
            Assert.Null(actual);
        }

        #endregion

        #region SafeGetDirectories

        [Fact]
        public void SafeGetDirectories_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeGetDirectories();
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetDirectories_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeGetDirectories("*");
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetDirectories_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeGetDirectories("*", SearchOption.AllDirectories);
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeGetDirectories_LinkedDirectory_NotFollowed()
        {
            // Linked directories are only skipped outside of Windows
            if (SystemEnvironment.IsWindows())
                return;

            string root = CreateLinkedDirectoryTree();
            try
            {
                var dirs = root.SafeGetDirectories("*", SearchOption.AllDirectories);
                Assert.Equal(2, dirs.Length);
            }
            finally
            {
                Directory.Delete(root, recursive: true);
            }
        }

        #endregion

        #region SafeGetFiles

        [Fact]
        public void SafeGetFiles_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeGetFiles();
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeGetFiles("*");
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeGetFiles("*", SearchOption.AllDirectories);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_PatternTopDirectoryOnly()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeGetFiles("*", SearchOption.TopDirectoryOnly);
            Assert.Equal(11, files.Length);
        }

        [Fact]
        public void SafeGetFiles_ValidDirectory_PatternWithDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData");
            var files = dir.SafeGetFiles(Path.Combine("Path", "Subdirectory", "*"), SearchOption.AllDirectories);
            Assert.Single(files);
        }

        [Fact]
        public void SafeGetFiles_LinkedDirectory_NotFollowed()
        {
            // Linked directories are only skipped outside of Windows
            if (SystemEnvironment.IsWindows())
                return;

            string root = CreateLinkedDirectoryTree();
            try
            {
                var files = root.SafeGetFiles("*", SearchOption.AllDirectories);
                Assert.Equal(3, files.Length);
            }
            finally
            {
                Directory.Delete(root, recursive: true);
            }
        }

        #endregion

        #region SafeGetFileSystemEntries

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeGetFileSystemEntries();
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeGetFileSystemEntries("*");
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeGetFileSystemEntries_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeGetFileSystemEntries("*", SearchOption.AllDirectories);
            Assert.NotEmpty(entries);
        }

        #endregion

        #region SafeEnumerateDirectories

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeEnumerateDirectories();
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeEnumerateDirectories("*");
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateDirectories_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var dirs = dir.SafeEnumerateDirectories("*", SearchOption.AllDirectories);
            Assert.Single(dirs);
        }

        [Fact]
        public void SafeEnumerateDirectories_LinkedDirectory_NotFollowed()
        {
            // Linked directories are only skipped outside of Windows
            if (SystemEnvironment.IsWindows())
                return;

            string root = CreateLinkedDirectoryTree();
            try
            {
                List<string> dirs = [.. root.SafeEnumerateDirectories("*", SearchOption.AllDirectories)];
                Assert.Equal(2, dirs.Count);
            }
            finally
            {
                Directory.Delete(root, recursive: true);
            }
        }

        #endregion

        #region SafeEnumerateFiles

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeEnumerateFiles();
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeEnumerateFiles("*");
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFiles_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var files = dir.SafeEnumerateFiles("*", SearchOption.AllDirectories);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void SafeEnumerateFiles_LinkedDirectory_NotFollowed()
        {
            // Linked directories are only skipped outside of Windows
            if (SystemEnvironment.IsWindows())
                return;

            string root = CreateLinkedDirectoryTree();
            try
            {
                List<string> files = [.. root.SafeEnumerateFiles("*", SearchOption.AllDirectories)];
                Assert.Equal(3, files.Count);
            }
            finally
            {
                Directory.Delete(root, recursive: true);
            }
        }

        [Fact]
        public void SafeEnumerateFiles_DirectoryWithoutExecutePermission_Skipped()
        {
            // Execute permission on directories only exists outside of Windows
            if (OperatingSystem.IsWindows())
                return;

            string root = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString());
            string locked = Path.Combine(root, "Locked");
            string other = Path.Combine(root, "Other", "Subdirectory");
            Directory.CreateDirectory(Path.Combine(locked, "Subdirectory"));
            Directory.CreateDirectory(other);
            File.WriteAllBytes(Path.Combine(locked, "file.bin"), []);
            File.WriteAllBytes(Path.Combine(other, "file.bin"), []);
            File.SetUnixFileMode(locked, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            try
            {
                // Only the subdirectory of the locked directory is left out
                List<string> files = [.. root.SafeEnumerateFiles("*", SearchOption.AllDirectories)];
                Assert.Equal(2, files.Count);
            }
            finally
            {
                File.SetUnixFileMode(locked, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
                Directory.Delete(root, recursive: true);
            }
        }

        #endregion

        #region SafeEnumerateFileSystemEntries

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeEnumerateFileSystemEntries();
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory_Pattern()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeEnumerateFileSystemEntries("*");
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_ValidDirectory_PatternOption()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, "TestData", "Path");
            var entries = dir.SafeEnumerateFileSystemEntries("*", SearchOption.AllDirectories);
            Assert.NotEmpty(entries);
        }

        [Fact]
        public void SafeEnumerateFileSystemEntries_LinkedDirectory_NotFollowed()
        {
            // Linked directories are only skipped outside of Windows
            if (SystemEnvironment.IsWindows())
                return;

            string root = CreateLinkedDirectoryTree();
            try
            {
                List<string> entries = [.. root.SafeEnumerateFileSystemEntries("*", SearchOption.AllDirectories)];
                Assert.Equal(5, entries.Count);
            }
            finally
            {
                Directory.Delete(root, recursive: true);
            }
        }

        #endregion

        /// <summary>
        /// Create a directory tree with a linked file and a directory linked to the directory it is in
        /// </summary>
        /// <returns>Root of the tree, to be deleted by the caller</returns>
        private static string CreateLinkedDirectoryTree()
        {
            string root = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString());
            string subdir = Path.Combine(root, "Subdirectory");
            Directory.CreateDirectory(subdir);
            File.WriteAllBytes(Path.Combine(root, "file.bin"), []);
            File.WriteAllBytes(Path.Combine(subdir, "file.bin"), []);
            File.CreateSymbolicLink(Path.Combine(subdir, "link.bin"), "file.bin");
            Directory.CreateSymbolicLink(Path.Combine(subdir, "self"), ".");
            return root;
        }
    }
}
