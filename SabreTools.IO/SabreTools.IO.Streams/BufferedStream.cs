using System.IO;

namespace SabreTools.IO.Streams
{
    /// <summary>
    /// Buffered stream that reads in blocks
    /// </summary>
    /// <remarks>Not a true <see cref="Stream"/> implementation yet</remarks>
    public class BufferedStream
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
