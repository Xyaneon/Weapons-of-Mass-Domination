using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player distributing propaganda.
/// </summary>
public class DistributePropagandaCommand : GameCommand<DistributePropagandaInput, DistributePropagandaResult>
{
    private const string InvalidOperationException_InsufficientFunds = "The current player does not have enough money to distribute the requested amount of propaganda.";
    private const string InvalidOperationException_NoFunds = "The current player does not have any money to distribute propaganda.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        !GameStateChecks.CurrentPlayerHasNoMoney(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, DistributePropagandaInput input) =>
        CanExecuteForState(gameState) && !CurrentPlayerHasInsufficientFunds(gameState, input);

    public override DistributePropagandaResult Execute([DisallowNull] GameState gameState, DistributePropagandaInput input)
    {
        if (GameStateChecks.CurrentPlayerHasNoMoney(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_NoFunds);
        }

        if (CurrentPlayerHasInsufficientFunds(gameState, input))
        {
            throw new InvalidOperationException(InvalidOperationException_InsufficientFunds);
        }

        int reputationGained = ReputationCalculator.CalculateReputationGainedFromSpendingOnPropaganda(gameState, gameState.CurrentPlayerIndex, input.MoneyToSpend);
        GameState updatedGameState = new GameStateUpdater(gameState)
            .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, -1 * input.MoneyToSpend)
            .AdjustReputationForPlayer(gameState.CurrentPlayerIndex, reputationGained)
            .AndReturnUpdatedGameState();

        return new DistributePropagandaResult(updatedGameState, gameState.CurrentPlayerIndex, input.MoneyToSpend, reputationGained);
    }

    private static bool CurrentPlayerHasInsufficientFunds(GameState gameState, DistributePropagandaInput input) =>
        input.MoneyToSpend > gameState.CurrentPlayer.State.Money;
}
