using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    internal static class AttacksCalculator
    {
        static AttacksCalculator()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        public static AttackCalculationsResult CalculateChangesResultingFromAttack([DisallowNull] GameState gameState, AttackPlayerInput input)
        {
            int henchmenAttackerLost = CalculateNumberOfHenchmenAttackerLost(gameState, input);
            int henchmenDefenderLost = CalculateNumberOfHenchmenDefenderLost(gameState, input);
            int reputationChangeForAttacker = 0;
            int reputationChangeForDefender = 0;

            return new(henchmenAttackerLost, henchmenDefenderLost, reputationChangeForAttacker, reputationChangeForDefender);
        }

        private static int CalculateNumberOfHenchmenAttackerLost([DisallowNull] GameState gameState, AttackPlayerInput input) =>
            GetDefenderNumberOfHenchmen(gameState, input) == 0
                ? 0
                : (int)Math.Round(GetAttackerNumberOfHenchmen(gameState) * CalculatePercentageOfHenchmenAttackerLost());

        private static int CalculateNumberOfHenchmenDefenderLost([DisallowNull] GameState gameState, AttackPlayerInput input) =>
            (int)Math.Round(GetDefenderNumberOfHenchmen(gameState, input) * CalculatePercentageOfHenchmenDefenderLost());

        private static double CalculatePercentageOfHenchmenAttackerLost() =>
            AttackConstants.BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenAttackerLost;

        private static double CalculatePercentageOfHenchmenDefenderLost() =>
            AttackConstants.BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenDefenderLost;

        private static int GetAttackerNumberOfHenchmen(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen;
        private static int GetDefenderNumberOfHenchmen(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen;
    }
}
