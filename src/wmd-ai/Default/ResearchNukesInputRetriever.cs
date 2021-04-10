using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class ResearchNukesInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState) =>
            CanResearchNukesThisTurn(gameState) ? new ResearchNukesInput() : null;

        private static bool CanResearchNukesThisTurn(GameState gameState) =>
            !GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState)
                && GameStateChecks.CurrentPlayerHasASecretBase(gameState)
                && NukeConstants.NukeResearchLevelCost <= gameState.CurrentPlayer.State.Money;
    }
}
