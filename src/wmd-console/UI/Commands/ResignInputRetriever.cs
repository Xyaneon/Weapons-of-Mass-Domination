using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class ResignInputRetriever : ICommandInputRetriever
{
    private const string ResignPrompt = "Are you really sure you want to resign?";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        return UserInput.GetConfirmation(ResignPrompt) ? new ResignInput() : null;
    }
}
