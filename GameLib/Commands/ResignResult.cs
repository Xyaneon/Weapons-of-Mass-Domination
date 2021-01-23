using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of the <see cref="ResignAction"/>.
    /// </summary>
    public record ResignResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResignResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the current <see cref="Player"/> whose action this is the result of.</param>
        public ResignResult(GameState updatedGameState, int playerIndex) : base(updatedGameState, playerIndex) { }
    }
}
