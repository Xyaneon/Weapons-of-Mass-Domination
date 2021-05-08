using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player skipping their turn.
    /// </summary>
    public class SkipTurnCommand : GameCommand<SkipTurnInput, SkipTurnResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState) => true;

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] SkipTurnInput input) => true;

        public override SkipTurnResult Execute([DisallowNull] GameState gameState, [DisallowNull] SkipTurnInput input) => new(gameState, gameState.CurrentPlayerIndex);
    }
}
