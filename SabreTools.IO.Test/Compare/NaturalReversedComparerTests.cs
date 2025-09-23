using System;
using System.Linq;
using SabreTools.IO.Compare;
using Xunit;

namespace SabreTools.IO.Test.Compare
{
    public class NaturalReversedComparerTests
    {
        [Fact]
        public void ListSort_Numeric()
        {
            // Setup arrays
            string[] sortable = ["0", "100", "5", "2", "1000"];
            string[] expected = ["1000", "100", "5", "2", "0"];

            // Run sorting on array
            Array.Sort(sortable, new NaturalReversedComparer());

            // Check the output
            Assert.True(sortable.SequenceEqual(expected));
        }

        [Fact]
        public void ListSort_Mixed()
        {
            // Setup arrays
            string[] sortable = ["b3b", "c", "b", "a", "a1"];
            string[] expected = ["c", "b3b", "b", "a1", "a"];

            // Run sorting on array
            Array.Sort(sortable, new NaturalReversedComparer());

            // Check the output
            Assert.True(sortable.SequenceEqual(expected));
        }
    }
}
