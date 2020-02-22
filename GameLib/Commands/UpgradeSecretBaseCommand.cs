using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player upgrading their secret base.
    /// </summary>
    public class UpgradeSecretBaseCommand : GameCommand<UpgradeSecretBaseInput, UpgradeSecretBaseResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return !CurrentPlayerDoesNotHaveASecretBase(gameState);
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, UpgradeSecretBaseInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return !(CurrentPlayerDoesNotHaveASecretBase(gameState) || CurrentPlayerDoesNotHaveEnoughMoney(gameState));
        }

        public override UpgradeSecretBaseResult Execute(GameState gameState, UpgradeSecretBaseInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (CurrentPlayerDoesNotHaveASecretBase(gameState))
            {
                throw new InvalidOperationException("The current player does not have a secret base to upgrade.");
            }

            if (CurrentPlayerDoesNotHaveEnoughMoney(gameState))
            {
                throw new InvalidOperationException("The current player does not have enough money to upgrade their secret base.");
            }

            decimal upgradePrice = CalculateUpgradePrice(gameState);
            gameState.CurrentPlayer.State.SecretBase.Level++;
            int newLevel = gameState.CurrentPlayer.State.SecretBase.Level;
            gameState.CurrentPlayer.State.Money -= upgradePrice;

            return new UpgradeSecretBaseResult(gameState.CurrentPlayer, gameState, newLevel, upgradePrice);
        }

        private static decimal CalculateUpgradePrice(GameState gameState)
        {
            return SecretBase.CalculateUpgradePrice(gameState.CurrentPlayer.State.SecretBase);
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
