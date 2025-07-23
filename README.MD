# SabreTools.IO

[![Build and Test](https://github.com/SabreTools/SabreTools.IO/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/SabreTools/SabreTools.IO/actions/workflows/build_and_test.yml)

This library compries of I/O functionality used by other SabreTools projects.

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.IO).

## Namespaces

Below are a list of the included namespaces and their overall utility.

### `SabreTools.IO`

Generic helper classes that involve custom functionality and utility.

### `SabreTools.IO.Compression`

Various compression implementations that are used across multiple projects. Most of the implementations are be ports of existing C and C++ code.

#### Supported Compressions

| Compression Name | Decompress | Compress | Notes |
| --- | --- | --- | --- |
| Blast | Yes | No | |
| BZip2 | Yes | Yes | Sourced from DotNetZip |
| Deflate | Yes | Yes | Sourced from DotNetZip |
| LZ | Yes | No | KWAJ, QBasic 4.5, and SZDD variants; KWAJ incomplete |
| LZX | No | No | |
| MSZIP | Yes* | No | |
| Quantum | Yes* | No | Partial implementation based on standalone archives |

**Note:** If something is marked with a `*` it means that it need testing.

#### External Libraries

| Library Name | Use |
| --- | ---|
| [DotNetZip](https://github.com/DinoChiesa/DotNetZip) | BZip2 and DEFLATE implementations; minor edits have been made |
| [ZLibPort](https://github.com/Nanook/zlib-C-To-CSharp-Port) | Adds zlib code for internal and external use; minor edits have been made |

### `SabreTools.IO.Extensions`

Extensions for `BinaryReader`, `byte[]`, and `Stream` to help with reading and writing various data types. Some data types are locked behind .NET version support.

This namespace also contains other various extensions that help with common functionality and safe access.

### `SabreTools.IO.Logging`

Logic for a logging system, including writing to console and textfile outputs. There are 4 possible log levels for logging statements to be invoked with. There is also a stopwatch implementation included for logging statements with automatic timespan tracking.

### `SabreTools.IO.Readers` and `SabreTools.IO.Writers`

Reading and writing support for the following file types:

- ClrMamePro-derived Metadata files
- Standard and non-standard INI files
- Separated-Value files (e.g. CSV, SSV, TSV)

For a generic INI implementation, see `SabreTools.IO.IniFile`.

### `SabreTools.IO.Streams`

Custom `Stream` implementations that are required for specialized use:

- `ReadOnlyBitStream`: A readonly stream implementation allowing bitwise reading
- `ReadOnlyCompositeStream`: A readonly stream implementation that wraps multiple source streams in a set order

## Releases

For the most recent stable build, download the latest release here: [Releases Page](https://github.com/SabreTools/SabreTools.IO/releases)

For the latest WIP build here: [Rolling Release](https://github.com/SabreTools/SabreTools.IO/releases/rolling)
