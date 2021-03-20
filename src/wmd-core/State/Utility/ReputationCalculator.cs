using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing propaganda- and reputation-related calculations.
    /// </summary>
    public static class ReputationCalculator
    {
        private const string ArgumentOutOfRangeException_playerIndex_outOfBounds = "The index of the player spending money on propaganda was not in bounds.";

        static ReputationCalculator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Calculates the amount of reputation a player gains by spending on propaganda.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player spending on propaganda.</param>
        /// <param name="moneySpent">The amount of money the player is spending on propaganda.</param>
        /// <returns>The amount of reputation the player will gain.</returns>
        public static int CalculateReputationGainedFromSpendingOnPropaganda([DisallowNull] GameState gameState, int playerIndex, decimal moneySpent)
        {
            if (!GameStateChecks.PlayerIndexIsInBounds(gameState, playerIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, ArgumentOutOfRangeException_playerIndex_outOfBounds);
            }

            int currentReputation = gameState.Players[playerIndex].State.ReputationPercentage;
            int maxReputationToBeGained = 100 - currentReputation;

            return Math.Min(20, maxReputationToBeGained);
        }

        private static Random _random;
    }
}
