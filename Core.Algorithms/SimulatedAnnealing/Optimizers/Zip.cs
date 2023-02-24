using System;
using System.Collections.Generic;

namespace Algorithms.SimulatedAnnealing.Optimizers
{
    internal static class MoreEnumerable
    {
        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.
        /// </summary>
        /// <remarks>
        /// If the two input sequences are of different lengths, the result sequence
        /// is terminated as soon as the shortest input sequence is exhausted.
        /// This operator uses deferred execution and streams its results.
        /// </remarks>
        /// <example>
        /// <code>
        /// int[] numbers = { 1, 2, 3 };
        /// string[] letters = { "A", "B", "C", "D" };
        /// var zipped = numbers.Zip(letters, (n, l) => n + l);
        /// </code>
        /// The <c>zipped</c> variable, when iterated over, will yield "1A", "2B", "3C", in turn.
        /// </example>
        /// <typeparam name="TFirst">Type of elements in first sequence</typeparam>
        /// <typeparam name="TSecond">Type of elements in second sequence</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence</typeparam>
        /// <param name="first">First sequence</param>
        /// <param name="second">Second sequence</param>
        /// <param name="resultSelector">Function to apply to each pair of elements</param>
        /// <returns>
        /// A sequence that contains elements of the two input sequences,
        /// combined by <paramref name="resultSelector"/>.
        /// </returns>

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return ZipImpl(first, second, resultSelector);
        }

        static IEnumerable<TResult> ZipImpl<TFirst, TSecond, TResult>(
            IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    if (e2.MoveNext())
                        yield return resultSelector(e1.Current, e2.Current);
                }
            }
        }
    }
}
