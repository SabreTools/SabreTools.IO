namespace SabreTools.Matching
{
    /// <summary>
    /// Represents a matcher for a particular type
    /// </summary>
    public interface IMatch<TNeedle>
    {
        /// <summary>
        /// Nullable typed data to be matched
        /// </summary>
        public TNeedle? Needle { get; }
    }
}
