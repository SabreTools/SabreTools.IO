# SabreTools.Security.Cryptography

This library contains various encryption implementations that are used across multiple projects. Most of the implementations are be ports of existing C and C++ code.

Relies on functionality found in `SabreTools.IO.Extensions`.

## Supported Encryption Schemes

| Encryption Scheme | Encrypt | Decrypt | Notes |
| --- | --- | --- | --- |
| AES/CTR | Yes | Yes | Subset of functionality exposed from [The Bouncy Castle Cryptography Library For .NET](https://github.com/bcgit/bc-csharp) |
| MoPaQ | No | Yes | Used to encrypt and decrypt MoPaQ tables for processing |
