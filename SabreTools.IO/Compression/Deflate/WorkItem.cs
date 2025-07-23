//#define Trace

// ParallelDeflateOutputStream.cs
// ------------------------------------------------------------------
//
// A DeflateStream that does compression only, it uses a
// divide-and-conquer approach with multiple threads to exploit multiple
// CPUs for the DEFLATE computation.
//
// last saved: <2011-July-31 14:49:40>
//
// ------------------------------------------------------------------
//
// Copyright (c) 2009-2011 by Dino Chiesa
// All rights reserved!
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

#nullable disable
#pragma warning disable CS0649
namespace SabreTools.IO.Compression.Deflate
{
    internal class WorkItem
    {
        public byte[] buffer;
        public byte[] compressed;
        public int crc;
        public int index;
        public int ordinal;
        public int inputBytesAvailable;
        public int compressedBytesAvailable;
        public ZlibCodec compressor;

        public WorkItem(int size,
                        SabreTools.IO.Compression.Deflate.CompressionLevel compressLevel,
                        CompressionStrategy strategy,
                        int ix)
        {
            this.buffer = new byte[size];
            // alloc 5 bytes overhead for every block (margin of safety= 2)
            int n = size + ((size / 32768) + 1) * 5 * 2;
            this.compressed = new byte[n];
            this.compressor = new ZlibCodec();
            this.compressor.InitializeDeflate(compressLevel, false);
            this.compressor.OutputBuffer = this.compressed;
            this.compressor.InputBuffer = this.buffer;
            this.index = ix;
        }
    }

}


