using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player upgrading their secret base.
    /// </summary>
    public class UpgradeSecretBaseCommand : GameCommand<UpgradeSecretBaseInput, UpgradeSecretBaseResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return !CurrentPlayerDoesNotHaveASecretBase(gameState);
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] UpgradeSecretBaseInput input)
        {
            return !(CurrentPlayerDoesNotHaveASecretBase(gameState) || CurrentPlayerDoesNotHaveEnoughMoney(gameState));
        }

        public override UpgradeSecretBaseResult Execute([DisallowNull] GameState gameState, [DisallowNull] UpgradeSecretBaseInput input)
        {
            if (gameState.CurrentPlayer.State.SecretBase == null)
            {
                throw new InvalidOperationException("The current player does not have a secret base to upgrade.");
            }

            if (CurrentPlayerDoesNotHaveEnoughMoney(gameState))
            {
                throw new InvalidOperationException("The current player does not have enough money to upgrade their secret base.");
            }

            GameState updatedGameState = gameState;
            decimal upgradePrice = CalculateUpgradePrice(gameState);
            if (gameState.CurrentPlayer.State.SecretBase != null)
            {
                updatedGameState = GameStateUpdater.IncrementSecretBaseLevel(gameState, gameState.CurrentPlayerIndex);
            }
            PlayerState updatedPlayerState = updatedGameState.CurrentPlayer.State;
            int newLevel = updatedPlayerState.SecretBase!.Level;
            updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(updatedGameState, updatedGameState.CurrentPlayerIndex, -1 * upgradePrice);

            return new UpgradeSecretBaseResult(updatedGameState, updatedGameState.CurrentPlayerIndex, newLevel, upgradePrice);
        }

        private static decimal CalculateUpgradePrice(GameState gameState)
        {
            return SecretBase.CalculateUpgradePrice(gameState.CurrentPlayer.State.SecretBase!);
        }

        private static bool CurrentPlayerDoesNotHaveASecretBase(GameState gameState)
        {
            return gameState.CurrentPlayer.State.SecretBase == null;
        }

        private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState)
        {
            return CalculateUpgradePrice(gameState) > gameState.CurrentPlayer.State.Money;
        }
    }
}
