# SabreTools.IO.Extensions

This library contains IO-related extensions with some functionality locked behind .NET version support.

Relies on functionality found in `SabreTools.IO.Matching`, `SabreTools.Numerics`, and `SabreTools.Text.Compare`.

| Class | Description |
| --- | --- |
| `BinaryReaderExtensions` | Extensions to `BinaryReader` for specialized string and type reading. |
| `BinaryWriterExtensions` | Extensions to `BinaryWriter` for specialized string and type writing. |
| `ByteArrayExtensions` | Utility `byte[]` and `byte?[]` extensions commonly used in SabreTools projects. |
| `ByteArrayReaderExtensions` | Extensions to `byte[]` for specialized string and type reading. |
| `ByteArrayWriterExtensions` | Extensions to `byte[]` for specialized string and type writing. |
| `DateTimeExtensions` | `DateTime` conversion, specifically between standard and MS-DOS formats. |
| `DictionaryExtensions` | Utility `Dictionary<string, List<string>>` extensions commonly used in SabreTools projects. |
| `EnumerableExtensions` | Specialized enumeration wrapper functionality. |
| `IOExtensions` | Path, directory, and file extensions. |
| `MarshalHelpers` | Internal-only reflection-based extensions used by some other extension classes. |
| `ParentablePathExtensions` | Extensions to `SabreTools.IO.ParentablePath` for filtering. |
| `ReadOnlyBitStreamExtensions` | Extensions to `SabreTools.IO.ReadOnlyBitStream` to allow non-bitwise reads. |
| `StreamExtensions` | Utility `Stream` extensions commonly used in other SabreTools projects. |
| `StreamReaderExtensions` | Extensions to `Stream` for specialized string and type reading. |
| `StreamWriterExtensions` | Extensions to `Stream` for specialized string and type writing. |
| `StringBuilderExtensions` | Formatting overloads for `StringBuilder` to allow for type-specific handling. |
| `StringExtensions` | Optional overloads to `string.Contains`, `string.EndsWith`, `string.Equals`, and `string.StartsWith`. |
| `XmlTextWriterExtensions` | Overloads to `XmlTextWriter.WriteAttributeString` and `XmlTextWriter.WriteElementString`. |
