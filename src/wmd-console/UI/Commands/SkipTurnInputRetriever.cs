using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class SkipTurnInputRetriever : ICommandInputRetriever
    {
        private const string SkipTurnPrompt = "Are you really sure you want to skip your turn?";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            return UserInput.GetConfirmation(SkipTurnPrompt) ? new SkipTurnInput() : null;
        }
    }
}
