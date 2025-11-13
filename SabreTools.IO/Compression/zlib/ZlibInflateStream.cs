using System;
using System.IO;

namespace SabreTools.IO.Compression.zlib
{
    public class ZlibInflateStream : Stream
    {
        private readonly bool _leaveOpen;
        private ZLib.z_stream_s? _s;
        private long _p;
        private byte[]? _b;
        private bool _complete;

        public ZlibInflateStream(Stream baseStream) : this(0, false, 0, baseStream, false)
        {
        }

        public ZlibInflateStream(Stream baseStream, bool leaveOpen) : this(0, false, 0, baseStream, leaveOpen)
        {
        }
        public ZlibInflateStream(bool headerless, Stream baseStream, bool leaveOpen) : this(0, headerless, 0, baseStream, leaveOpen)
        {
        }
        public ZlibInflateStream(bool headerless, int bufferSize, Stream baseStream, bool leaveOpen) : this(0, headerless, bufferSize, baseStream, leaveOpen)
        {
        }

        public ZlibInflateStream(int bufferSize, Stream baseStream, bool leaveOpen) : this(0, false, bufferSize, baseStream, leaveOpen)
        {
        }

        public ZlibInflateStream(long maxRead, bool headerless, int bufferSize, Stream baseStream, bool leaveOpen)
        {
            this.MaxRead = maxRead == 0 ? int.MaxValue : maxRead;
            this.Headerless = headerless;
            this.BaseStream = baseStream;
            _leaveOpen = leaveOpen;
            _s = null;
            _b = new byte[bufferSize == 0 ? 0x10000 : bufferSize];
            _complete = false;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _p;

        public override long Position { get => _p; set => throw new NotImplementedException(); }
        public long MaxRead { get; private set; }
        public bool Headerless { get; }
        public Stream BaseStream { get; }
        public string Version { get => ZLib.zlibVersion(); }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        unsafe public override int Read(byte[] buffer, int offset, int count)
        {

            if (buffer == null) throw new ArgumentNullException();
            if (offset < 0 || count < 0) throw new ArgumentOutOfRangeException();
            if ((offset + count) > buffer.Length) throw new ArgumentException();
            if (_complete)
                return 0;

            int err = 0;
            int hdr = 0;
            if (_s == null)
            {
                _s = new ZLib.z_stream_s();
                ZLib.inflateInit_(_s, this.Version, 0); //0 = sizeof(z_stream_s) not used
                if (this.Headerless)
                {
                    _b![0] = 0x78;
                    _b[1] = 0x9c; //da
                    hdr = 2;
                }
                _s.total_in = 0u;
                _s.total_out = 0u;
                _s.avail_in = 0u;
                _s.avail_out = 0u;
            }

            int read;
            _s.avail_out = (uint)count;

            fixed (byte* i = _b, o = buffer)
            {
                _s.next_in = i + _s.total_in;
                _s.next_out = o;

                while (err == 0 && (_s.avail_out != 0 && !_complete)) //process the buffer
                {
                    if (_s.avail_in == 0) //get more data
                    {
                        _s.total_in = 0;
                        read = (int)Math.Min(this.MaxRead - _p, (long)_b!.Length);
                        if (hdr != 0) //test once to save on the extra calculations
                        {
                            _s.avail_in = (uint)(hdr + (read = BaseStream.Read(_b, hdr, Math.Min(read, _b.Length - hdr))));
                            hdr = 0;
                        }
                        else
                            _s.avail_in = (uint)(read = BaseStream.Read(_b, 0, read));
                        _complete = read == 0;
                        _p += (long)read;
                        _s.next_in = i;
                    }

                    if (_s.avail_in != 0 || (!_complete && _s.total_out != 0))
                        err = ZLib.inflate(_s, 2);
                }
            }

            uint ret = _s.total_out;
            _s.total_out = 0u;
            return (int)ret;
        }

        /// <summary>
        /// Allow blocks to be read from the base stream without overreading. Call when write is finished with.
        /// Used for reading block seekable files. The caller must manage blocks, indexes and lengths. Seek the BaseStream
        /// </summary>
        public long BlockFlush(int maxRead)
        {
            this.MaxRead = maxRead;
            if (_s != null)
            {
                ZLib.deflateEnd(_s);
                _s = null;
            }
            _complete = false;
            long ret = _p;
            _p = 0;
            return ret;
        }

        protected override void Dispose(bool disposing)
        {
            BlockFlush(0);
            _complete = true;
            _b = null;
            if (!_leaveOpen)
                this.BaseStream.Dispose();
        }
    }
}
