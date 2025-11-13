namespace SabreTools.IO.Compression.Quantum
{
    /// <see href="http://www.russotto.net/quantumcomp.html"/>
    internal static class Constants
    {
        public static readonly int[] PositionSlot =
        [
            0x00000, 0x00001, 0x00002, 0x00003, 0x00004, 0x00006, 0x00008, 0x0000c,
            0x00010, 0x00018, 0x00020, 0x00030, 0x00040, 0x00060, 0x00080, 0x000c0,
            0x00100, 0x00180, 0x00200, 0x00300, 0x00400, 0x00600, 0x00800, 0x00c00,
            0x01000, 0x01800, 0x02000, 0x03000, 0x04000, 0x06000, 0x08000, 0x0c000,
            0x10000, 0x18000, 0x20000, 0x30000, 0x40000, 0x60000, 0x80000, 0xc0000,
            0x100000, 0x180000
        ];

        public static readonly int[] PositionExtraBits =
        [
            0,  0,  0,  0,  1,  1,  2,  2,
            3,  3,  4,  4,  5,  5,  6,  6,
            7,  7,  8,  8,  9,  9, 10, 10,
            11, 11, 12, 12, 13, 13, 14, 14,
            15, 15, 16, 16, 17, 17, 18, 18,
            19, 19
        ];

        public static readonly int[] LengthSlot =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x08,
            0x0a, 0x0c, 0x0e, 0x12, 0x16, 0x1a, 0x1e, 0x26,
            0x2e, 0x36, 0x3e, 0x4e, 0x5e, 0x6e, 0x7e, 0x9e,
            0xbe, 0xde, 0xfe
        ];

        public static readonly int[] LengthExtraBits =
        [
            0,  0,  0,  0,  0,  0,  1,  1,
            1,  1,  2,  2,  2,  2,  3,  3,
            3,  3,  4,  4,  4,  4,  5,  5,
            5,  5,  0
        ];

        /// <summary>
        /// Number of position slots for (tsize - 10)
        /// </summary>
        public static readonly int[] NumPositionSlots =
        [
            20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42
        ];
    }
}
