using System;
using System.Collections.Generic;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal static class GameStateRoundAdvancer
    {
        private const string ArgumentException_RoundUpdateResultContainedInvalidItem = "The provided round update result contained an invalid item.";

        private static readonly IEnumerable<RoundUpdateResultOccurrencesCreator> _occurrencesCreators;

        static GameStateRoundAdvancer()
        {
            _occurrencesCreators = new List<RoundUpdateResultOccurrencesCreator>
            {
                new PlayerHenchmenPaidOccurrencesCreator(),
                new PlayerHenchmenQuitOccurrencesCreator(),
                new ReputationChangeOccurrencesCreator(),
                new GovernmentInterventionOccurrencesCreator(),
            };
        }

        public static (GameState, RoundUpdateResult) AdvanceToNextRound(GameState gameState)
        {
            var result = CreateRoundUpdateResult(gameState);
            var updatedGameState = ApplyRoundUpdates(gameState, result);
            updatedGameState = updatedGameState with { CurrentRound = updatedGameState.CurrentRound + 1 };

            return (updatedGameState, result);
        }

        private static GameState ApplyGovernmentIntervention(GameState gameState, GovernmentIntervention intervention) => intervention switch
        {
            GovernmentTakesBackMoney occurrence => GameStateUpdater.AdjustMoneyForPlayer(gameState, occurrence.PlayerIndex, -1 * occurrence.AmountTaken),
            GovernmentDenouncesPlayer occurrence => GameStateUpdater.AdjustReputationForPlayer(gameState, occurrence.PlayerIndex, -1 * occurrence.ReputationDecrease),
            _ => throw new ArgumentException($"Unrecognized {typeof(GovernmentIntervention).Name} subclass: {intervention.GetType().Name}."),
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
            PlayerHenchmenPaid playerHenchmenPaid => GameStateUpdater.AdjustMoneyForPlayer(gameState, playerHenchmenPaid.PlayerIndex, -1 * playerHenchmenPaid.TotalPaidAmount),
            PlayerHenchmenQuit playerHenchmenQuit => GameStateUpdater.AdjustHenchmenForPlayer(gameState, playerHenchmenQuit.PlayerIndex, -1 * playerHenchmenQuit.NumberOfHenchmenQuit),
            ReputationChange reputationChange => GameStateUpdater.AdjustReputationForPlayer(gameState, reputationChange.PlayerIndex, reputationChange.ReputationPercentageChanged),
            GovernmentIntervention governmentIntervention => ApplyGovernmentIntervention(gameState, governmentIntervention),
            _ => throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}."),
        };

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState) =>
            new(gameState, gameState.CurrentRound, CreateRoundUpdateResultItemsCollection(gameState));

        private static IEnumerable<RoundUpdateResultItem> CreateRoundUpdateResultItemsCollection(GameState gameState)
        {
            foreach (var occurrencesCreator in _occurrencesCreators)
            {
                foreach (var item in occurrencesCreator.CreateOccurrences(gameState))
                {
                    yield return item;
                }
            }
        }
    }
}
