using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default
{
    internal sealed class HireHenchmenInputRetriever : ICommandInputRetriever
    {
        private const int MaximumPositionsToOffer = 10;

        static HireHenchmenInputRetriever() => _random = new Random();

        public CommandInput? GetCommandInput(GameState gameState)
        {
            int openPositionsToOffer = CalculatePositionsToOffer();

            return openPositionsToOffer > 0 ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer } : null;
        }

        private static int CalculatePositionsToOffer()
        {
            return _random.Next(0, MaximumPositionsToOffer);
        }

        private static readonly Random _random;
    }
}
