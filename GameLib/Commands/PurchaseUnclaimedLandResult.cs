using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of purchasing unclaimed land.
    /// </summary>
    public class PurchaseUnclaimedLandResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseUnclaimedLandResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="landAreaPurchased">The total land area (in square kilometers) which was purchased.</param>
        /// <param name="totalPurchasePrice">The total purchase price of the land.</param>
        public PurchaseUnclaimedLandResult(Player player, GameState gameState, int landAreaPurchased, decimal totalPurchasePrice) : base(player, gameState)
        {
            LandAreaPurchased = landAreaPurchased;
            TotalPurchasePrice = totalPurchasePrice;
        }

        /// <summary>
        /// Gets the total land area (in square kilometers) which was purchased.
        /// </summary>
        public int LandAreaPurchased { get; }

        /// <summary>
        /// Gets the total purchase price of the land.
        /// </summary>
        public decimal TotalPurchasePrice { get; }
    }
}
