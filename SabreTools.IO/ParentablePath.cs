﻿using System;
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
        public string CurrentPath { get; }

        /// <summary>
        /// Possible parent path represented (may be null or empty)
        /// </summary>
        public string? ParentPath { get; }

        public ParentablePath(string currentPath, string? parentPath = null)
        {
            CurrentPath = currentPath.Trim();
            ParentPath = parentPath?.Trim();
        }

        /// <summary>
        /// Get the proper filename (with subpath) from the file and parent combination
        /// </summary>
        /// <param name="sanitize">True if path separators should be converted to '-', false otherwise</param>
        /// <returns>Subpath for the file</returns>
        public string? GetNormalizedFileName(bool sanitize)
        {
            // If the current path is empty, we can't do anything
            if (CurrentPath.Length == 0)
                return null;

            // Assume the current path is the filename
            string filename = Path.GetFileName(CurrentPath);

            // If we have a true ParentPath, remove it from CurrentPath and return the remainder
            if (!string.IsNullOrEmpty(ParentPath) && !PathsEqual(CurrentPath, ParentPath))
                filename = CurrentPath.Remove(0, ParentPath!.Length + 1);

            // If we're sanitizing the path after, do so
            if (sanitize)
                filename = filename.Replace('\\', '-').Replace('/', '-');

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
            // If the current path is empty
            if (CurrentPath.Length == 0)
                return null;

            // If we have an inplace output
            if (inplace)
                return Path.GetDirectoryName(CurrentPath);

            // If the output dir is empty after trimming
            outDir = outDir?.Trim();
            if (string.IsNullOrEmpty(outDir))
                return null;

            // If the parent path is empty or the paths are equal
            if (string.IsNullOrEmpty(ParentPath) || PathsEqual(CurrentPath, ParentPath))
                return outDir;

            // By default, the working parent directory is the parent path
            string workingParent = ParentPath!;

            // TODO: Should this be the default? Always create a subfolder if a folder is found?
            // If we are processing a path that is coming from a directory and we are outputting to the current directory, we want to get the subfolder to write to
            if (outDir == Environment.CurrentDirectory)
                workingParent = Path.GetDirectoryName(ParentPath) ?? string.Empty;

            // Handle bizarre Windows-like paths on Linux
            if (workingParent.EndsWith(":") && Path.DirectorySeparatorChar == '/')
                workingParent += '/';

            // Determine the correct subfolder based on the working parent directory
            int extraLength = workingParent.EndsWith(":")
                || workingParent.EndsWith("\\")
                || workingParent.EndsWith("/") ? 0 : 1;

            string strippedPath = CurrentPath.Remove(0, workingParent.Length + extraLength);
            string combinedPath = Path.Combine(outDir!, strippedPath);
            return Path.GetDirectoryName(combinedPath);
        }

        /// <summary>
        /// Determine if two paths are equal or not
        /// </summary>
        private static bool PathsEqual(string? path1, string? path2, bool caseSenstive = false)
        {
            // Handle null path cases
            if (path1 == null && path2 == null)
                return true;
            else if (path1 == null ^ path2 == null)
                return false;

            // Normalize the paths before comparing
            path1 = NormalizeDirectorySeparators(path1);
            path2 = NormalizeDirectorySeparators(path2);

            // Compare and return
            return string.Equals(path1, path2, caseSenstive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Normalize directory separators for the current system
        /// </summary>
        /// <param name="input">Input path that may contain separators</param>
        /// <returns>Normalized path with separators fixed, if possible</returns>
        private static string? NormalizeDirectorySeparators(string? input)
        {
            // Null inputs are skipped
            if (input == null)
                return null;

            // Replace '\' with '/'
            return input.Replace('\\', '/');
        }
    }
}
