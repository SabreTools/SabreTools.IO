namespace SabreTools.Text.Compare
{
    internal static class NaturalComparerUtil
    {
        /// <summary>
        /// Compare two strings by path parts
        /// </summary>
        public static int ComparePaths(string? left, string? right)
        {
            // If both strings are null, return
            if (left == null && right == null)
                return 0;

            // If one is null, then say that's less than
            if (left == null)
                return -1;
            if (right == null)
                return 1;

            // Normalize the path seperators
            left = left.Replace('\\', '/');
            right = right.Replace('\\', '/');

            // Save the orginal adjusted strings
            string leftOrig = left;
            string rightOrig = right;

            // Normalize strings by lower-case
            left = leftOrig.ToLowerInvariant();
            right = rightOrig.ToLowerInvariant();

            // If the strings are the same exactly, return
            if (left == right)
                return leftOrig.CompareTo(rightOrig);

            // Now split into path parts
            string[] leftParts = left.Split('/');
            string[] rightParts = right.Split('/');

            // Then compare each part in turn
            for (int i = 0; i < leftParts.Length && i < rightParts.Length; i++)
            {
                int partCompare = ComparePathSegment(leftParts[i], rightParts[i]);
                if (partCompare != 0)
                    return partCompare;
            }

            // If we got out here, then it looped through at least one of the strings
            if (leftParts.Length > rightParts.Length)
                return 1;
            if (leftParts.Length < rightParts.Length)
                return -1;

            return leftOrig.CompareTo(rightOrig);
        }

        /// <summary>
        /// Compare two path segments deterministically
        /// </summary>
        private static int ComparePathSegment(string left, string right)
        {
            // If the lengths are both zero, they're equal
            if (left.Length == 0 && right.Length == 0)
                return 0;

            // Shorter strings are sorted before
            if (left.Length == 0)
                return -1;
            if (right.Length == 0)
                return 1;

            // Otherwise, loop through until we have an answer
            for (int i = 0; i < left.Length && i < right.Length; i++)
            {
                // Get the next characters from the inputs as integers
                int leftChar = left[i];
                int rightChar = right[i];

                // If the characters are the same, continue
                if (leftChar == rightChar)
                    continue;

                // If they're different, check which one was larger
                return leftChar > rightChar ? 1 : -1;
            }

            // If we got out here, then it looped through at least one of the strings
            if (left.Length > right.Length)
                return 1;
            if (left.Length < right.Length)
                return -1;

            return 0;
        }
    }
}
