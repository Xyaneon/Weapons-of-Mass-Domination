using System;
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
        private const string InvalidOperationException_NoLandToSell = "The current player does not have any land to sell.";
        private const string InvalidOperationException_NotEnoughLandToSellForInput = "The current player has less land than they want to sell.";

        public override bool CanExecuteForState([DisallowNull] GameState gameState) => !CurrentPlayerHasNoLandToSell(gameState);

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input) =>
            !(CurrentPlayerHasNoLandToSell(gameState) || CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input));

        public override SellLandResult Execute([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input)
        {
            if (CurrentPlayerHasNoLandToSell(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_NoLandToSell);
            }

            if (CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_NotEnoughLandToSellForInput);
            }

            decimal totalSalePrice = gameState.UnclaimedLandPurchasePrice * input.AreaToSell;
            GameState updatedGameState = new GameStateUpdater(gameState)
                .HavePlayerGiveUpLand(gameState.CurrentPlayerIndex, input.AreaToSell)
                .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, totalSalePrice)
                .AndReturnUpdatedGameState();

            return new SellLandResult(updatedGameState, gameState.CurrentPlayerIndex, input.AreaToSell, totalSalePrice);
        }

        private static bool CurrentPlayerDoesNotHaveEnoughLandToSellForInput([DisallowNull] GameState gameState, [DisallowNull] SellLandInput input) =>
            input.AreaToSell > gameState.CurrentPlayer.State.Land;

        private static bool CurrentPlayerHasNoLandToSell([DisallowNull] GameState gameState) => gameState.CurrentPlayer.State.Land == 0;
    }
}
