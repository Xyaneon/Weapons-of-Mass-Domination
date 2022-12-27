using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player hiring henchmen.
/// </summary>
public class HireHenchmenCommand : GameCommand<HireHenchmenInput, HireHenchmenResult>
{
    private const string InvalidOperationException_NeutralPopulationDepleted = "The neutral population is depleted, and henchmen cannot be hired from it.";
    private const string InvalidOperationException_NotEnoughMoney = "The current player does not have enough money to hire henchmen.";
    private const string InvalidOperationException_NotEnoughMoneyForPositionsOffered = "The current player does not have enough money for the open positions offered.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        !GameStateChecks.CurrentPlayerHasNoMoney(gameState) && !GameStateChecks.NeutralPopulationIsDepleted(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input) =>
        CanExecuteForState(gameState) && PlayerHasOneDayPayForAllOfferedPositions(gameState, input);

    public override HireHenchmenResult Execute([DisallowNull] GameState gameState, [DisallowNull] HireHenchmenInput input)
    {
        if (GameStateChecks.CurrentPlayerHasNoMoney(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_NotEnoughMoney);
        }

        if (GameStateChecks.NeutralPopulationIsDepleted(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_NeutralPopulationDepleted);
        }

        if (!PlayerHasOneDayPayForAllOfferedPositions(gameState, input))
        {
            throw new InvalidOperationException(InvalidOperationException_NotEnoughMoneyForPositionsOffered);
        }

        long henchmenHired = HenchmenCalculator.CalculateNumberOfHenchmenHired(gameState, input.OpenPositionsOffered);
        GameState updatedGameState = new GameStateUpdater(gameState)
            .ConvertNeutralPopulationToPlayerHenchmen(gameState.CurrentPlayerIndex, henchmenHired)
            .AndReturnUpdatedGameState();

        return new HireHenchmenResult(updatedGameState, gameState.CurrentPlayerIndex, henchmenHired);
    }

    private static bool PlayerHasOneDayPayForAllOfferedPositions(GameState gameState, HireHenchmenInput input) =>
        gameState.CurrentPlayer.State.Money >= gameState.CurrentPlayer.State.WorkforceState.DailyPayRate * input.OpenPositionsOffered;
}
