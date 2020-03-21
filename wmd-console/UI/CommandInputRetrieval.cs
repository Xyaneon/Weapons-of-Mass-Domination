using System;
using System.Collections.Generic;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Commands;

namespace WMD.Console.UI
{
    static class CommandInputRetrieval
    {
        private const string PositionsToOfferPrompt = "Please enter how many open positions you would like to offer";
        private const string UnclaimedLandPurchasePrompt = "Please enter how many square kilometers of land you would like to purchase";

        static CommandInputRetrieval()
        {
            _inputDict = new Dictionary<Type, Func<GameState, CommandInput?>>
            {
                { typeof(BuildSecretBaseInput), GetBuildSecretBaseInput },
                { typeof(HireHenchmenInput), GetHireHenchmenInput },
                { typeof(PurchaseUnclaimedLandInput), GetPurchaseUnclaimedLandInput },
                { typeof(ResignInput), GetResignInput },
                { typeof(SellLandInput), GetSellLandInput },
                { typeof(SkipTurnInput), GetSkipTurnInput },
                { typeof(StealMoneyInput), GetStealMoneyInput },
                { typeof(UpgradeSecretBaseInput), GetUpgradeSecretBaseInput },
            };
        }

        private static IReadOnlyDictionary<Type, Func<GameState, CommandInput?>> _inputDict;

        public static CommandInput? GetCommandInput(GameState gameState, Type commandInputType)
        {
            if (!_inputDict.TryGetValue(commandInputType, out var commandFunction))
            {
                throw new ArgumentException("Command input type not recognized.", nameof(commandInputType));
            }
            return commandFunction.Invoke(gameState);
        }

        private static BuildSecretBaseInput? GetBuildSecretBaseInput(GameState gameState)
        {
            SecretBase secretBase = gameState.CurrentPlayer.State.SecretBase;
            decimal buildPrice = SecretBase.SecretBaseBuildPrice;

            if (secretBase != null)
            {
                PrintingUtility.PrintAlreadyHaveASecretBase();
                return null;
            }

            if (buildPrice > gameState.CurrentPlayer.State.Money)
            {
                PrintingUtility.PrintInsufficientFundsForBuildingSecretBase(buildPrice);
                return null;
            }

            string prompt = $"You can build your very own secret base for {buildPrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new BuildSecretBaseInput()
                : null;
        }

        private static HireHenchmenInput? GetHireHenchmenInput(GameState gameState)
        {
            var allowedPositionsToOffer = new IntRange(0, int.MaxValue);
            int openPositionsToOffer = UserInput.GetInteger(PositionsToOfferPrompt, allowedPositionsToOffer);
            if (openPositionsToOffer <= 0)
            {
                PrintingUtility.PrintNoPositionsToOffer();
                return null;
            }
            return UserInput.GetConfirmation($"You will be looking to fill {openPositionsToOffer:N0} positions. Continue?")
                ? new HireHenchmenInput(openPositionsToOffer)
                : null;
        }

        private static PurchaseUnclaimedLandInput? GetPurchaseUnclaimedLandInput(GameState gameState)
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

        private static ResignInput? GetResignInput(GameState gameState)
        {
            return UserInput.GetConfirmation("Are you really sure you want to resign?")
                ? new ResignInput()
                : null;
        }

        private static SellLandInput? GetSellLandInput(GameState gameState)
        {
            if (gameState.CurrentPlayer.State.Land <= 0)
            {
                PrintingUtility.PrintNoLandToSell();
                return null;
            }

            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            var allowedSaleAmounts = new IntRange(0, gameState.CurrentPlayer.State.Land);
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

        private static SkipTurnInput? GetSkipTurnInput(GameState gameState)
        {
            return UserInput.GetConfirmation("Are you really sure you want to skip your turn?")
                ? new SkipTurnInput()
                : null;
        }

        private static StealMoneyInput GetStealMoneyInput(GameState gameState)
        {
            // TODO: Collect more input.
            return new StealMoneyInput();
        }

        private static UpgradeSecretBaseInput? GetUpgradeSecretBaseInput(GameState gameState)
        {
            SecretBase secretBase = gameState.CurrentPlayer.State.SecretBase;

            if (secretBase == null)
            {
                PrintingUtility.PrintDoNotHaveASecretBase();
                return null;
            }

            decimal upgradePrice = SecretBase.CalculateUpgradePrice(secretBase);

            if (upgradePrice > gameState.CurrentPlayer.State.Money)
            {
                PrintingUtility.PrintInsufficientFundsForBuildingSecretBase(upgradePrice);
                return null;
            }

            string prompt = $"You can upgrade your secret base to Level {secretBase.Level + 1:N0} for {upgradePrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new UpgradeSecretBaseInput()
                : null;
        }

        private static int CalculateMaxPurchaseableArea(GameState gameState)
        {
            decimal availableFunds = gameState.CurrentPlayer.State.Money;
            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            return (int)Math.Floor(availableFunds / pricePerSquareKilometer);
        }
    }
}
