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
        /// The action of the current player hiring henchmen.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="HireHenchmenResult"/> instance describing the result of the action.</returns>
        public static HireHenchmenResult CurrentPlayerHiresHenchmen(GameState gameState, HireHenchmenInput input)
        {
            // TODO: Introduce variance for how many henchmen actually get hired.
            int henchmenHired = input.OpenPositionsOffered;
            gameState.CurrentPlayer.State.Henchmen += henchmenHired;
            return new HireHenchmenResult(gameState.CurrentPlayer, gameState, henchmenHired);
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
            decimal totalPurchasePrice = gameState.UnclaimedLandPurchasePrice * input.AreaToPurchase;
            if (totalPurchasePrice > gameState.CurrentPlayer.State.Money)
            {
                throw new InvalidOperationException("The current player does not have enough money to purchase the requested amount of land.");
            }

            try
            {
                GameStateUpdater.GiveUnclaimedLandToPlayer(gameState, gameState.CurrentPlayerIndex, input.AreaToPurchase);
                gameState.CurrentPlayer.State.Money -= totalPurchasePrice;
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
            gameState.CurrentPlayer.State.HasResigned = true;
            return new ResignResult(gameState.CurrentPlayer, gameState);
        }

        /// <summary>
        /// The action of the current player selling land they control.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="SellLandResult"/> instance describing the result of the action.</returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="input"/> specifies more land to sell than the current player actually has.
        /// </exception>
        public static SellLandResult CurrentPlayerSellsLand(GameState gameState, SellLandInput input)
        {
            if (input.AreaToSell > gameState.CurrentPlayer.State.Land)
            {
                throw new InvalidOperationException("The current player has less land than they want to sell.");
            }

            decimal totalSalePrice = gameState.UnclaimedLandPurchasePrice * input.AreaToSell;
            GameStateUpdater.HavePlayerGiveUpLand(gameState, gameState.CurrentPlayerIndex, input.AreaToSell);
            gameState.CurrentPlayer.State.Money += totalSalePrice;

            return new SellLandResult(gameState.CurrentPlayer, gameState, input.AreaToSell, totalSalePrice);
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
            decimal moneyStolenByHenchmen = gameState.CurrentPlayer.State.Henchmen * (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolen = moneyStolenByPlayer + moneyStolenByHenchmen;
            gameState.CurrentPlayer.State.Money += moneyStolen;

            return new StealMoneyResult(gameState.CurrentPlayer, gameState, moneyStolen);
        }

        /// <summary>
        /// The action of the current player upgrading their secret base.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <param name="input">The additional input data for the action.</param>
        /// <returns>A new <see cref="UpgradeSecretBaseResult"/> instance describing the result of the action.</returns>
        /// <exception cref="InvalidOperationException">
        /// The current player does not have enough money to upgrade their secret base.
        /// </exception>
        public static UpgradeSecretBaseResult CurrentPlayerUpgradesTheirSecretBase(GameState gameState, UpgradeSecretBaseInput input)
        {
            decimal upgradePrice = SecretBase.CalculateUpgradePrice(gameState.CurrentPlayer.State.SecretBase);

            if (upgradePrice > gameState.CurrentPlayer.State.Money)
            {
                throw new InvalidOperationException("The current player does not have enough money to upgrade their secret base.");
            }

            if (gameState.CurrentPlayer.State.SecretBase == null)
            {
                gameState.CurrentPlayer.State.SecretBase = new SecretBase();
            }
            else
            {
                gameState.CurrentPlayer.State.SecretBase.Level++;
            }
            int newLevel = gameState.CurrentPlayer.State.SecretBase.Level;
            gameState.CurrentPlayer.State.Money -= upgradePrice;

            return new UpgradeSecretBaseResult(gameState.CurrentPlayer, gameState, newLevel, upgradePrice);
        }
    }
}
