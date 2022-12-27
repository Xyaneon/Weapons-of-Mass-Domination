using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default;

internal sealed class SellLandInputRetriever : ICommandInputRetriever
{
    static SellLandInputRetriever() => _random = new Random();

    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (!CanSellLandThisTurn(gameState))
        {
            return null;
        }

        int areaToSell = CalculateAreaToSell(gameState);

        return areaToSell > 0 ? new SellLandInput() with { AreaToSell = areaToSell } : null;
    }

    private static bool CanSellLandThisTurn(GameState gameState) =>
        gameState.CurrentPlayer.State.Land > 0;

    private static int CalculateAreaToSell(GameState gameState)
    {
        int maximumAllowedSellingAmount = gameState.CurrentPlayer.State.Land;
        return _random.Next(0, maximumAllowedSellingAmount);
    }

    private static readonly Random _random;
}
