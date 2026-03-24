# SabreTools.IO.Compression

This library contains various compression implementations that are used across multiple projects. Most of the implementations are be ports of existing C and C++ code.

Relies on functionality found in `SabreTools.Hashing`, `SabreTools.IO`, `SabreTools.IO.Extensions`, and `SabreTools.Numerics.Extensions`.

## Supported Compressions

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

## External Libraries

| Library Name | Use |
| --- | --- |
| [DotNetZip](https://github.com/DinoChiesa/DotNetZip) | BZip2 and DEFLATE implementations; minor edits have been made |
| [ZLibPort](https://github.com/Nanook/zlib-C-To-CSharp-Port) | Adds zlib code for internal and external use; minor edits have been made |
