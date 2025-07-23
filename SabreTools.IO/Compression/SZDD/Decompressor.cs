using System;
using System.IO;
using SabreTools.Models.LZ;

namespace SabreTools.IO.Compression.SZDD
{
    /// <see href="https://www.cabextract.org.uk/libmspack/doc/szdd_kwaj_format.html"/>
    public class Decompressor
    {
        /// <summary>
        /// Window to deflate data into
        /// </summary>
        private readonly byte[] _window = new byte[4096];

        /// <summary>
        /// Source stream for the decompressor
        /// </summary>
        private readonly BufferedStream _source;

        /// <summary>
        /// SZDD format being decompressed
        /// </summary>
        private Format _format;

        #region Constructors

        /// <summary>
        /// Create a SZDD decompressor
        /// </summary>
        private Decompressor(Stream source)
        {
            // Validate the inputs
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!source.CanRead)
                throw new InvalidOperationException(nameof(source));

            // Initialize the window with space characters
            _window = Array.ConvertAll(_window, b => (byte)0x20);
            _source = new BufferedStream(source);
        }

        /// <summary>
        /// Create a KWAJ decompressor
        /// </summary>
        public static Decompressor CreateKWAJ(byte[] source, KWAJCompressionType compressionType)
            => CreateKWAJ(new MemoryStream(source), compressionType);

        /// <summary>
        /// Create a KWAJ decompressor
        /// </summary>
        public static Decompressor CreateKWAJ(Stream source, KWAJCompressionType compressionType)
        {
            // Create the decompressor
            var decompressor = new Decompressor(source);

            // Set the format and return
            decompressor._format = compressionType switch
            {
                KWAJCompressionType.NoCompression => Format.KWAJNoCompression,
                KWAJCompressionType.NoCompressionXor => Format.KWAJXor,
                KWAJCompressionType.QBasic => Format.KWAJQBasic,
                KWAJCompressionType.LZH => Format.KWAJLZH,
                KWAJCompressionType.MSZIP => Format.KWAJMSZIP,
                _ => throw new IndexOutOfRangeException(nameof(source)),
            };
            return decompressor;
        }

        /// <summary>
        /// Create a QBasic 4.5 installer SZDD decompressor
        /// </summary>
        public static Decompressor CreateQBasic(byte[] source)
            => CreateQBasic(new MemoryStream(source));

        /// <summary>
        /// Create a QBasic 4.5 installer SZDD decompressor
        /// </summary>
        public static Decompressor CreateQBasic(Stream source)
        {
            // Create the decompressor
            var decompressor = new Decompressor(source);

            // Set the format and return
            decompressor._format = Format.QBasic;
            return decompressor;
        }

        /// <summary>
        /// Create a standard SZDD decompressor
        /// </summary>
        public static Decompressor CreateSZDD(byte[] source)
            => CreateSZDD(new MemoryStream(source));

        /// <summary>
        /// Create a standard SZDD decompressor
        /// </summary>
        public static Decompressor CreateSZDD(Stream source)
        {
            // Create the decompressor
            var decompressor = new Decompressor(source);

            // Set the format and return
            decompressor._format = Format.SZDD;
            return decompressor;
        }

        #endregion

        /// <summary>
        /// Decompress source data to an output stream
        /// </summary>
        public bool CopyTo(Stream dest)
        {
            // Ignore unwritable streams
            if (!dest.CanWrite)
                return false;

            // Handle based on the format
            return _format switch
            {
                Format.SZDD => DecompressSZDD(dest, 4096 - 16),
                Format.QBasic => DecompressSZDD(dest, 4096 - 18),
                Format.KWAJNoCompression => CopyKWAJ(dest, xor: false),
                Format.KWAJXor => CopyKWAJ(dest, xor: true),
                Format.KWAJQBasic => DecompressSZDD(dest, 4096 - 18),
                Format.KWAJLZH => false,
                Format.KWAJMSZIP => false,
                _ => false,
            };
        }

        /// <summary>
        /// Decompress using SZDD
        /// </summary>
        private bool DecompressSZDD(Stream dest, int offset)
        {
            // Ignore unwritable streams
            if (!dest.CanWrite)
                return false;

            // Loop and decompress
            while (true)
            {
                // Get the control byte
                byte? control = _source.ReadNextByte();
                if (control == null)
                    break;

                for (int cbit = 0x01; (cbit & 0xFF) != 0; cbit <<= 1)
                {
                    // Literal value
                    if ((control & cbit) != 0)
                    {
                        // Read the literal byte
                        byte? literal = _source.ReadNextByte();
                        if (literal == null)
                            break;

                        // Store the data in the window and write
                        _window[offset] = literal.Value;
                        dest.WriteByte(_window[offset]);

                        // Set the next offset value
                        offset++;
                        offset &= 4095;
                        continue;
                    }

                    // Read the match position
                    int? matchpos = _source.ReadNextByte();
                    if (matchpos == null)
                        break;

                    // Read the match length
                    int? matchlen = _source.ReadNextByte();
                    if (matchlen == null)
                        break;

                    // Adjust the position and length
                    matchpos |= (matchlen & 0xF0) << 4;
                    matchlen = (matchlen & 0x0F) + 3;

                    // Loop over the match length
                    while (matchlen-- > 0)
                    {
                        // Copy the window value and write
                        _window[offset] = _window[matchpos.Value];
                        dest.WriteByte(_window[offset]);

                        // Set the next offset value
                        offset++; matchpos++;
                        offset &= 4095; matchpos &= 4095;
                    }
                }
            }

            // Flush and return
            dest.Flush();
            return true;
        }

        /// <summary>
        /// Copy KWAJ data, optionally using XOR
        /// </summary>
        private bool CopyKWAJ(Stream dest, bool xor)
        {
            // Ignore unwritable streams
            if (!dest.CanWrite)
                return false;

            // Loop and copy
            while (true)
            {
                // Read the next byte
                byte? next = _source.ReadNextByte();
                if (next == null)
                    break;

                // XOR with 0xFF if required
                if (xor)
                    next = (byte)(next ^ 0xFF);

                // Write the byte
                dest.WriteByte(next.Value);
            }

            // Flush and return
            dest.Flush();
            return true;
        }

        /// <summary>
        /// Buffered stream that reads in blocks
        /// </summary>
        private class BufferedStream
        {
            /// <summary>
            /// Source stream for populating the buffer
            /// </summary>
            private readonly Stream _source;

            /// <summary>
            /// Internal buffer to read
            /// </summary>
            private readonly byte[] _buffer = new byte[2048];

            /// <summary>
            /// Current pointer into the buffer
            /// </summary>
            private int _bufferPtr = 0;

            /// <summary>
            /// Represents the number of available bytes
            /// </summary>
            private int _available = -1;

            /// <summary>
            /// Create a new buffered stream
            /// </summary>
            public BufferedStream(Stream source)
            {
                _source = source;
            }

            /// <summary>
            /// Read the next byte from the buffer, if possible
            /// </summary>
            public byte? ReadNextByte()
            {
                // Ensure the buffer first
                if (!EnsureBuffer())
                    return null;

                // Return the next available value
                return _buffer[_bufferPtr++];
            }

            /// <summary>
            /// Ensure the buffer has data to read
            /// </summary>
            private bool EnsureBuffer()
            {
                // Force an update if in the initial state
                if (_available == -1)
                {
                    _available = _source.Read(_buffer, 0, _buffer.Length);
                    _bufferPtr = 0;
                    return _available != 0;
                }

                // If the pointer is out of range
                if (_bufferPtr >= _available)
                {
                    _available = _source.Read(_buffer, 0, _buffer.Length);
                    _bufferPtr = 0;
                    return _available != 0;
                }

                // Otherwise, assume data is available
                return true;
            }
        }
    }
}