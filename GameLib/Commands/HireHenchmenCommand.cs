using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player hiring henchmen.
    /// </summary>
    public class HireHenchmenCommand : GameCommand<HireHenchmenInput, HireHenchmenResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input)
        {
            return true;
        }

        public override HireHenchmenResult Execute([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input)
        {
            int henchmenHired = input.OpenPositionsOffered;
            GameState updatedGameState = GameStateUpdater.AdjustHenchmenForPlayer(gameState, gameState.CurrentPlayerIndex, henchmenHired);

            return new HireHenchmenResult(updatedGameState, gameState.CurrentPlayerIndex, henchmenHired);
        }
    }
}
