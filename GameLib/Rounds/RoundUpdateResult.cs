using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        public RoundUpdateResult(int roundWhichEnded, [DisallowNull] IEnumerable<RoundUpdateResultItem> items)
        {
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
