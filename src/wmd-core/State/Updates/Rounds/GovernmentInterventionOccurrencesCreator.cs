using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class GovernmentInterventionOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        private const decimal BaseAmountOfMoneyTakenBack = 100;
        private const int BaseAmountOfReputationLost = 5;
        private const double BaseChanceOfGovernmentIntervention = 0.1;
        private const int MinimumNoticeableReputationPercentage = 10;

        static GovernmentInterventionOccurrencesCreator() => _random = new Random();

        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(playerIndex => gameState.Players[playerIndex].State.ReputationPercentage > MinimumNoticeableReputationPercentage)
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

            return interventions;
        }

        private static GovernmentDenouncesPlayer CreateDenouncesPlayerOccurrence(GameState gameState, int playerIndex) =>
            new(gameState, playerIndex, Math.Min(gameState.Players[playerIndex].State.ReputationPercentage, BaseAmountOfReputationLost));
        
        private static GovernmentTakesBackMoney CreateTakesBackMoneyOccurrence(GameState gameState, int playerIndex) =>
            new(gameState, playerIndex, Math.Min(gameState.Players[playerIndex].State.Money, BaseAmountOfMoneyTakenBack));

        private static bool GovernmentDecidesToTakeIntervention(GameState gameState, int playerIndex)
        {
            double additionalChanceOfIntervention = Math.Max(
                (gameState.Players[playerIndex].State.ReputationPercentage - MinimumNoticeableReputationPercentage) / 100.0,
                1 - BaseChanceOfGovernmentIntervention
            );

            return _random.NextDouble() < BaseChanceOfGovernmentIntervention + additionalChanceOfIntervention;
        }

        private static readonly Random _random;
    }
}
