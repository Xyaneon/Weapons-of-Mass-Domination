using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of the <see cref="ResignAction"/>.
    /// </summary>
    public class ResignResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResignResult"/> class.
        /// </summary>
        /// <param name="player">The current <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        public ResignResult(Player player, GameState gameState) : base(player, gameState) { }
    }
}
