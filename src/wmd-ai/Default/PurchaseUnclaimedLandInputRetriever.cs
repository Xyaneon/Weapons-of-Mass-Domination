using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class PurchaseUnclaimedLandInputRetriever : ICommandInputRetriever
    {
        static PurchaseUnclaimedLandInputRetriever()
        {
            _random = new Random();
        }

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (!CanPurchaseLandThisTurn(gameState))
            {
                return null;
            }

            int areaToPurchase = CalculateAreaToPurchase(gameState);

            return areaToPurchase >= 1 ? new PurchaseUnclaimedLandInput() with { AreaToPurchase = areaToPurchase } : null;
        }

        private static int CalculateAreaToPurchase(GameState gameState)
        {
            int maxPurchaseableArea = LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState);
            return _random.Next(0, maxPurchaseableArea);
        }

        private static bool CanPurchaseLandThisTurn(GameState gameState) =>
            gameState.Planet.UnclaimedLandArea >= 1 && GameStateChecks.CurrentPlayerCouldPurchaseLand(gameState);

        private static readonly Random _random;
    }
}
