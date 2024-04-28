using System.Runtime.InteropServices;

namespace SabreTools.IO.Test.Extensions
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TestStructSequential
    {
        public int FirstValue;

        public int SecondValue;

        public ushort ThirdValue;
        
        public short FourthValue;

        [MarshalAs(UnmanagedType.LPStr)]
        public string? FifthValue;
    }
}
