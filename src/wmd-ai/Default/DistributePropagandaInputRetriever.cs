using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class DistributePropagandaInputRetriever : ICommandInputRetriever
    {
        static DistributePropagandaInputRetriever()
        {
            _random = new Random();
        }

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (GameStateChecks.CurrentPlayerHasNoMoney(gameState))
            {
                return null;
            }

            decimal amountToSpend = CalculateAmountToSpend(gameState.CurrentPlayer.State.Money);

            return amountToSpend > 0.0M ? new DistributePropagandaInput() with { MoneyToSpend = amountToSpend } : null;
        }

        private static decimal CalculateAmountToSpend(decimal maximum) =>
            (decimal)Math.Round(_random.NextDouble() * (double)maximum, 2);

        private static readonly Random _random;
    }
}
