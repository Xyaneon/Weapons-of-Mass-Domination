using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of purchasing unclaimed land.
    /// </summary>
    public record PurchaseUnclaimedLandResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseUnclaimedLandResult"/> class.
        /// </summary>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="landAreaPurchased">The total land area (in square kilometers) which was purchased.</param>
        /// <param name="totalPurchasePrice">The total purchase price of the land.</param>
        public PurchaseUnclaimedLandResult(GameState gameState, int playerIndex, int landAreaPurchased, decimal totalPurchasePrice) : base(gameState, playerIndex)
        {
            LandAreaPurchased = landAreaPurchased;
            TotalPurchasePrice = totalPurchasePrice;
        }

        /// <summary>
        /// Gets the total land area (in square kilometers) which was purchased.
        /// </summary>
        public int LandAreaPurchased { get; init; }

        /// <summary>
        /// Gets the total purchase price of the land.
        /// </summary>
        public decimal TotalPurchasePrice { get; init; }
    }
}
