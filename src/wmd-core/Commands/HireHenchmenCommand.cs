using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player hiring henchmen.
    /// </summary>
    public class HireHenchmenCommand : GameCommand<HireHenchmenInput, HireHenchmenResult>
    {
        private const string InvalidOperationException_NotEnoughMoney = "The current player does not have enough money to hire henchmen.";

        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return !GameStateChecks.CurrentPlayerHasNoMoney(gameState);
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input)
        {
            return CanExecuteForState(gameState);
        }

        public override HireHenchmenResult Execute([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input)
        {
            if (GameStateChecks.CurrentPlayerHasNoMoney(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_NotEnoughMoney);
            }

            int henchmenHired = HenchmenCalculator.CalculateNumberOfHenchmenHired(gameState, input.OpenPositionsOffered);
            GameState updatedGameState = GameStateUpdater.AdjustHenchmenForPlayer(gameState, gameState.CurrentPlayerIndex, henchmenHired);

            return new HireHenchmenResult(updatedGameState, gameState.CurrentPlayerIndex, henchmenHired);
        }
    }
}
