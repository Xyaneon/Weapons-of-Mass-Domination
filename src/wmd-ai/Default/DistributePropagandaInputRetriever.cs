using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class DistributePropagandaInputRetriever : ICommandInputRetriever
    {
        private const int StopReputationIncreasesThreshold = 90;

        static DistributePropagandaInputRetriever()
        {
            _random = new Random();
        }

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (GameStateChecks.CurrentPlayerHasNoMoney(gameState) || !WantToIncreaseReputation(gameState))
            {
                return null;
            }

            decimal amountToSpend = CalculateAmountToSpend(gameState.CurrentPlayer.State.Money);

            return amountToSpend > 0.0M ? new DistributePropagandaInput() with { MoneyToSpend = amountToSpend } : null;
        }

        private static decimal CalculateAmountToSpend(decimal maximum)
        {
            decimal halfOfMaximum = Math.Round(maximum / 2, 2);
            decimal baseAmount = halfOfMaximum;
            decimal additionalAmount = (decimal)Math.Round(_random.NextDouble() * (double)halfOfMaximum, 2);

            return baseAmount + additionalAmount;
        }

        private static bool WantToIncreaseReputation(GameState gameState) =>
            gameState.CurrentPlayer.State.ReputationPercentage < StopReputationIncreasesThreshold;

        private static readonly Random _random;
    }
}
