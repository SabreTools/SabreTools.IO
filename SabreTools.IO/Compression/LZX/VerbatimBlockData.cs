namespace SabreTools.IO.Compression.LZX
{
    /// <summary>
    /// The fields of a verbatim block that follow the generic block header
    /// </summary>
    /// <see href="https://interoperability.blob.core.windows.net/files/MS-PATCH/%5bMS-PATCH%5d.pdf"/>
    internal class VerbatimBlockData : BlockData
    {
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