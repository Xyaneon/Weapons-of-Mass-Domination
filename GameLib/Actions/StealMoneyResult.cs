namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player stealing money.
    /// </summary>
    public class StealMoneyResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StealMoneyResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="stolenAmount">The amount of money the player stole.</param>
        public StealMoneyResult(Player player, GameState gameState, decimal stolenAmount) : base(player, gameState)
        {
            StolenAmount = stolenAmount;
        }

        /// <summary>
        /// Gets the amount of money the player stole.
        /// </summary>
        public decimal StolenAmount { get; }
    }
}
