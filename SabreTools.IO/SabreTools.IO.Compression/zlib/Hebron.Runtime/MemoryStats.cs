using System.Threading;

namespace SabreTools.IO.Compression.zlib
{
#pragma warning disable IDE0036
#pragma warning disable IDE0380
    public unsafe static class MemoryStats
    {
        private static int _allocations;

        public static int Allocations
        {
            get
            {
                return _allocations;
            }
        }

        internal static void Allocated()
        {
            Interlocked.Increment(ref _allocations);
        }

        internal static void Freed()
        {
            Interlocked.Decrement(ref _allocations);
        }
    }
}
