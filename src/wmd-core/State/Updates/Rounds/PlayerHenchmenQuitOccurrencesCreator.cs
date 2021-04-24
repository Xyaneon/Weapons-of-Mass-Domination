using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class PlayerHenchmenQuitOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(index => PlayerCannotPayTheirHenchmen(gameState.Players[index].State) || PlayerHenchmenAreUnderpaid(gameState.Players[index].State))
                .Select(index => CreatePlayerHenchmenQuitOccurrence(gameState, index));

        private static PlayerHenchmenQuit CreatePlayerHenchmenQuitOccurrence(GameState gameState, int index)
        {
            PlayerState playerState = gameState.Players[index].State;

            if (PlayerCannotPayTheirHenchmen(playerState))
            {
                return new(index, playerState.WorkforceState.NumberOfHenchmen);
            }

            if (PlayerHenchmenAreUnderpaid(playerState))
            {
                int henchmenQuit = CalculateHenchmenQuitDueToUnderpay(playerState.WorkforceState);
                return new(index, henchmenQuit);
            }

            throw new InvalidOperationException($"Conditions not met for player's henchmen to quit (index={index}).");
        }

        private static bool PlayerCannotPayTheirHenchmen(PlayerState playerState) =>
            playerState.Money <= 0 && playerState.WorkforceState.NumberOfHenchmen > 0;

        private static bool PlayerHenchmenAreUnderpaid(PlayerState playerState) =>
            playerState.WorkforceState.DailyPayRate < HenchmenConstants.MinimumDailyWage && playerState.WorkforceState.NumberOfHenchmen > 0;

        private static int CalculateHenchmenQuitDueToUnderpay(WorkforceState workforceState)
        {
            double underpayPercentage = 1 - (double)workforceState.DailyPayRate / (double)HenchmenConstants.MinimumDailyWage;
            return (int)Math.Ceiling(workforceState.NumberOfHenchmen * underpayPercentage);
        }
    }
}
