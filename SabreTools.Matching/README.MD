# SabreTools.Matching

This library contains classes designed to make matching contents and paths easier. These classes allow for both grouped and single matching as well as post-processing of matched information.

## Interfaces

| Interface | Notes |
| --- | --- |
| `IMatch<T>` | Represents a matcher for a generic type |
| `IMatchSet<T, U>` | Represents a set of `IMatch<T>` types |

## Matching Classes

| Class | Implements | Notes |
| --- | --- | --- |
| `ContentMatch` | `IMatch<byte?[]>` | Matches contents of a byte array, allowing wildcard bytes using `null` |
| `ContentMatchSet` | `IMatchSet<ContentMatch, byte?[]>` | Group of logically-linked `ContentMatch` |
| `FilePathMatch` | `IMatch<string>` | Specialization of `PathMatch` that assumes the string is a filename |
| `PathMatch` | `IMatch<string>` | Matches a set of strings representing a directory hierarchy |
| `PathMatchSet` | `IMatchSet<PathMatch, string>` | Group of logically-linked `PathMatch` |

## Utility Classes

| Class | Description |
| `ByteArrayExtensions` | Extensions for searching and matching within a byte array, similar to `System.Linq` |
| `MatchUtil` | Utility class for easier invocation of `SabreTools.Matching` functionality |
