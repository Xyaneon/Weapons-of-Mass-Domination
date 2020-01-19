using System;
using System.Collections.Generic;

namespace WMD.Game.Rounds
{
    /// <summary>
    /// Provides details of what happened in between rounds.
    /// </summary>
    public class RoundUpdateResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoundUpdateResult"/> class.
        /// </summary>
        /// <param name="roundWhichEnded">The number of the round which ended.</param>
        /// <param name="items">The list of items which occurred between rounds.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items"/> is <see langword="null"/>.
        /// </exception>
        public RoundUpdateResult(int roundWhichEnded, IEnumerable<RoundUpdateResultItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The collection of round update items cannot be null.");
            }

            RoundWhichEnded = roundWhichEnded;
            Items = new List<RoundUpdateResultItem>(items).AsReadOnly();
        }

        /// <summary>
        /// Gets the list of items which occurred in between rounds.
        /// </summary>
        public IReadOnlyList<RoundUpdateResultItem> Items { get; }

        /// <summary>
        /// Gets the number of the round which ended.
        /// </summary>
        public int RoundWhichEnded { get; }
    }
}
