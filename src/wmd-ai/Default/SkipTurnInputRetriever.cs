using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default
{
    internal sealed class SkipTurnInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState) => new SkipTurnInput();
    }
}
