using System;
using SabreTools.Numerics.Extensions;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Align the array position to a byte-size boundary
        /// </summary>
        /// <param name="input">Input array to try aligning</param>
        /// <param name="offset">Offset into the byte array</param>
        /// <param name="alignment">Number of bytes to align on</param>
        /// <returns>True if the array could be aligned, false otherwise</returns>
        public static bool AlignToBoundary(this byte[]? input, ref int offset, byte alignment)
        {
            // If the array is invalid
            if (input is null || input.Length == 0)
                return false;

            // If already at the end of the array
            if (offset >= input.Length)
                return false;

            // Align the stream position
            while (offset % alignment != 0 && offset < input.Length)
            {
                _ = input.ReadByteValue(ref offset);
            }

            // Return if the alignment completed
            return offset % alignment == 0;
        }

        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero
        /// </summary>
        public static bool IsNullOrEmpty(this Array? array)
        {
            return array is null || array.Length == 0;
        }

        /// <summary>
        /// Indicates if an array contains all ASCII numeric digits
        /// </summary>
        public static bool IsNumericArray(this byte[] arr)
        {
            // Empty arrays cannot be numeric
            if (arr.Length == 0)
                return false;

            // '0' to '9'
            return Array.TrueForAll(arr, b => b >= 0x30 && b <= 0x39);
        }

        #region Math

        /// <summary>
        /// Add an integer value to a number represented by a byte array
        /// </summary>
        /// <param name="self">Byte array to add to</param>
        /// <param name="add">Amount to add</param>
        /// <returns>Byte array representing the new value</returns>
        /// <remarks>Assumes array values are in big-endian format</remarks>
        public static byte[] Add(this byte[] self, uint add)
        {
            // If nothing is being added, just return
            if (add == 0)
                return self;

            // Get the big-endian representation of the value
            byte[] addBytes = BitConverter.GetBytes(add);
            Array.Reverse(addBytes);

            // Pad the array out to 16 bytes
            byte[] paddedBytes = new byte[16];
            Array.Copy(addBytes, 0, paddedBytes, 12, 4);

            // If the input is empty, just return the added value
            if (self.Length == 0)
                return paddedBytes;

            return self.Add(paddedBytes);
        }

        /// <summary>
        /// Add two numbers represented by byte arrays
        /// </summary>
        /// <param name="self">Byte array to add to</param>
        /// <param name="add">Amount to add</param>
        /// <returns>Byte array representing the new value</returns>
        /// <remarks>Assumes array values are in big-endian format</remarks>
        public static byte[] Add(this byte[] self, byte[] add)
        {
            // If either input is empty
            if (self.Length == 0 && add.Length == 0)
                return [];
            else if (self.Length > 0 && add.Length == 0)
                return self;
            else if (self.Length == 0 && add.Length > 0)
                return add;

            // Setup the output array
            int outLength = Math.Max(self.Length, add.Length);
            byte[] output = new byte[outLength];

            // Loop adding with carry
            uint carry = 0;
            for (int i = 0; i < outLength; i++)
            {
                int selfIndex = self.Length - i - 1;
                uint selfValue = selfIndex >= 0 ? self[selfIndex] : 0u;

                int addIndex = add.Length - i - 1;
                uint addValue = addIndex >= 0 ? add[addIndex] : 0u;

                uint next = selfValue + addValue + carry;
                carry = next >> 8;

                int outputIndex = output.Length - i - 1;
                output[outputIndex] = (byte)(next & 0xFF);
            }

            return output;
        }

        /// <summary>
        /// Perform a rotate left on a byte array
        /// </summary>
        /// <param name="self">Byte array value to rotate</param>
        /// <param name="numBits">Number of bits to rotate</param>
        /// <returns>Rotated byte array value</returns>
        /// <remarks>Assumes array values are in big-endian format</remarks>
        public static byte[] RotateLeft(this byte[] self, int numBits)
        {
            // If either input is empty
            if (self.Length == 0)
                return [];
            else if (numBits == 0)
                return self;

            byte[] output = new byte[self.Length];
            Array.Copy(self, output, output.Length);

            // Shift by bytes
            while (numBits >= 8)
            {
                byte temp = output[0];
                for (int i = 0; i < output.Length - 1; i++)
                {
                    output[i] = output[i + 1];
                }

                output[output.Length - 1] = temp;
                numBits -= 8;
            }

            // Shift by bits
            if (numBits > 0)
            {
                byte bitMask = (byte)(8 - numBits), carry, wrap = 0;
                for (int i = 0; i < output.Length; i++)
                {
                    carry = (byte)(((255 << bitMask) & output[i]) >> bitMask);

                    // Make sure the first byte carries to the end
                    if (i == 0)
                        wrap = carry;

                    // Otherwise, move to the last byte
                    else
                        output[i - 1] |= carry;

                    // Shift the current bits
                    output[i] <<= numBits;
                }

                // Make sure the wrap happens
                output[output.Length - 1] |= wrap;
            }

            return output;
        }

        /// <summary>
        /// XOR two numbers represented by byte arrays
        /// </summary>
        /// <param name="self">Byte array to XOR to</param>
        /// <param name="xor">Amount to XOR</param>
        /// <returns>Byte array representing the new value</returns>
        /// <remarks>Assumes array values are in big-endian format</remarks>
        public static byte[] Xor(this byte[] self, byte[] xor)
        {
            // If either input is empty
            if (self.Length == 0 && xor.Length == 0)
                return [];
            else if (self.Length > 0 && xor.Length == 0)
                return self;
            else if (self.Length == 0 && xor.Length > 0)
                return xor;

            // Setup the output array
            int outLength = Math.Max(self.Length, xor.Length);
            byte[] output = new byte[outLength];

            // Loop XOR
            for (int i = 0; i < outLength; i++)
            {
                int selfIndex = self.Length - i - 1;
                uint selfValue = selfIndex >= 0 ? self[selfIndex] : 0u;

                int xorIndex = xor.Length - i - 1;
                uint xorValue = xorIndex >= 0 ? xor[xorIndex] : 0u;

                uint next = selfValue ^ xorValue;

                int outputIndex = output.Length - i - 1;
                output[outputIndex] = (byte)(next & 0xFF);
            }

            return output;
        }

        #endregion
    }
}
