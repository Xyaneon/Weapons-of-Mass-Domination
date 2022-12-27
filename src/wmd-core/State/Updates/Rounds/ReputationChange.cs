namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// An occurrence of a player's reputation changing due to the passage of time.
/// </summary>
public record ReputationChange : RoundUpdateResultItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReputationChange"/> class.
    /// </summary>
    /// <param name="playerIndex">The index of the player whose reputation changed due to the passage of time.</param>
    /// <param name="reputationPercentageChanged">The percentage of reputation changed due to the passage of time.</param>
    public ReputationChange(int playerIndex, int reputationPercentageChanged)
    {
        PlayerIndex = playerIndex;
        ReputationPercentageChanged = reputationPercentageChanged;
    }

    /// <summary>
    /// Gets the index of the player whose reputation changed.
    /// </summary>
    public int PlayerIndex { get; init; }

    /// <summary>
    /// Gets the percentage of reputation changed due to the passage of time.
    /// </summary>
    public int ReputationPercentageChanged { get; init; }
}
