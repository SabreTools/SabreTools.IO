using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Methods around path operations
    /// </summary>
    public static class IOExtensions
    {
        /// <summary>
        /// Ensure the output directory is a proper format and can be created
        /// </summary>
        /// <param name="dir">Directory to check</param>
        /// <param name="create">True if the directory should be created, false otherwise (default)</param>
        /// <returns>Full path to the directory</returns>
        public static string Ensure(this string? dir, bool create = false)
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
                FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (file == null)
                    return Encoding.Default;

                // Read the BOM
                var bom = new byte[4];
                file.Read(bom, 0, 4);
                file.Dispose();

                // Disable warning about UTF7 usage
#pragma warning disable SYSLIB0001

                // Analyze the BOM
                if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
                if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
                if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
                if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
                return Encoding.Default;

#pragma warning restore SYSLIB0001
            }
            catch
            {
                return Encoding.Default;
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

        /// <summary>
        /// Get all empty folders within a root folder
        /// </summary>
        /// <param name="root">Root directory to parse</param>
        /// <returns>IEumerable containing all directories that are empty, an empty enumerable if the root is empty, null otherwise</returns>
        public static List<string>? ListEmpty(this string? root)
        {
            // Check null or empty first
            if (string.IsNullOrEmpty(root))
                return null;

            // Then, check if the root exists
            if (!Directory.Exists(root))
                return null;

            // If it does and it is empty, return a blank enumerable
#if NET20 || NET35
            if (!root!.SafeGetFileSystemEntries("*").Any())
#else
            if (!root!.SafeEnumerateFileSystemEntries("*", SearchOption.AllDirectories).Any())
#endif
                return [];

            // Otherwise, get the complete list
#if NET20 || NET35
            return root!.SafeGetDirectories("*", SearchOption.AllDirectories)
                .Where(dir => !dir.SafeGetFileSystemEntries("*").Any())
                .ToList();
#else
            return root!.SafeEnumerateDirectories("*", SearchOption.AllDirectories)
                .Where(dir => !dir.SafeEnumerateFileSystemEntries("*", SearchOption.AllDirectories).Any())
                .ToList();
#endif
        }

        #region Safe Directory Enumeration

        /// <inheritdoc cref="Directory.GetDirectories(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetDirectories(this string path)
        {
            try
            {
                var enumerable = Directory.GetDirectories(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetDirectories(this string path, string searchPattern)
        {
            try
            {
                var enumerable = Directory.GetDirectories(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string, SearchOption)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetDirectories(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.GetDirectories(path, searchPattern, searchOption);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFiles(this string path)
        {
            try
            {
                var enumerable = Directory.GetFiles(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFiles(this string path, string searchPattern)
        {
            try
            {
                var enumerable = Directory.GetFiles(path, searchPattern);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFiles(string, string, SearchOption)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFiles(this string path, string searchPattern, SearchOption searchOption)
        {
            try
            {
                var enumerable = Directory.GetFiles(path, searchPattern, searchOption);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetFileSystemEntries(string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFileSystemEntries(this string path)
        {
            try
            {
                var enumerable = Directory.GetFileSystemEntries(path);
                return enumerable.SafeEnumerate();
            }
            catch
            {
                return [];
            }
        }

        /// <inheritdoc cref="Directory.GetDirectories(string, string)"/>
        /// <remarks>Returns an empty enumerable on any exception</remarks>
        public static IEnumerable<string> SafeGetFileSystemEntries(this string path, string searchPattern)
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
#else
        /// <inheritdoc cref="Directory.EnumerateDirectories(string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path)
        {
            var enumerable = Directory.EnumerateDirectories(path);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern)
        {
            var enumerable = Directory.EnumerateDirectories(path, searchPattern);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateDirectories(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateDirectories(this string path, string searchPattern, SearchOption searchOption)
        {
            var enumerable = Directory.EnumerateDirectories(path, searchPattern, searchOption);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path)
        {
            var enumerable = Directory.EnumerateFiles(path);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern)
        {
            var enumerable = Directory.EnumerateFiles(path, searchPattern);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFiles(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFiles(this string path, string searchPattern, SearchOption searchOption)
        {
            var enumerable = Directory.EnumerateFiles(path, searchPattern, searchOption);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path)
        {
            var enumerable = Directory.EnumerateFileSystemEntries(path);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern)
        {
            var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern);
            return enumerable.SafeEnumerate();
        }

        /// <inheritdoc cref="Directory.EnumerateFileSystemEntries(string, string, SearchOption)"/>
        public static IEnumerable<string> SafeEnumerateFileSystemEntries(this string path, string searchPattern, SearchOption searchOption)
        {
            var enumerable = Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
            return enumerable.SafeEnumerate();
        }
#endif

        #endregion
    }
}
