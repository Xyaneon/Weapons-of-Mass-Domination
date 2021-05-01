﻿using System;
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
            int reputationChangeForAttacker = CalculateReputationChangeForAttacker(gameState, input, henchmenAttackerLost, henchmenDefenderLost);
            int reputationChangeForDefender = CalculateReputationChangeForDefender(gameState, input, henchmenAttackerLost, henchmenDefenderLost);

            return new(henchmenAttackerLost, henchmenDefenderLost, reputationChangeForAttacker, reputationChangeForDefender);
        }

        private static int CalculateNumberOfHenchmenAttackerLost(GameState gameState, AttackPlayerInput input) =>
            GetDefenderNumberOfHenchmen(gameState, input) == 0
                ? 0
                : (int)Math.Round(GetAttackerNumberOfHenchmen(gameState) * CalculatePercentageOfHenchmenAttackerLost());

        private static int CalculateNumberOfHenchmenDefenderLost(GameState gameState, AttackPlayerInput input) =>
            (int)Math.Round(GetDefenderNumberOfHenchmen(gameState, input) * CalculatePercentageOfHenchmenDefenderLost());

        private static double CalculatePercentageOfHenchmenAttackerLost() =>
            AttackConstants.BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenAttackerLost;

        private static double CalculatePercentageOfHenchmenDefenderLost() =>
            AttackConstants.BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * AttackConstants.MaxAdditionalPercentageOfHenchmenDefenderLost;

        private static int CalculateReputationChangeForAttacker(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost) =>
            ClampReputationChangeAmount(CalculatePotentialReputationChangeForAttacker(gameState, input, henchmenAttackerLost, henchmenDefenderLost), GetAttackerReputation(gameState));

        private static int CalculateReputationChangeForDefender(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost) =>
            ClampReputationChangeAmount(CalculatePotentialReputationChangeForDefender(gameState, input, henchmenAttackerLost, henchmenDefenderLost), GetDefenderReputation(gameState, input));

        private static int CalculatePotentialReputationChangeForAttacker(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost)
        {
            int defenderNumberOfHenchmenBeforeAttack = GetDefenderNumberOfHenchmen(gameState, input);

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
            int defenderNumberOfHenchmenBeforeAttack = GetDefenderNumberOfHenchmen(gameState, input);

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

        private static int ClampReputationChangeAmount(int potentialChangeAmount, int currentReputation) => potentialChangeAmount switch
        {
            _ when potentialChangeAmount < 0 => Math.Max(potentialChangeAmount, ReputationConstants.MinReputationPercentage - currentReputation),
            _ when potentialChangeAmount > 0 => Math.Min(potentialChangeAmount, ReputationConstants.MaxReputationPercentage - currentReputation),
            _ => 0,
        };

        private static int GetAttackerNumberOfHenchmen(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen;

        private static int GetDefenderNumberOfHenchmen(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen;

        private static int GetAttackerReputation(GameState gameState) => gameState.CurrentPlayer.State.ReputationPercentage;

        private static int GetDefenderReputation(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.ReputationPercentage;
    }
}
