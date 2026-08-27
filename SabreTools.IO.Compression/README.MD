# SabreTools.IO.Compression

This library contains various compression implementations that are used across multiple projects. Most of the implementations are be ports of existing C and C++ code.

Relies on functionality found in `SabreTools.Hashing`, `SabreTools.IO`, `SabreTools.IO.Extensions`, and `SabreTools.Numerics.Extensions`.

## Supported Compressions

| Compression Name | Decompress | Compress | Notes |
| --- | --- | --- | --- |
| Blast | Yes | No | |
| Deflate | Yes | Yes | Sourced from DotNetZip |
| LZ | Yes | No | KWAJ, QBasic 4.5, and SZDD variants; KWAJ incomplete |
| LZX | No | No | |
| MSZIP | Yes | No | |
| PURGE | Yes | Yes | Used by the WIA and RVZ image formats |
| Quantum | Yes* | No | Partial implementation based on standalone archives; not working |
| RVZ-Pack | Yes | Yes | Used by the WIA and RVZ image formats |

**Note:** If something is marked with a `*` it means that it need testing.

## External Libraries

| Library Name | Use |
| --- | --- |
| [DotNetZip](https://github.com/DinoChiesa/DotNetZip) | DEFLATE implementation; minor edits have been made |
