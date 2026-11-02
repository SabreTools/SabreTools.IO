using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class DeviceExtensionsTests
    {
        #region EnumerateUnixFloppyBlockPaths

        [Fact]
        public void EnumerateUnixFloppyBlockPaths_Null_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixFloppyBlockPaths(null);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixFloppyBlockPaths_Empty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixFloppyBlockPaths(string.Empty);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixFloppyBlockPaths_MissingDirectory_ReturnsEmpty()
        {
            string devRoot = "INVALID";
            var actual = DeviceExtensions.EnumerateUnixFloppyBlockPaths(devRoot);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixFloppyBlockPaths_OnlyMatchesFdFollowedByDigits()
        {
            string devRoot = Path.Combine(Environment.CurrentDirectory, "TestData", "dev");
            string[] expected = ["fd0", "fd1", "fd7"];

            var actual = DeviceExtensions.EnumerateUnixFloppyBlockPaths(devRoot);

            var actualNames = new List<string>();
            foreach (var p in actual)
            {
                actualNames.Add(Path.GetFileName(p));
            }

            actualNames.Sort(StringComparer.Ordinal);
            Assert.Equal(expected, actualNames);
        }

        #endregion

        #region EnumerateUnixOpticalBlockPaths

        [Fact]
        public void EnumerateUnixOpticalBlockPaths_Null_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalBlockPaths(null);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalBlockPaths_Empty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalBlockPaths(string.Empty);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalBlockPaths_MissingDirectory_ReturnsEmpty()
        {
            string devRoot = "INVALID";
            var actual = DeviceExtensions.EnumerateUnixOpticalBlockPaths(devRoot);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalBlockPaths_OnlyMatchesSrFollowedByDigits()
        {
            string devRoot = Path.Combine(Environment.CurrentDirectory, "TestData", "dev");
            string[] expected = ["sr0", "sr1", "sr15"];

            var actual = DeviceExtensions.EnumerateUnixOpticalBlockPaths(devRoot);

            var actualNames = new List<string>();
            foreach (var p in actual)
            {
                actualNames.Add(Path.GetFileName(p));
            }

            actualNames.Sort(StringComparer.Ordinal);
            Assert.Equal(expected, actualNames);
        }

        #endregion

        #region EnumerateUnixOpticalGenericPaths

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_DevRootNull_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(null, "/sys/class/scsi_generic");
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_DevRootEmpty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(string.Empty, "/sys/class/scsi_generic");
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_SysBlockRootNull_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths("/dev", null);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_SysBlockRootEmpty_ReturnsEmpty()
        {
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths("/dev", string.Empty);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_MissingDirectory_ReturnsEmpty()
        {
            string sysfsScsiGenericRoot = "INVALID";
            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths("/dev/", sysfsScsiGenericRoot);
            Assert.Empty(actual);
        }

        [Fact]
        public void EnumerateUnixOpticalGenericPaths_OnlyMatchesOpticalSgNodes()
        {
            string devRoot = "/dev";
            string sysfsScsiGenericRoot = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "class", "scsi_generic");

            // sg0 - Optical, kept
            // sg1 - Direct-access disk, skipped
            // sg2 - Optical (trailing newline), kept
            // sg3 - Sequential tape, skipped
            // sg10 - Optical (multi-digit), kept
            // sga - Name not all-digits, skipped
            // sgX - Name not all-digits, skipped
            List<string> expected = ["sg0", "sg10", "sg2"];

            var actual = DeviceExtensions.EnumerateUnixOpticalGenericPaths(devRoot, sysfsScsiGenericRoot);

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
