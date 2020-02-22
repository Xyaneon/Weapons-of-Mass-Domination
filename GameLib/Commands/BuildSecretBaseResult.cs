using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player building their secret base.
    /// </summary>
    public class BuildSecretBaseResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildSecretBaseResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="buildPrice">The amount of money spent to build the new secret base.</param>
        public BuildSecretBaseResult(Player player, GameState gameState, decimal buildPrice) : base(player, gameState)
        {
            BuildPrice = buildPrice;
        }

        /// <summary>
        /// Gets the amount of money spent to build the new secret base.
        /// </summary>
        public decimal BuildPrice { get; }
    }
}
