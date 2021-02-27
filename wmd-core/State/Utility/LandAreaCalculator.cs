using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing land area-related calculations.
    /// </summary>
    public static class LandAreaCalculator
    {
        /// <summary>
        /// Calculates the maximum amount of land area the current player can purchase with their funds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The maximum amount of land area the current player can purchase with their funds, in square kilometers.</returns>
        public static int CalculateMaxPurchaseableLandAreaForCurrentPlayer([DisallowNull] GameState gameState)
        {
            decimal availableFunds = gameState.CurrentPlayer.State.Money;
            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            return (int)Math.Floor(availableFunds / pricePerSquareKilometer);
        }
    }
}
