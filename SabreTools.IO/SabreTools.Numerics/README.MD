# SabreTools.Numerics

This library contains custom numeric types and related functionality.

## Supported Numeric Types

| Type Name | Description |
| --- | --- |
| `BothInt8` | Both-endian `Int8` value |
| `BothUInt8` | Both-endian `UInt8` value |
| `BothInt16` | Both-endian `Int16` value |
| `BothUInt16` | Both-endian `UInt16` value |
| `BothInt32` | Both-endian `Int32` value |
| `BothUInt32` | Both-endian `UInt32` value |
| `BothInt64` | Both-endian `Int64` value |
| `BothUInt64` | Both-endian `UInt64` value |
| `Int24` | 24-bit (3 byte) signed value |
| `UInt24` | 24-bit (3 byte) unsigned value |
| `Int48` | 48-bit (3 byte) signed value |
| `UInt48` | 48-bit (3 byte) unsigned value |

**Both-endian** or **bi-endian** numbers are represented by a little-endian value followed by a big-endian value, where both values should be the same number.
