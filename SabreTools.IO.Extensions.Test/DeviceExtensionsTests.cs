using System;
using System.IO;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class DeviceExtensionsTests
    {
        #region IsExcludedUnixBlockDevice

        [Theory]
        [InlineData("dm-0", true)] // Device-mapper (LVM/LUKS, etc.) virtual devices
        [InlineData("fd0", true)] // Floppy drive
        [InlineData("loop0", true)] // Loopback-mounted image
        [InlineData("md0", true)] // Software RAID (mdadm)
        [InlineData("nbd0", true)] // Network block device
        [InlineData("ram0", true)] // Ramdisk
        [InlineData("sda", false)] // Static storage
        [InlineData("sg0", false)] // Generic SCSI
        [InlineData("sr0", true)] // Optical drive
        [InlineData("zd0", true)] // ZFS volume (zvol)
        [InlineData("zram0", true)] // Compressed ramdisk]
        public void IsExcludedUnixBlockDeviceTest(string name, bool expected)
        {
            bool actual = DeviceExtensions.IsExcludedUnixBlockDevice(name);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ReadUnixBlockDeviceSize

        [Theory]
        [InlineData("nvme0n1", 2880)] // Not an sd* device
        [InlineData("sda", 2880)] // Fixed disk of floppy size, not removable
        [InlineData("sdb", 30310400)] // USB flash drive, not a floppy size
        [InlineData("sdc", 0)] // Empty floppy drive (no media)
        [InlineData("sdh", 2880)] // USB floppy, 1.44 MB
        [InlineData("sdi", 1440)] // USB floppy, 720 KB (trailing newline)
        public void ReadUnixBlockDeviceSizeTest(string device, long valueInFile)
        {
            long expected = valueInFile * 512;
            string sysBlockEntry = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "block", device);
            long actual = DeviceExtensions.ReadUnixBlockDeviceSize(sysBlockEntry);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ReadUnixBlockDeviceType

        [Theory]
        [InlineData("sg0", 5)]
        [InlineData("sg1", 0)]
        [InlineData("sg2", 5)]
        [InlineData("sg3", 1)]
        [InlineData("sg10", 5)]
        [InlineData("sga", 5)]
        [InlineData("sgX", 5)]
        public void ReadUnixBlockDeviceTypeTest(string device, int expected)
        {
            string sysGenericEntry = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "class", "scsi_generic", device);
            int actual = DeviceExtensions.ReadUnixBlockDeviceType(sysGenericEntry);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ReadUnixRemovableFlag

        [Theory]
        [InlineData("nvme0n1", true)] // Not an sd* device
        [InlineData("sda", false)] // Fixed disk of floppy size, not removable
        [InlineData("sdb", true)] // USB flash drive, not a floppy size
        [InlineData("sdc", true)] // Empty floppy drive (no media)
        [InlineData("sdh", true)] // USB floppy, 1.44 MB
        [InlineData("sdi", true)] // USB floppy, 720 KB (trailing newline)
        public void ReadUnixRemovableFlagTest(string device, bool expected)
        {
            string sysBlockEntry = Path.Combine(Environment.CurrentDirectory, "TestData", "sys", "block", device);
            bool actual = DeviceExtensions.ReadUnixRemovableFlag(sysBlockEntry);
            Assert.Equal(expected, actual);
        }

        #endregion

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
        // Generic block
        [InlineData("sg0", "sg", true)]
        [InlineData("sg1", "sg", true)]
        [InlineData("sg2", "sg", true)]
        [InlineData("sg3", "sg", true)]
        [InlineData("sg10", "sg", true)]
        [InlineData("sga", "sg", false)]
        [InlineData("sgX", "sg", false)]
        public void HasDeviceIndexSuffixTest(string name, string prefix, bool expected)
        {
            bool actual = DeviceExtensions.HasDeviceIndexSuffix(name, prefix);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
