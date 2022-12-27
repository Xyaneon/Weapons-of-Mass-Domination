using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// Provides details of what happened in between rounds.
/// </summary>
public record RoundUpdateResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundUpdateResult"/> class.
    /// </summary>
    /// <param name="gameState">The <see cref="GameState"/> after the round ended.</param>
    /// <param name="roundWhichEnded">The number of the round which ended.</param>
    /// <param name="items">The list of items which occurred between rounds.</param>
    public RoundUpdateResult([DisallowNull] GameState gameState, int roundWhichEnded, [DisallowNull] IEnumerable<RoundUpdateResultItem> items)
    {
        GameState = gameState;
        RoundWhichEnded = roundWhichEnded;
        Items = new List<RoundUpdateResultItem>(items).AsReadOnly();
    }

    /// <summary>
    /// Gets the <see cref="GameState"/> after the round ended.
    /// </summary>
    public GameState GameState { get; init; }

    /// <summary>
    /// Gets the list of items which occurred in between rounds.
    /// </summary>
    public IReadOnlyList<RoundUpdateResultItem> Items { get; init; }

    /// <summary>
    /// Gets the number of the round which ended.
    /// </summary>
    public int RoundWhichEnded { get; init; }
}
