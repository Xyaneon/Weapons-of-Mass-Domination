namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player skipping their turn.
    /// </summary>
    public class SkipTurnResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkipTurnResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        public SkipTurnResult(Player player) : base(player) { }
    }
}
