# SabreTools.IO

This library contains both custom stream implementations and utility classes for general IO operations.

**Note:** This package is called `SabreTools.IO.Common` due to the former monolithic library that was also just called `SabreTools.IO`. That package is now reserved for the `SabreTools.IO` meta-package, which is built using `SabreTools.IO.Meta` in this repository.

## Stream Implementations

| Class | Notes |
| --- | --- |
| `BufferedStream` | Not a true stream implementation used for buffered, single-byte reads |
| `ReadOnlyBitStream` | Read-only stream implementation allowing bitwise reading |
| `ReadOnlyCompositeStream` | Read-only stream implementation that wraps multiple source streams in a set order |
| `ReadOnlyViewStream` | Read-only stream implementation representing a view into source data |

## Utility Classes

| Class | Notes |
| --- | --- |
| `ParentablePath` | Class that represents a path that is rooted by a parent directory |
| `PathTool` | Utility class for common Path-related functions |
