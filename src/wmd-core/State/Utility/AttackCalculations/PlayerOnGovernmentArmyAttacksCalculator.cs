using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility.AttackCalculations
{
    internal static class PlayerOnGovernmentArmyAttacksCalculator
    {
        static PlayerOnGovernmentArmyAttacksCalculator() => _random = new Random();

        private static readonly Random _random;

        public static PlayerOnGovernmentArmyAttackCalculationsResult CalculateChangesResultingFromAttack([DisallowNull] GameState gameState, AttackGovernmentArmyInput input)
        {
            long henchmenAttackerLost = CalculateHenchmenAttackerLost(gameState, input);
            long soldiersGovernmentArmyLost = CalculateSoldiersGovernmentArmyLost(gameState, input);
            int reputationChangeForAttacker = CalculateReputationChangeForAttacker(gameState, input);

            return new(henchmenAttackerLost, soldiersGovernmentArmyLost, reputationChangeForAttacker);
        }

        private static int CalculateReputationChangeForAttacker(GameState gameState, AttackGovernmentArmyInput input) =>
            ReputationCalculator.ClampReputationChangeAmount(5 + _random.Next(0, 2), gameState.CurrentPlayer.State.ReputationPercentage);

        private static long CalculateHenchmenAttackerLost(GameState gameState, AttackGovernmentArmyInput input) =>
            (long)Math.Ceiling((double)input.NumberOfAttackingHenchmen / 2);

        private static long CalculateSoldiersGovernmentArmyLost(GameState gameState, AttackGovernmentArmyInput input)
        {
            long potentialSoldiersLost = input.NumberOfAttackingHenchmen / 10;
            return Math.Min(potentialSoldiersLost, gameState.GovernmentState.NumberOfSoldiers);
        }
    }
}
