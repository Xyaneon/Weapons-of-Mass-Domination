using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player purchasing unclaimed land.
    /// </summary>
    public class PurchaseUnclaimedLandCommand : GameCommand<PurchaseUnclaimedLandInput, PurchaseUnclaimedLandResult>
    {
        private const string InvalidOperationException_PlayerCannotPurchaseAnyLand = "The current player is unable to purchase any land.";
        private const string InvalidOperationException_PlayerCannotPurchaseRequestedAmountOfLand = "The current player does not have enough money to purchase the requested amount of land.";
        private const string InvalidOperationException_PlayerRequestingToPurchaseMoreLandThanIsAvailable = "There is not enough unclaimed land left to satisfy the current player's requested amount to purchase.";

        public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
            GameStateChecks.CurrentPlayerCouldPurchaseLand(gameState);

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] PurchaseUnclaimedLandInput input) =>
            CanExecuteForState(gameState) && (!(CurrentPlayerHasInsufficientFunds(gameState, input) || NotEnoughLandToSatisfyPurchaseAmount(gameState, input)));

        public override PurchaseUnclaimedLandResult Execute([DisallowNull] GameState gameState, [DisallowNull] PurchaseUnclaimedLandInput input)
        {
            if (!GameStateChecks.CurrentPlayerCouldPurchaseLand(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_PlayerCannotPurchaseAnyLand);
            }

            decimal totalPurchasePrice = LandAreaCalculator.CalculateTotalPurchasePrice(gameState, input.AreaToPurchase);

            if (CurrentPlayerHasInsufficientFunds(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_PlayerCannotPurchaseRequestedAmountOfLand);
            }

            if (NotEnoughLandToSatisfyPurchaseAmount(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_PlayerRequestingToPurchaseMoreLandThanIsAvailable);
            }

            GameState updatedGameState = new GameStateUpdater(gameState)
                .GiveUnclaimedLandToPlayer(gameState.CurrentPlayerIndex, input.AreaToPurchase)
                .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, -1 * totalPurchasePrice)
                .AndReturnUpdatedGameState();

            return new PurchaseUnclaimedLandResult(updatedGameState, gameState.CurrentPlayerIndex, input.AreaToPurchase, totalPurchasePrice);
        }

        private static bool CurrentPlayerHasInsufficientFunds(GameState gameState, PurchaseUnclaimedLandInput input) =>
            LandAreaCalculator.CalculateTotalPurchasePrice(gameState, input.AreaToPurchase) > gameState.CurrentPlayer.State.Money;

        private static bool NotEnoughLandToSatisfyPurchaseAmount(GameState gameState, PurchaseUnclaimedLandInput input) =>
            input.AreaToPurchase > gameState.Planet.UnclaimedLandArea;
    }
}
