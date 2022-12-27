using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player upgrading their secret base.
/// </summary>
public class UpgradeSecretBaseCommand : GameCommand<UpgradeSecretBaseInput, UpgradeSecretBaseResult>
{
    private const string InvalidOperationException_NoSecretBase = "The current player does not have a secret base to upgrade.";
    private const string InvalidOperationException_InsufficientFunds = "The current player does not have enough money to upgrade their secret base.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        GameStateChecks.CurrentPlayerHasASecretBase(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] UpgradeSecretBaseInput input) =>
        GameStateChecks.CurrentPlayerHasASecretBase(gameState)
            && !CurrentPlayerDoesNotHaveEnoughMoney(gameState);

    public override UpgradeSecretBaseResult Execute([DisallowNull] GameState gameState, [DisallowNull] UpgradeSecretBaseInput input)
    {
        if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_NoSecretBase);
        }

        if (CurrentPlayerDoesNotHaveEnoughMoney(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_InsufficientFunds);
        }

        decimal upgradePrice = CalculateUpgradePrice(gameState);

        GameState updatedGameState = new GameStateUpdater(gameState)
            .IncrementSecretBaseLevel(gameState.CurrentPlayerIndex)
            .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, -1 * upgradePrice)
            .AndReturnUpdatedGameState();
        int newLevel = updatedGameState.CurrentPlayer.State.SecretBase!.Level;

        return new UpgradeSecretBaseResult(updatedGameState, updatedGameState.CurrentPlayerIndex, newLevel, upgradePrice);
    }

    private static decimal CalculateUpgradePrice(GameState gameState) => SecretBase.CalculateUpgradePrice(gameState.CurrentPlayer.State.SecretBase!);

    private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState) => CalculateUpgradePrice(gameState) > gameState.CurrentPlayer.State.Money;
}
