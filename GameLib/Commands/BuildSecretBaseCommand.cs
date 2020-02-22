using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player building a secret base.
    /// </summary>
    public class BuildSecretBaseCommand : GameCommand<BuildSecretBaseInput, BuildSecretBaseResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return !(CurrentPlayerDoesNotHaveEnoughMoney(gameState) || CurrentPlayerAlreadyHasASecretBase(gameState));
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, BuildSecretBaseInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return CanExecuteForState(gameState);
        }

        public override BuildSecretBaseResult Execute(GameState gameState, BuildSecretBaseInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (CurrentPlayerDoesNotHaveEnoughMoney(gameState))
            {
                throw new InvalidOperationException("The current player does not have enough money to upgrade their secret base.");
            }

            if (!CurrentPlayerAlreadyHasASecretBase(gameState))
            {
                gameState.CurrentPlayer.State.SecretBase = new SecretBase();
            }
            else
            {
                throw new InvalidOperationException("The current player already has a secret base.");
            }

            decimal buildPrice = CalculateBuildPrice(gameState);
            gameState.CurrentPlayer.State.Money -= buildPrice;

            return new BuildSecretBaseResult(gameState.CurrentPlayer, gameState, buildPrice);
        }

        private static bool CurrentPlayerAlreadyHasASecretBase(GameState gameState)
        {
            return gameState.CurrentPlayer.State.SecretBase != null;
        }

        private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState)
        {
            return CalculateBuildPrice(gameState) > gameState.CurrentPlayer.State.Money;
        }

        private static decimal CalculateBuildPrice(GameState gameState)
        {
            return SecretBase.SecretBaseBuildPrice;
        }
    }
}
