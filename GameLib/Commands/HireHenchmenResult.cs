using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player hiring henchmen.
    /// </summary>
    public record HireHenchmenResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HireHenchmenResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="henchmenHired">The number of henchmen successfully hired.</param>
        public HireHenchmenResult(GameState updatedGameState, int playerIndex, int henchmenHired) : base(updatedGameState, playerIndex)
        {
            HenchmenHired = henchmenHired;
        }

        /// <summary>
        /// Gets the number of henchmen successfully hired.
        /// </summary>
        public int HenchmenHired { get; init; }
    }
}
