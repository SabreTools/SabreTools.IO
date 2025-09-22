namespace SabreTools.IO.Compression.Quantum
{
    /// <see href="http://www.russotto.net/quantumcomp.html"/>
    internal sealed class Model
    {
        public int Entries { get; set; }

        /// <remarks>
        /// All the models are initialized with the symbols in symbol
        /// order in the table, and with every symbol in the table
        /// having a frequency of 1
        /// </remarks>
        public ModelSymbol[]? Symbols { get; set; }

        /// <remarks>
        /// The initial total frequency is equal to the number of entries
        /// in the table
        /// </remarks>
        public int TotalFrequency { get; set; }

        /// <remarks>The initial time_to_reorder value is 4</remarks>
        public int TimeToReorder { get; set; }
    }
}