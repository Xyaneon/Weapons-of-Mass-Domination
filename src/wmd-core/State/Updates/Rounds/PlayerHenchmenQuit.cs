using System;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// An occurrence of a player's henchmen quitting.
/// </summary>
public record PlayerHenchmenQuit : RoundUpdateResultItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerHenchmenQuit"/> class.
    /// </summary>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose henchmen quit.</param>
    /// <param name="numberOfHenchmenQuit">The number of henchmen who quit.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="numberOfHenchmenQuit"/> is less than one.
    /// </exception>
    public PlayerHenchmenQuit(int playerIndex, long numberOfHenchmenQuit)
    {
        if (numberOfHenchmenQuit <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfHenchmenQuit), numberOfHenchmenQuit, "The number of henchmen who quit must be at least one.");
        }

        PlayerIndex = playerIndex;
        NumberOfHenchmenQuit = numberOfHenchmenQuit;
    }

    /// <summary>
    /// Gets the number of henchmen who quit.
    /// </summary>
    public long NumberOfHenchmenQuit { get; init; }

    /// <summary>
    /// Gets the index of the <see cref="Player"/> who lost henchmen.
    /// </summary>
    public int PlayerIndex { get; init; }
}
