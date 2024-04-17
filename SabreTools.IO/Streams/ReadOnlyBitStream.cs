using System;
using System.IO;

namespace SabreTools.IO.Streams
{
    /// <summary>
    /// Wrapper to allow reading bits from a source stream
    /// </summary>
    public class ReadOnlyBitStream
    {
        /// <inheritdoc cref="Stream.Position"/>
        public long Position => _source.Position;

        /// <inheritdoc cref="Stream.Length"/>
        public long Length => _source.Length;

        /// <summary>
        /// Original stream source
        /// </summary>
        private readonly Stream _source;

        /// <summary>
        /// Last read byte value from the stream
        /// </summary>
        private byte? _bitBuffer;

        /// <summary>
        /// Index in the byte of the current bit
        /// </summary>
        private int _bitIndex;

        /// <summary>
        /// Create a new BitStream from a source Stream
        /// </summary>
        public ReadOnlyBitStream(Stream source)
        {
            _source = source;
            _bitBuffer = null;
            _bitIndex = 0;

            // Verify the stream
            if (!source.CanRead || !source.CanSeek)
                throw new ArgumentException($"{nameof(source)} needs to be readable and seekable");
        }

        /// <summary>
        /// Discard the current cached byte
        /// </summary>
        public void Discard()
        {
            _bitBuffer = null;
            _bitIndex = 0;
        }

        /// <summary>
        /// Read a single bit, if possible
        /// </summary>
        /// <returns>The next bit encoded in a byte, null on error or end of stream</returns>
        public byte? ReadBit()
        {
            // If we reached the end of the stream
            if (_source.Position >= _source.Length)
                return null;

            // If we don't have a value cached
            if (_bitBuffer == null)
            {
                // Read the next byte, if possible
                _bitBuffer = ReadSourceByte();
                if (_bitBuffer == null)
                    return null;

                // Reset the bit index
                _bitIndex = 0;
            }

            // Get the value by bit-shifting
            int value = _bitBuffer.Value & 0x01;
            _bitBuffer = (byte?)(_bitBuffer >> 1);
            _bitIndex++;

            // Reset the byte if we're at the end
            if (_bitIndex >= 8)
                Discard();

            return (byte)value;
        }

        /// <summary>
        /// Read a multiple bits in LSB, if possible
        /// </summary>
        /// <returns>The next bits encoded in a UInt32, null on error or end of stream</returns>
        public uint? ReadBitsLSB(int bits)
        {
            uint value = 0;
            for (int i = 0; i < bits; i++)
            {
                // Read the next bit
                byte? bitValue = ReadBit();
                if (bitValue == null)
                    return null;

                // Add the bit shifted by the current index
                value += (uint)(bitValue.Value << i);
            }

            return value;
        }

        /// <summary>
        /// Read a multiple bits in MSB, if possible
        /// </summary>
        /// <returns>The next bits encoded in a UInt32, null on error or end of stream</returns>
        public uint? ReadBitsMSB(int bits)
        {
            uint value = 0;
            for (int i = 0; i < bits; i++)
            {
                // Read the next bit
                byte? bitValue = ReadBit();
                if (bitValue == null)
                    return null;

                // Add the bit shifted by the current index
                value = (value << 1) + bitValue.Value;
            }

            return value;
        }

        /// <summary>
        /// Read a byte, if possible
        /// </summary>
        /// <returns>The next byte, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public byte? ReadByte()
        {
            try
            {
                Discard();
                return _source.ReadByteValue();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt16, if possible
        /// </summary>
        /// <returns>The next UInt16, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public ushort? ReadUInt16()
        {
            try
            {
                Discard();
                return _source.ReadUInt16();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt32, if possible
        /// </summary>
        /// <returns>The next UInt32, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public uint? ReadUInt32()
        {
            try
            {
                Discard();
                return _source.ReadUInt32();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt64, if possible
        /// </summary>
        /// <returns>The next UInt64, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public ulong? ReadUInt64()
        {
            try
            {
                Discard();
                return _source.ReadUInt64();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read <paramref name="bytes"/> bytes, if possible
        /// </summary>
        /// <param name="bytes">Number of bytes to read</param>
        /// <returns>The next <paramref name="bytes"/> bytes, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public byte[]? ReadBytes(int bytes)
        {
            try
            {
                Discard();
                return _source.ReadBytes(bytes);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a single byte from the underlying stream, if possible
        /// </summary>
        /// <returns>The next full byte from the stream, null on error or end of stream</returns>
        private byte? ReadSourceByte()
        {
            try
            {
                return _source.ReadByteValue();
            }
            catch
            {
                return null;
            }
        }
    }
}