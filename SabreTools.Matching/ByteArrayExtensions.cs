using System;
using System.Collections.Generic;

namespace SabreTools.Matching
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Find all positions of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static List<int> FindAllPositions(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return FindAllPositions(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find all positions of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static List<int> FindAllPositions(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // Get the outgoing list
            List<int> positions = [];

            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return positions;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return positions;

            // Normalize the end value, if necessary
            if (end == -1)
                end = stack.Length;

            // Validate the start and end values
            if (start < 0 || start >= stack.Length)
                return positions;
            if (end < -1 || end < start || end > stack.Length)
                return positions;

            // Loop while there is data to check
            while (start < end)
            {
                // Create a new matcher for this segment
                var matcher = new ContentMatch(needle, start, end);

                // Get the next matching position
                int position = matcher.Match(stack, reverse: false);
                if (position < 0)
                    break;

                // Append the position and reset the start index
                positions.Add(position);
                start = position + 1;
            }

            return positions;
        }

        /// <summary>
        /// Find the first position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int FirstPosition(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return FirstPosition(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find the first position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int FirstPosition(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return -1;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return -1;

            var matcher = new ContentMatch(needle, start, end);
            return matcher.Match(stack, reverse: false);
        }

        /// <summary>
        /// Find the last position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int LastPosition(this byte[] stack, byte[] needle, int start = 0, int end = -1)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return LastPosition(stack, nullableNeedle, start, end);
        }

        /// <summary>
        /// Find the last position of one array in another, if possible
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        /// <param name="start">Optional starting position in the stack, defaults to 0</param>
        /// <param name="end">Optional ending position in the stack, defaults to -1 (length of stack)</param>
        public static int LastPosition(this byte[] stack, byte?[] needle, int start = 0, int end = -1)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return -1;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return -1;

            var matcher = new ContentMatch(needle, start, end);
            return matcher.Match(stack, reverse: true);
        }

        /// <summary>
        /// Check if a byte array exactly matches another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EqualsExactly(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return EqualsExactly(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array exactly matches another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EqualsExactly(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is not the exact length of the stack
            if (needle.Length != stack.Length)
                return false;

            return FirstPosition(stack, needle, start: 0, end: 1) == 0;
        }

        /// <summary>
        /// Check if a byte array starts with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool StartsWith(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return StartsWith(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array starts with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool StartsWith(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return false;

            return FirstPosition(stack, needle, start: 0, end: 1) > -1;
        }

        /// <summary>
        /// Check if a byte array ends with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EndsWith(this byte[] stack, byte[] needle)
        {
            byte?[] nullableNeedle = Array.ConvertAll(needle, b => (byte?)b);
            return EndsWith(stack, nullableNeedle);
        }

        /// <summary>
        /// Check if a byte array ends with another
        /// </summary>
        /// <param name="stack">Byte array to search within</param>
        /// <param name="needle">Byte array representing the search value</param>
        public static bool EndsWith(this byte[] stack, byte?[] needle)
        {
            // If either set is null or empty
            if (stack.Length == 0 || needle.Length == 0)
                return false;

            // If the needle is longer than the stack
            if (needle.Length > stack.Length)
                return false;

            return FirstPosition(stack, needle, start: stack.Length - needle.Length) > -1;
        }
    }
}
