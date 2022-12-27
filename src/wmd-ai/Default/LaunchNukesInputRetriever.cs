using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default;

internal sealed class LaunchNukesInputRetriever : ICommandInputRetriever
{
    static LaunchNukesInputRetriever() => _random = new Random();

    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (!CanLaunchNukesThisTurn(gameState))
        {
            return null;
        }

        int targetPlayerIndex = ChooseTargetPlayerIndex(gameState);
        int nukesToLaunch = CalculateNumberOfNukesToLaunch(gameState);

        return nukesToLaunch > 0
            ? new LaunchNukesInput() { TargetPlayerIndex = targetPlayerIndex, NumberOfNukesLaunched = nukesToLaunch }
            : null;
    }

    private static int ChooseTargetPlayerIndex(GameState gameState) =>
        GameStateChecks.SelectRandomNonCurrentPlayerIndex(gameState);

    private static int CalculateNumberOfNukesToLaunch(GameState gameState) =>
        _random.Next(0, gameState.CurrentPlayer.State.Nukes);

    private static bool CanLaunchNukesThisTurn(GameState gameState) =>
        GameStateChecks.CurrentPlayerHasAnyNukes(gameState)
        && GameStateChecks.CurrentPlayerHasASecretBase(gameState);

    private static readonly Random _random;
}
