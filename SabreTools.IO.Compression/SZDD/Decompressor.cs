using System;
using System.IO;

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
        private readonly BufferedStreamReader _source;

        /// <summary>
        /// SZDD format being decompressed
        /// </summary>
        private Format _format;

        #region Constructors

        /// <summary>
        /// Create a SZDD decompressor
        /// </summary>
        /// <param name="source">Source data stream</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="source"/> has a length of 0.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="source"/> is not marked as readable.
        /// </exception>
        private Decompressor(Stream source)
        {
            // Validate the inputs
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!source.CanRead)
                throw new InvalidOperationException(nameof(source));

            // Initialize the window with space characters
            _window = Array.ConvertAll(_window, b => (byte)0x20);
            _source = new BufferedStreamReader(source);
        }

        /// <summary>
        /// Create a KWAJ decompressor
        /// </summary>
        public static Decompressor CreateKWAJ(byte[] source, ushort compressionType)
            => CreateKWAJ(new MemoryStream(source), compressionType);

        /// <summary>
        /// Create a KWAJ decompressor
        /// </summary>
        /// <param name="source">Source data stream</param>
        /// <param name="compressionType">Compression type value</param>
        /// <returns>Decompressor representing the compression type</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="compressionType"/> is not between 0x0000 and 0x0004.
        /// </exception>
        public static Decompressor CreateKWAJ(Stream source, ushort compressionType)
        {
            // Create the decompressor
            var decompressor = new Decompressor(source);

            // Set the format and return
            decompressor._format = compressionType switch
            {
                0x0000 => Format.KWAJNoCompression,
                0x0001 => Format.KWAJXor,
                0x0002 => Format.KWAJQBasic,
                0x0003 => Format.KWAJLZH,
                0x0004 => Format.KWAJMSZIP,
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
            // Create the decompressors
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
                if (control is null)
                    break;

                for (int cbit = 0x01; (cbit & 0xFF) != 0; cbit <<= 1)
                {
                    // Literal value
                    if ((control & cbit) != 0)
                    {
                        // Read the literal byte
                        byte? literal = _source.ReadNextByte();
                        if (literal is null)
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
                    if (matchpos is null)
                        break;

                    // Read the match length
                    int? matchlen = _source.ReadNextByte();
                    if (matchlen is null)
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
                if (next is null)
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
    }
}
