using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class EnumerableExtensionsTests
    {
        #region IterateWithAction

        [Fact]
        public void IterateWithActionTest()
        {
            List<int> source = [1, 2, 3, 4];
            int actual = 0;

            source.IterateWithAction(i => Interlocked.Add(ref actual, i));
            Assert.Equal(10, actual);
        }

        #endregion

        #region SafeEnumerate

        [Fact]
        public void SafeEnumerate_Empty()
        {
            var source = Enumerable.Empty<string>();
            var safe = source.SafeEnumerate();
            var list = safe.ToList();
            Assert.Empty(list);
        }

        [Fact]
        public void SafeEnumerate_Throws()
        {
            var source = new List<string> { "a", "ab", "abc" };
            var wrapper = new ThrowsEnumerable(source);

            var safe = wrapper.SafeEnumerate();
            var list = safe.ToList();
            Assert.Empty(list);
        }

        [Fact]
        public void SafeEnumerate_NoError()
        {
            var source = new List<string> { "a", "ab", "abc" };
            var safe = source.SafeEnumerate();
            var list = safe.ToList();
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void SafeEnumerate_ErrorMid()
        {
            var source = new List<string> { "a", "ab", "abc" };
            var wrapper = new ErrorEnumerable(source);

            var safe = wrapper.SafeEnumerate();
            var list = safe.ToList();
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void SafeEnumerate_ErrorLast()
        {
            var source = new List<string> { "a", "ab", "abc", "abcd" };
            var wrapper = new ErrorEnumerable(source);

            var safe = wrapper.SafeEnumerate();
            var list = safe.ToList();
            Assert.Equal(2, list.Count);
        }

        #endregion

        /// <summary>
        /// Fake enumerable that uses <see cref="ErrorEnumerator"/> 
        /// </summary>
        private class ErrorEnumerable : IEnumerable<string>
        {
            /// <summary>
            /// Enumerator to use during enumeration
            /// </summary>
            private readonly ErrorEnumerator _enumerator;

            public ErrorEnumerable(IEnumerable<string> source)
            {
                _enumerator = new ErrorEnumerator(source);
            }

            /// <inheritdoc/>
            public IEnumerator<string> GetEnumerator() => _enumerator;

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => _enumerator;
        }

        /// <summary>
        /// Fake enumerator that throws an exception every other item while moving to the next item
        /// </summary>
        private class ErrorEnumerator : IEnumerator<string>
        {
            /// <inheritdoc/>
            public string Current
            {
                get
                {
                    if (_index == -1)
                        throw new InvalidOperationException();

                    return _enumerator.Current;
                }
            }

            /// <inheritdoc/>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Enumerator from the source enumerable
            /// </summary>
            private readonly IEnumerator<string> _enumerator;

            /// <summary>
            /// Enumerators start before the data
            /// </summary>
            private int _index = -1;

            public ErrorEnumerator(IEnumerable<string> source)
            {
                _enumerator = source.GetEnumerator();
            }

            /// <inheritdoc/>
            public void Dispose() { }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                // Move to the next item, if possible
                bool moved = _enumerator.MoveNext();
                if (!moved)
                    return false;

                // Get the next real item
                _index++;

                // Every other move, throw an exception
                if (_index % 2 == 1)
                    throw new Exception("Access issue for this item in the enumerable");

                return true;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                _enumerator.Reset();
                _index = -1;
            }
        }

        /// <summary>
        /// Fake enumerable that throws an exception for the enumerator
        /// </summary>
        private class ThrowsEnumerable : IEnumerable<string>
        {
            public ThrowsEnumerable(IEnumerable<string> source) { }

            /// <inheritdoc/>
            public IEnumerator<string> GetEnumerator() => throw new Exception();

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => throw new Exception();
        }
    }
}
