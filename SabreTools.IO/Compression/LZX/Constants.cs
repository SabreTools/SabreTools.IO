namespace SabreTools.IO.Compression.LZX
{
    internal static class Constants
    {
        /* some constants defined by the LZX specification */
        public const int LZX_MIN_MATCH = 2;
        public const int LZX_MAX_MATCH = 257;
        public const int LZX_NUM_CHARS = 256;
        public const int LZX_PRETREE_NUM_ELEMENTS = 20;

        /// <summary>
        /// aligned offset tree #elements
        /// </summary>
        public const int LZX_ALIGNED_NUM_ELEMENTS = 8;

        /// <summary>
        /// this one missing from spec!
        /// </summary>
        public const int LZX_NUM_PRIMARY_LENGTHS = 7;

        /// <summary>
        /// length tree #elements
        /// </summary>
        public const int LZX_NUM_SECONDARY_LENGTHS = 249;

        /* LZX huffman defines: tweak tablebits as desired */
        public const int LZX_PRETREE_MAXSYMBOLS = LZX_PRETREE_NUM_ELEMENTS;
        public const int LZX_PRETREE_TABLEBITS = 6;
        public const int LZX_MAINTREE_MAXSYMBOLS = LZX_NUM_CHARS + 50 * 8;
        public const int LZX_MAINTREE_TABLEBITS = 12;
        public const int LZX_LENGTH_MAXSYMBOLS = LZX_NUM_SECONDARY_LENGTHS + 1;
        public const int LZX_LENGTH_TABLEBITS = 12;
        public const int LZX_ALIGNED_MAXSYMBOLS = LZX_ALIGNED_NUM_ELEMENTS;
        public const int LZX_ALIGNED_TABLEBITS = 7;

        public const int LZX_LENTABLE_SAFETY = 64; /* we allow length table decoding overruns */
    }
}