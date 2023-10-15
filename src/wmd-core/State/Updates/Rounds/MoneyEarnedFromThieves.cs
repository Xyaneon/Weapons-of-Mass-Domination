namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// An occurrence of a player earning money from their thieves.
/// </summary>
public record MoneyEarnedFromThieves : RoundUpdateResultItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoneyEarnedFromThieves"/>
    /// class.
    /// </summary>
    /// <param name="playerIndex">The index of the player who earned money from their thieves.</param>
    /// <param name="amount">The total amount of money the player earned from their thieves this turn.</param>
    public MoneyEarnedFromThieves(int playerIndex, decimal amount)
    {
        PlayerIndex = playerIndex;
        Amount = amount;
    }

    /// <summary>
    /// Gets the index of the player who earned money from their thieves.
    /// </summary>
    public int PlayerIndex { get; init; }

    /// <summary>
    /// Gets the total amount of money the player earned from their thieves
    /// this turn.
    /// </summary>
    public decimal Amount { get; init; }
}
