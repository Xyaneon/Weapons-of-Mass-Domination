﻿using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player selling land they control.
    /// </summary>
    public class SellLandCommand : GameCommand<SellLandInput, SellLandResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return !CurrentPlayerHasNoLandToSell(gameState);
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input)
        {
            return !(CurrentPlayerHasNoLandToSell(gameState) || CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input));
        }

        public override SellLandResult Execute([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input)
        {
            if (CurrentPlayerHasNoLandToSell(gameState))
            {
                throw new InvalidOperationException("The current player does not have any land to sell.");
            }

            if (CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input))
            {
                throw new InvalidOperationException("The current player has less land than they want to sell.");
            }

            decimal totalSalePrice = gameState.UnclaimedLandPurchasePrice * input.AreaToSell;
            GameState updatedGameState = GameStateUpdater.HavePlayerGiveUpLand(gameState, gameState.CurrentPlayerIndex, input.AreaToSell);
            updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(updatedGameState, gameState.CurrentPlayerIndex, totalSalePrice);

            return new SellLandResult(updatedGameState, gameState.CurrentPlayerIndex, input.AreaToSell, totalSalePrice);
        }

        private static bool CurrentPlayerDoesNotHaveEnoughLandToSellForInput([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input)
        {
            return input.AreaToSell > gameState.CurrentPlayer.State.Land;
        }

        private static bool CurrentPlayerHasNoLandToSell([DisallowNull] GameState gameState)
        {
            return gameState.CurrentPlayer.State.Land == 0;
        }
    }
}
