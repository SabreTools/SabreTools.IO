using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Extensions;

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
    }
}
