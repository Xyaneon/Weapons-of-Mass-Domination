using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class ManufactureNukesInputRetriever : ICommandInputRetriever
    {
        static ManufactureNukesInputRetriever()
        {
            _random = new Random();
        }

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (!GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
            {
                return null;
            }

            int nukesToManufacture = CalculateNumberOfNukesToManufacture(gameState);

            if (nukesToManufacture <= 0)
            {
                return null;
            }

            return new ManufactureNukesInput() with { NumberOfNukesToManufacture = nukesToManufacture };
        }

        private static int CalculateNumberOfNukesToManufacture(GameState gameState)
        {
            int maximumAllowedNukeQuantity = NukesCalculator.CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture(gameState);
            return _random.Next(0, maximumAllowedNukeQuantity);
        }

        private static readonly Random _random;
    }
}
