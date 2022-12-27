using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing nukes-related calculations.
    /// </summary>
    public static class NukesCalculator
    {
        private const string ArgumentOutOfRangeException_numberOfLaunchedNukes_lessThanZero = "The number of launched nukes cannot be less than zero.";
        private const string ArgumentOutOfRangeException_numberOfSuccessfulHits_lessThanZero = "The number of successful nuke hits cannot be less than zero.";
        private const string ArgumentOutOfRangeException_playerIndex_outOfBounds = "The index of the player losing henchmen to nukes was not in bounds.";

        static NukesCalculator()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        /// <summary>
        /// Calculates the maximum amount of nukes the current player could manufacture with their funds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The maximum amount of nukes the current player could manufacture with their funds.</returns>
        /// <remarks>This method does not take the current status of nuke manufacturing prerequisites for the current player into account; it assumes they are already met.</remarks>
        public static int CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture([DisallowNull] GameState gameState)
        {
            try
            {
                return (int)Math.Floor(gameState.CurrentPlayer.State.Money / NukeConstants.ManufacturingPrice);
            }
            catch (System.OverflowException)
            {
                return Int32.MaxValue;
            }
            
        }

        /// <summary>
        /// Calculates the number of henchmen a given player loses in a nuclear attack.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player being attacked.</param>
        /// <param name="numberOfSuccessfulHits">The number of successful nuke hits made on the player.</param>
        /// <returns>The total number of henchmen lost by the player in the attack.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="playerIndex"/> is out of bounds.
        /// -or-
        /// <paramref name="numberOfSuccessfulHits"/> is less than zero.
        /// </exception>
        public static long CalculateNumberOfHenchmenLostToNukes([DisallowNull] GameState gameState, int playerIndex, int numberOfSuccessfulHits)
        {
            if (!GameStateChecks.PlayerIndexIsInBounds(gameState, playerIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, ArgumentOutOfRangeException_playerIndex_outOfBounds);
            }

            if (numberOfSuccessfulHits < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfSuccessfulHits), numberOfSuccessfulHits, ArgumentOutOfRangeException_numberOfSuccessfulHits_lessThanZero);
            }

            if (numberOfSuccessfulHits == 0)
            {
                return 0;
            }

            long initialHenchmenCount = gameState.Players[playerIndex].State.WorkforceState.NumberOfHenchmen;
            long newHenchmenCount = initialHenchmenCount;
            for (int i = 0; i < numberOfSuccessfulHits; i++)
            {
                newHenchmenCount = (long)Math.Floor((double)newHenchmenCount / 2);
            }

            return initialHenchmenCount - newHenchmenCount;
        }

        /// <summary>
        /// Calculates the number of launched nukes which successfully hit the target.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="numberOfLaunchedNukes">The number of launched nukes.</param>
        /// <returns>The number of launched nukes which successfully hit the target. This will be a value between 0 and <paramref name="numberOfLaunchedNukes"/>, inclusive.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="numberOfLaunchedNukes"/> is less than zero.</exception>
        public static int CalculateNumberOfSuccessfulNukeHits([DisallowNull] GameState gameState, int numberOfLaunchedNukes)
        {
            if (numberOfLaunchedNukes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfLaunchedNukes), numberOfLaunchedNukes, ArgumentOutOfRangeException_numberOfLaunchedNukes_lessThanZero);
            }

            if (numberOfLaunchedNukes == 0)
            {
                return 0;
            }

            int numberOfSuccessfulHits = 0;

            for (int i = 0; i < numberOfLaunchedNukes; i++)
            {
                if (_random.NextDouble() > NukeConstants.BaseDudProbability)
                {
                    numberOfSuccessfulHits++;
                }
            }

            return numberOfSuccessfulHits;
        }

        /// <summary>
        /// Calculates the amount by which the reputation changes for the player who launched nukes.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="attackingPlayerIndex">The index of the player who launched nukes.</param>
        /// <param name="numberOfSuccessfulHits">The number of launched nukes which successfully hit the target.</param>
        /// <returns>The amount by which the reputation changes for the player who launched nukes.</returns>
        public static int CalculateReputationChangeAmount(GameState gameState, int attackingPlayerIndex, int numberOfSuccessfulHits)
        {
            int currentReputation = gameState.Players[attackingPlayerIndex].State.ReputationPercentage;
            int potentialChangeAmount = numberOfSuccessfulHits > 0
                ? NukeConstants.ReputationChangeAmountForSuccessfulNukeHits
                : NukeConstants.ReputationChangeAmountForNoSuccessfulNukeHits;
            return ReputationCalculator.ClampReputationChangeAmount(potentialChangeAmount, currentReputation);
        }

        /// <summary>
        /// Calculates the total manufacturing price for the given quantity of nukes the current player will manufacture.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The total manufacturing price for the given quantity of nukes the current player will manufacture.</returns>
        /// <remarks>This method does not take the current status of nuke manufacturing prerequisites for the current player into account; it assumes they are already met.</remarks>
        public static decimal CalculateTotalManufacturingPrice([DisallowNull] GameState gameState, int quantity) =>
            NukeConstants.ManufacturingPrice * quantity;
    }
}
