using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Compression.zlib
{
    public class ZlibDeflateStream : Stream
    {
        private readonly bool _leaveOpen;
        private ZLib.z_stream_s? _s;
        private long _p;
        private byte[]? _b;

        public ZlibDeflateStream(int level, Stream baseStream) : this(level, false, 0, baseStream, false)
        {
        }

        public ZlibDeflateStream(int level, Stream baseStream, bool leaveOpen) : this(level, false, 0, baseStream, leaveOpen)
        {
        }
        public ZlibDeflateStream(int level, bool headerless, Stream baseStream, bool leaveOpen) : this(level, headerless, 0, baseStream, leaveOpen)
        {
        }
        public ZlibDeflateStream(int level, int bufferSize, Stream baseStream, bool leaveOpen) : this(level, false, bufferSize, baseStream, leaveOpen)
        {
        }

        public ZlibDeflateStream(int level, bool headerless, int bufferSize, Stream baseStream, bool leaveOpen)
        {
            this.Level = level;
            this.Headerless = headerless;
            this.BaseStream = baseStream;
            _leaveOpen = leaveOpen;
            _s = null;
            _b = new byte[bufferSize == 0 ? 0x10000 : bufferSize];
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => _p;

        public override long Position { get => _p; set => throw new NotImplementedException(); }

        public int Level { get; }
        public bool Headerless { get; }
        public Stream BaseStream { get; }
        public string Version { get => ZLib.zlibVersion(); }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        unsafe public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException();
            if (offset < 0 || count < 0) throw new ArgumentOutOfRangeException();
            if ((offset + count) > buffer.Length) throw new ArgumentException();

            int err = 0;
            int hdr = 0;

            if (_s == null)
            {
                _s = new ZLib.z_stream_s();
                ZLib.deflateInit_(_s, this.Level, this.Version, 0); //0 = sizeof(z_stream_s) not used
                if (this.Headerless)
                    hdr = 2;
                _s.total_in = 0u;
                _s.total_out = 0u;
                _s.avail_in = 0u;
                _s.avail_out = 0u;
            }

            _s.avail_in = (uint)count;

            fixed (byte* i = buffer, o = _b)
            {
                _s.next_in = i;
                _s.next_out = o + _s.total_out;

                while (err >= 0 && _s.avail_in != 0) //process the buffer
                {
                    if (_s.avail_out == 0) //get more data
                    {
                        if (_s.total_out != 0)
                        {
                            if (hdr != 0)
                            {
                                BaseStream.Write(_b!, hdr, (int)_s.total_out - hdr);
                                _s.total_out -= (uint)hdr;
                                hdr = 0;
                            }
                            else
                                BaseStream.Write(_b!, 0, (int)_s.total_out);
                        }
                        _p += _s.total_out;
                        _s.avail_out = (uint)_b!.Length;
                        _s.next_out = o;
                        _s.total_out = 0;
                    }

                    if (_s.avail_in != 0 || _s.avail_out != 0)
                        err = ZLib.deflate(_s, 2);
                }

            }
        }

        /// <summary>
        /// Allow blocks to be written to the base stream. Call when write is finished with.
        /// Used for creating block seekable files. The caller must manage blocks, indexes and lengths
        /// </summary>
        unsafe public long BlockFlush()
        {
            //finish previous stream
            if (_s != null)
            {
                int err = 0;
                fixed (byte* o = _b)
                {
                    _s.next_in = null;
                    _s.avail_in = 0;
                    _s.next_out = o + _s.total_out; //point to correct location

                    int hdr = _p == 0 && Headerless ? 2 : 0;
                    while (err == 0 && (_s.total_out != 0 || _s.state!.pending != 0))
                    {
                        this.BaseStream.Write(_b!, hdr, (int)_s.total_out - hdr);
                        _s.avail_out = (uint)_b!.Length;
                        _p += _s.total_out - hdr;
                        hdr = 0;
                        _s.next_out = o;
                        _s.total_out = 0;
                        if (_s.state!.pending != 0)
                            err = ZLib.deflate(_s, 2);
                    }

                    err = ZLib.deflate(_s, 4);

                }
                ZLib.deflateEnd(_s);
                _s = null;
            }
            long ret = _p;
            _p = 0;
            return ret;
        }

        unsafe protected override void Dispose(bool disposing)
        {
            this.BlockFlush();
            _b = null;
            if (!_leaveOpen)
                this.BaseStream.Dispose();
        }
    }
}
