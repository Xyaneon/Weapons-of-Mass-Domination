using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class StealMoneyInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState)
    {
        return new StealMoneyInput();
    }
}
