using System.Runtime.InteropServices;

namespace SabreTools.IO.Test.Extensions
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct TestStructArrays
    {
        /// <summary>
        /// 4 entry byte array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[]? ByteArray;

        /// <summary>
        /// 4 entry int array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[]? IntArray;

        /// <summary>
        /// 4 entry struct array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public TestStructPoint[]? StructArray;
        
        // /// <summary>
        // /// 4 entry nested byte array
        // /// </summary>
        // /// <remarks>This will likely fail</remarks>
        // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        // public byte[][]? NestedByteArray;
    }

    /// <summary>
    /// Struct for nested tests
    /// </summary>
    internal struct TestStructPoint
    {
        public ushort X;
        public ushort Y;
    }
}
