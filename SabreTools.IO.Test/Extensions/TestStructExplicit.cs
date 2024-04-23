using System.Runtime.InteropServices;

namespace SabreTools.IO.Test.Extensions
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct TestStructExplicit
    {
        [FieldOffset(0)]
        public int FirstValue;

        [FieldOffset(4)]
        public int SecondValue;

        [FieldOffset(4)]
        public ushort ThirdValue;

        [FieldOffset(6)]
        public short FourthValue;
    }
}
