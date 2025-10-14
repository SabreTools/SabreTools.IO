using System;
using System.Collections.Generic;
using System.IO;

namespace SabreTools.IO.Transform
{
    /// <summary>
    /// Helpers to combine inputs
    /// </summary>
    public static class Combine
    {
        /// <summary>
        /// Concatenate all files in the order provided, if possible
        /// </summary>
        /// <param name="paths">List of paths to combine</param>
        /// <param name="output">Path to the output file</param>
        /// <returns>True if the files were concatenated successfully, false otherwise</returns>
        public static bool Concatenate(List<string> paths, string output)
        {
            // If the path list is empty
            if (paths.Count == 0)
                return false;

            // If the output filename is invalid
            if (string.IsNullOrEmpty(output))
                return false;

            try
            {
                // Try to build the new output file
                using var ofs = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                for (int i = 0; i < paths.Count; i++)
                {
                    // Get the next file
                    string next = paths[i];
                    if (!File.Exists(next))
                        break;

                    // Copy the next input to the output
                    using var ifs = File.Open(next, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    // Write in blocks
                    int read = 0;
                    do
                    {
                        byte[] buffer = new byte[3 * 1024 * 1024];

                        read = ifs.Read(buffer, 0, buffer.Length);
                        if (read == 0)
                            break;

                        ofs.Write(buffer, 0, read);
                        ofs.Flush();
                    } while (read > 0);
                }

                return true;
            }
            catch
            {
                // Absorb the exception right now
                return false;
            }
        }

        /// <summary>
        /// Interleave two files into a single output
        /// </summary>
        /// <param name="even">First file to interleave</param>
        /// <param name="odd">Second file to interleave</param>
        /// <param name="output">Path to the output file</param>
        /// <param name="type"><see cref="BlockSize"> representing how to process the inputs</param>
        /// <returns>True if the files were interleaved successfully, false otherwise</returns>
        public static bool Interleave(string even, string odd, string output, BlockSize type)
        {
            // If either file does not exist
            if (!File.Exists(even) || !File.Exists(odd))
                return false;

            try
            {
                // Get the input streams
                using var evenStream = File.Open(even, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var oddStream = File.Open(odd, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Interleave the streams
                using var interleaved = Interleave(evenStream, oddStream, type);
                if (interleaved == null)
                    return false;

                // Open the output file
                using var outputStream = File.Open(output, FileMode.Create, FileAccess.Write, FileShare.None);

                // Write the interleaved data
                interleaved.CopyTo(outputStream);
                outputStream.Flush();
            }
            catch
            {
                // Absorb all errors for now
                return false;
            }

            return true;
        }

        /// <summary>
        /// Interleave two streams into a single output
        /// </summary>
        /// <param name="even">First stream to interleave</param>
        /// <param name="odd">Second stream to interleave</param>
        /// <param name="output">Path to the output file</param>
        /// <param name="type"><see cref="BlockSize"> representing how to process the inputs</param>
        /// <returns>A filled stream on success, null otherwise</returns>
        public static Stream? Interleave(Stream even, Stream odd, BlockSize type)
        {
            // If either stream is unreadable
            if (!even.CanRead || !odd.CanRead)
                return null;

            // Get the number of bytes to process
            int byteCount = type switch
            {
                BlockSize.Byte => 1,
                BlockSize.Word => 2,
                BlockSize.Dword => 4,
                BlockSize.Qword => 8,
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };

            try
            {
                // Create an output stream
                var outputStream = new MemoryStream();

                // Alternate between inputs during reading
                bool useEven = true;
                while (even.Position < even.Length || odd.Position < odd.Length)
                {
                    byte[] read = new byte[byteCount];
                    int actual = (useEven ? even : odd).Read(read, 0, byteCount);
                    outputStream.Write(read, 0, actual);
                    outputStream.Flush();
                    useEven = !useEven;
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
            catch
            {
                // Absorb all errors for now
                return null;
            }
        }
    }
}
