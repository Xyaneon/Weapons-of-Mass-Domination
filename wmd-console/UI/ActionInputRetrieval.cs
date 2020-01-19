using System;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console.UI
{
    static class ActionInputRetrieval
    {
        private const string UnclaimedLandPurchasePrompt = "Please enter how many square kilometers of land you would like to purchase";

        public static HireHenchmenInput? GetHireHenchmenInput(GameState gameState)
        {
            // TODO: Add more control over the hiring process.
            int openPositionsToOffer = 10;
            return UserInput.GetConfirmation($"You will be looking to fill {openPositionsToOffer:N0} positions. Continue?")
                ? new HireHenchmenInput(openPositionsToOffer)
                : null;
        }

        public static PurchaseUnclaimedLandInput? GetPurchaseUnclaimedLandInput(GameState gameState)
        {
            if (gameState.Planet.UnclaimedLandArea < 1)
            {
                PrintingUtility.PrintNoUnclaimedLandLeftToPurchase();
                return null;
            }

            PrintingUtility.PrintCurrentUnclaimedLand(gameState);
            int maxPurchaseableArea = CalculateMaxPurchaseableArea(gameState);
            if (maxPurchaseableArea < 1)
            {
                PrintingUtility.PrintInsufficientFundsForAnyLandPurchase();
                return null;
            }

            var allowedPurchaseAmounts = new IntRange(0, maxPurchaseableArea);
            string prompt = $"{UnclaimedLandPurchasePrompt} ({allowedPurchaseAmounts.Minimum} to {allowedPurchaseAmounts.Maximum})";
            int areaToPurchase = UserInput.GetInteger(prompt, allowedPurchaseAmounts);
            if (areaToPurchase < 1)
            {
                return null;
            }

            decimal totalPurchasePrice = areaToPurchase * gameState.UnclaimedLandPurchasePrice;
            string confirmationPrompt = $"This transaction will cost you {totalPurchasePrice:C}. Proceed?";
            return UserInput.GetConfirmation(confirmationPrompt)
                ? new PurchaseUnclaimedLandInput(areaToPurchase)
                : null;
        }

        public static ResignInput GetResignInput(GameState gameState) => new ResignInput();

        public static SellLandInput? GetSellLandInput(GameState gameState)
        {
            if (gameState.CurrentPlayer.Land <= 0)
            {
                PrintingUtility.PrintNoLandToSell();
                return null;
            }

            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            var allowedSaleAmounts = new IntRange(0, gameState.CurrentPlayer.Land);
            string prompt = $"Land is currently selling at {pricePerSquareKilometer:C}/km². How much do you want to sell? ({allowedSaleAmounts.Minimum} to {allowedSaleAmounts.Maximum})";
            int areaToSell = UserInput.GetInteger(prompt, allowedSaleAmounts);
            if (areaToSell <= 0)
            {
                return null;
            }

            decimal totalSalePrice = areaToSell * pricePerSquareKilometer;
            string confirmationPrompt = $"This transaction will earn you {totalSalePrice:C}. Proceed?";
            return UserInput.GetConfirmation(confirmationPrompt)
                ? new SellLandInput(areaToSell)
                : null;
        }

        public static SkipTurnInput GetSkipTurnInput(GameState gameState) => new SkipTurnInput();

        public static StealMoneyInput GetStealMoneyInput(GameState gameState)
        {
            // TODO: Collect more input.
            return new StealMoneyInput();
        }

        public static UpgradeSecretBaseInput? GetUpgradeSecretBaseInput(GameState gameState)
        {
            SecretBase secretBase = gameState.CurrentPlayer.SecretBase;
            decimal upgradePrice = SecretBase.CalculateUpgradePrice(secretBase);

            if (upgradePrice > gameState.CurrentPlayer.Money)
            {
                if (secretBase != null)
                {
                    PrintingUtility.PrintInsufficientFundsForUpgradingSecretBase(upgradePrice);
                }
                else
                {
                    PrintingUtility.PrintInsufficientFundsForBuildingSecretBase(upgradePrice);
                }

                return null;
            }

            string prompt = secretBase != null
                ? $"You can upgrade your secret base to Level {secretBase.Level + 1:N0} for {upgradePrice:C}. Proceed?"
                : $"You can build your very own secret base for {upgradePrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new UpgradeSecretBaseInput()
                : null;
        }

        private static int CalculateMaxPurchaseableArea(GameState gameState)
        {
            decimal availableFunds = gameState.CurrentPlayer.Money;
            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            return (int)Math.Floor(availableFunds / pricePerSquareKilometer);
        }
    }
}
