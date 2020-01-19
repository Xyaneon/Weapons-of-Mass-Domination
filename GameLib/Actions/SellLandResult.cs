using WMD.Game.Players;

namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player selling land they controlled.
    /// </summary>
    public class SellLandResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellLandResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="landAreaSold">The total land area (in square kilometers) which was sold.</param>
        /// <param name="totalSalePrice">The total sale price of the land.</param>
        public SellLandResult(Player player, GameState gameState, int landAreaSold, decimal totalSalePrice) : base(player, gameState)
        {
            LandAreaSold = landAreaSold;
            TotalSalePrice = totalSalePrice;
        }

        /// <summary>
        /// Gets the total land area (in square kilometers) which was sold.
        /// </summary>
        public int LandAreaSold { get; }

        /// <summary>
        /// Gets the total sale price of the land.
        /// </summary>
        public decimal TotalSalePrice { get; }
    }
}
