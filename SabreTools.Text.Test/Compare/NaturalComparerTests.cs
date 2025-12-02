using System;
using System.Linq;
using SabreTools.Text.Compare;
using Xunit;

namespace SabreTools.Text.Test.Compare
{
    public class NaturalComparerTests
    {
        [Fact]
        public void ListSort_Numeric()
        {
            // Setup arrays
            string[] sortable = ["0", "100", "5", "2", "1000"];
            string[] expected = ["0", "2", "5", "100", "1000"];

            // Run sorting on array
            Array.Sort(sortable, new NaturalComparer());

            // Check the output
            Assert.True(sortable.SequenceEqual(expected));
        }

        [Fact]
        public void ListSort_Mixed()
        {
            // Setup arrays
            string[] sortable = ["b3b", "c", "b", "a", "a1"];
            string[] expected = ["a", "a1", "b", "b3b", "c"];

            // Run sorting on array
            Array.Sort(sortable, new NaturalComparer());

            // Check the output
            Assert.True(sortable.SequenceEqual(expected));
        }
    }
}
