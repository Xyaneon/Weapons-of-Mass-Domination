using WMD.Game.State.Data;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player distributing propaganda.
/// </summary>
public record DistributePropagandaResult : CommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DistributePropagandaResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="moneySpent">The amount of money spent to distribute propaganda.</param>
    /// <param name="reputationGained">The amount of reputation gained from distributing propaganda.</param>
    public DistributePropagandaResult(GameState updatedGameState, int playerIndex, decimal moneySpent, int reputationGained) : base(updatedGameState, playerIndex)
    {
        MoneySpent = moneySpent;
        ReputationGained = reputationGained;
    }

    /// <summary>
    /// Gets the amount of money spent to distribute propaganda.
    /// </summary>
    public decimal MoneySpent { get; init; } = 0.0M;

    /// <summary>
    /// Gets the amount of reputation gained from distributing propaganda.
    /// </summary>
    public int ReputationGained { get; init; } = 0;
}
