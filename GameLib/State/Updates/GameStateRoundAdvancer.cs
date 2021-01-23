using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.State.Updates
{
    internal static class GameStateRoundAdvancer
    {
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
                throw new ArgumentException("The provided round update result contained an invalid item.", nameof(roundUpdates), ex);
            }

            return updatedGameState;
        }

        private static GameState ApplyRoundUpdateItem(GameState gameState, RoundUpdateResultItem roundUpdate)
        {
            switch (roundUpdate)
            {
                case PlayerHenchmenPaid playerHenchmenPaid:
                    return GameStateUpdater.AdjustMoneyForPlayer(gameState, playerHenchmenPaid.PlayerIndex, -1 * playerHenchmenPaid.TotalPaidAmount);
                case PlayerHenchmenQuit playerHenchmenQuit:
                    return GameStateUpdater.AdjustHenchmenForPlayer(gameState, playerHenchmenQuit.PlayerIndex, -1 * playerHenchmenQuit.NumberOfHenchmenQuit);
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}.");
            }
        }

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState)
        {
            var henchmenPayments = Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenPaid(gameState, index));

            var henchmenQuittings = Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.Money <= 0 && gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenQuit(index, gameState.Players[index].State.WorkforceState.NumberOfHenchmen));

            var allUpdates = new List<RoundUpdateResultItem>()
                .Concat(henchmenPayments)
                .Concat(henchmenQuittings);

            return new RoundUpdateResult(gameState, gameState.CurrentRound, allUpdates);
        }
    }
}
