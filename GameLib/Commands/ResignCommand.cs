using System.Diagnostics.CodeAnalysis;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player resigning.
    /// </summary>
    public class ResignCommand : GameCommand<ResignInput, ResignResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] ResignInput input)
        {
            return true;
        }

        public override ResignResult Execute([DisallowNull] GameState gameState, [DisallowNull] ResignInput input)
        {
            gameState.CurrentPlayer.State = gameState.CurrentPlayer.State with { HasResigned = true };
            return new ResignResult(gameState.CurrentPlayer, gameState);
        }
    }
}
