using System.Runtime.InteropServices;

namespace SabreTools.IO.Test.Extensions
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class TestStructInheritanceParent
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[]? Signature;

        public uint IdentifierType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class TestStructInheritanceChild1 : TestStructInheritanceParent
    {
        public uint FieldA;

        public uint FieldB;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class TestStructInheritanceChild2 : TestStructInheritanceParent
    {
        public ushort FieldA;

        public ushort FieldB;
    }
}
