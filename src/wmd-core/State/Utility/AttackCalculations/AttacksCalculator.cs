using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility.AttackCalculations
{
    internal static class AttacksCalculator
    {
        static AttacksCalculator() => _random = new Random();

        private static readonly Random _random;

        public static AttackCalculationsResult CalculateChangesResultingFromAttack([DisallowNull] GameState gameState, AttackPlayerInput input)
        {
            var percentageOfHenchmenDefenderLost = CalculatePercentageOfHenchmenDefenderLost();
            var henchmenAttackerLost = CalculateNumberOfHenchmenAttackerLost(gameState, input);
            var henchmenDefenderLost = CalculateNumberOfHenchmenDefenderLost(gameState, input, percentageOfHenchmenDefenderLost);
            var reputationChangeForAttacker = CalculateReputationChangeForAttacker(gameState, input, henchmenAttackerLost, henchmenDefenderLost);
            var reputationChangeForDefender = CalculateReputationChangeForDefender(gameState, input, henchmenAttackerLost, henchmenDefenderLost);
            var landAreaChangeForDefender = CalculateLandLostForDefender(gameState, input, percentageOfHenchmenDefenderLost);

            return new(henchmenAttackerLost, henchmenDefenderLost, reputationChangeForAttacker, reputationChangeForDefender, landAreaChangeForDefender);
        }

        private static int CalculateLandLostForDefender(GameState gameState, AttackPlayerInput input, double percentageOfHenchmenDefenderLost) =>
            LandAreaCalculator.ClampLandAreaChangeAmount(gameState, input.TargetPlayerIndex, CalculatePotentialLandLostForDefender(gameState, input, percentageOfHenchmenDefenderLost));

        private static int CalculateNumberOfHenchmenAttackerLost(GameState gameState, AttackPlayerInput input) =>
            GetDefenderNumberOfHenchmen(gameState, input) != 0
                ? (int)Math.Round(input.NumberOfAttackingHenchmen * CalculatePercentageOfHenchmenAttackerLost())
                : 0;

        private static int CalculateNumberOfHenchmenDefenderLost(GameState gameState, AttackPlayerInput input, double percentageOfHenchmenDefenderLost) =>
            (int)Math.Round(GetDefenderNumberOfHenchmen(gameState, input) * percentageOfHenchmenDefenderLost);

        private static double CalculatePercentageOfHenchmenAttackerLost() =>
            AttackConstants.BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenAttackerLost;

        private static double CalculatePercentageOfHenchmenDefenderLost() =>
            AttackConstants.BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenDefenderLost;

        private static int CalculateReputationChangeForAttacker(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost) =>
            ReputationCalculator.ClampReputationChangeAmount(CalculatePotentialReputationChangeForAttacker(gameState, input, henchmenAttackerLost, henchmenDefenderLost), GetAttackerReputation(gameState));

        private static int CalculateReputationChangeForDefender(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost) =>
            ReputationCalculator.ClampReputationChangeAmount(CalculatePotentialReputationChangeForDefender(gameState, input, henchmenAttackerLost, henchmenDefenderLost), GetDefenderReputation(gameState, input));

        private static int CalculatePotentialLandLostForDefender(GameState gameState, AttackPlayerInput input, double percentageOfHenchmenDefenderLost) =>
            -1 * (int)Math.Floor(gameState.Players[input.TargetPlayerIndex].State.Land * percentageOfHenchmenDefenderLost);

        private static int CalculatePotentialReputationChangeForAttacker(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost)
        {
            var defenderNumberOfHenchmenBeforeAttack = GetDefenderNumberOfHenchmen(gameState, input);

            if (defenderNumberOfHenchmenBeforeAttack <= 0)
            {
                // Defender had no henchmen to lose.
                return AttackConstants.BaseReputationChangeAmountForAttacker;
            }
            else if (henchmenDefenderLost == 0)
            {
                // All defending henchmen survived.
                return -1 * AttackConstants.BaseReputationChangeAmountForAttacker;
            }
            else if (henchmenDefenderLost >= defenderNumberOfHenchmenBeforeAttack)
            {
                // All defending henchmen were lost.
                return AttackConstants.ReputationChangeFactorForDefeatedDefender * AttackConstants.BaseReputationChangeAmountForAttacker;
            }
            else
            {
                // Some defending henchmen were lost, but not all.
                return AttackConstants.BaseReputationChangeAmountForAttacker;
            }
        }

        private static int CalculatePotentialReputationChangeForDefender(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost)
        {
            var defenderNumberOfHenchmenBeforeAttack = GetDefenderNumberOfHenchmen(gameState, input);

            if (defenderNumberOfHenchmenBeforeAttack <= 0)
            {
                // Defender had no henchmen to lose.
                return -1 * AttackConstants.BaseReputationChangeAmountForDefender;
            }
            else if (henchmenDefenderLost == 0)
            {
                // All defending henchmen survived.
                return AttackConstants.BaseReputationChangeAmountForDefender;
            }
            else if (henchmenDefenderLost >= defenderNumberOfHenchmenBeforeAttack)
            {
                // All defending henchmen were lost.
                return -1 * AttackConstants.ReputationChangeFactorForDefeatedDefender * AttackConstants.BaseReputationChangeAmountForDefender;
            }
            else
            {
                // Some defending henchmen were lost, but not all.
                return -1 * AttackConstants.BaseReputationChangeAmountForDefender;
            }
        }

        private static long GetDefenderNumberOfHenchmen(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen;

        private static int GetAttackerReputation(GameState gameState) => gameState.CurrentPlayer.State.ReputationPercentage;

        private static int GetDefenderReputation(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.ReputationPercentage;
    }
}
