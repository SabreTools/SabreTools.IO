# SabreTools.IO

This library contains both custom stream implementations and utility classes for general IO operations.

## Stream Implementations

| Class | Notes |
| --- | --- |
| `BufferedStream` | Not a true stream implementation used for buffered, single-byte reads |
| `ReadOnlyBitStream` | Read-only stream implementation allowing bitwise reading |
| `ReadOnlyCompositeStream` | Read-only stream implementation that wraps multiple source streams in a set order |
| `ViewStream` | Read-only stream implementation representing a view into source data |

## Utility Classes

| Class | Notes |
| --- | --- |
| `ParentablePath` | Class that represents a path that is rooted by a parent directory |
| `PathTool` | Utility class for common Path-related functions |
| `Transform` | Utility class that allows common data transformations for streams and byte arrays |

`Transform` functionality includes:

- Combine using either ordered concatenation or interleaving
- Split by even/odd chunks or based on block size
- Convert data either by bit-swapping, byte-swapping, word-swapping, or word/byte-swapping
