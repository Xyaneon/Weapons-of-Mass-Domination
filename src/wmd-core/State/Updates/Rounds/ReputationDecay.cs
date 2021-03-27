namespace WMD.Game.State.Updates.Rounds
{
    /// <summary>
    /// An occurrence of a player losing reputation due to the passage of time.
    /// </summary>
    public record ReputationDecay : RoundUpdateResultItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReputationDecay"/> class.
        /// </summary>
        /// <param name="playerIndex">The index of the player who lost reputation due to the passage of time.</param>
        /// <param name="reputationPercentageLost">The percentage of reputation lost due to the passage of time.</param>
        public ReputationDecay(int playerIndex, int reputationPercentageLost)
        {
            PlayerIndex = playerIndex;
            ReputationPercentageLost = reputationPercentageLost;
        }

        /// <summary>
        /// Gets the index of the player who lost reputation.
        /// </summary>
        public int PlayerIndex { get; init; }

        /// <summary>
        /// Gets the percentage of reputation lost due to the passage of time.
        /// </summary>
        public int ReputationPercentageLost { get; init; }
    }
}
