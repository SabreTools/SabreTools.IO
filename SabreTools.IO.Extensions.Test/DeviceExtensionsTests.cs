using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class DeviceExtensionsTests
    {
        #region HasDeviceIndexSuffix

        [Theory]
        // Floppy disk
        [InlineData("fd", "fd", false)]
        [InlineData("fd0", "fd", true)]
        [InlineData("fd0u1440", "fd", false)]
        [InlineData("fd1", "fd", true)]
        [InlineData("fd7", "fd", true)]
        [InlineData("fda", "fd", false)]
        // Optical block
        [InlineData("sr", "sr", false)]
        [InlineData("sr0", "sr", true)]
        [InlineData("sr0a", "sr", false)]
        [InlineData("sr1", "sr", true)]
        [InlineData("sr15", "sr", true)]
        [InlineData("src", "sr", false)]
        [InlineData("srm", "sr", false)]
        public void HasDeviceIndexSuffixTest(string name, string prefix, bool expected)
        {
            bool actual = DeviceExtensions.HasDeviceIndexSuffix(name, prefix);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region EnumerateUnixOpticalGenericPaths

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_SysBlockRootNull_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(null);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_SysBlockRootEmpty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(string.Empty);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_MissingDirectory_ReturnsEmpty()
        {
            string sysfsScsiGenericRoot = "INVALID";
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(sysfsScsiGenericRoot);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_OnlyMatchesOpticalSgNodes()
        {
            string sysfsScsiGenericRoot = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "class", "scsi_generic");

            // sg0 - Optical, kept
            // sg1 - Direct-access disk, skipped
            // sg2 - Optical (trailing newline), kept
            // sg3 - Sequential tape, skipped
            // sg10 - Optical (multi-digit), kept
            // sga - Name not all-digits, skipped
            // sgX - Name not all-digits, skipped
            List<string> expected = ["sg0", "sg10", "sg2"];

            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(sysfsScsiGenericRoot);

            var actualNames = new List<string>();
            foreach (var p in actual)
            {
                actualNames.Add(Path.GetFileName(p));
            }

            actualNames.Sort(StringComparer.Ordinal);

            Assert.Equal(expected, actualNames);
        }

        #endregion

        #region EnumerateUnixUsbFloppyBlockPaths

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_DevRootNull_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths(null, "/sys/block");
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_DevRootEmpty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths(string.Empty, "/sys/block");
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_SysBlockRootNull_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths("/dev", null);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_SysBlockRootEmpty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths("/dev", string.Empty);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_MissingDirectory_ReturnsEmpty()
        {
            string sysBlockRoot = "INVALID";
            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths("/dev/", sysBlockRoot);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixUsbFloppyBlockPaths_OnlyMatchesRemovableFloppySizedDisks()
        {
            string devRoot = "/dev";
            string sysBlockRoot = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "block");

            // nvme0n1 - Not an sd* device, not scanned
            // sda - Fixed disk of floppy size, not removable, skipped
            // sdb - USB flash drive, not a floppy size, skipped
            // sdc - Empty floppy drive (no media), skipped
            // sdh - USB floppy, 1.44 MB, matched
            // sdi - USB floppy, 720 KB (trailing newline), matched
            List<string> expected = ["sdh", "sdi"];

            var actual = DeviceExtensions.EnumerateUnixUsbFloppyBlockPaths(devRoot, sysBlockRoot);

            var actualNames = new List<string>();
            foreach (var p in actual)
            {
                actualNames.Add(Path.GetFileName(p));
            }

            actualNames.Sort(StringComparer.Ordinal);

            Assert.Equal(expected, actualNames);
        }

        #endregion
    }
}
