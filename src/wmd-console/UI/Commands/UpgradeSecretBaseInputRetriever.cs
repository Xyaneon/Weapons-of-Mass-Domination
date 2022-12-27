using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands;

class UpgradeSecretBaseInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState)
    {
        var secretBase = gameState.CurrentPlayer.State.SecretBase;

        if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
        {
            PrintingUtility.PrintDoNotHaveASecretBase();
            return null;
        }

        var upgradePrice = SecretBase.CalculateUpgradePrice(secretBase);

        if (upgradePrice > gameState.CurrentPlayer.State.Money)
        {
            PrintingUtility.PrintInsufficientFundsForBuildingSecretBase(upgradePrice);
            return null;
        }

        var prompt = $"You can upgrade your secret base to Level {secretBase!.Level + 1:N0} for {upgradePrice:C}. Proceed?";

        return UserInput.GetConfirmation(prompt)
            ? new UpgradeSecretBaseInput()
            : null;
    }
}
