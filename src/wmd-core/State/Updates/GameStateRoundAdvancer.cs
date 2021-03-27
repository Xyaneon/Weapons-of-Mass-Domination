using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.State.Updates
{
    internal static class GameStateRoundAdvancer
    {
        private const string ArgumentException_RoundUpdateResultContainedInvalidItem = "The provided round update result contained an invalid item.";

        public static (GameState, RoundUpdateResult) AdvanceToNextRound(GameState gameState)
        {
            var result = CreateRoundUpdateResult(gameState);
            var updatedGameState = ApplyRoundUpdates(gameState, result);
            updatedGameState = updatedGameState with { CurrentRound = updatedGameState.CurrentRound + 1 };

            return (updatedGameState, result);
        }

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

        private static GameState ApplyRoundUpdateItem(GameState gameState, RoundUpdateResultItem roundUpdate)
        {
            return roundUpdate switch
            {
                PlayerHenchmenPaid playerHenchmenPaid => GameStateUpdater.AdjustMoneyForPlayer(gameState, playerHenchmenPaid.PlayerIndex, -1 * playerHenchmenPaid.TotalPaidAmount),
                PlayerHenchmenQuit playerHenchmenQuit => GameStateUpdater.AdjustHenchmenForPlayer(gameState, playerHenchmenQuit.PlayerIndex, -1 * playerHenchmenQuit.NumberOfHenchmenQuit),
                _ => throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}."),
            };
        }

        private static IEnumerable<PlayerHenchmenPaid> CreateHenchmenPaidOccurrences(GameState gameState) =>
            Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenPaid(gameState, index));

        private static IEnumerable<PlayerHenchmenQuit> CreateHenchmenQuitOccurrences(GameState gameState) =>
            Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.Money <= 0 && gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenQuit(index, gameState.Players[index].State.WorkforceState.NumberOfHenchmen));

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState)
        {
            var allUpdates = new List<RoundUpdateResultItem>()
                .Concat(CreateHenchmenPaidOccurrences(gameState))
                .Concat(CreateHenchmenQuitOccurrences(gameState));

            return new RoundUpdateResult(gameState, gameState.CurrentRound, allUpdates);
        }
    }
}
