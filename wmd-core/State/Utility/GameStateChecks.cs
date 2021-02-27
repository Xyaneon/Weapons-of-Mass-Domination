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
        /// Determines whether the current player has completed their nukes research.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns><see langword="true"/> if the current player has completed their nukes research; otherwise, <see langword="false"/>.</returns>
        public static bool CurrentPlayerHasCompletedNukesResearch([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel;
        }
    }
}
