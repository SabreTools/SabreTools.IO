using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SabreTools.IO.Extensions;
using SabreTools.Matching.Compare;

namespace SabreTools.IO
{
    /// <summary>
    /// Methods around path operations
    /// </summary>
    public static class PathTool
    {
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
                    List<string> directories = GetDirectoriesOrdered(input, pattern);
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
        private static List<string> GetDirectoriesOrdered(string dir, string pattern = "*")
        {
            return GetDirectoriesOrderedHelper(dir, [], pattern);
        }

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
                    List<string> files = GetFilesOrdered(input, pattern);
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
        public static List<string> GetFilesOrdered(string dir, string pattern = "*")
        {
            return GetFilesOrderedHelper(dir, [], pattern);
        }

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
        /// Get the home directory for the current user
        /// </summary>
        public static string GetHomeDirectory()
        {
#if NET20 || NET35
            return Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
#else
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif
        }

        /// <summary>
        /// Get the runtime directory
        /// </summary>
        public static string GetRuntimeDirectory()
        {
#if NET20 || NET35 || NET40 || NET452
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
#else
            return AppContext.BaseDirectory;
#endif
        }

        /// <inheritdoc cref="NormalizeFilePath(string?, bool)"/>
        public static string NormalizeFilePath(string? path)
            => NormalizeFilePath(path, fullPath: true);

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
        public static string NormalizeFilePath(string? path, bool fullPath)
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
    }
}
