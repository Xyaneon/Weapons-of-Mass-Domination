using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands;

class LaunchNukesInputRetriever : ICommandInputRetriever
{
    private const string NukesToLaunchPrompt = "Please enter how many nukes you would like to launch";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (!GameStateChecks.CurrentPlayerHasAnyNukes(gameState))
        {
            PrintingUtility.PrintHasNoNukesToLaunch();
            return null;
        }

        if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
        {
            PrintingUtility.PrintHasNoSecretBaseToLaunchNukesFrom();
            return null;
        }

        var targetPlayerIndex = UserInput.GetAttackTargetPlayerIndex(gameState);

        var allowedAmounts = new IntRange(0, gameState.CurrentPlayer.State.Nukes);

        var prompt = $"{NukesToLaunchPrompt} ({allowedAmounts.Minimum} to {allowedAmounts.Maximum})";
        var nukesToLaunch = UserInput.GetInteger(prompt, allowedAmounts);

        if (nukesToLaunch <= 0)
        {
            PrintingUtility.PrintChoseNoNukesToLaunch();
            return null;
        }

        return targetPlayerIndex.HasValue
            ? new LaunchNukesInput() { TargetPlayerIndex = targetPlayerIndex.Value, NumberOfNukesLaunched = nukesToLaunch }
            : null;
    }
}
