using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player selling land they controlled.
    /// </summary>
    public record SellLandResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellLandResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="landAreaSold">The total land area (in square kilometers) which was sold.</param>
        /// <param name="totalSalePrice">The total sale price of the land.</param>
        public SellLandResult(GameState updatedGameState, int playerIndex, int landAreaSold, decimal totalSalePrice) : base(updatedGameState, playerIndex)
        {
            LandAreaSold = landAreaSold;
            TotalSalePrice = totalSalePrice;
        }

        /// <summary>
        /// Gets the total land area (in square kilometers) which was sold.
        /// </summary>
        public int LandAreaSold { get; init; }

        /// <summary>
        /// Gets the total sale price of the land.
        /// </summary>
        public decimal TotalSalePrice { get; init; }
    }
}
