using System;
using System.IO;
using SabreTools.IO.Extensions;

namespace SabreTools.IO.Compression.MSZIP
{
    /// <see href="https://officeprotocoldoc.z19.web.core.windows.net/files/MS-MCI/%5bMS-MCI%5d.pdf"/>
    public class Decompressor
    {
        /// <summary>
        /// Last uncompressed block data
        /// </summary>
        private byte[]? _history = null;

        /// <summary>
        /// Required output buffer size (32KiB)
        /// </summary>
        private const int _bufferSize = 0x8000;

        #region Constructors

        /// <summary>
        /// Create a MS-ZIP decompressor
        /// </summary>
        private Decompressor() { }

        /// <summary>
        /// Create a MS-ZIP decompressor
        /// </summary>
        public static Decompressor Create() => new();

        #endregion

        /// <summary>
        /// Decompress source data to an output stream
        /// </summary>
        public bool CopyTo(byte[] source, Stream dest)
            => CopyTo(new MemoryStream(source), dest);

        /// <summary>
        /// Decompress source data to an output stream
        /// </summary>
        public bool CopyTo(Stream source, Stream dest)
        {
            // Ignore unwritable streams
            if (!dest.CanWrite)
                return false;

            // Ignore if the end of the stream is reached
            if (source.Position >= source.Length)
                return false;

            // Validate the header
            var header = new BlockHeader();
            header.Signature = source.ReadUInt16LittleEndian();
            if (header.Signature != 0x4B43)
                throw new InvalidDataException(nameof(source));

            byte[] buffer = new byte[_bufferSize];
            var blockStream = new Deflate.DeflateStream(source, Deflate.CompressionMode.Decompress, leaveOpen: true);
            if (_history != null)
                blockStream.SetDictionary(_history, check: false);

            int read = blockStream.Read(buffer, 0, _bufferSize);
            if (read > 0)
            {
                // Write to output
                dest.Write(buffer, 0, _bufferSize);

                // Save the history for rollover
                _history = new byte[_bufferSize];
                Array.Copy(buffer, _history, _bufferSize);
            }

            // Flush and return
            dest.Flush();
            return true;
        }
    }
}
