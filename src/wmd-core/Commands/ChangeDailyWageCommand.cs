using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player changing their daily wage for their henchmen.
    /// </summary>
    public class ChangeDailyWageCommand : GameCommand<ChangeDailyWageInput, ChangeDailyWageResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState) => true;

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, ChangeDailyWageInput input) =>
            CanExecuteForState(gameState);

        public override ChangeDailyWageResult Execute([DisallowNull] GameState gameState, [DisallowNull] ChangeDailyWageInput input)
        {
            decimal oldDailyWage = gameState.CurrentPlayer.State.WorkforceState.DailyPayRate;
            GameState updatedGameState = new GameStateUpdater(gameState)
                .SetDailyWageForPlayer(gameState.CurrentPlayerIndex, input.NewDailyWage)
                .AndReturnUpdatedGameState();

            return new ChangeDailyWageResult(updatedGameState, gameState.CurrentPlayerIndex, oldDailyWage, input.NewDailyWage);
        }
    }
}
