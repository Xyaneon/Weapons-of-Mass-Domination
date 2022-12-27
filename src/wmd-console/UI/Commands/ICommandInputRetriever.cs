using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

interface ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState);
}
