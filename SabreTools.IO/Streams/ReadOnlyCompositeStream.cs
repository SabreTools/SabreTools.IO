using System;
using System.Collections.Generic;
using System.IO;

namespace SabreTools.IO.Streams
{
    /// <summary>
    /// Read-only stream wrapper around multiple, consecutive streams
    /// </summary>
    public class ReadOnlyCompositeStream : Stream
    {
        #region Properties

        /// <inheritdoc/>
        public override bool CanRead => true;

        /// <inheritdoc/>
        public override bool CanSeek => true;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <inheritdoc/>
        public override long Length => _length;

        /// <inheritdoc/>
        public override long Position
        {
            get => _position;
            set
            {
                _position = value;
                if (_position < 0)
                    _position = 0;
                else if (_position >= _length)
                    _position = _length - 1;
            }
        }

        #endregion

        #region Instance Variables

        /// <summary>
        /// Internal collection of streams to read from
        /// </summary>
        private readonly List<Stream> _streams;

        /// <summary>
        /// Total length of all internal streams
        /// </summary>
        private long _length;

        /// <summary>
        /// Overall position in the stream wrapper
        /// </summary>
        private long _position;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new, empty ReadOnlyCompositeStream
        /// </summary>
        public ReadOnlyCompositeStream()
        {
            _streams = [];
            _length = 0;
            _position = 0;
        }

        /// <summary>
        /// Create a new ReadOnlyCompositeStream from a single Stream
        /// </summary>
        /// <param name="stream"></param>
        public ReadOnlyCompositeStream(Stream stream)
        {
            _streams = [stream];
            _length = 0;
            _position = 0;

            // Verify the stream and add to the length
            if (!stream.CanRead || !stream.CanSeek)
                throw new ArgumentException($"{nameof(stream)} needs to be readable and seekable");

            _length += stream.Length;
        }

        /// <summary>
        /// Create a new ReadOnlyCompositeStream from an existing collection of Streams
        /// </summary>
        public ReadOnlyCompositeStream(Stream[] streams)
        {
            _streams = [.. streams];
            _length = 0;
            _position = 0;

            // Verify the streams and add to the length
            foreach (var stream in streams)
            {
                if (!stream.CanRead || !stream.CanSeek)
                    throw new ArgumentException($"All members of {nameof(streams)} need to be readable and seekable");

                _length += stream.Length;
            }
        }

        /// <summary>
        /// Create a new ReadOnlyCompositeStream from an existing collection of Streams
        /// </summary>
        public ReadOnlyCompositeStream(IEnumerable<Stream> streams)
        {
            _streams = [.. streams];
            _length = 0;
            _position = 0;

            // Verify the streams and add to the length
            foreach (var stream in streams)
            {
                if (!stream.CanRead || !stream.CanSeek)
                    throw new ArgumentException($"All members of {nameof(streams)} need to be readable and seekable");

                _length += stream.Length;
            }
        }

        #endregion

        #region Data

        /// <summary>
        /// Add a new stream to the collection
        /// </summary>
        public bool AddStream(Stream stream)
        {
            // Verify the stream
            if (!stream.CanRead || !stream.CanSeek)
                return false;

            // Add the stream to the end
            _streams.Add(stream);
            _length += stream.Length;
            return true;
        }

        #endregion

        #region Stream Implementations

        /// <inheritdoc/>
        public override void Flush()
            => throw new NotImplementedException();

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // Determine which stream we start reading from
            int streamIndex = DetermineStreamIndex(_position, out long streamOffset);
            if (streamIndex == -1)
                return 0;

            // Determine if the stream fully contains the requested segment
            bool singleStream = StreamContains(streamIndex, streamOffset, count);

            // If we can read from a single stream
            if (singleStream)
            {
                _position += count;
                _streams[streamIndex].Seek(streamOffset, SeekOrigin.Begin);
                return _streams[streamIndex].Read(buffer, offset, count);
            }

            // For all other cases, we read until there's no more
            int readBytes = 0, originalCount = count;
            while (readBytes < originalCount)
            {
                // Determine how much can be read from the current stream
                long currentBytes = _streams[streamIndex].Length - streamOffset;
                int shouldRead = Math.Min((int)currentBytes, count);

                // Read from the current stream
                _position += shouldRead;
                _streams[streamIndex].Seek(streamOffset, SeekOrigin.Begin);
                readBytes += _streams[streamIndex].Read(buffer, offset, shouldRead);

                // Update the read variables
                offset += shouldRead;
                count -= shouldRead;

                // Move to the next stream
                streamIndex++;
                streamOffset = 0;

                // Validate the next stream exists
                if (streamIndex >= _streams.Count)
                    break;
            }

            // Return the number of bytes that could be read
            return readBytes;
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            // Handle the "seek"
            switch (origin)
            {
                case SeekOrigin.Begin: Position = offset; break;
                case SeekOrigin.Current: Position += offset; break;
                case SeekOrigin.End: Position = _length + offset - 1; break;
                default: throw new ArgumentException($"Invalid value for {nameof(origin)}");
            }

            return Position;
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
            => throw new NotImplementedException();

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotImplementedException();

        #endregion

        #region Helpers

        /// <summary>
        /// Determine the index of the stream that contains a particular offset
        /// </summary>
        /// <param name="realOffset">Output parameter representing the real offset in the stream, -1 on error</param>
        /// <returns>Index of the stream containing the offset, -1 on error</returns>
        private int DetermineStreamIndex(long offset, out long realOffset)
        {
            // If the offset is out of bounds
            if (offset < 0 || offset >= _length)
            {
                realOffset = -1;
                return -1;
            }

            // Seek through until we hit the correct offset
            long currentLength = 0;
            for (int i = 0; i < _streams.Count; i++)
            {
                currentLength += _streams[i].Length;
                if (currentLength > offset)
                {
                    realOffset = offset - (currentLength - _streams[i].Length);
                    return i;
                }
            }

            // Should never happen
            realOffset = -1;
            return -1;
        }

        /// <summary>
        /// Determines if a stream contains a particular segment
        /// </summary>
        private bool StreamContains(int streamIndex, long offset, int length)
        {
            // Ensure the arguments are valid
            if (streamIndex < 0 || streamIndex >= _streams.Count)
                throw new ArgumentOutOfRangeException(nameof(streamIndex));
            if (offset < 0 || offset >= _streams[streamIndex].Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            // Handle the general case
            return _streams[streamIndex].Length - offset >= length;
        }

        #endregion
    }
}
