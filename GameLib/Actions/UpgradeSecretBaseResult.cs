namespace WMD.Game.Actions
{
    /// <summary>
    /// Represents the result of a player upgrading their secret base.
    /// </summary>
    public class UpgradeSecretBaseResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeSecretBaseResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="newLevel">The secret base's new level.</param>
        /// <param name="upgradePrice">The amount of money spent to perform the upgrade.</param>
        public UpgradeSecretBaseResult(Player player, GameState gameState, int newLevel, decimal upgradePrice) : base(player, gameState)
        {
            NewLevel = newLevel;
            UpgradePrice = upgradePrice;
        }

        /// <summary>
        /// Gets the secret base's new level.
        /// </summary>
        public int NewLevel { get; }

        /// <summary>
        /// Gets the amount of money spent to perform the upgrade.
        /// </summary>
        public decimal UpgradePrice { get; }
    }
}
