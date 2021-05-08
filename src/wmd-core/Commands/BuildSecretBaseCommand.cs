using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player building a secret base.
    /// </summary>
    public class BuildSecretBaseCommand : GameCommand<BuildSecretBaseInput, BuildSecretBaseResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return !(CurrentPlayerDoesNotHaveEnoughMoney(gameState) || GameStateChecks.CurrentPlayerHasASecretBase(gameState));
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] BuildSecretBaseInput input)
        {
            return CanExecuteForState(gameState);
        }

        public override BuildSecretBaseResult Execute([DisallowNull] GameState gameState, [DisallowNull] BuildSecretBaseInput input)
        {
            if (CurrentPlayerDoesNotHaveEnoughMoney(gameState))
            {
                throw new InvalidOperationException("The current player does not have enough money to upgrade their secret base.");
            }

            if (GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                throw new InvalidOperationException("The current player already has a secret base.");
            }

            decimal buildPrice = CalculateBuildPrice(gameState);
            PlayerState updatedPlayerState = gameState.CurrentPlayer.State with { SecretBase = new SecretBase() };

            GameState updatedGameState = new GameStateUpdater(gameState)
                .UpdatePlayerState(gameState.CurrentPlayerIndex, updatedPlayerState)
                .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, -1 * buildPrice)
                .AndReturnUpdatedGameState();

            return new BuildSecretBaseResult(updatedGameState, gameState.CurrentPlayerIndex, buildPrice);
        }

        private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState) =>
            CalculateBuildPrice(gameState) > gameState.CurrentPlayer.State.Money;

        private static decimal CalculateBuildPrice(GameState gameState) => SecretBase.SecretBaseBuildPrice;
    }
}
