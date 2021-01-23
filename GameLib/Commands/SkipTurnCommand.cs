using System.Diagnostics.CodeAnalysis;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player skipping their turn.
    /// </summary>
    public class SkipTurnCommand : GameCommand<SkipTurnInput, SkipTurnResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] SkipTurnInput input)
        {
            return true;
        }

        public override SkipTurnResult Execute([DisallowNull] GameState gameState, [DisallowNull] SkipTurnInput input)
        {
            return new SkipTurnResult(gameState, gameState.CurrentPlayerIndex);
        }
    }
}
