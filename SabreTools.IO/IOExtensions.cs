using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SabreTools.IO
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
            dir = Path.GetFullPath(dir.Trim('"'));

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
            if (!Directory.GetFiles(root, "*", SearchOption.AllDirectories).Any())
#else
            if (!Directory.EnumerateFileSystemEntries(root, "*", SearchOption.AllDirectories).Any())
#endif
                return [];

            // Otherwise, get the complete list
#if NET20 || NET35
            return Directory.GetDirectories(root, "*", SearchOption.AllDirectories)
                .Where(dir => !Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Any())
                .ToList();
#else
            return Directory.EnumerateDirectories(root, "*", SearchOption.AllDirectories)
                .Where(dir => !Directory.EnumerateFileSystemEntries(dir, "*", SearchOption.AllDirectories).Any())
                .ToList();
#endif
        }
    }
}
