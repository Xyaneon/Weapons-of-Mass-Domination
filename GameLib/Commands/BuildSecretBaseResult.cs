using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player building their secret base.
    /// </summary>
    public record BuildSecretBaseResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildSecretBaseResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="buildPrice">The amount of money spent to build the new secret base.</param>
        public BuildSecretBaseResult(GameState updatedGameState, int playerIndex, decimal buildPrice) : base(updatedGameState, playerIndex)
        {
            BuildPrice = buildPrice;
        }

        /// <summary>
        /// Gets the amount of money spent to build the new secret base.
        /// </summary>
        public decimal BuildPrice { get; init; }
    }
}
