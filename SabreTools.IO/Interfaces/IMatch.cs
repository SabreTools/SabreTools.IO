namespace SabreTools.IO.Interfaces
{
    /// <summary>
    /// Represents a matcher for a particular type
    /// </summary>
    public interface IMatch<T>
    {
        /// <summary>
        /// Nullable typed data to be matched
        /// </summary>
        T? Needle { get; }
    }
}
