using System;
using System.IO;
using SabreTools.IO.Interfaces;

namespace SabreTools.IO.Matching
{
    /// <summary>
    /// Content matching criteria
    /// </summary>
    public class ContentMatch : IMatch<byte?[]>
    {
        /// <summary>
        /// Content to match
        /// </summary>
        public byte?[] Needle { get; }

        /// <summary>
        /// Starting index for matching
        /// </summary>
        private readonly int _start;

        /// <summary>
        /// Ending index for matching
        /// </summary>
        private readonly int _end;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">Byte array representing the search</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public ContentMatch(byte[] needle, int start = 0, int end = -1)
        {
            // Validate the inputs
            if (needle.Length == 0)
                throw new InvalidDataException(nameof(needle));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (end < -1)
                throw new ArgumentOutOfRangeException(nameof(end));

            Needle = Array.ConvertAll(needle, b => (byte?)b);
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">Nullable byte array representing the search</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public ContentMatch(byte?[] needle, int start = 0, int end = -1)
        {
            // Validate the inputs
            if (needle.Length == 0)
                throw new InvalidDataException(nameof(needle));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (end < -1)
                throw new ArgumentOutOfRangeException(nameof(end));

            Needle = needle;
            _start = start;
            _end = end;
        }

        #region Conversion

        /// <summary>
        /// Allow conversion from byte array to ContentMatch
        /// </summary>
        public static implicit operator ContentMatch(byte[] needle) => new(needle);

        /// <summary>
        /// Allow conversion from nullable byte array to ContentMatch
        /// </summary>
        public static implicit operator ContentMatch(byte?[] needle) => new(needle);

        #endregion

        #region Array Matching

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">Array to search for the given content</param>
        /// <param name="reverse">True to search from the end of the array, false from the start</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        public int Match(byte[]? stack, bool reverse = false)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Needle.Length == 0)
                return -1;

            // Get the adjusted end value for comparison
            int end = _end < 0 ? stack.Length : _end;
            end = end > stack.Length ? stack.Length : end;

            // If the stack window is invalid
            if (end < _start)
                return -1;

            // If the needle is larger than the stack window, it can't be contained within
            if (Needle.Length > stack.Length - _start)
                return -1;

            // If the needle and stack window are identically sized, short-circuit
            if (Needle.Length == stack.Length - _start)
                return EqualAt(stack, _start) ? _start : -1;

            // Return based on the direction of search
            return reverse ? MatchReverse(stack) : MatchForward(stack);
        }

        /// <summary>
        /// Match within a stack starting from the smallest index
        /// </summary>
        /// <param name="stack">Array to search for the given content</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        private int MatchForward(byte[] stack)
        {
            // Set the default start and end values
            int start = _start < 0 ? 0 : _start;
            int end = _end < 0 ? stack.Length - Needle.Length : _end;

            // Loop starting from the smallest index
            for (int i = start; i < end; i++)
            {
                // If we somehow have an invalid end and we haven't matched, return
                if (i > stack.Length)
                    return -1;

                // Check to see if the values are equal
                if (EqualAt(stack, i))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Match within a stack starting from the largest index
        /// </summary>
        /// <param name="stack">Array to search for the given content</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        private int MatchReverse(byte[] stack)
        {
            // Set the default start and end values
            int start = _start < 0 ? 0 : _start;
            int end = _end < 0 ? stack.Length - Needle.Length : _end;

            // Loop starting from the largest index
            for (int i = end; i > start; i--)
            {
                // If we somehow have an invalid end and we haven't matched, return
                if (i > stack.Length)
                    return -1;

                // Check to see if the values are equal
                if (EqualAt(stack, i))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Get if a stack at a certain index is equal to a needle
        /// </summary>
        /// <param name="stack">Array to search for the given content</param>
        /// <param name="index">Starting index to check equality</param>
        /// <returns>True if the needle matches the stack at a given index</returns>
        private bool EqualAt(byte[] stack, int index)
        {
            // If the index is invalid, we can't do anything
            if (index < 0)
                return false;

            // If we're too close to the end of the stack, return false
            if (Needle.Length > stack.Length - index)
                return false;

            // Loop through and check the value
            for (int i = 0; i < Needle.Length; i++)
            {
                // A null value is a wildcard
                if (Needle[i] == null)
                    continue;
                else if (stack[i + index] != Needle[i])
                    return false;
            }

            return true;
        }

        #endregion

        #region Stream Matching

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">Stream to search for the given content</param>
        /// <param name="reverse">True to search from the end of the array, false from the start</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        public int Match(Stream? stack, bool reverse = false)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Needle.Length == 0)
                return -1;

            // Get the adjusted end value for comparison
            int end = _end < 0 ? (int)stack.Length : _end;
            end = end > (int)stack.Length ? (int)stack.Length : end;

            // If the stack window is invalid
            if (end < _start)
                return -1;

            // If the needle is larger than the stack window, it can't be contained within
            if (Needle.Length > stack.Length - _start)
                return -1;

            // If the needle and stack window are identically sized, short-circuit
            if (Needle.Length == stack.Length - _start)
                return EqualAt(stack, _start) ? _start : -1;

            // Return based on the direction of search
            return reverse ? MatchReverse(stack) : MatchForward(stack);
        }

        /// <summary>
        /// Match within a stack starting from the smallest index
        /// </summary>
        /// <param name="stack">Stream to search for the given content</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        private int MatchForward(Stream stack)
        {
            // Set the default start and end values
            int start = _start < 0 ? 0 : _start;
            int end = _end < 0 ? (int)stack.Length - Needle.Length : _end;

            // Loop starting from the smallest index
            for (int i = start; i < end; i++)
            {
                // If we somehow have an invalid end and we haven't matched, return
                if (i > stack.Length)
                    return -1;

                // Check to see if the values are equal
                if (EqualAt(stack, i))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Match within a stack starting from the largest index
        /// </summary>
        /// <param name="stack">Stream to search for the given content</param>
        /// <returns>Found position on success, -1 otherwise</returns>
        private int MatchReverse(Stream stack)
        {
            // Set the default start and end values
            int start = _start < 0 ? 0 : _start;
            int end = _end < 0 ? (int)stack.Length - Needle.Length : _end;

            // Loop starting from the largest index
            for (int i = end; i > start; i--)
            {
                // If we somehow have an invalid end and we haven't matched, return
                if (i > stack.Length)
                    return -1;

                // Check to see if the values are equal
                if (EqualAt(stack, i))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Get if a stack at a certain index is equal to a needle
        /// </summary>
        /// <param name="stack">Stream to search for the given content</param>
        /// <param name="index">Starting index to check equality</param>
        /// <returns>True if the needle matches the stack at a given index</returns>
        private bool EqualAt(Stream stack, int index)
        {
            // If the index is invalid, we can't do anything
            if (index < 0)
                return false;

            // If we're too close to the end of the stack, return false
            if (Needle.Length > stack.Length - index)
                return false;

            // Save the current position and move to the index
            long currentPosition = stack.Position;
            stack.Seek(index, SeekOrigin.Begin);

            // Set the return value
            bool matched = true;

            // Loop through and check the value
            for (int i = 0; i < Needle.Length; i++)
            {
                byte stackValue = (byte)stack.ReadByte();

                // A null value is a wildcard
                if (Needle[i] == null)
                {
                    continue;
                }
                else if (stackValue != Needle[i])
                {
                    matched = false;
                    break;
                }
            }

            // Reset the position and return the value
            stack.Seek(currentPosition, SeekOrigin.Begin);
            return matched;
        }

        #endregion
    }
}
