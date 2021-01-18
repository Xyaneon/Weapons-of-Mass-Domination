using System.Diagnostics.CodeAnalysis;
using WMD.Game.Players;

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
            PlayerState playerState = gameState.CurrentPlayer.State;
            gameState.CurrentPlayer.State = playerState with
            {
                WorkforceState = playerState.WorkforceState with
                {
                    NumberOfHenchmen = playerState.WorkforceState.NumberOfHenchmen + henchmenHired
                }
            };
            return new HireHenchmenResult(gameState.CurrentPlayer, gameState, henchmenHired);
        }
    }
}
