using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player stealing money.
    /// </summary>
    public record StealMoneyResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StealMoneyResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="stolenAmount">The amount of money the player stole.</param>
        public StealMoneyResult(GameState updatedGameState, int playerIndex, decimal stolenAmount) : base(updatedGameState, playerIndex)
        {
            StolenAmount = stolenAmount;
        }

        /// <summary>
        /// Gets the amount of money the player stole.
        /// </summary>
        public decimal StolenAmount { get; init; }
    }
}
