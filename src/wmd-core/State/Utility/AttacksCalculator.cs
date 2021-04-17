using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing attack-related calculations.
    /// </summary>
    public static class AttacksCalculator
    {
        static AttacksCalculator()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        /// <summary>
        /// Calculates the number of henchmen lost by the attacker.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The number of henchmen lost by the attacker.</returns>
        /// <remarks>This method assumes the attacker is the current player in the given <paramref name="gameState"/>.</remarks>
        public static int CalculateNumberOfHenchmenAttackerLost([DisallowNull] GameState gameState) =>
            (int)Math.Round(gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen * CalculatePercentageOfHenchmenAttackerLost());

        /// <summary>
        /// Calculates the number of henchmen lost by the defender.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The number of henchmen lost by the defender.</returns>
        public static int CalculateNumberOfHenchmenDefenderLost([DisallowNull] GameState gameState, AttackPlayerInput input) =>
            (int)Math.Round(gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen * CalculatePercentageOfHenchmenDefenderLost());

        private static double CalculatePercentageOfHenchmenAttackerLost() =>
            AttackConstants.BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenAttackerLost;

        private static double CalculatePercentageOfHenchmenDefenderLost() =>
            AttackConstants.BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenDefenderLost;
    }
}
