using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player upgrading their secret base.
    /// </summary>
    public record UpgradeSecretBaseResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeSecretBaseResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="newLevel">The secret base's new level.</param>
        /// <param name="upgradePrice">The amount of money spent to perform the upgrade.</param>
        public UpgradeSecretBaseResult(GameState updatedGameState, int playerIndex, int newLevel, decimal upgradePrice) : base(updatedGameState, playerIndex)
        {
            NewLevel = newLevel;
            UpgradePrice = upgradePrice;
        }

        /// <summary>
        /// Gets the secret base's new level.
        /// </summary>
        public int NewLevel { get; init; }

        /// <summary>
        /// Gets the amount of money spent to perform the upgrade.
        /// </summary>
        public decimal UpgradePrice { get; init; }
    }
}
