// Zlib.cs
// ------------------------------------------------------------------
//
// Copyright (c) 2009-2011 Dino Chiesa and Microsoft Corporation.
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
// Last Saved: <2011-August-03 19:52:28>
//
// ------------------------------------------------------------------
//
// This module defines classes for ZLIB compression and
// decompression. This code is derived from the jzlib implementation of
// zlib, but significantly modified.  The object model is not the same,
// and many of the behaviors are new or different.  Nonetheless, in
// keeping with the license for jzlib, the copyright to that code is
// included below.
//
// ------------------------------------------------------------------
//
// The following notice applies to jzlib:
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
// jzlib is based on zlib-1.1.3.
//
// The following notice applies to zlib:
//
// -----------------------------------------------------------------------
//
// Copyright (C) 1995-2004 Jean-loup Gailly and Mark Adler
//
//   The ZLIB software is provided 'as-is', without any express or implied
//   warranty.  In no event will the authors be held liable for any damages
//   arising from the use of this software.
//
//   Permission is granted to anyone to use this software for any purpose,
//   including commercial applications, and to alter it and redistribute it
//   freely, subject to the following restrictions:
//
//   1. The origin of this software must not be misrepresented; you must not
//      claim that you wrote the original software. If you use this software
//      in a product, an acknowledgment in the product documentation would be
//      appreciated but is not required.
//   2. Altered source versions must be plainly marked as such, and must not be
//      misrepresented as being the original software.
//   3. This notice may not be removed or altered from any source distribution.
//
//   Jean-loup Gailly jloup@gzip.org
//   Mark Adler madler@alumni.caltech.edu
//
// -----------------------------------------------------------------------

namespace SabreTools.IO.Compression.Deflate
{
    internal static class InternalConstants
    {
        internal static readonly int MAX_BITS     = 15;
        internal static readonly int BL_CODES     = 19;
        internal static readonly int D_CODES      = 30;
        internal static readonly int LITERALS     = 256;
        internal static readonly int LENGTH_CODES = 29;
        internal static readonly int L_CODES      = (LITERALS + 1 + LENGTH_CODES);

        // Bit length codes must not exceed MAX_BL_BITS bits
        internal static readonly int MAX_BL_BITS  = 7;

        // repeat previous bit length 3-6 times (2 bits of repeat count)
        internal static readonly int REP_3_6      = 16;

        // repeat a zero length 3-10 times  (3 bits of repeat count)
        internal static readonly int REPZ_3_10    = 17;

        // repeat a zero length 11-138 times  (7 bits of repeat count)
        internal static readonly int REPZ_11_138  = 18;

    }

}