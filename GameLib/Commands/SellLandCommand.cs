using System;
using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player selling land they control.
    /// </summary>
    public class SellLandCommand : GameCommand<SellLandInput, SellLandResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return !CurrentPlayerHasNoLandToSell(gameState);
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, SellLandInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return !(CurrentPlayerHasNoLandToSell(gameState) || CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input));
        }

        public override SellLandResult Execute(GameState gameState, SellLandInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (CurrentPlayerHasNoLandToSell(gameState))
            {
                throw new InvalidOperationException("The current player does not have any land to sell.");
            }

            if (CurrentPlayerDoesNotHaveEnoughLandToSellForInput(gameState, input))
            {
                throw new InvalidOperationException("The current player has less land than they want to sell.");
            }

            decimal totalSalePrice = gameState.UnclaimedLandPurchasePrice * input.AreaToSell;
            GameStateUpdater.HavePlayerGiveUpLand(gameState, gameState.CurrentPlayerIndex, input.AreaToSell);
            PlayerState playerState = gameState.CurrentPlayer.State;
            gameState.CurrentPlayer.State = playerState with { Money = playerState.Money + totalSalePrice };

            return new SellLandResult(gameState.CurrentPlayer, gameState, input.AreaToSell, totalSalePrice);
        }

        private static bool CurrentPlayerDoesNotHaveEnoughLandToSellForInput(GameState gameState, SellLandInput input)
        {
            return input.AreaToSell > gameState.CurrentPlayer.State.Land;
        }

        private static bool CurrentPlayerHasNoLandToSell(GameState gameState)
        {
            return gameState.CurrentPlayer.State.Land == 0;
        }
    }
}
