using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Methods around device access and identification
    /// </summary>
    public static class DeviceExtensions
    {
        #region Unix

        /// <summary>
        /// sysfs block-device name prefixes that the fixed-drive enumerator does not surface:
        /// loopback images, ramdisks, device-mapper and software RAID virtual devices, network
        /// block devices, ZFS volumes, and the floppy and optical drives (each surfaced
        /// separately by its own enumerator rather than as a fixed or removable disk).
        /// </summary>
        private static readonly string[] _excludedUnixBlockPrefixes =
        [
            "dm-",  // device-mapper (LVM/LUKS, etc.) virtual devices
            "fd",   // floppy drive (surfaced separately by the floppy enumerator)
            "loop", // loopback-mounted image
            "md",   // software RAID (mdadm)
            "nbd",  // network block device
            "ram",  // ramdisk
            "sr",   // optical drive (surfaced separately by the optical enumerator)
            "zd",   // ZFS volume (zvol)
            "zram", // compressed ramdisk
        ];

        /// <summary>
        /// Floppy media sizes in bytes for the standard 5.25" and 3.5" PC formats
        /// (360 KB, 720 KB, 1.2 MB, 1.44 MB, 2.88 MB). A removable SCSI disk reporting one of
        /// these exact capacities is a USB floppy with media inserted; no other removable
        /// storage uses these sizes, so this doubles as the floppy identity check.
        /// </summary>
        private static readonly List<long> _unixFloppyMediaSizes =
        [
            368640,   // 360 KB (5.25" DD)
            737280,   // 720 KB (3.5" DD)
            1228800,  // 1.2 MB (5.25" HD)
            1474560,  // 1.44 MB (3.5" HD)
            2949120,  // 2.88 MB (3.5" ED)
        ];

        /// <summary>
        /// Enumerate Linux floppy block devices under a directory by matching the kernel
        /// convention "fd" followed by one or more digits (fd0, fd1, ...).
        /// </summary>
        /// <param name="devRoot">Root directory to scan (typically "/dev")</param>
        /// <returns>Device paths, or an empty list when the directory is unreadable</returns>
        /// <remarks>
        /// This deliberately rejects the "/dev/fd" symlink (no trailing digits) and
        /// format-specific nodes such as "fd0u1440" (embedded non-digit characters),
        /// swhich are not whole-device targets.
        /// </remarks>
        public static List<string> EnumerateUnixFloppyBlockPaths(string? devRoot = "/dev")
        {
            // If the device root is invalid
            if (string.IsNullOrEmpty(devRoot) || !Directory.Exists(devRoot))
                return [];

            // Attempt to retrieve all matching devices
            string[] entries = IOExtensions.SafeGetFiles(devRoot!, "fd*");

            // Return only valid devices
            var result = new List<string>();
            foreach (var path in entries)
            {
                if (HasDeviceIndexSuffix(Path.GetFileName(path), "fd"))
                    result.Add(path);
            }

            return result;
        }

        /// <summary>
        /// Enumerate Linux optical block devices under a directory by matching
        /// the kernel convention "sr" followed by one or more digits (sr0, sr1, ...).
        /// </summary>
        /// <param name="devRoot">Root directory to scan (typically "/dev")</param>
        /// <returns>Device paths, or an empty list when the directory is unreadable</returns>
        public static List<string> EnumerateUnixOpticalBlockPaths(string? devRoot = "/dev")
        {
            // If the device root is invalid
            if (string.IsNullOrEmpty(devRoot) || !Directory.Exists(devRoot))
                return [];

            // Attempt to retrieve all matching devices
            string[] entries = IOExtensions.SafeGetFiles(devRoot!, "sr*");

            // Return only valid devices
            var result = new List<string>();
            foreach (var path in entries)
            {
                if (HasDeviceIndexSuffix(Path.GetFileName(path), "sr"))
                    result.Add(path);
            }

            return result;
        }

        /// <summary>
        /// Enumerate Linux optical drives via their generic SCSI nodes (/dev/sg*).
        /// </summary>
        /// <param name="devRoot">Root directory the nodes live under (typically "/dev")</param>
        /// <param name="sysfsScsiGenericRoot">sysfs class directory (typically "/sys/class/scsi_generic")</param>
        /// <returns>Device paths, or an empty list when the sysfs directory is unreadable</returns>
        /// <remarks>
        /// Unlike the block nodes, "sg" is not optical-specific, so each candidate is
        /// confirmed against sysfs: an optical drive reports SCSI peripheral device type 5.
        /// </remarks>
        public static List<string> EnumerateUnixOpticalGenericPaths(string? devRoot = "/dev", string? sysfsScsiGenericRoot = "/sys/class/scsi_generic")
        {
            // If the device root is invalid
            if (string.IsNullOrEmpty(devRoot) || !Directory.Exists(devRoot))
                return [];

            // If the system generic root is invalid
            if (string.IsNullOrEmpty(sysfsScsiGenericRoot) || !Directory.Exists(sysfsScsiGenericRoot))
                return [];

            // Attempt to retrieve all matching devices
            string[] entries = IOExtensions.SafeGetFileSystemEntries(sysfsScsiGenericRoot!, "sg*");

            // Return only valid devices
            var result = new List<string>();
            foreach (var entry in entries)
            {
                string name = Path.GetFileName(entry);
                if (!HasDeviceIndexSuffix(name, "sg"))
                    continue;

                // "sg" is generic SCSI; keep only optical drives (peripheral device type 5)
                int scsiType = ReadUnixBlockDeviceType(entry);
                if (scsiType != 5)
                    continue;

                result.Add(Path.Combine(devRoot, name));
            }

            return result;
        }

        /// <summary>
        /// Enumerate USB floppy drives, which the kernel exposes as ordinary SCSI disks
        /// (/dev/sd*) rather than /dev/fd* nodes.
        /// </summary>
        /// <param name="devRoot">Root directory device nodes live under (typically "/dev")</param>
        /// <param name="sysBlockRoot">sysfs block directory (typically "/sys/block")</param>
        /// <returns>Device paths, or an empty list when the directory is unreadable</returns>
        /// <remarks>
        /// A disk is treated as a floppy when it reports itself removable and its media is one of
        /// the standard floppy sizes. This mirrors how Windows identifies a floppy from the
        /// inserted medium (Win32_LogicalDisk.MediaType) and, like that path, only recognizes the
        /// drive while floppy media is present.
        /// </remarks>
        public static List<string> EnumerateUnixUsbFloppyBlockPaths(string? devRoot = "/dev", string? sysBlockRoot = "/sys/block")
        {
            // If the device root is invalid
            if (string.IsNullOrEmpty(devRoot) || !Directory.Exists(devRoot))
                return [];

            // If the system block root is invalid
            if (string.IsNullOrEmpty(sysBlockRoot) || !Directory.Exists(sysBlockRoot))
                return [];

            // Attempt to retrieve all matching devices
            string[] entries = IOExtensions.SafeGetFileSystemEntries(sysBlockRoot!, "sd*");

            // Return only valid devices
            var result = new List<string>();
            foreach (var entry in entries)
            {
                // Ensure the drive has the removable flag set
                if (!ReadUnixRemovableFlag(entry))
                    continue;

                // Filter out non-standard floppy sizes for now
                if (!_unixFloppyMediaSizes.Contains(ReadUnixBlockDeviceSize(entry)))
                    continue;

                string devicePath = Path.Combine(devRoot, Path.GetFileName(entry));
                result.Add(devicePath);
            }

            return result;
        }

        /// <summary>
        /// Check whether a sysfs block-device name is a virtual or non-target device that
        /// should not be surfaced as a dump target.
        /// </summary>
        /// <param name="name">Block-device name (e.g. "sda", "loop0")</param>
        /// <returns>True when the device should be skipped</returns>
        public static bool IsExcludedUnixBlockDevice(string name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            foreach (var prefix in _excludedUnixBlockPrefixes)
            {
                if (name.StartsWith(prefix, StringComparison.Ordinal))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get the mount point based on the block device mapping for generic SCSI
        /// </summary>
        /// <param name="devicePath">Device path to check</param>
        /// <returns>Mount point for the generic device on success, null otherwise</returns>
        public static string? GetMountPointGeneric(string? devicePath)
        {
            // If the device path is invalid
            if (devicePath is null)
                return null;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
            // If the device does not match the generic pattern
            if (!Regex.IsMatch(devicePath, @"/dev/sg.+"))
                return null;
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

            // Get the SCSI name from the path by trimming `/dev/`
#if NETFRAMEWORK || NETSTANDARD
            string scsiName = devicePath.Substring(5);
#else
            string scsiName = devicePath[5..];
#endif

            // Build the path to the block directory
            string blockDirectory = $"/sys/class/scsi_generic/{scsiName}/device/block/";
            if (!Directory.Exists(blockDirectory))
                return null;

            // If there are no block mappings
            var blockDevices = IOExtensions.SafeGetDirectories(blockDirectory);
            if (blockDevices.Length == 0)
                return null;

            // Take the first found block mapping and reformat to `/dev/sr*`
            string mappedDevice = blockDevices[0];
#if NETFRAMEWORK || NETSTANDARD
            mappedDevice = mappedDevice.Substring($"/sys/class/scsi_generic/{scsiName}/device/block/".Length);
#else
            mappedDevice = mappedDevice[$"/sys/class/scsi_generic/{scsiName}/device/block/".Length..];
#endif
            mappedDevice = $"/dev/{mappedDevice}";

            // Return the mount point for that device
            return GetMountPointMounts(mappedDevice);
        }

        /// <summary>
        /// Get the mount point based a mapping to /proc/mounts
        /// </summary>
        /// <param name="devicePath">Device path to check</param>
        /// <returns>Mount point for the device on success, null otherwise</returns>
        public static string? GetMountPointMounts(string? devicePath)
        {
            // If the device path is invalid
            if (devicePath is null)
                return null;

            // Try reading the mounts file for information
            string mountsPath = "/proc/mounts";
            try
            {
                if (!File.Exists(mountsPath))
                    return null;

                // Read all lines from the mounts file
                string[] mountsLines = File.ReadAllLines(mountsPath);
                foreach (string mount in mountsLines)
                {
                    // Split the whitespace-separated mount entry line
#if NETFRAMEWORK || NETSTANDARD
                    string[] entry = mount.Split(' ');
#else
                    string[] entry = mount.Split(' ', StringSplitOptions.RemoveEmptyEntries);
#endif
                    if (entry.Length < 2)
                        continue;

                    // Extract the two required pieces
                    string entryDevicePath = entry[0];
                    string entryMountPoint = entry[1];

                    // If the drive does not match
                    if (!string.Equals(devicePath, entryDevicePath))
                        continue;

                    // Return the mount point
                    return entryMountPoint;
                }

                // No matches were found
                return null;
            }
            catch
            {
                // Absorb errors for now
                return null;
            }
        }

        /// <summary>
        /// Read the device size from sysfs (reported in 512-byte sectors) and convert to bytes
        /// </summary>
        /// <param name="sysBlockEntry">Path to the device directory under the sysfs block root</param>
        /// <returns>The device size in bytes, 0 on error</returns>
        public static long ReadUnixBlockDeviceSize(string sysBlockEntry)
        {
            string sizePath = Path.Combine(sysBlockEntry, "size");
            try
            {
                if (!File.Exists(sizePath))
                    return 0;

                string sizeString = File.ReadAllText(sizePath).Trim();
                if (long.TryParse(sizeString, out long sectors) && sectors > 0)
                    return sectors * 512;
            }
            catch
            {
                // Unreadable size; report it as unknown
            }

            return 0;
        }

        /// <summary>
        /// Read the device size from sysfs (reported in 512-byte sectors) and convert to bytes
        /// </summary>
        /// <param name="sysGenericEntry">Path to the device directory under the sysfs block root</param>
        /// <returns>The device size in bytes, 0 on error</returns>
        public static int ReadUnixBlockDeviceType(string sysGenericEntry)
        {
            string typePath = Path.Combine(Path.Combine(sysGenericEntry, "device"), "type");
            try
            {
                if (!File.Exists(typePath))
                    return 0;

                string typeString = File.ReadAllText(typePath).Trim();
                if (int.TryParse(typeString, out int scsiType))
                    return scsiType;
            }
            catch
            {
                // Unreadable type; report it as unknown
            }

            return 0;
        }

        /// <summary>
        /// Read the sysfs "removable" flag for a block device
        /// </summary>
        /// <param name="sysBlockEntry">Path to the device directory under the sysfs block root</param>
        /// <returns>True when the device reports itself as removable, false otherwise</returns>
        public static bool ReadUnixRemovableFlag(string sysBlockEntry)
        {
            string removablePath = Path.Combine(sysBlockEntry, "removable");
            try
            {
                if (!File.Exists(removablePath))
                    return false;

                string removableString = File.ReadAllText(removablePath).Trim();
                return removableString == "1";
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check that a device node name is the given prefix followed by one or more digits
        /// (e.g. "sr0", "sg12"), rejecting names with trailing or embedded non-digit characters.
        /// </summary>
        /// <param name="name">Device node name to check</param>
        /// <param name="prefix">Required leading text (e.g. "sr", "sg")</param>
        /// <returns>True when the name is the prefix followed only by digits</returns>
        private static bool HasDeviceIndexSuffix(string name, string prefix)
        {
            // If the prefix is longer than the name
            if (name.Length <= prefix.Length)
                return false;

            // If the prefix is invalid for the provided name
            if (!name.StartsWith(prefix, StringComparison.Ordinal))
                return false;

            // Check that all following characters are numeric
            for (int i = prefix.Length; i < name.Length; i++)
            {
                if (!char.IsDigit(name[i]))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
