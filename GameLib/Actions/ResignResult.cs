namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player resigning.
    /// </summary>
    public class ResignResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResignResult"/> class.
        /// </summary>
        /// <param name="player">The current <see cref="Player"/> whose action this is the result of.</param>
        public ResignResult(Player player) : base(player) { }
    }
}
