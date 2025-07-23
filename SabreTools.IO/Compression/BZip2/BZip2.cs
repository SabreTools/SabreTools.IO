// BZip2InputStream.cs
// ------------------------------------------------------------------
//
// Copyright (c) 2011 Dino Chiesa.
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
// Last Saved: <2011-July-31 11:57:32>
//
// ------------------------------------------------------------------
//
// This module defines the BZip2InputStream class, which is a decompressing
// stream that handles BZIP2. This code is derived from Apache commons source code.
// The license below applies to the original Apache code.
//
// ------------------------------------------------------------------

/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

/*
 * This package is based on the work done by Keiron Liddle, Aftex Software
 * <keiron@aftexsw.com> to whom the Ant project is very grateful for his
 * great code.
 */

// compile: msbuild
// not: csc.exe /t:library /debug+ /out:SabreTools.IO.Compression.BZip2.dll BZip2InputStream.cs BCRC32.cs Rand.cs

namespace SabreTools.IO.Compression.BZip2
{
    // /**
    //  * Checks if the signature matches what is expected for a bzip2 file.
    //  *
    //  * @param signature
    //  *            the bytes to check
    //  * @param length
    //  *            the number of bytes to check
    //  * @return true, if this stream is a bzip2 compressed stream, false otherwise
    //  *
    //  * @since Apache Commons Compress 1.1
    //  */
    // public static boolean MatchesSig(byte[] signature)
    // {
    //     if ((signature.Length < 3) ||
    //         (signature[0] != 'B') ||
    //         (signature[1] != 'Z') ||
    //         (signature[2] != 'h'))
    //         return false;
    //
    //     return true;
    // }

    internal static class BZip2
    {
        internal static T[][] InitRectangularArray<T>(int d1, int d2)
        {
            var x = new T[d1][];
            for (int i = 0; i < d1; i++)
            {
                x[i] = new T[d2];
            }
            return x;
        }

        public static readonly int BlockSizeMultiple = 100000;
        public static readonly int MinBlockSize = 1;
        public static readonly int MaxBlockSize = 9;
        public static readonly int MaxAlphaSize = 258;
        public static readonly int MaxCodeLength = 23;
        public static readonly char RUNA = (char)0;
        public static readonly char RUNB = (char)1;
        public static readonly int NGroups = 6;
        public static readonly int G_SIZE = 50;
        public static readonly int N_ITERS = 4;
        public static readonly int MaxSelectors = (2 + (900000 / G_SIZE));
        public static readonly int NUM_OVERSHOOT_BYTES = 20;
        /*
         * <p> If you are ever unlucky/improbable enough to get a stack
         * overflow whilst sorting, increase the following constant and
         * try again. In practice I have never seen the stack go above 27
         * elems, so the following limit seems very generous.  </p>
         */
        internal static readonly int QSORT_STACK_SIZE = 1000;


    }

}