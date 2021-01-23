using WMD.Game.Players;

namespace WMD.Game.Rounds
{
    /// <summary>
    /// An occurrence of a player's henchmen getting paid.
    /// </summary>
    public record PlayerHenchmenPaid : RoundUpdateResultItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHenchmenPaid"/> class.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player who paid their henchmen.</param>
        public PlayerHenchmenPaid(GameState gameState, int playerIndex)
        {
            PlayerState playerState = gameState.Players[playerIndex].State;
            DailyPayRate = playerState.WorkforceState.DailyPayRate;
            NumberOfHenchmenPaid = playerState.WorkforceState.NumberOfHenchmen;
            PlayerIndex = playerIndex;
        }

        /// <summary>
        /// Gets the player's daily pay rate for their henchmen.
        /// </summary>
        public decimal DailyPayRate { get; init; }

        /// <summary>
        /// Gets the number of henchmen the player paid.
        /// </summary>
        public int NumberOfHenchmenPaid { get; init; }

        /// <summary>
        /// Gets the index of the player who paid their henchmen.
        /// </summary>
        public int PlayerIndex { get; init; }

        /// <summary>
        /// Gets the total amount of money the player paid their henchmen.
        /// </summary>
        public decimal TotalPaidAmount { get => DailyPayRate * NumberOfHenchmenPaid; }
    }
}
