﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player purchasing unclaimed land.
    /// </summary>
    public class PurchaseUnclaimedLandCommand : GameCommand<PurchaseUnclaimedLandInput, PurchaseUnclaimedLandResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] PurchaseUnclaimedLandInput input)
        {
            return !(CurrentPlayerHasInsufficientFunds(gameState, input) || NotEnoughLandToSatisfyPurchaseAmount(gameState, input));
        }

        public override PurchaseUnclaimedLandResult Execute([DisallowNull] GameState gameState, [DisallowNull] PurchaseUnclaimedLandInput input)
        {
            decimal totalPurchasePrice = CalculateTotalPurchasePrice(gameState, input);

            if (CurrentPlayerHasInsufficientFunds(gameState, input))
            {
                throw new InvalidOperationException("The current player does not have enough money to purchase the requested amount of land.");
            }

            if (NotEnoughLandToSatisfyPurchaseAmount(gameState, input))
            {
                throw new InvalidOperationException("There is not enough unclaimed land left to satisfy the current player's requested amount to purchase.");
            }

            GameState updatedGameState = GameStateUpdater.GiveUnclaimedLandToPlayer(gameState, gameState.CurrentPlayerIndex, input.AreaToPurchase);
            updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(updatedGameState, gameState.CurrentPlayerIndex, -1 * totalPurchasePrice);

            return new PurchaseUnclaimedLandResult(updatedGameState, gameState.CurrentPlayerIndex, input.AreaToPurchase, totalPurchasePrice);
        }

        private static decimal CalculateTotalPurchasePrice(GameState gameState, PurchaseUnclaimedLandInput input)
        {
            return gameState.UnclaimedLandPurchasePrice * input.AreaToPurchase;
        }

        private static bool CurrentPlayerHasInsufficientFunds(GameState gameState, PurchaseUnclaimedLandInput input)
        {
            return CalculateTotalPurchasePrice(gameState, input) > gameState.CurrentPlayer.State.Money;
        }

        private static bool NotEnoughLandToSatisfyPurchaseAmount(GameState gameState, PurchaseUnclaimedLandInput input)
        {
            return input.AreaToPurchase > gameState.Planet.UnclaimedLandArea;
        }
    }
}
