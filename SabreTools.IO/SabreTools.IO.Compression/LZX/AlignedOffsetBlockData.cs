namespace SabreTools.IO.Compression.LZX
{
    /// <summary>
    /// An aligned offset block is identical to the verbatim block except for the presence of the aligned offset
    /// tree preceding the other trees.
    /// </summary>
    /// <see href="https://interoperability.blob.core.windows.net/files/MS-PATCH/%5bMS-PATCH%5d.pdf"/>
    internal class AlignedOffsetBlockData : BlockData
    {
        /// <summary>
        /// Aligned offset tree
        /// </summary>
        /// <remarks>8 elements, 3 bits each</remarks>
        public byte[]? AlignedOffsetTree { get; set; }

        /// <summary>
        /// Pretree for first 256 elements of main tree
        /// </summary>
        /// <remarks>20 elements, 4 bits each</remarks>
        public byte[]? PretreeFirst256 { get; set; }

        /// <summary>
        /// Path lengths of first 256 elements of main tree
        /// </summary>
        /// <remarks>Encoded using pretree</remarks>
        public int[]? PathLengthsFirst256 { get; set; }

        /// <summary>
        /// Pretree for remainder of main tree
        /// </summary>
        /// <remarks>20 elements, 4 bits each</remarks>
        public byte[]? PretreeRemainder { get; set; }

        /// <summary>
        /// Path lengths of remaining elements of main tree
        /// </summary>
        /// <remarks>Encoded using pretree</remarks>
        public int[]? PathLengthsRemainder { get; set; }

        /// <summary>
        /// Pretree for length tree
        /// </summary>
        /// <remarks>20 elements, 4 bits each</remarks>
        public byte[]? PretreeLengthTree { get; set; }

        /// <summary>
        /// Path lengths of elements in length tree
        /// </summary>
        /// <remarks>Encoded using pretree</remarks>
        public int[]? PathLengthsLengthTree { get; set; }

        /// <summary>
        /// Token sequence (matches and literals)
        /// </summary>
        /// <remarks>Variable</remarks>
        public byte[]? TokenSequence { get; set; }
    }
}
