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
        /// 4 entry int array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public TestEnum[]? EnumArray;

        /// <summary>
        /// 4 entry struct array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public TestStructPoint[]? StructArray;

        /// <summary>
        /// Length of <see cref="LPByteArray"/> 
        /// </summary>
        public ushort LPByteArrayLength;

        /// <summary>
        /// 4 entry byte array whose length is defined by <see cref="LPByteArrayLength"/>
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
        public byte[]? LPByteArray;
        
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
