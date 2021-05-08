using System;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class ChangeDailyWageInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState)
        {
            decimal maxAffordableDailyPayRate = CalculateMaxAffordableDailyPayRate(gameState);

            if (!GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState))
            {
                return gameState.CurrentPlayer.State.WorkforceState.DailyPayRate != HenchmenConstants.MinimumDailyWage
                    ? new ChangeDailyWageInput() with { NewDailyWage = HenchmenConstants.MinimumDailyWage }
                    : null;
            }
            
            if (CannotAffordCurrentPayRate(gameState))
            {
                return new ChangeDailyWageInput() with { NewDailyWage = maxAffordableDailyPayRate };
            }

            decimal newPayRate = FindHighestDailyWageAmongOpponents(gameState) + 0.01m;
            return newPayRate <= maxAffordableDailyPayRate && newPayRate != gameState.CurrentPlayer.State.WorkforceState.DailyPayRate
                ? new ChangeDailyWageInput() with { NewDailyWage = newPayRate }
                : null;
        }

        private static decimal CalculateMaxAffordableDailyPayRate(GameState gameState)
        {
            PlayerState currentPlayerState = gameState.CurrentPlayer.State;
            return Math.Max(Math.Round(currentPlayerState.Money / Math.Max(currentPlayerState.WorkforceState.NumberOfHenchmen, 1), 2, MidpointRounding.ToZero), 0.00m);
        }

        private static bool CannotAffordCurrentPayRate(GameState gameState)
        {
            PlayerState currentPlayerState = gameState.CurrentPlayer.State;
            return currentPlayerState.WorkforceState.TotalDailyPay > currentPlayerState.Money;
        }

        private static decimal FindHighestDailyWageAmongOpponents(GameState gameState) =>
            GameStateChecks.FindIndicesOfPlayersOtherThanCurrent(gameState)
                .Select(index => gameState.Players[index].State.WorkforceState.DailyPayRate)
                .Max();
    }
}
