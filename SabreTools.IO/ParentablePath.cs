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
        public string CurrentPath { get; private set; }

        /// <summary>
        /// Possible parent path represented (may be null or empty)
        /// </summary>
        public string? ParentPath { get; private set; }

        public ParentablePath(string currentPath, string? parentPath = null)
        {
            CurrentPath = currentPath.Trim().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            ParentPath = parentPath?.Trim()?.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Get the proper filename (with subpath) from the file and parent combination
        /// </summary>
        /// <param name="sanitize">True if path separators should be converted to '-', false otherwise</param>
        /// <returns>Subpath for the file</returns>
        public string? GetNormalizedFileName(bool sanitize)
        {
            // If the current path is empty, we can't do anything
            if (string.IsNullOrEmpty(CurrentPath))
                return null;

            // Assume the current path is the filename
            string filename = Path.GetFileName(CurrentPath);

            // If we have a true ParentPath, remove it from CurrentPath and return the remainder
            if (!string.IsNullOrEmpty(ParentPath) && !string.Equals(CurrentPath, ParentPath, StringComparison.Ordinal))      
                filename = CurrentPath.Remove(0, ParentPath!.Length + 1);

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
        public string? GetOutputPath(string? outDir, bool inplace)
        {
            // If the current path is empty, we can't do anything
            if (string.IsNullOrEmpty(CurrentPath))
                return null;

            // If the output dir is empty (and we're not inplace), we can't do anything
            outDir = outDir?.Trim();
            if (string.IsNullOrEmpty(outDir) && !inplace)
                return null;

            // Check if we have a split path or not
            bool splitpath = !string.IsNullOrEmpty(ParentPath);

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
            int extraLength = workingParent.EndsWith(":")
                || workingParent.EndsWith(Path.DirectorySeparatorChar.ToString())
                || workingParent.EndsWith(Path.AltDirectorySeparatorChar.ToString()) ? 0 : 1;

            return Path.GetDirectoryName(Path.Combine(outDir!, CurrentPath.Remove(0, workingParent.Length + extraLength)));
        }
    }
}
