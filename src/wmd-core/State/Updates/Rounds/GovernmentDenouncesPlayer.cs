using System;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// An occurrence of a government denouncing a player.
/// </summary>
public record GovernmentDenouncesPlayer : GovernmentIntervention
{
    private const string ArgumentOutOfRangeException_ReputationLostExceedsPlayerAmount = "The amount of reputation lost cannot exceed the amount the player actually has.";

    /// <summary>
    /// Initializes a new instance of the <see cref="GovernmentDenouncesPlayer"/> class.
    /// </summary>
    /// <param name="gameState">The <see cref="GameState"/>.</param>
    /// <param name="playerIndex">The index of the player who was denounced.</param>
    /// <param name="reputationLost">The amount of reputation the player lost.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="reputationLost"/> is greater than the amount of reputation the player actually has.
    /// </exception>
    public GovernmentDenouncesPlayer(GameState gameState, int playerIndex, int reputationLost)
    {
        var playerState = gameState.Players[playerIndex].State;
        if (reputationLost > playerState.ReputationPercentage)
        {
            throw new ArgumentOutOfRangeException(nameof(reputationLost), reputationLost, ArgumentOutOfRangeException_ReputationLostExceedsPlayerAmount);
        }
        PlayerIndex = playerIndex;
        ReputationDecrease = reputationLost;
    }

    /// <summary>
    /// Gets the index of the player who was denounced.
    /// </summary>
    public int PlayerIndex { get; init; }

    /// <summary>
    /// Gets the amount of reputation the player lost.
    /// </summary>
    public int ReputationDecrease { get; init; }
}
