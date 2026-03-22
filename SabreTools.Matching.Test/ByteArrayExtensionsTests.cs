using System;
using Xunit;

namespace SabreTools.Matching.Test
{
    public class ByteArrayExtensionsTests
    {
        #region FindAllPositions

        [Fact]
        public void FindAllPositions_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            var positions = stack.FindAllPositions([0x01]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions(Array.Empty<byte>());
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02], start: -1);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], start: 2);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            var positions = stack.FindAllPositions([0x01, 0x02], end: -2);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], end: 0);
            Assert.Empty(positions);

            positions = stack.FindAllPositions([0x01, 0x02], end: 2);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            int position = Assert.Single(positions);
            Assert.Equal(0, position);
        }

        [Fact]
        public void FindAllPositions_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            var positions = stack.FindAllPositions([0x01, 0x02]);
            Assert.Empty(positions);
        }

        [Fact]
        public void FindAllPositions_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            var positions = stack.FindAllPositions([0x01]);
            Assert.Equal(2, positions.Count);
        }

        #endregion

        #region FirstPosition

        [Fact]
        public void FirstPosition_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            int position = stack.FirstPosition([0x01]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition(Array.Empty<byte>());
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02], start: -1);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], start: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.FirstPosition([0x01, 0x02], end: -2);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], end: 0);
            Assert.Equal(-1, position);

            position = stack.FirstPosition([0x01, 0x02], end: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(0, position);
        }

        [Fact]
        public void FirstPosition_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            int position = stack.FirstPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void FirstPosition_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            int position = stack.FirstPosition([0x01]);
            Assert.Equal(0, position);
        }

        #endregion

        #region LastPosition

        [Fact]
        public void LastPosition_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            int position = stack.LastPosition([0x01]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition(Array.Empty<byte>());
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_InvalidStart_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02], start: -1);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], start: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_InvalidEnd_NoMatches()
        {
            byte[] stack = [0x01];
            int position = stack.LastPosition([0x01, 0x02], end: -2);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], end: 0);
            Assert.Equal(-1, position);

            position = stack.LastPosition([0x01, 0x02], end: 2);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(0, position);
        }

        [Fact]
        public void LastPosition_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            int position = stack.LastPosition([0x01, 0x02]);
            Assert.Equal(-1, position);
        }

        [Fact]
        public void LastPosition_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            int position = stack.LastPosition([0x01]);
            Assert.Equal(1, position);
        }

        #endregion

        #region EqualsExactly

        [Fact]
        public void EqualsExactly_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.EqualsExactly([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EqualsExactly(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_ShorterNeedle_NoMatches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EqualsExactly([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EqualsExactly_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void EqualsExactly_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.EqualsExactly([0x01, 0x02]);
            Assert.False(found);
        }

        #endregion

        #region StartsWith

        [Fact]
        public void StartsWith_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.StartsWith([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void StartsWith_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void StartsWith_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            bool found = stack.StartsWith([0x01]);
            Assert.True(found);
        }

        #endregion

        #region EndsWith

        [Fact]
        public void EndsWith_EmptyStack_NoMatches()
        {
            byte[] stack = [];
            bool found = stack.EndsWith([0x01]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_EmptyNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.EndsWith(Array.Empty<byte>());
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_LongerNeedle_NoMatches()
        {
            byte[] stack = [0x01];
            bool found = stack.StartsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_Matching_Matches()
        {
            byte[] stack = [0x01, 0x02];
            bool found = stack.EndsWith([0x01, 0x02]);
            Assert.True(found);
        }

        [Fact]
        public void EndsWith_Mismatch_NoMatches()
        {
            byte[] stack = [0x01, 0x03];
            bool found = stack.EndsWith([0x01, 0x02]);
            Assert.False(found);
        }

        [Fact]
        public void EndsWith_Multiple_Matches()
        {
            byte[] stack = [0x01, 0x01];
            bool found = stack.EndsWith([0x01]);
            Assert.True(found);
        }

        #endregion
    }
}
