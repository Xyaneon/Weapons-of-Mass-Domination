using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides utlity methods for checking the game state.
    /// </summary>
    public static class GameStateChecks
    {
        /// <summary>
        /// Determines whether the current player could purchase any unclaimed land with their current funds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player could purchase any unclaimed land with their current funds; otherwise, <see langword="false"/>.</returns>
        /// <remarks>This method does not take into account the actual amount of remaining land area available for purchase.</remarks>
        public static bool CurrentPlayerCouldPurchaseLand([DisallowNull] GameState gameState)
        {
            return LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState) > 0;
        }

        /// <summary>
        /// Determines whether the current player has a secret base.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has a secret base; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasASecretBase([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.SecretBase != null;
        }

        /// <summary>
        /// Determines whether the current player has any henchmen.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has any henchmen; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasAnyHenchmen([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen > 0;
        }

        /// <summary>
        /// Determines whether the current player has any nukes in their inventory.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has any nukes in their inventory; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasAnyNukes([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.Nukes > 0;
        }

        /// <summary>
        /// Determines whether the current player has completed their nukes research.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has completed their nukes research; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasCompletedNukesResearch([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel;
        }

        /// <summary>
        /// Determines whether the current player has no money.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has no money; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasNoMoney([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.Money <= 0;
        }

        /// <summary>
        /// Determines whether the current player is attacking themselves.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="targetPlayerIndex">The index of the player being attacked.</param>
        /// <returns><see langword="true"/> if the current player is attacking themselves; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerIsAttackingThemselves([DisallowNull] GameState gameState, int targetPlayerIndex)
        {
            return gameState.CurrentPlayerIndex == targetPlayerIndex;
        }

        /// <summary>
        /// Determines whether the provided <paramref name="playerIndex"/> is in bounds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="targetPlayerIndex">The index of the player.</param>
        /// <returns><see langword="true"/> if the player index is in bounds; otherwise, <see langword="false"/>.</returns>
        public static bool PlayerIndexIsInBounds([DisallowNull] GameState gameState, int playerIndex)
        {
            return playerIndex >= 0 && playerIndex < gameState.Players.Count;
        }
    }
}
