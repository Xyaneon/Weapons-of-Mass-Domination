using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands
{
    class BuildSecretBaseInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState)
        {
            var buildPrice = SecretBase.SecretBaseBuildPrice;

            if (GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                PrintingUtility.PrintAlreadyHaveASecretBase();
                return null;
            }

            if (buildPrice > gameState.CurrentPlayer.State.Money)
            {
                PrintingUtility.PrintInsufficientFundsForBuildingSecretBase(buildPrice);
                return null;
            }

            var prompt = $"You can build your very own secret base for {buildPrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new BuildSecretBaseInput()
                : null;
        }
    }
}
