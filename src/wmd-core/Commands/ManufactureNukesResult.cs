using WMD.Game.State.Data;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player manufacturing nukes.
    /// </summary>
    public record ManufactureNukesResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManufactureNukesResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="nukesManufactured">The number of nukes manufactured.</param>
        public ManufactureNukesResult(GameState updatedGameState, int playerIndex, int nukesManufactured) : base(updatedGameState, playerIndex)
        {
            NukesManufactured = nukesManufactured;
        }

        /// <summary>
        /// Gets the number of nukes manufactured.
        /// </summary>
        public int NukesManufactured { get; init; }
    }
}
