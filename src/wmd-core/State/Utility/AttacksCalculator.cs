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
            (GetDefenderNumberOfHenchmen(gameState, input) > 0 && henchmenDefenderLost == 0)
                ? Math.Max(-1 * AttackConstants.BaseReputationChangeAmountForAttacker, ReputationConstants.MinReputationPercentage - GetAttackerReputation(gameState))
                : Math.Min(AttackConstants.BaseReputationChangeAmountForAttacker, ReputationConstants.MaxReputationPercentage - GetAttackerReputation(gameState));

        private static int CalculateReputationChangeForDefender(GameState gameState, AttackPlayerInput input, int henchmenAttackerLost, int henchmenDefenderLost) =>
            (GetDefenderNumberOfHenchmen(gameState, input) > 0 && henchmenDefenderLost == 0)
                ? Math.Min(AttackConstants.BaseReputationChangeAmountForDefender, ReputationConstants.MaxReputationPercentage - GetDefenderReputation(gameState, input))
                : Math.Max(-1 * AttackConstants.BaseReputationChangeAmountForDefender, ReputationConstants.MinReputationPercentage - GetDefenderReputation(gameState, input));

        private static int GetAttackerNumberOfHenchmen(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen;

        private static int GetDefenderNumberOfHenchmen(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen;

        private static int GetAttackerReputation(GameState gameState) => gameState.CurrentPlayer.State.ReputationPercentage;

        private static int GetDefenderReputation(GameState gameState, AttackPlayerInput input) => gameState.Players[input.TargetPlayerIndex].State.ReputationPercentage;
    }
}
