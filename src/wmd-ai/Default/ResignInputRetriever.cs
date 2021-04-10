using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default
{
    internal sealed class ResignInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState) => new ResignInput();
    }
}
