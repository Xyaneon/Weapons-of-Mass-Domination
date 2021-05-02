using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing henchmen-related calculations.
    /// </summary>
    public static class HenchmenCalculator
    {
        private const string ArgumentOutOfRangeException_numberOfPositionsOffered_lessThanZero = "The number of positions offered cannot be less than zero.";

        static HenchmenCalculator()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        /// <summary>
        /// Calculates the number of henchmen the current player actually hires for the number of positions offered.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <param name="numberOfPositionsOffered">The number of positions offered.</param>
        /// <returns>The number of henchmen the current player actually hires for the number of positions offered.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfPositionsOffered"/> is less than zero.
        /// </exception>
        public static long CalculateNumberOfHenchmenHired([DisallowNull] GameState gameState, long numberOfPositionsOffered)
        {
            if (numberOfPositionsOffered < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfPositionsOffered), numberOfPositionsOffered, ArgumentOutOfRangeException_numberOfPositionsOffered_lessThanZero);
            }

            double reputationPercentage = gameState.CurrentPlayer.State.ReputationPercentage / 100.0;
            double percentageOfPositionsFilled = Math.Min(_random.NextDouble(), reputationPercentage);
            var numberOfPositionsWouldBeFilled = (long)Math.Floor(percentageOfPositionsFilled * numberOfPositionsOffered);

            return Math.Min(numberOfPositionsWouldBeFilled, gameState.Planet.NeutralPopulation);
        }
    }
}
