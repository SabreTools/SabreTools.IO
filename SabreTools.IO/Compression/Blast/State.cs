using System;
using System.IO;
using static SabreTools.IO.Compression.Blast.Constants;

namespace SabreTools.IO.Compression.Blast
{
    /// <summary>
    /// Input and output state
    /// </summary>
    public class State
    {
        #region Input State

        /// <summary>
        /// Opaque information passed to InputFunction()
        /// </summary>
        private readonly Stream _source;

        /// <summary>
        /// Next input location
        /// </summary>
        private readonly byte[] _input = new byte[MAXWIN];

        /// <summary>
        /// Pointer to the next input location
        /// </summary>
        private int _inputPtr;

        /// <summary>
        /// Available input at in
        /// </summary>
        private uint _available;

        /// <summary>
        /// Bit buffer
        /// </summary>
        public int BitBuf { get; set; }

        /// <summary>
        /// Number of bits in bit buffer
        /// </summary>
        public int BitCnt { get; set; }

        #endregion

        #region Output State

        /// <summary>
        /// Opaque information passed to OutputFunction()
        /// </summary>
        private readonly Stream _dest;

        /// <summary>
        /// Index of next write location in out[]
        /// </summary>
        public uint Next { get; set; }

        /// <summary>
        /// True to check distances (for first 4K)
        /// </summary>
        public bool First { get; set; }

        /// <summary>
        /// Output buffer and sliding window
        /// </summary>
        private readonly byte[] _output = new byte[MAXWIN];

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public State(Stream source, Stream dest)
        {
            _source = source;
            _inputPtr = 0;
            _available = 0;
            BitBuf = 0;
            BitCnt = 0;

            _dest = dest;
            Next = 0;
            First = true;
        }

        /// <summary>
        /// Copy bytes in the output buffer between locations
        /// </summary>
        public void CopyOutputBytes(int to, int from, int len)
        {
            do
            {
                _output[to++] = _output[from++];
            }
            while (--len > 0);
        }

        /// <summary>
        /// Return need bits from the input stream.  This always leaves less than
        /// eight bits in the buffer.  bits() works properly for need == 0.
        /// </summary>
        /// <param name="need">Number of bits to read</param>
        /// <remarks>
        /// Bits are stored in bytes from the least significant bit to the most
        /// significant bit.  Therefore bits are dropped from the bottom of the bit
        /// buffer, using shift right, and new bytes are appended to the top of the
        /// bit buffer, using shift left.
        /// </remarks>
        public int ReadBits(int need)
        {
            // Load at least need bits into val
            int val = BitBuf;
            while (BitCnt < need)
            {
                // Load eight bits
                EnsureAvailable();
                val |= _input[_inputPtr++] << BitCnt;
                _available--;
                BitCnt += 8;
            }

            // Drop need bits and update buffer, always zero to seven bits left
            BitBuf = val >> need;
            BitCnt -= need;

            // Return need bits, zeroing the bits above that
            return val & ((1 << need) - 1);
        }

        /// <summary>
        /// Process output for the current state
        /// </summary>
        /// <returns>True if the output could be added, false otherwise</returns>
        public bool ProcessOutput()
        {
            try
            {
                _dest.Write(_output, 0, (int)Next);
                _dest.Flush();

                Next = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read the next byte from the input buffer
        /// </summary>
        public byte ReadNextByte()
        {
            EnsureAvailable();
            return _input[_inputPtr++];
        }

        /// <summary>
        /// Write a byte value to the output buffer
        /// </summary>
        public void WriteToOutput(byte value)
            => _output[Next++] = value;

        /// <summary>
        /// Ensure there are bytes available, if possible
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private void EnsureAvailable()
        {
            // If there are bytes
            if (_inputPtr < _available)
                return;

            // Read the next block
            _available = (uint)_source.Read(_input, 0, MAXWIN);
            if (_available == 0)
                throw new IndexOutOfRangeException();

            // Reset the pointer
            _inputPtr = 0;
        }
    }
}