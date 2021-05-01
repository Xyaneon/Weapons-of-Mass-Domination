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
    internal static class AttacksCalculator
    {
        static AttacksCalculator()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        public static AttackCalculationsResult CalculateChangesResultingFromAttack([DisallowNull] GameState gameState, AttackPlayerInput input)
        {
            int henchmenAttackerLost = CalculateNumberOfHenchmenAttackerLost(gameState);
            int henchmenDefenderLost = CalculateNumberOfHenchmenDefenderLost(gameState, input);
            int reputationChangeForAttacker = 0;
            int reputationChangeForDefender = 0;

            return new(henchmenAttackerLost, henchmenDefenderLost, reputationChangeForAttacker, reputationChangeForDefender);
        }

        private static int CalculateNumberOfHenchmenAttackerLost([DisallowNull] GameState gameState) =>
            (int)Math.Round(gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen * CalculatePercentageOfHenchmenAttackerLost());

        private static int CalculateNumberOfHenchmenDefenderLost([DisallowNull] GameState gameState, AttackPlayerInput input) =>
            (int)Math.Round(gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen * CalculatePercentageOfHenchmenDefenderLost());

        private static double CalculatePercentageOfHenchmenAttackerLost() =>
            AttackConstants.BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenAttackerLost;

        private static double CalculatePercentageOfHenchmenDefenderLost() =>
            AttackConstants.BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenDefenderLost;
    }
}
