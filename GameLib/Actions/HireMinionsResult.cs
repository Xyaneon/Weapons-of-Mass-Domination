namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player hiring minions.
    /// </summary>
    public class HireMinionsResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HireMinionsResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="minionsHired">The number of minions successfully hired.</param>
        public HireMinionsResult(Player player, GameState gameState, int minionsHired) : base(player, gameState)
        {
            MinionsHired = minionsHired;
        }

        /// <summary>
        /// Gets the number of minions successfully hired.
        /// </summary>
        public int MinionsHired { get; }
    }
}
