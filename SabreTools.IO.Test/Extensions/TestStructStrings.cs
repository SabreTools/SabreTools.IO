using System.Runtime.InteropServices;

namespace SabreTools.IO.Test.Extensions
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct TestStructStrings
    {
        /// <summary>
        /// ASCII-encoded, byte-length-prefixed string
        /// </summary>
        [MarshalAs(UnmanagedType.AnsiBStr)]
        public string? AnsiBStr;

        /// <summary>
        /// Unicode-encoded, WORD-length-prefixed string
        /// </summary>
        [MarshalAs(UnmanagedType.BStr)]
        public string? BStr;

        /// <summary>
        /// Fixed length string
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string? ByValTStr;

        /// <summary>
        /// ASCII-encoded, null-terminated string
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string? LPStr;

        /// <summary>
        /// Unicode-encoded, null-terminated string
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string? LPWStr;
    }
}
