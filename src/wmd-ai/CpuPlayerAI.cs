using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI
{
    /// <summary>
    /// Provides a default AI for computer players.
    /// </summary>
    public sealed class CpuPlayerAI : ICpuPlayerAI
    {
        public AICommandSelection ChooseCommandAndInputForGameState([DisallowNull] GameState gameState) =>
            new(new SkipTurnCommand(), new SkipTurnInput());
    }
}
