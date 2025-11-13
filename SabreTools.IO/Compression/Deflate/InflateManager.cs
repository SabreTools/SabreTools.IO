// Inflate.cs
// ------------------------------------------------------------------
//
// Copyright (c) 2009 Dino Chiesa and Microsoft Corporation.
// All rights reserved.
//
// This code module is part of DotNetZip, a zipfile class library.
//
// ------------------------------------------------------------------
//
// This code is licensed under the Microsoft Public License.
// See the file License.txt for the license details.
// More info on: http://dotnetzip.codeplex.com
//
// ------------------------------------------------------------------
//
// last saved (in emacs):
// Time-stamp: <2010-January-08 18:32:12>
//
// ------------------------------------------------------------------
//
// This module defines classes for decompression. This code is derived
// from the jzlib implementation of zlib, but significantly modified.
// The object model is not the same, and many of the behaviors are
// different.  Nonetheless, in keeping with the license for jzlib, I am
// reproducing the copyright to that code here.
//
// ------------------------------------------------------------------
//
// Copyright (c) 2000,2001,2002,2003 ymnk, JCraft,Inc. All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright
// notice, this list of conditions and the following disclaimer in
// the documentation and/or other materials provided with the distribution.
//
// 3. The names of the authors may not be used to endorse or promote products
// derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL JCRAFT,
// INC. OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// -----------------------------------------------------------------------
//
// This program is based on zlib-1.1.3; credit to authors
// Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
// and contributors of zlib.
//
// -----------------------------------------------------------------------


using System;

#nullable disable
namespace SabreTools.IO.Compression.Deflate
{
    internal sealed class InflateManager
    {
        // preset dictionary flag in zlib header
        private const int PRESET_DICT = 0x20;

        private const int Z_DEFLATED = 8;

        private enum InflateManagerMode
        {
            METHOD = 0,  // waiting for method byte
            FLAG = 1,  // waiting for flag byte
            DICT4 = 2,  // four dictionary check bytes to go
            DICT3 = 3,  // three dictionary check bytes to go
            DICT2 = 4,  // two dictionary check bytes to go
            DICT1 = 5,  // one dictionary check byte to go
            DICT0 = 6,  // waiting for inflateSetDictionary
            BLOCKS = 7,  // decompressing blocks
            CHECK4 = 8,  // four check bytes to go
            CHECK3 = 9,  // three check bytes to go
            CHECK2 = 10, // two check bytes to go
            CHECK1 = 11, // one check byte to go
            DONE = 12, // finished check, done
            BAD = 13, // got an error--stay here
        }

        private InflateManagerMode mode; // current inflate mode
        internal ZlibCodec _codec; // pointer back to this zlib stream

        // mode dependent information
        internal int method; // if FLAGS, method byte

        // if CHECK, check values to compare
        internal uint computedCheck; // computed check value
        internal uint expectedCheck; // stream check value

        // if BAD, inflateSync's marker bytes count
        internal int marker;

        // mode independent information
        //internal int nowrap; // flag for no wrapper
        private bool _handleRfc1950HeaderBytes = true;
        internal bool HandleRfc1950HeaderBytes
        {
            get { return _handleRfc1950HeaderBytes; }
            set { _handleRfc1950HeaderBytes = value; }
        }
        internal int wbits; // log2(window size)  (8..15, defaults to 15)

        internal InflateBlocks blocks; // current inflate_blocks state

        public InflateManager() { }

        public InflateManager(bool expectRfc1950HeaderBytes)
        {
            _handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
        }

        internal int Reset()
        {
            _codec.TotalBytesIn = _codec.TotalBytesOut = 0;
            _codec.Message = null;
            mode = HandleRfc1950HeaderBytes ? InflateManagerMode.METHOD : InflateManagerMode.BLOCKS;
            blocks.Reset();
            return ZlibConstants.Z_OK;
        }

        internal int End()
        {
            blocks?.Free();
            blocks = null;
            return ZlibConstants.Z_OK;
        }

        internal int Initialize(ZlibCodec codec, int w)
        {
            _codec = codec;
            _codec.Message = null;
            blocks = null;

            // handle undocumented nowrap option (no zlib header or check)
            //nowrap = 0;
            //if (w < 0)
            //{
            //    w = - w;
            //    nowrap = 1;
            //}

            // set window size
            if (w < 8 || w > 15)
            {
                End();
                throw new ZlibException("Bad window size.");

                //return ZlibConstants.Z_STREAM_ERROR;
            }
            wbits = w;

            blocks = new InflateBlocks(codec,
                HandleRfc1950HeaderBytes ? this : null,
                1 << w);

            // reset state
            Reset();
            return ZlibConstants.Z_OK;
        }


        internal int Inflate(FlushType flush)
        {
            int b;

            if (_codec.InputBuffer == null)
                throw new ZlibException("InputBuffer is null. ");

            //             int f = (flush == FlushType.Finish)
            //                 ? ZlibConstants.Z_BUF_ERROR
            //                 : ZlibConstants.Z_OK;

            // workitem 8870
            int f = ZlibConstants.Z_OK;
            int r = ZlibConstants.Z_BUF_ERROR;

            while (true)
            {
                switch (mode)
                {
                    case InflateManagerMode.METHOD:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        if (((method = _codec.InputBuffer[_codec.NextIn++]) & 0xf) != Z_DEFLATED)
                        {
                            mode = InflateManagerMode.BAD;
                            _codec.Message = String.Format("unknown compression method (0x{0:X2})", method);
                            marker = 5; // can't try inflateSync
                            break;
                        }
                        if ((method >> 4) + 8 > wbits)
                        {
                            mode = InflateManagerMode.BAD;
                            _codec.Message = String.Format("invalid window size ({0})", (method >> 4) + 8);
                            marker = 5; // can't try inflateSync
                            break;
                        }
                        mode = InflateManagerMode.FLAG;
                        break;


                    case InflateManagerMode.FLAG:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        b = (_codec.InputBuffer[_codec.NextIn++]) & 0xff;

                        if ((((method << 8) + b) % 31) != 0)
                        {
                            mode = InflateManagerMode.BAD;
                            _codec.Message = "incorrect header check";
                            marker = 5; // can't try inflateSync
                            break;
                        }

                        mode = ((b & PRESET_DICT) == 0)
                            ? InflateManagerMode.BLOCKS
                            : InflateManagerMode.DICT4;
                        break;

                    case InflateManagerMode.DICT4:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        expectedCheck = (uint)((_codec.InputBuffer[_codec.NextIn++] << 24) & 0xff000000);
                        mode = InflateManagerMode.DICT3;
                        break;

                    case InflateManagerMode.DICT3:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 16) & 0x00ff0000);
                        mode = InflateManagerMode.DICT2;
                        break;

                    case InflateManagerMode.DICT2:

                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 8) & 0x0000ff00);
                        mode = InflateManagerMode.DICT1;
                        break;


                    case InflateManagerMode.DICT1:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--; _codec.TotalBytesIn++;
                        expectedCheck += (uint)(_codec.InputBuffer[_codec.NextIn++] & 0x000000ff);
                        _codec._Adler32 = expectedCheck;
                        mode = InflateManagerMode.DICT0;
                        return ZlibConstants.Z_NEED_DICT;


                    case InflateManagerMode.DICT0:
                        mode = InflateManagerMode.BAD;
                        _codec.Message = "need dictionary";
                        marker = 0; // can try inflateSync
                        return ZlibConstants.Z_STREAM_ERROR;


                    case InflateManagerMode.BLOCKS:
                        r = blocks.Process(r);
                        if (r == ZlibConstants.Z_DATA_ERROR)
                        {
                            mode = InflateManagerMode.BAD;
                            marker = 0; // can try inflateSync
                            break;
                        }

                        if (r == ZlibConstants.Z_OK) r = f;

                        if (r != ZlibConstants.Z_STREAM_END)
                            return r;

                        r = f;
                        computedCheck = blocks.Reset();
                        if (!HandleRfc1950HeaderBytes)
                        {
                            mode = InflateManagerMode.DONE;
                            return ZlibConstants.Z_STREAM_END;
                        }
                        mode = InflateManagerMode.CHECK4;
                        break;

                    case InflateManagerMode.CHECK4:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        expectedCheck = (uint)((_codec.InputBuffer[_codec.NextIn++] << 24) & 0xff000000);
                        mode = InflateManagerMode.CHECK3;
                        break;

                    case InflateManagerMode.CHECK3:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--; _codec.TotalBytesIn++;
                        expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 16) & 0x00ff0000);
                        mode = InflateManagerMode.CHECK2;
                        break;

                    case InflateManagerMode.CHECK2:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--;
                        _codec.TotalBytesIn++;
                        expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 8) & 0x0000ff00);
                        mode = InflateManagerMode.CHECK1;
                        break;

                    case InflateManagerMode.CHECK1:
                        if (_codec.AvailableBytesIn == 0) return r;
                        r = f;
                        _codec.AvailableBytesIn--; _codec.TotalBytesIn++;
                        expectedCheck += (uint)(_codec.InputBuffer[_codec.NextIn++] & 0x000000ff);
                        if (computedCheck != expectedCheck)
                        {
                            mode = InflateManagerMode.BAD;
                            _codec.Message = "incorrect data check";
                            marker = 5; // can't try inflateSync
                            break;
                        }
                        mode = InflateManagerMode.DONE;
                        return ZlibConstants.Z_STREAM_END;

                    case InflateManagerMode.DONE:
                        return ZlibConstants.Z_STREAM_END;

                    case InflateManagerMode.BAD:
                        throw new ZlibException(String.Format("Bad state ({0})", _codec.Message));

                    default:
                        throw new ZlibException("Stream error.");

                }
            }
        }



        internal int SetDictionary(byte[] dictionary, bool check = true)
        {
            int index = 0;
            int length = dictionary.Length;

            if (check)
            {
                if (mode != InflateManagerMode.DICT0)
                    throw new ZlibException("Stream error.");

                if (Adler.Adler32(1, dictionary, 0, dictionary.Length) != _codec._Adler32)
                {
                    return ZlibConstants.Z_DATA_ERROR;
                }
            }

            _codec._Adler32 = Adler.Adler32(0, null, 0, 0);

            if (length >= (1 << wbits))
            {
                length = (1 << wbits) - 1;
                index = dictionary.Length - length;
            }
            blocks.SetDictionary(dictionary, index, length);
            mode = InflateManagerMode.BLOCKS;
            return ZlibConstants.Z_OK;
        }


        private static readonly byte[] mark = [0, 0, 0xff, 0xff];

        internal int Sync()
        {
            int n; // number of bytes to look at
            int p; // pointer to bytes
            int m; // number of marker bytes found in a row
            long r, w; // temporaries to save total_in and total_out

            // set up
            if (mode != InflateManagerMode.BAD)
            {
                mode = InflateManagerMode.BAD;
                marker = 0;
            }
            if ((n = _codec.AvailableBytesIn) == 0)
                return ZlibConstants.Z_BUF_ERROR;
            p = _codec.NextIn;
            m = marker;

            // search
            while (n != 0 && m < 4)
            {
                if (_codec.InputBuffer[p] == mark[m])
                {
                    m++;
                }
                else if (_codec.InputBuffer[p] != 0)
                {
                    m = 0;
                }
                else
                {
                    m = 4 - m;
                }
                p++; n--;
            }

            // restore
            _codec.TotalBytesIn += p - _codec.NextIn;
            _codec.NextIn = p;
            _codec.AvailableBytesIn = n;
            marker = m;

            // return no joy or set up to restart on a new block
            if (m != 4)
            {
                return ZlibConstants.Z_DATA_ERROR;
            }
            r = _codec.TotalBytesIn;
            w = _codec.TotalBytesOut;
            Reset();
            _codec.TotalBytesIn = r;
            _codec.TotalBytesOut = w;
            mode = InflateManagerMode.BLOCKS;
            return ZlibConstants.Z_OK;
        }


        // Returns true if inflate is currently at the end of a block generated
        // by Z_SYNC_FLUSH or Z_FULL_FLUSH. This function is used by one PPP
        // implementation to provide an additional safety check. PPP uses Z_SYNC_FLUSH
        // but removes the length bytes of the resulting empty stored block. When
        // decompressing, PPP checks that at the end of input packet, inflate is
        // waiting for these length bytes.
        internal int SyncPoint(ZlibCodec z)
        {
            return blocks.SyncPoint();
        }
    }
}
