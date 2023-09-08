using System;
using System.IO;

namespace SabreTools.IO
{
    /// <summary>
    /// A path that optionally contains a parent root
    /// </summary>
    public class ParentablePath
    {
        /// <summary>
        /// Current full path represented
        /// </summary>
#if NET48
        public string CurrentPath { get; private set; }
#else
        public string CurrentPath { get; init; }
#endif

        /// <summary>
        /// Possible parent path represented (may be null or empty)
        /// </summary>
#if NET48
        public string ParentPath { get; private set; }
#else
        public string? ParentPath { get; init; }
#endif

#if NET48
        public ParentablePath(string currentPath, string parentPath = null)
#else
        public ParentablePath(string currentPath, string? parentPath = null)
#endif
        {
            CurrentPath = currentPath;
            ParentPath = parentPath;
        }

        /// <summary>
        /// Get the proper filename (with subpath) from the file and parent combination
        /// </summary>
        /// <param name="sanitize">True if path separators should be converted to '-', false otherwise</param>
        /// <returns>Subpath for the file</returns>
#if NET48
        public string GetNormalizedFileName(bool sanitize)
#else
        public string? GetNormalizedFileName(bool sanitize)
#endif
        {
            // If the current path is empty, we can't do anything
            if (string.IsNullOrWhiteSpace(CurrentPath))
                return null;

            // Assume the current path is the filename
            string filename = Path.GetFileName(CurrentPath);

            // If we have a true ParentPath, remove it from CurrentPath and return the remainder
            if (!string.IsNullOrWhiteSpace(ParentPath) && !string.Equals(CurrentPath, ParentPath, StringComparison.Ordinal))
                filename = CurrentPath.Remove(0, ParentPath.Length + 1);

            // If we're sanitizing the path after, do so
            if (sanitize)
                filename = filename.Replace(Path.DirectorySeparatorChar, '-').Replace(Path.AltDirectorySeparatorChar, '-');

            return filename;
        }

        /// <summary>
        /// Get the proper output path for a given input file and output directory
        /// </summary>
        /// <param name="outDir">Output directory to use</param>
        /// <param name="inplace">True if the output file should go to the same input folder, false otherwise</param>
        /// <returns>Complete output path</returns>
#if NET48
        public string GetOutputPath(string outDir, bool inplace)
#else
        public string? GetOutputPath(string outDir, bool inplace)
#endif
        {
            // If the current path is empty, we can't do anything
            if (string.IsNullOrWhiteSpace(CurrentPath))
                return null;

            // If the output dir is empty (and we're not inplace), we can't do anything
            if (string.IsNullOrWhiteSpace(outDir) && !inplace)
                return null;

            // Check if we have a split path or not
            bool splitpath = !string.IsNullOrWhiteSpace(ParentPath);

            // If we have an inplace output, use the directory name from the input path
            if (inplace)
                return Path.GetDirectoryName(CurrentPath);

            // If the current and parent paths are the same, just use the output directory
            if (!splitpath || CurrentPath.Length == (ParentPath?.Length ?? 0))
                return outDir;

            // By default, the working parent directory is the parent path
            string workingParent = ParentPath ?? string.Empty;

            // TODO: Should this be the default? Always create a subfolder if a folder is found?
            // If we are processing a path that is coming from a directory and we are outputting to the current directory, we want to get the subfolder to write to
            if (outDir == Environment.CurrentDirectory)
                workingParent = Path.GetDirectoryName(ParentPath ?? string.Empty) ?? string.Empty;

            // Determine the correct subfolder based on the working parent directory
#if NET48
            int extraLength = workingParent.EndsWith(":")
                || workingParent.EndsWith(Path.DirectorySeparatorChar.ToString())
                || workingParent.EndsWith(Path.AltDirectorySeparatorChar.ToString()) ? 0 : 1;
#else
            int extraLength = workingParent.EndsWith(':')
                || workingParent.EndsWith(Path.DirectorySeparatorChar)
                || workingParent.EndsWith(Path.AltDirectorySeparatorChar) ? 0 : 1;
#endif

            return Path.GetDirectoryName(Path.Combine(outDir, CurrentPath.Remove(0, workingParent.Length + extraLength)));
        }
    }
}
