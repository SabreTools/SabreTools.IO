using System;
using System.IO;
using SabreTools.IO.Extensions;
using SabreTools.Models.Compression.MSZIP;

namespace SabreTools.IO.Compression.MSZIP
{
    /// <see href="https://msopenspecs.azureedge.net/files/MS-MCI/%5bMS-MCI%5d.pdf"/>
    public class Decompressor
    {
        /// <summary>
        /// Last uncompressed block data
        /// </summary>
        private byte[]? _history = null;

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

            // Validate the header
            var header = new BlockHeader();
            header.Signature = source.ReadUInt16LittleEndian();
            if (header.Signature != 0x4B43)
                throw new InvalidDataException(nameof(source));

            byte[] buffer = new byte[32 * 1024];
            var blockStream = new Deflate.DeflateStream(source, Deflate.CompressionMode.Decompress);
            if (_history != null)
                blockStream.SetDictionary(_history, check: false);

            int read = blockStream.Read(buffer, 0, buffer.Length);
            if (read > 0)
            {
                // Write to output
                dest.Write(buffer, 0, read);

                // Save the history for rollover
                _history = new byte[read];
                Array.Copy(buffer, _history, read);
            }

            // Flush and return
            dest.Flush();
            return true;
        }
    }
}