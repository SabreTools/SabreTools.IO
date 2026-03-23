using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SabreTools.Collections.Extensions;
using SabreTools.Text.Compare;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Methods around path operations
    /// </summary>
    public static class IOExtensions
    {
        #region Directory

        /// <summary>
        /// Ensure the output directory is a proper format and can be created
        /// </summary>
        /// <param name="dir">Directory to check</param>
        /// <param name="create">True if the directory should be created, false otherwise (default)</param>
        /// <returns>Full path to the directory</returns>
        public static string EnsureDirectory(this string? dir, bool create = false)
        {
            // If the output directory is invalid
            if (string.IsNullOrEmpty(dir))
                dir = PathTool.GetRuntimeDirectory();

            // Get the full path for the output directory
            dir = Path.GetFullPath(dir!.Trim('"'));

            // If we're creating the output folder, do so
            if (create && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return dir;
        }

        /// <summary>
        /// Retrieve a list of just directories from inputs
        /// </summary>
        /// <param name="inputs">List of strings representing directories and files</param>
        /// <param name="appendParent">True if the parent name should be included in the ParentablePath, false otherwise (default)</param>
        /// <returns>List of strings representing just directories from the inputs</returns>
        public static List<ParentablePath> GetDirectoriesOnly(List<string> inputs, bool appendParent = false)
        {
            var outputs = new List<ParentablePath>();
            for (int i = 0; i < inputs.Count; i++)
            {
                string input = inputs[i];

                // If we have a null or empty path
                if (string.IsNullOrEmpty(input))
                    continue;

                // If we have a wildcard
                string pattern = "*";
                if (input.Contains("*") || input.Contains("?"))
                {
                    pattern = Path.GetFileName(input);
                    input = input.Substring(0, input.Length - pattern.Length);
                }

                // Get the parent path in case of appending
                string parentPath = Path.GetFullPath(input);
                if (Directory.Exists(input))
                {
                    List<string> directories = input.GetDirectoriesOrdered(pattern);
                    foreach (string dir in directories)
                    {
                        outputs.Add(new ParentablePath(Path.GetFullPath(dir), appendParent ? parentPath : string.Empty));
                    }
                }
            }

            return outputs;
        }

        /// <summary>
        /// Retrieve a list of directories from a directory recursively in proper order
        /// </summary>
        /// <param name="dir">Directory to parse</param>
        /// <param name="pattern">Optional pattern to search for directory names</param>
        /// <returns>List with all new files</returns>
        public static List<string> GetDirectoriesOrdered(this string dir, string pattern = "*")
            => GetDirectoriesOrderedHelper(dir, [], pattern);

        /// <summary>
        /// Retrieve a list of directories from a directory recursively in proper order
        /// </summary>
        /// <param name="dir">Directory to parse</param>
        /// <param name="infiles">List representing existing files</param>
        /// <param name="pattern">Optional pattern to search for directory names</param>
        /// <returns>List with all new files</returns>
        private static List<string> GetDirectoriesOrderedHelper(string dir, List<string> infiles, string pattern)
        {
            // Take care of the files in the top directory
            List<string> toadd = [.. dir.SafeEnumerateDirectories(pattern, SearchOption.TopDirectoryOnly)];
            toadd.Sort(new NaturalComparer());
            infiles.AddRange(toadd);

            // Then recurse through and add from the directories
            foreach (string subDir in toadd)
            {
                infiles = GetDirectoriesOrderedHelper(subDir, infiles, pattern);
            }

            // Return the new list
            return infiles;
        }

        /// <summary>
        /// Get all empty folders within a root folder
        /// </summary>
        /// <param name="root">Root directory to parse</param>
        /// <returns>IEumerable containing all directories that are empty, an empty enumerable if the root is empty, null otherwise</returns>
        public static List<string>? ListEmpty(this string? root)
        {
            // Check null or empty first
            if (root is null)
                return null;

            // Then, check if the root exists
            if (!Directory.Exists(root))
                return null;

            // Otherwise, get the complete list
            var empty = new List<string>();
            foreach (var dir in SafeGetDirectories(root, "*", SearchOption.AllDirectories))
            {
                if (SafeGetFiles(dir).Length == 0)
                    empty.Add(dir);
            }

            return empty;
        }

        #endregion

        #region File

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        /// <link>http://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding</link>
        public static Encoding GetEncoding(this string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return Encoding.Default;

            if (!File.Exists(filename))
                return Encoding.Default;

            // Try to open the file
            try
            {
                var file = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (file is null)
                    return Encoding.Default;

                // Read the BOM
                var bom = new byte[4];
                int read = file.Read(bom, 0, 4);
                file.Dispose();

                // Disable warning about UTF7 usage
#if NET5_0_OR_GREATER
#pragma warning disable SYSLIB0001
#endif

                // Analyze the BOM
                if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
                if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
                if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
                if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
                return Encoding.Default;

                // Restore warning about UTF7 usage
#if NET5_0_OR_GREATER
#pragma warning restore SYSLIB0001
#endif
            }
            catch
            {
                return Encoding.Default;
            }
        }

        /// <summary>
        /// Retrieve a list of just files from inputs
        /// </summary>
        /// <param name="inputs">List of strings representing directories and files</param>
        /// <param name="appendParent">True if the parent name should be be included in the ParentablePath, false otherwise (default)</param>
        /// <returns>List of strings representing just files from the inputs</returns>
        public static List<ParentablePath> GetFilesOnly(List<string> inputs, bool appendParent = false)
        {
            var outputs = new List<ParentablePath>();
            for (int i = 0; i < inputs.Count; i++)
            {
                string input = inputs[i].Trim('"');

                // If we have a null or empty path
                if (string.IsNullOrEmpty(input))
                    continue;

                // If we have a wildcard
                string pattern = "*";
                if (input.Contains("*") || input.Contains("?"))
                {
                    pattern = Path.GetFileName(input);
                    input = input.Substring(0, input.Length - pattern.Length);
                }

                // Get the parent path in case of appending
                string parentPath = Path.GetFullPath(input);
                if (Directory.Exists(input))
                {
                    List<string> files = input.GetFilesOrdered(pattern);
                    foreach (string file in files)
                    {
                        outputs.Add(new ParentablePath(Path.GetFullPath(file), appendParent ? parentPath : string.Empty));
                    }
                }
                else if (File.Exists(input))
                {
                    outputs.Add(new ParentablePath(Path.GetFullPath(input), appendParent ? parentPath : string.Empty));
                }
            }

            return outputs;
        }

        /// <summary>
        /// Retrieve a list of files from a directory recursively in proper order
        /// </summary>
        /// <param name="dir">Directory to parse</param>
        /// <param name="pattern">Optional pattern to search for directory names</param>
        /// <returns>List with all new files</returns>
        public static List<string> GetFilesOrdered(this string dir, string pattern = "*")
            => GetFilesOrderedHelper(dir, [], pattern);

        /// <summary>
        /// Retrieve a list of files from a directory recursively in proper order
        /// </summary>
        /// <param name="dir">Directory to parse</param>
        /// <param name="infiles">List representing existing files</param>
        /// <param name="pattern">Optional pattern to search for directory names</param>
        /// <returns>List with all new files</returns>
        private static List<string> GetFilesOrderedHelper(string dir, List<string> infiles, string pattern)
        {
            // Take care of the files in the top directory
            List<string> toadd = [.. dir.SafeEnumerateFiles(pattern, SearchOption.TopDirectoryOnly)];
            toadd.Sort(new NaturalComparer());
            infiles.AddRange(toadd);

            // Then recurse through and add from the directories
            List<string> subDirs = [.. dir.SafeEnumerateDirectories(pattern, SearchOption.TopDirectoryOnly)];
            subDirs.Sort(new NaturalComparer());
            foreach (string subdir in subDirs)
            {
                infiles = GetFilesOrderedHelper(subdir, infiles, pattern);
            }

            // Return the new list
            return infiles;
        }

        /// <summary>
        /// Helper to get the filesize from a path
        /// </summary>
        /// <returns>Size of the file path, -1 on error</returns>
        public static long GetFileSize(this string? filename)
        {
            // Invalid filenames are ignored
            if (string.IsNullOrEmpty(filename))
                return -1;

            // Non-file paths are ignored
            if (!File.Exists(filename))
                return -1;

            try
            {
                return new FileInfo(filename).Length;
            }
            catch
            {
                // Ignore errors
                return -1;
            }
        }

        /// <summary>
        /// Get the extension from the path, if possible
        /// </summary>
        /// <param name="path">Path to get extension from</param>
        /// <returns>Extension, if possible</returns>
        public static string? GetNormalizedExtension(this string? path)
        {
            // Check null or empty first
            if (string.IsNullOrEmpty(path))
                return null;

            // Get the extension from the path, if possible
            string? ext = Path.GetExtension(path)?.ToLowerInvariant();

            // Check if the extension is null or empty
            if (string.IsNullOrEmpty(ext))
                return null;

            // Make sure that extensions are valid
            ext = ext!.TrimStart('.');

            return ext;
        }

        /// <inheritdoc cref="NormalizeFilePath(string?, bool)"/>
        public static string NormalizeFilePath(this string? path)
            => path.NormalizeFilePath(fullPath: true);

        /// <summary>
        /// Normalize a file path
        /// </summary>
        /// <param name="path">Path value to normalize</param>
        /// <param name="fullPath">Indicates if the full path is used</param>
        /// <returns>The normalized path on success, an empty path if null, or the original path on error</returns>
        /// <remarks>
        /// This method performs the following steps:
        /// - Remove quotes and angle brackets from the start and end
        /// - Replaces invalid path characters with '_'
        /// - Remove spaces before and after directory separators
        /// </remarks>
        public static string NormalizeFilePath(this string? path, bool fullPath)
        {
            try
            {
                // If we have an invalid path
                if (string.IsNullOrEmpty(path))
                    return string.Empty;

                // Remove quotes and angle brackets from path
                path = path!.Trim('\"');
                path = path!.Trim('<');
                path = path!.Trim('>');

                // Remove invalid path characters
                foreach (char c in Path.GetInvalidPathChars())
                {
                    path = path.Replace(c, '_');
                }

                // Try getting the combined path and returning that directly
                string usablePath = fullPath ? Path.GetFullPath(path) : path;
                var fullDirectory = Path.GetDirectoryName(usablePath)?.Trim();
                string fullFile = Path.GetFileName(usablePath).Trim();

                // Remove invalid filename characters
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    fullFile = fullFile.Replace(c, '_');
                }

                // Rebuild the path, if necessary
                if (!string.IsNullOrEmpty(fullDirectory))
                    fullFile = Path.Combine(fullDirectory, fullFile);

                // Remove spaces before and after separators
                return Regex.Replace(fullFile, @"\s*([\\|/])\s*", @"$1");
            }
            catch { }

            return path ?? string.Empty;
        }

        #endregion

        #region Safe Directory Enumeration

        /// <inheritdoc cref="Directory.GetDirectories(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetDirectories(this string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetDirectories(this string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string, SearchOption)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetDirectories(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern, searchOption);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetFiles(this string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetFiles(this string path, string searchPattern)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string, string, SearchOption)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetFiles(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern, searchOption);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFileSystemEntries(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetFileSystemEntries(this string path)
        {
            try
            {
                return Directory.GetFileSystemEntries(path);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static string[] SafeGetFileSystemEntries(this string path, string searchPattern)
        {
            try
            {
                return Directory.GetFileSystemEntries(path, searchPattern);
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string, SearchOption)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFileSystemEntries(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.GetFileSystemEntries(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

#if NET20 || NET35
        /// <inheritdoc cref="Directory.GetDirectories(string)"/>
        /// <remarks>Calls <see cref="SafeGetDirectories(string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path)
            => path.SafeGetDirectories();

        /// <inheritdoc cref="Directory.GetDirectories(string, string)"/>
        /// <remarks>Calls <see cref="SafeGetDirectories(string, string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern)
            => path.SafeGetDirectories(searchPattern);

        /// <inheritdoc cref="Directory.GetDirectories(string, string, SearchOption)"/>
        /// <remarks>Calls <see cref="SafeGetDirectories(string, string, SearchOption)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern, SearchOption searchOption)
            => path.SafeGetDirectories(searchPattern, searchOption);

        /// <inheritdoc cref="Directory.GetFiles(string)"/>
        /// <remarks>Calls <see cref="SafeGetFiles(string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFiles(this string path)
            => path.SafeGetFiles();

        /// <inheritdoc cref="Directory.GetFiles(string, string)"/>
        /// <remarks>Calls <see cref="SafeGetFiles(string, string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern)
            => path.SafeGetFiles(searchPattern);

        /// <inheritdoc cref="Directory.GetFiles(string, string, SearchOption)"/>
        /// <remarks>Calls <see cref="SafeGetFiles(string, string, SearchOption)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern, SearchOption searchOption)
            => path.SafeGetFiles(searchPattern, searchOption);

        /// <inheritdoc cref="Directory.GetFileSystemEntries(string)"/>
        /// <remarks>Calls <see cref="SafeGetFileSystemEntries(string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path)
            => path.SafeGetFileSystemEntries();

        /// <inheritdoc cref="Directory.GetFileSystemEntries(string, string)"/>
        /// <remarks>Calls <see cref="SafeGetFileSystemEntries(string, string)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern)
            => path.SafeGetFileSystemEntries(searchPattern);

        /// <inheritdoc cref="Directory.GetFileSystemEntries(string, string)"/>
        /// <remarks>Calls <see cref="SafeGetFileSystemEntries(string, string, SearchOption)"/> implementation</remarks>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern, SearchOption searchOption)
            => path.SafeGetFileSystemEntries(searchPattern, searchOption);
#elif NET40_OR_GREATER || NETSTANDARD2_0
        /// <inheritdoc cref="Directory.EnumerateDirectories(string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path)
        {
            try
            {
                var enumerable = Directory.EnumerateDirectories(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern)
        {
            try
            {
                var enumerable = Directory.EnumerateDirectories(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.EnumerateDirectories(path, searchPattern, searchOption);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path)
        {
            try
            {
                var enumerable = Directory.EnumerateFiles(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern)
        {
            try
            {
                var enumerable = Directory.EnumerateFiles(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.EnumerateFiles(path, searchPattern, searchOption);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path)
        {
            try
            {
                var enumerable = Directory.EnumerateFileSystemEntries(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern)
        {
            try
            {
                var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }
#else
        /// <inheritdoc cref="Directory.EnumerateDirectories(string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path)
        {
            try
            {
                string searchPattern = "*";
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateDirectories(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern)
        {
            try
            {
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateDirectories(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateDirectories(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path)
        {
            try
            {
                string searchPattern = "*";
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFiles(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern)
        {
            try
            {
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFiles(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFiles(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path)
        {
            try
            {
                string searchPattern = "*";
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern)
        {
            try
            {
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerationOptions = FromSearchOption(searchOption);
                enumerationOptions.IgnoreInaccessible = true;
                var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern, enumerationOptions);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <summary>Initializes a new instance of the <see cref="EnumerationOptions" /> class with the recommended default options.</summary>
        /// <see href="https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/IO/EnumerationOptions.cs#L42"</remarks>
        private static EnumerationOptions FromSearchOption(SearchOption searchOption)
        {
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new System.ArgumentOutOfRangeException(nameof(searchOption));

            return searchOption == SearchOption.AllDirectories
                ? new EnumerationOptions { RecurseSubdirectories = true, MatchType = MatchType.Win32, AttributesToSkip = 0, IgnoreInaccessible = false }
                : new EnumerationOptions { MatchType = MatchType.Win32, AttributesToSkip = 0, IgnoreInaccessible = false };
        }
#endif

        #endregion
    }
}
