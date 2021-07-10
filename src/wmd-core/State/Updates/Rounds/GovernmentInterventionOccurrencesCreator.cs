using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Utility.AttackCalculations;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class GovernmentInterventionOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        static GovernmentInterventionOccurrencesCreator() => _random = new Random();

        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(playerIndex => gameState.Players[playerIndex].State.ReputationPercentage > GovernmentConstants.MinimumNoticeableReputationPercentage)
                .SelectMany(playerIndex => CreateOccurrencesForPlayer(gameState, playerIndex));

        private static IEnumerable<RoundUpdateResultItem> CreateOccurrencesForPlayer(GameState gameState, int playerIndex)
        {
            // At most one occurrence of each intervention type can be created for a player.

            var interventions = new Queue<GovernmentIntervention>();

            if (GovernmentDecidesToTakeIntervention(gameState, playerIndex) && gameState.Players[playerIndex].State.Money > 0)
            {
                interventions.Enqueue(CreateTakesBackMoneyOccurrence(gameState, playerIndex));
            }

            if (GovernmentDecidesToTakeIntervention(gameState, playerIndex) && gameState.Players[playerIndex].State.ReputationPercentage > 0)
            {
                interventions.Enqueue(CreateDenouncesPlayerOccurrence(gameState, playerIndex));
            }

            if (GovernmentDecidesToTakeIntervention(gameState, playerIndex) && gameState.Players[playerIndex].State.WorkforceState.NumberOfHenchmen > 0)
            {
                interventions.Enqueue(CreateGovernmentAttacksPlayerOccurrence(gameState, playerIndex));
            }

            return interventions;
        }

        private static GovernmentAttacksPlayer CreateGovernmentAttacksPlayerOccurrence(GameState gameState, int playerIndex) =>
            new(gameState, playerIndex, CalculateCombatantsInGovernmentAttack(gameState, playerIndex));

        private static AttackCombatantsChanges CalculateCombatantsInGovernmentAttack(GameState gameState, int playerIndex)
        {
            long governmentSoldiersSent = Min(gameState.GovernmentState.NumberOfSoldiers, 100, gameState.Players[playerIndex].State.WorkforceState.NumberOfHenchmen);
            long defendingPlayerHenchmen = gameState.Players[playerIndex].State.WorkforceState.NumberOfHenchmen;
            long governmentSoldiersLost = Math.Min(governmentSoldiersSent, defendingPlayerHenchmen);
            long defendingPlayerHenchmenLost = Math.Min(defendingPlayerHenchmen, governmentSoldiersSent / 5);

            return new AttackCombatantsChanges(governmentSoldiersSent, defendingPlayerHenchmen, governmentSoldiersLost, defendingPlayerHenchmenLost);
        }

        private static GovernmentDenouncesPlayer CreateDenouncesPlayerOccurrence(GameState gameState, int playerIndex) =>
            new(gameState, playerIndex, Math.Min(gameState.Players[playerIndex].State.ReputationPercentage, GovernmentConstants.BaseAmountOfReputationLost));

        private static GovernmentTakesBackMoney CreateTakesBackMoneyOccurrence(GameState gameState, int playerIndex) =>
            new(gameState, playerIndex, Math.Min(gameState.Players[playerIndex].State.Money, GovernmentConstants.BaseAmountOfMoneyTakenBack));

        private static bool GovernmentDecidesToTakeIntervention(GameState gameState, int playerIndex)
        {
            int reputationPercentage = gameState.Players[playerIndex].State.ReputationPercentage;

            if (reputationPercentage < GovernmentConstants.MinimumNoticeableReputationPercentage)
            {
                return false;
            }

            double additionalChanceOfIntervention = Math.Min(
                (reputationPercentage - GovernmentConstants.MinimumNoticeableReputationPercentage) / 100.0,
                1 - GovernmentConstants.BaseChanceOfGovernmentIntervention
            );

            return _random.NextDouble() < GovernmentConstants.BaseChanceOfGovernmentIntervention + additionalChanceOfIntervention;
        }

        private static long Min(params long[] values) => values.Min();

        private static readonly Random _random;
    }
}
