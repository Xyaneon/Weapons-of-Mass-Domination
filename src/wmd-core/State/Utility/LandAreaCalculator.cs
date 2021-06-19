﻿using System;
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
        public static decimal CalculateTotalPurchasePrice([DisallowNull] GameState gameState, int areaToPurchase) =>
            gameState.UnclaimedLandPurchasePrice * areaToPurchase;

        /// <summary>
        /// Takes a potential change in land area owned by a player and returns an amount which would stay within the allowed bounds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player for which the change is taking place.</param>
        /// <param name="potentialAreaChange">The amount by which the player's owned land area could change if there was no limitation.</param>
        /// <returns>
        /// <paramref name="potentialAreaChange"/> if the resulting land area change for the player would result in a value between 0 and their
        /// remaining land area if the change is negative, or between <paramref name="potentialAreaChange"/> and the total remaining unclaimed
        /// land area if the change is positive; otherwise, 0.
        /// </returns>
        public static int ClampLandAreaChangeAmount([DisallowNull] GameState gameState, int playerIndex, int potentialAreaChange) => potentialAreaChange switch
        {
            _ when potentialAreaChange < 0 => Math.Max(potentialAreaChange, Math.Min(gameState.Players[playerIndex].State.Land, 0)),
            _ when potentialAreaChange > 0 => Math.Min(potentialAreaChange, gameState.Planet.UnclaimedLandArea),
            _ => 0,
        };
    }
}
