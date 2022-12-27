using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default;

internal sealed class HireHenchmenInputRetriever : ICommandInputRetriever
{
    private const int MaximumPositionsToOffer = 100;

    static HireHenchmenInputRetriever() => _random = new Random();

    public CommandInput? GetCommandInput(GameState gameState)
    {
        int openPositionsToOffer = CalculatePositionsToOffer(gameState);

        return openPositionsToOffer > 0 ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer } : null;
    }

    private static int CalculatePositionsToOffer(GameState gameState)
    {
        if (gameState.CurrentPlayer.State.ReputationPercentage <= 0 || gameState.CurrentPlayer.State.WorkforceState.DailyPayRate == 0m)
        {
            return 0;
        }
        int maximum = (int)Math.Floor(gameState.CurrentPlayer.State.Money / gameState.CurrentPlayer.State.WorkforceState.DailyPayRate);
        if (maximum < 1)
        {
            return 0;
        }

        return _random.Next(1, Math.Min(maximum, MaximumPositionsToOffer));
    }

    private static readonly Random _random;
}
