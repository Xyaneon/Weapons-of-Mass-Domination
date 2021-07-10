using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal static class GameStateRoundAdvancer
    {
        private const string ArgumentException_RoundUpdateResultContainedInvalidItem = "The provided round update result contained an invalid item.";

        private static readonly IEnumerable<RoundUpdateResultOccurrencesCreator> _occurrencesCreators;

        static GameStateRoundAdvancer() => _occurrencesCreators = new List<RoundUpdateResultOccurrencesCreator>
        {
            new PlayerHenchmenPaidOccurrencesCreator(),
            new PlayerHenchmenQuitOccurrencesCreator(),
            new ReputationChangeOccurrencesCreator(),
            new GovernmentInterventionOccurrencesCreator(),
        };

        public static (GameState, RoundUpdateResult) AdvanceToNextRound(GameState gameState)
        {
            var result = CreateRoundUpdateResult(gameState);
            var updatedGameState = ApplyRoundUpdates(gameState, result);
            updatedGameState = updatedGameState with { CurrentRound = updatedGameState.CurrentRound + 1 };

            return (updatedGameState, result);
        }

        private static GameState ApplyGovernmentIntervention(GameState gameState, GovernmentIntervention intervention) => intervention switch
        {
            GovernmentAttacksPlayer occurrence => new GameStateUpdater(gameState)
                .AdjustStateAfterGovernmentAttackOnPlayer(occurrence)
                .AndReturnUpdatedGameState(),
            GovernmentDenouncesPlayer occurrence => new GameStateUpdater(gameState)
                .AdjustReputationForPlayer(occurrence.PlayerIndex, -1 * occurrence.ReputationDecrease)
                .AndReturnUpdatedGameState(),
            GovernmentTakesBackMoney occurrence => new GameStateUpdater(gameState)
                .AdjustMoneyForPlayer(occurrence.PlayerIndex, -1 * occurrence.AmountTaken)
                .AndReturnUpdatedGameState(),
            _ => throw new UnsupportedArgumentSubclassException(typeof(GovernmentIntervention), intervention.GetType()),
        };

        private static GameState ApplyRoundUpdates(GameState gameState, RoundUpdateResult roundUpdates)
        {
            var updatedGameState = gameState;

            try
            {
                foreach (var roundUpdate in roundUpdates.Items)
                {
                    updatedGameState = ApplyRoundUpdateItem(updatedGameState, roundUpdate);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ArgumentException_RoundUpdateResultContainedInvalidItem, nameof(roundUpdates), ex);
            }

            return updatedGameState;
        }

        private static GameState ApplyRoundUpdateItem(GameState gameState, RoundUpdateResultItem roundUpdate) => roundUpdate switch
        {
            PlayerHenchmenPaid playerHenchmenPaid => new GameStateUpdater(gameState)
                .AdjustMoneyForPlayer(playerHenchmenPaid.PlayerIndex, -1 * playerHenchmenPaid.TotalPaidAmount)
                .AndReturnUpdatedGameState(),
            PlayerHenchmenQuit playerHenchmenQuit => new GameStateUpdater(gameState)
                .ConvertPlayerHenchmenToNeutralPopulation(playerHenchmenQuit.PlayerIndex, playerHenchmenQuit.NumberOfHenchmenQuit)
                .AndReturnUpdatedGameState(),
            ReputationChange reputationChange => new GameStateUpdater(gameState)
                .AdjustReputationForPlayer(reputationChange.PlayerIndex, reputationChange.ReputationPercentageChanged)
                .AndReturnUpdatedGameState(),
            GovernmentIntervention governmentIntervention => ApplyGovernmentIntervention(gameState, governmentIntervention),
            _ => throw new UnsupportedArgumentSubclassException(typeof(RoundUpdateResultItem), roundUpdate.GetType()),
        };

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState) =>
            new(gameState, gameState.CurrentRound, CreateRoundUpdateResultItemsCollection(gameState));

        private static IEnumerable<RoundUpdateResultItem> CreateRoundUpdateResultItemsCollection(GameState gameState) =>
            _occurrencesCreators.SelectMany(occurrenceCreator => occurrenceCreator.CreateOccurrences(gameState));
    }
}
