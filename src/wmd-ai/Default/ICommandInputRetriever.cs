using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default;

internal interface ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState);
}
