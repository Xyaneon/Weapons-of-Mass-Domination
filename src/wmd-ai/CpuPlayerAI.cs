using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI
{
    public sealed class CpuPlayerAI : ICpuPlayerAI
    {
        public AICommandSelection ChooseCommandAndInputForGameState(GameState gameState) =>
            new(new SkipTurnCommand(), new SkipTurnInput());
    }
}
