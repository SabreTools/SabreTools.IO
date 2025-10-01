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
| MSZIP | Yes | No | |
| Quantum | Yes* | No | Partial implementation based on standalone archives; not working |

**Note:** If something is marked with a `*` it means that it need testing.

#### External Libraries

| Library Name | Use |
| --- | ---|
| [DotNetZip](https://github.com/DinoChiesa/DotNetZip) | BZip2 and DEFLATE implementations; minor edits have been made |
| [ZLibPort](https://github.com/Nanook/zlib-C-To-CSharp-Port) | Adds zlib code for internal and external use; minor edits have been made |

### `SabreTools.IO.Encryption`

Various encryption implementations that are used across multiple projects. Most of the implementations are be ports of existing C and C++ code.

#### Supported Encryption Schemes

| Encryption Scheme | Encrypt | Decrypt | Notes |
| --- | --- | --- | --- |
| AES/CTR | Yes | Yes | Subset of functionality exposed from [The Bouncy Castle Cryptography Library For .NET](https://github.com/bcgit/bc-csharp) |
| MoPaQ | No | Yes | Used to encrypt and decrypt MoPaQ tables for processing |

### `SabreTools.IO.Extensions`

Extensions for `BinaryReader`, `byte[]`, and `Stream` to help with reading and writing various data types. Some data types are locked behind .NET version support.

This namespace also contains other various extensions that help with common functionality and safe access.

### `SabreTools.IO.Interfaces`

Common interfaces used mainly internal to the library.

| Interface | Notes |
| --- | --- |
| `IMatch<T>` | Represents a matcher for a generic type |
| `IMatchSet<T, U>` | Represents a set of `IMatch<T>` types |

### `SabreTools.IO.Logging`

Logic for a logging system, including writing to console and textfile outputs. There are 4 possible log levels for logging statements to be invoked with. There is also a stopwatch implementation included for logging statements with automatic timespan tracking.

### `SabreTools.IO.Matching`

Classes designed to make matching contents and paths easier. These classes allow for both grouped and single matching as well as post-processing of matched information.

### `SabreTools.IO.Readers` and `SabreTools.IO.Writers`

Reading and writing support for the following file types:

- ClrMamePro-derived Metadata files
- Standard and non-standard INI files
- Separated-Value files (e.g. CSV, SSV, TSV)

For a generic INI implementation, see `SabreTools.IO.IniFile`.

### `SabreTools.IO.Streams`

Custom `Stream` implementations that are required for specialized use:

- `BufferedStream`: A format that is not a true stream implementation used for buffered, single-byte reads
- `ReadOnlyBitStream`: A readonly stream implementation allowing bitwise reading
- `ReadOnlyCompositeStream`: A readonly stream implementation that wraps multiple source streams in a set order
- `ViewStream`: A readonly stream implementation representing a view into source data

### `SabreTools.Text.Compare`

Classes focused on string comparison by natural sorting. For example, "5" would be sorted before "100".

## Releases

For the most recent stable build, download the latest release here: [Releases Page](https://github.com/SabreTools/SabreTools.IO/releases)

For the latest WIP build here: [Rolling Release](https://github.com/SabreTools/SabreTools.IO/releases/rolling)
