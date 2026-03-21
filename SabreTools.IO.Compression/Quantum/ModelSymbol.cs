namespace SabreTools.IO.Compression.Quantum
{
    /// <see href="http://www.russotto.net/quantumcomp.html"/>
    internal sealed class ModelSymbol
    {
        public ushort Symbol { get; set; }

        /// <summary>
        /// The cumulative frequency is the frequency of all the symbols
        /// which are at a higher index in the table than that symbol —
        /// thus the last entry in the table has a cumulative frequency of 0.
        /// </summary>
        public ushort CumulativeFrequency { get; set; }
    }
}
