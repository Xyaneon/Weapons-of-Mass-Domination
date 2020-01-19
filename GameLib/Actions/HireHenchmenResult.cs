namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player hiring henchmen.
    /// </summary>
    public class HireHenchmenResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HireHenchmenResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="henchmenHired">The number of henchmen successfully hired.</param>
        public HireHenchmenResult(Player player, GameState gameState, int henchmenHired) : base(player, gameState)
        {
            HenchmenHired = henchmenHired;
        }

        /// <summary>
        /// Gets the number of henchmen successfully hired.
        /// </summary>
        public int HenchmenHired { get; }
    }
}
