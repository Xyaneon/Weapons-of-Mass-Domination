using System;

namespace WMD.Game.Actions
{
    /// <summary>
    /// Provides actions a player may take on their turn.
    /// </summary>
    public static class PlayerActions
    {
        private const decimal BaseMoneyStealAmount = 200;

        /// <summary>
        /// Initializes static members of the <see cref="PlayerActions"/> class.
        /// </summary>
        static PlayerActions()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        /// <summary>
        /// The action of the current player hiring minions.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="HireMinionsResult"/> instance describing the result of the action.</returns>
        public static HireMinionsResult CurrentPlayerHiresMinions(GameState gameState, HireMinionsInput input)
        {
            // TODO: Introduce variance for how many minions actually get hired.
            int minionsHired = input.OpenPositionsOffered;
            gameState.CurrentPlayer.Minions += minionsHired;
            return new HireMinionsResult(gameState.CurrentPlayer, gameState, minionsHired);
        }

        /// <summary>
        /// The action of the current player purchasing unclaimed land.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="PurchaseUnclaimedLandResult"/> instance describing the result of the action.</returns>
        /// <exception cref="InvalidOperationException">
        /// The current player does not have enough money to purchase the requested amount of land.
        /// -or-
        /// There is not enough unclaimed land left to satisfy the current player's requested amount to purchase.
        /// </exception>
        public static PurchaseUnclaimedLandResult CurrentPlayerPurchasesUnclaimedLand(GameState gameState, PurchaseUnclaimedLandInput input)
        {
            decimal totalPurchasePrice = gameState.CalculateUnclaimedLandPurchasePrice() * input.AreaToPurchase;
            if (totalPurchasePrice > gameState.CurrentPlayer.Money)
            {
                throw new InvalidOperationException("The current player does not have enough money to purchase the requested amount of land.");
            }

            try
            {
                gameState.GiveUnclaimedLandToPlayer(gameState.CurrentPlayerIndex, input.AreaToPurchase);
                gameState.CurrentPlayer.Money -= totalPurchasePrice;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("There is not enough unclaimed land left to satisfy the current player's requested amount to purchase.");
            }

            return new PurchaseUnclaimedLandResult(gameState.CurrentPlayer, gameState, input.AreaToPurchase, totalPurchasePrice);
        }

        /// <summary>
        /// The action of the current player resigning.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="ResignResult"/> instance describing the result of the action.</returns>
        public static ResignResult CurrentPlayerResigns(GameState gameState, ResignInput input)
        {
            gameState.CurrentPlayer.HasResigned = true;
            return new ResignResult(gameState.CurrentPlayer, gameState);
        }

        /// <summary>
        /// The action of the current player skipping their turn.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="SkipTurnResult"/> instance describing the result of the action.</returns>
        public static SkipTurnResult CurrentPlayerSkipsTurn(GameState gameState, SkipTurnInput input) => new SkipTurnResult(gameState.CurrentPlayer, gameState);

        /// <summary>
        /// The action of the current player stealing money.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="StealMoneyResult"/> instance describing the result of the action.</returns>
        public static StealMoneyResult CurrentPlayerStealsMoney(GameState gameState, StealMoneyInput input)
        {
            decimal moneyStolenByPlayer = (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 *_random.NextDouble(), 2);
            decimal moneyStolenByMinions = gameState.CurrentPlayer.Minions * (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolen = moneyStolenByPlayer + moneyStolenByMinions;
            gameState.CurrentPlayer.Money += moneyStolen;

            return new StealMoneyResult(gameState.CurrentPlayer, gameState, moneyStolen);
        }
    }
}
