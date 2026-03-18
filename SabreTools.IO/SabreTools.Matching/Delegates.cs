using System.Collections.Generic;
using System.IO;

namespace SabreTools.Matching
{
    /// <summary>
    /// Get a version number from a file
    /// </summary>
    /// <param name="path">File path to get the version from</param>
    /// <param name="content">Optional file contents as a byte array</param>
    /// <param name="positions">List of positions in the array that were matched</param>
    /// <returns>Version string on success, null on failure</returns>
    public delegate string? GetArrayVersion(string path, byte[]? content, List<int> positions);

    /// <summary>
    /// Get a version number from an input path
    /// </summary>
    /// <param name="path">File or directory path to get the version from</param>
    /// <param name="files">Optional set of files in the directory</param>
    /// <returns>Version string on success, null on failure</returns>
    public delegate string? GetPathVersion(string path, List<string>? files);

    /// <summary>
    /// Get a version number from a file
    /// </summary>
    /// <param name="path">File path to get the version from</param>
    /// <param name="content">Optional file contents as a Stream</param>
    /// <param name="positions">List of positions in the Stream that were matched</param>
    /// <returns>Version string on success, null on failure</returns>
    public delegate string? GetStreamVersion(string path, Stream? content, List<int> positions);
}
