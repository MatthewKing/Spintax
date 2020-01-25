using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Spintax
{
    /// <summary>
    /// Provides functionality to spin text.
    /// </summary>
    public static class Spinner
    {
        /// <summary>
        /// RNG seed.
        /// </summary>
        private static int RngSeed;

        /// <summary>
        /// Thread-local RNG.
        /// </summary>
        private static readonly ThreadLocal<Random> Rng;

        /// <summary>
        /// Initializes static members of the <see cref="Spinner"/> class.
        /// </summary>
        static Spinner()
        {
            RngSeed = Environment.TickCount;
            Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref RngSeed)));
        }

        /// <summary>
        /// Returns a randomly chosen permutation of the specified text.
        /// </summary>
        /// <param name="spintax">The text to spin (in spintax format).</param>
        /// <returns>A randomly chosen permutation of the text.</returns>
        public static string Spin(string spintax)
        {
            const string pattern = "{[^{}]*}";

            var match = Regex.Match(spintax, pattern);
            while (match.Success)
            {
                var segment = spintax.Substring(match.Index + 1, match.Length - 2);
                var choices = segment.Split('|');
                spintax = string.Concat(
                    spintax.Substring(0, match.Index),
                    choices[Rng.Value.Next(choices.Length)],
                    spintax.Substring(match.Index + match.Length));

                match = Regex.Match(spintax, pattern);
            }

            return spintax;
        }

        /// <summary>
        /// Returns all permutations of the specified text.
        /// </summary>
        /// <param name="spintax">The text to spin (in spintax format).</param>
        /// <returns>A set containing all permutasions of the text.</returns>
        public static ISet<string> SpinAll(string spintax)
        {
            var set = new HashSet<string>();
            PopulatePermutations(spintax, set);
            return set;
        }

        /// <summary>
        /// Populates the specified set with all permutations of the specified text.
        /// </summary>
        /// <param name="spintax">The text to spin (in spintax format).</param>
        /// <param name="set">The set to populate.</param>
        private static void PopulatePermutations(string spintax, ISet<string> set)
        {
            // This is a bit of a hack, and can lead to the same permutation being inserted many times.
            // Which is why I am using a set - avoiding any duplicates. Quick and dirty.
            // This code should be re-written to something more efficient...

            const string pattern = "{[^{}]*}";

            var match = Regex.Match(spintax, pattern);
            if (match.Success)
            {
                var segment = spintax.Substring(match.Index + 1, match.Length - 2);
                var choices = segment.Split('|');
                foreach (var choice in choices)
                {
                    var newValue = string.Concat(spintax.Substring(0, match.Index), choice, spintax.Substring(match.Index + match.Length));
                    PopulatePermutations(newValue, set);
                }
            }
            else
            {
                set.Add(spintax);
            }
        }
    }
}
