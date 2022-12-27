using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player manufacturing nukes.
/// </summary>
public class ManufactureNukesCommand : GameCommand<ManufactureNukesInput, ManufactureNukesResult>
{
    private const string InvalidOperationException_InsufficientResearchLevel = "The current player has not attained the required level of research to manufacture nukes.";
    private const string InvalidOperationException_InsufficientFunds = "The current player does not have enough money to manufacture the requested quantity of nukes.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, ManufactureNukesInput input) =>
        !CurrentPlayerDoesNotHaveEnoughMoney(gameState, input.NumberOfNukesToManufacture)
            && GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState);

    public override ManufactureNukesResult Execute([DisallowNull] GameState gameState, ManufactureNukesInput input)
    {
        if (!GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_InsufficientResearchLevel);
        }

        if (CurrentPlayerDoesNotHaveEnoughMoney(gameState, input.NumberOfNukesToManufacture))
        {
            throw new InvalidOperationException(InvalidOperationException_InsufficientFunds);
        }

        decimal manufacturingPrice = NukesCalculator.CalculateTotalManufacturingPrice(gameState, input.NumberOfNukesToManufacture);
        GameState updatedGameState = new GameStateUpdater(gameState)
            .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, -1 * manufacturingPrice)
            .AdjustNukesForPlayer(gameState.CurrentPlayerIndex, input.NumberOfNukesToManufacture)
            .AndReturnUpdatedGameState();

        return new ManufactureNukesResult(updatedGameState, gameState.CurrentPlayerIndex, input.NumberOfNukesToManufacture);
    }

    private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState, int quantity) =>
        NukesCalculator.CalculateTotalManufacturingPrice(gameState, quantity) > gameState.CurrentPlayer.State.Money;
}
