using System;
using Xunit;

namespace SabreTools.IO.Test
{
    public class ParentablePathTests
    {
        [Theory]
        [InlineData("", null, false, null)]
        [InlineData("", null, true, null)]
        [InlineData("      ", null, false, null)]
        [InlineData("      ", null, true, null)]
        [InlineData("C:\\Directory\\Filename.ext", null, false, "Filename.ext")]
        [InlineData("C:\\Directory\\Filename.ext", null, true, "Filename.ext")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", false, "Filename.ext")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", true, "Filename.ext")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", false, "SubDir\\Filename.ext")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", true, "SubDir-Filename.ext")]
        public void NormalizedFileNameTest(string current, string? parent, bool sanitize, string? expected)
        {
            // Hack to support Windows paths on Linux for testing only
            if (System.IO.Path.DirectorySeparatorChar == '/')
            {
                current = current.Replace('\\', '/');
                parent = parent?.Replace('\\', '/');
                expected = expected?.Replace('\\', '/');
            }

            var path = new ParentablePath(current, parent);
            string? actual = path.GetNormalizedFileName(sanitize);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null, null, false, null)]
        [InlineData("", null, null, true, null)]
        [InlineData("      ", null, null, false, null)]
        [InlineData("      ", null, null, true, null)]
        [InlineData("C:\\Directory\\Filename.ext", null, null, false, null)]
        [InlineData("C:\\Directory\\Filename.ext", null, null, true, "C:\\Directory")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", null, false, null)]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", null, true, "C:\\Directory")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", null, false, null)]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", null, true, "C:\\Directory\\SubDir")]
        [InlineData("", null, "D:\\OutputDirectory", false, null)]
        [InlineData("", null, "D:\\OutputDirectory", true, null)]
        [InlineData("      ", null, "D:\\OutputDirectory", false, null)]
        [InlineData("      ", null, "D:\\OutputDirectory", true, null)]
        [InlineData("C:\\Directory\\Filename.ext", null, "D:\\OutputDirectory", false, "D:\\OutputDirectory")]
        [InlineData("C:\\Directory\\Filename.ext", null, "D:\\OutputDirectory", true, "C:\\Directory")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", "D:\\OutputDirectory", false, "D:\\OutputDirectory")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", "D:\\OutputDirectory", true, "C:\\Directory")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", "D:\\OutputDirectory", false, "D:\\OutputDirectory\\SubDir")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", "D:\\OutputDirectory", true, "C:\\Directory\\SubDir")]
        [InlineData("", null, "%cd%", false, null)]
        [InlineData("", null, "%cd%", true, null)]
        [InlineData("      ", null, "%cd%", false, null)]
        [InlineData("      ", null, "%cd%", true, null)]
        [InlineData("C:\\Directory\\Filename.ext", null, "%cd%", false, "%cd%")]
        [InlineData("C:\\Directory\\Filename.ext", null, "%cd%", true, "C:\\Directory")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", "%cd%", false, "%cd%")]
        [InlineData("C:\\Directory\\Filename.ext", "C:\\Directory\\Filename.ext", "%cd%", true, "C:\\Directory")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", "%cd%", false, "%cd%\\Directory\\SubDir")]
        [InlineData("C:\\Directory\\SubDir\\Filename.ext", "C:\\Directory", "%cd%", true, "C:\\Directory\\SubDir")]
        public void GetOutputPathTest(string current, string? parent, string? outDir, bool inplace, string? expected)
        {
            // Hacks because I can't use environment vars as parameters
            if (outDir == "%cd%")
                outDir = Environment.CurrentDirectory.TrimEnd('\\', '/');
            if (expected?.Contains("%cd%") == true)
                expected = expected.Replace("%cd%", Environment.CurrentDirectory.TrimEnd('\\', '/'));

            // Hack to support Windows paths on Linux for testing only
            if (System.IO.Path.DirectorySeparatorChar == '/')
            {
                current = current.Replace('\\', '/');
                parent = parent?.Replace('\\', '/');
                outDir = outDir?.Replace('\\', '/');
                expected = expected?.Replace('\\', '/');
            }

            var path = new ParentablePath(current, parent);
            string? actual = path.GetOutputPath(outDir, inplace);
            Assert.Equal(expected, actual);
        }
    }
}