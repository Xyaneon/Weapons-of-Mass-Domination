namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player's action.
    /// This class cannot be instantiated.
    /// </summary>
    public abstract class ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        public ActionResult(Player player, GameState gameState)
        {
            Player = player;
            GameState = gameState;
        }

        /// <summary>
        /// Gets the updated <see cref="GameState"/> resulting from this action.
        /// </summary>
        public GameState GameState { get; }

        /// <summary>
        /// Gets the <see cref="Player"/> whose action this is the result of.
        /// </summary>
        public Player Player { get; }
    }
}
