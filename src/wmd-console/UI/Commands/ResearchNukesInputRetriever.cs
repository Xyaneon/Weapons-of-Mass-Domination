using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands;

class ResearchNukesInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState)
    {
        var currentResearchLevel = gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel;

        if (GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
        {
            PrintingUtility.PrintNukesResearchAlreadyMaxedOut();
            return null;
        }

        if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
        {
            PrintingUtility.PrintDoNotHaveASecretBase();
            return null;
        }

        var researchPrice = NukeConstants.NukeResearchLevelCost;

        if (researchPrice > gameState.CurrentPlayer.State.Money)
        {
            PrintingUtility.PrintInsufficientFundsForResearchingNukes(researchPrice);
            return null;
        }

        var prompt = $"You can advance your nukes research to Level {currentResearchLevel + 1:N0} for {researchPrice:C}. Proceed?";

        return UserInput.GetConfirmation(prompt)
            ? new ResearchNukesInput()
            : null;
    }
}
