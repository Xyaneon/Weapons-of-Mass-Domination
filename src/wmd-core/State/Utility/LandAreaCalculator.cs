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
        /// Calculates the maximum amount of land area the current player could purchase with their funds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The maximum amount of land area the current player could purchase with their funds, in square kilometers.</returns>
        /// <remarks>This method does not take into account the actual amount of remaining land area available for purchase.</remarks>
        public static int CalculateMaximumLandAreaCurrentPlayerCouldPurchase([DisallowNull] GameState gameState)
        {
            decimal availableFunds = gameState.CurrentPlayer.State.Money;
            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            return (int)Math.Floor(availableFunds / pricePerSquareKilometer);
        }


        /// <summary>
        /// Calculates the total purchase price for the given area of unclaimed land.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="areaToPurchase">The unclaimed land area to purchase, in square kilometers.</param>
        /// <returns>The total purchase price for the given area of unclaimed land.</returns>
        /// <remarks>This method does not take into account the actual amount of remaining land area available for purchase.</remarks>
        public static decimal CalculateTotalPurchasePrice([DisallowNull] GameState gameState, int areaToPurchase)
        {
            return gameState.UnclaimedLandPurchasePrice * areaToPurchase;
        }
    }
}
