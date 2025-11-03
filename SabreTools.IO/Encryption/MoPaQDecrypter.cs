using System;
using System.IO;
using System.Text;
using SabreTools.Hashing;
using SabreTools.IO.Extensions;

namespace SabreTools.IO.Encryption
{
    /// <summary>
    /// Handler for decrypting MoPaQ block and table data
    /// </summary>
    public class MoPaQDecrypter
    {
        #region Constants

        /// <summary>
        /// Converts ASCII characters to lowercase
        /// </summary>
        /// <remarks>Converts slash (0x2F) to backslash (0x5C)</remarks>
        private static readonly byte[] AsciiToLowerTable = new byte[256]
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x5C,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF,
            0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF,
            0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF,
            0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,
            0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF,
            0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF
        };

        /// <summary>
        /// Converts ASCII characters to uppercase
        /// </summary>
        /// <remarks>Converts slash (0x2F) to backslash (0x5C)</remarks>
        private static readonly byte[] AsciiToUpperTable = new byte[256]
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x5C,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,
            0x60, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF,
            0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF,
            0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF,
            0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,
            0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF,
            0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF
        };

        /// <summary>
        /// Converts ASCII characters to uppercase
        /// </summary>
        /// <remarks>Does NOT convert slash (0x2F) to backslash (0x5C)</remarks>
        private static readonly byte[] AsciiToUpperTable_Slash = new byte[256]
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,
            0x60, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF,
            0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF,
            0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF,
            0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,
            0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF,
            0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF
        };

        private const uint MPQ_HASH_TABLE_INDEX = 0x000;

        private const uint MPQ_HASH_NAME_A = 0x100;

        private const uint MPQ_HASH_NAME_B = 0x200;

        private const uint MPQ_HASH_FILE_KEY = 0x300;

        private const uint MPQ_HASH_KEY2_MIX = 0x400;

        private const uint STORM_BUFFER_SIZE = 0x500;

        #endregion

        #region Private Instance Variables

        /// <summary>
        /// Buffer for encryption and decryption
        /// </summary>
        private readonly uint[] _stormBuffer = new uint[STORM_BUFFER_SIZE];

        #endregion

        public MoPaQDecrypter()
        {
            PrepareCryptTable();
        }

        /// <summary>
        /// Prepare the encryption table
        /// </summary>
        private void PrepareCryptTable()
        {
            uint seed = 0x00100001;
            for (uint index1 = 0; index1 < 0x100; index1++)
            {
                for (uint index2 = index1, i = 0; i < 5; i++, index2 += 0x100)
                {
                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    uint temp1 = (seed & 0xFFFF) << 0x10;

                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    uint temp2 = (seed & 0xFFFF);

                    _stormBuffer[index2] = (temp1 | temp2);
                }
            }
        }

        /// <summary>
        /// Load a table block by optionally decompressing and
        /// decrypting before returning the data.
        /// </summary>
        /// <param name="data">Stream to parse</param>
        /// <param name="offset">Data offset to parse</param>
        /// <param name="expectedHash">Optional MD5 hash for validation</param>
        /// <param name="compressedSize">Size of the table in the file</param>
        /// <param name="tableSize">Expected size of the table</param>
        /// <param name="key">Encryption key to use</param>
        /// <param name="realTableSize">Output represening the real table size</param>
        /// <returns>Byte array representing the processed table</returns>
        public byte[]? LoadTable(Stream data,
            long offset,
            byte[]? expectedHash,
            uint compressedSize,
            uint tableSize,
            uint key,
            out long realTableSize)
        {
            byte[]? tableData;
            byte[]? readBytes;
            long bytesToRead = tableSize;

            // Allocate the MPQ table
            tableData = readBytes = new byte[tableSize];

            // Check if the MPQ table is compressed
            if (compressedSize != 0 && compressedSize < tableSize)
            {
                // Allocate temporary buffer for holding compressed data
                readBytes = new byte[compressedSize];
                bytesToRead = compressedSize;
            }

            // Get the file offset from which we will read the table
            // Note: According to Storm.dll from Warcraft III (version 2002),
            // if the hash table position is 0xFFFFFFFF, no SetFilePointer call is done
            // and the table is loaded from the current file offset
            if (offset == 0xFFFFFFFF)
                offset = data.Position;

            // Is the sector table within the file?
            if (offset >= data.Length)
            {
                realTableSize = 0;
                return null;
            }

            // The hash table and block table can go beyond EOF.
            // Storm.dll reads as much as possible, then fills the missing part with zeros.
            // Abused by Spazzler map protector which sets hash table size to 0x00100000
            // Abused by NP_Protect in MPQs v4 as well
            if ((offset + bytesToRead) > data.Length)
                bytesToRead = (uint)(data.Length - offset);

            // Give the caller information that the table was cut
            realTableSize = bytesToRead;

            // If everything succeeded, read the raw table from the MPQ
            data.Seek(offset, SeekOrigin.Begin);
            _ = data.Read(readBytes, 0, (int)bytesToRead);

            // Verify the MD5 of the table, if present
            byte[]? actualHash = HashTool.GetByteArrayHashArray(readBytes, HashType.MD5);
            if (expectedHash != null && actualHash != null && !actualHash.EqualsExactly(expectedHash))
            {
                Console.WriteLine("Table is corrupt!");
                return null;
            }

            // First of all, decrypt the table
            if (key != 0)
                tableData = DecryptBlock(readBytes, bytesToRead, key);

            // If the table is compressed, decompress it
            if (compressedSize != 0 && compressedSize < tableSize)
            {
                Console.WriteLine("Table is compressed, it will not read properly!");
                return null;

                // TODO: Handle decompression
                // int cbOutBuffer = (int)tableSize;
                // int cbInBuffer = (int)compressedSize;

                // if (!SCompDecompress2(readBytes, &cbOutBuffer, tableData, cbInBuffer))
                //     errorCode = SErrGetLastError();

                // tableData = readBytes;
            }

            // Return the MPQ table
            return tableData;
        }

        /// <summary>
        /// Decrypt a single block of data
        /// </summary>
        public unsafe byte[] DecryptBlock(byte[] block, long length, uint key)
        {
            uint seed = 0xEEEEEEEE;

            uint[] castBlock = new uint[length >> 2];
            Buffer.BlockCopy(block, 0, castBlock, 0, (int)length);
            int castBlockPtr = 0;

            // Round to uints
            length >>= 2;

            while (length-- > 0)
            {
                seed += _stormBuffer[MPQ_HASH_KEY2_MIX + (key & 0xFF)];
                uint ch = castBlock[castBlockPtr] ^ (key + seed);

                key = ((~key << 0x15) + 0x11111111) | (key >> 0x0B);
                seed = ch + seed + (seed << 5) + 3;
                castBlock[castBlockPtr++] = ch;
            }

            Buffer.BlockCopy(castBlock, 0, block, 0, block.Length >> 2);
            return block;
        }

        #region Hashing

        //
        // Note: Implementation of this function in WorldEdit.exe and storm.dll
        // incorrectly treats the character as signed, which leads to the
        // a buffer underflow if the character in the file name >= 0x80:
        // The following steps happen when *pbKey == 0xBF and hashType == 0x0000
        // (calculating hash index)
        //
        // 1) Result of AsciiToUpperTable_Slash[*pbKey++] is sign-extended to 0xffffffbf
        // 2) The "ch" is added to hashType (0xffffffbf + 0x0000 => 0xffffffbf)
        // 3) The result is used as index to the StormBuffer table,
        // thus dereferences a random value BEFORE the begin of StormBuffer.
        //
        // As result, MPQs containing files with non-ANSI characters will not work between
        // various game versions and localizations. Even WorldEdit, after importing a file
        // with Korean characters in the name, cannot open the file back.
        //

        /// <summary>
        /// Hash a string representing a filename based on the hash type
        /// using upper-case normalization
        /// </summary>
        /// <param name="filename">Filename to hash</param>
        /// <param name="hashType">Hash type to perform</param>
        /// <returns>Value representing the hashed filename</returns>
        public uint HashString(string filename, uint hashType)
        {
            uint seed1 = 0x7FED7FED;
            uint seed2 = 0xEEEEEEEE;

            byte[] key = Encoding.ASCII.GetBytes(filename);
            int keyPtr = 0;
            while (key[keyPtr] != 0)
            {
                // Convert the input character to uppercase
                // Convert slash (0x2F) to backslash (0x5C)
                byte ch = AsciiToUpperTable[key[keyPtr++]];

                seed1 = _stormBuffer[hashType + ch] ^ (seed1 + seed2);
                seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3;
            }

            return seed1;
        }

        /// <summary>
        /// Hash a string representing a filename based on the hash type
        /// using upper-case normalization
        /// </summary>
        /// <param name="filename">Filename to hash</param>
        /// <param name="hashType">Hash type to perform</param>
        /// <returns>Value representing the hashed filename</returns>
        /// <remarks>This preserves slashes when hashing</remarks>
        public uint HashStringSlash(string filename, uint hashType)
        {
            uint seed1 = 0x7FED7FED;
            uint seed2 = 0xEEEEEEEE;

            byte[] key = Encoding.ASCII.GetBytes(filename);
            int keyPtr = 0;
            while (key[keyPtr] != 0)
            {
                // Convert the input character to uppercase
                // DON'T convert slash (0x2F) to backslash (0x5C)
                byte ch = AsciiToUpperTable_Slash[key[keyPtr++]];

                seed1 = _stormBuffer[hashType + ch] ^ (seed1 + seed2);
                seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3;
            }

            return seed1;
        }

        /// <summary>
        /// Hash a string representing a filename based on the hash type
        /// using lower-case normalization
        /// </summary>
        /// <param name="filename">Filename to hash</param>
        /// <param name="hashType">Hash type to perform</param>
        /// <returns>Value representing the hashed filename</returns>
        public uint HashStringLower(string filename, uint hashType)
        {
            uint seed1 = 0x7FED7FED;
            uint seed2 = 0xEEEEEEEE;

            byte[] key = Encoding.ASCII.GetBytes(filename);
            int keyPtr = 0;
            while (key[keyPtr] != 0)
            {
                // Convert the input character to lower
                // DON'T convert slash (0x2F) to backslash (0x5C)
                byte ch = AsciiToLowerTable[key[keyPtr++]];

                seed1 = _stormBuffer[hashType + ch] ^ (seed1 + seed2);
                seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3;
            }

            return seed1;
        }

        #endregion
    }
}
