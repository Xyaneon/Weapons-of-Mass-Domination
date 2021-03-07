using System;
using System.Collections.Generic;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Utility;

namespace WMD.Console.UI
{
    static class CommandInputRetrieval
    {
        private const string ArgumentException_commandInputTypeNotRecognized = "Command input type not recognized.";

        private const string PositionsToOfferPrompt = "Please enter how many open positions you would like to offer";
        private const string NukesToLaunchPrompt = "Please enter how many nukes you would like to launch";
        private const string NukesToManufacturePrompt = "Please enter how many nukes you would like to manufacture";
        private const string UnclaimedLandPurchasePrompt = "Please enter how many square kilometers of land you would like to purchase";

        static CommandInputRetrieval()
        {
            _inputDict = new Dictionary<Type, Func<GameState, CommandInput?>>
            {
                { typeof(AttackPlayerInput), GetAttackPlayerInput },
                { typeof(BuildSecretBaseInput), GetBuildSecretBaseInput },
                { typeof(HireHenchmenInput), GetHireHenchmenInput },
                { typeof(LaunchNukesInput), GetLaunchNukesInput },
                { typeof(ManufactureNukesInput), GetManufactureNukesInput },
                { typeof(PurchaseUnclaimedLandInput), GetPurchaseUnclaimedLandInput },
                { typeof(ResearchNukesInput), GetResearchNukesInput },
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
                throw new ArgumentException(ArgumentException_commandInputTypeNotRecognized, nameof(commandInputType));
            }
            return commandFunction.Invoke(gameState);
        }

        private static AttackPlayerInput? GetAttackPlayerInput(GameState gameState)
        {
            int? targetPlayerIndex = UserInput.GetAttackTargetPlayerIndex(gameState);
            return targetPlayerIndex.HasValue
                ? new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex.Value }
                : null;
        }

        private static BuildSecretBaseInput? GetBuildSecretBaseInput(GameState gameState)
        {
            decimal buildPrice = SecretBase.SecretBaseBuildPrice;

            if (GameStateChecks.CurrentPlayerHasASecretBase(gameState))
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
                ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer }
                : null;
        }

        private static LaunchNukesInput? GetLaunchNukesInput(GameState gameState)
        {
            if (!GameStateChecks.CurrentPlayerHasAnyNukes(gameState))
            {
                PrintingUtility.PrintHasNoNukesToLaunch();
                return null;
            }

            if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                PrintingUtility.PrintHasNoSecretBaseToLaunchNukesFrom();
                return null;
            }

            int? targetPlayerIndex = UserInput.GetAttackTargetPlayerIndex(gameState);

            var allowedAmounts = new IntRange(0, gameState.CurrentPlayer.State.Nukes);

            string prompt = $"{NukesToLaunchPrompt} ({allowedAmounts.Minimum} to {allowedAmounts.Maximum})";
            int nukesToLaunch = UserInput.GetInteger(prompt, allowedAmounts);

            if (nukesToLaunch <= 0)
            {
                PrintingUtility.PrintChoseNoNukesToLaunch();
                return null;
            }

            return targetPlayerIndex.HasValue
                ? new LaunchNukesInput() { TargetPlayerIndex = targetPlayerIndex.Value, NumberOfNukesLaunched = nukesToLaunch }
                : null;
        }

        private static ManufactureNukesInput? GetManufactureNukesInput(GameState gameState)
        {
            if (!GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
            {
                PrintingUtility.PrintNukesResearchNotCompleted();
                return null;
            }

            var maximumAllowedNukeQuantity = NukesCalculator.CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture(gameState);
            var allowedAmounts = new IntRange(0, maximumAllowedNukeQuantity);

            string prompt = $"{NukesToManufacturePrompt} ({allowedAmounts.Minimum} to {allowedAmounts.Maximum})";
            int nukesToManufacture = UserInput.GetInteger(prompt, allowedAmounts);

            if (nukesToManufacture <= 0)
            {
                PrintingUtility.PrintNoNukesToManufacture();
                return null;
            }

            decimal manufacturingPrice = NukesCalculator.CalculateTotalManufacturingPrice(gameState, nukesToManufacture);

            return UserInput.GetConfirmation($"You will manufacture {nukesToManufacture:N0} new nukes for {manufacturingPrice:C}. Continue?")
                ? new ManufactureNukesInput() with { NumberOfNukesToManufacture = nukesToManufacture }
                : null;
        }

        private static PurchaseUnclaimedLandInput? GetPurchaseUnclaimedLandInput(GameState gameState)
        {
            if (gameState.Planet.UnclaimedLandArea < 1)
            {
                PrintingUtility.PrintNoUnclaimedLandLeftToPurchase();
                return null;
            }

            PrintingUtility.PrintCurrentUnclaimedLandAreaAndPrice(gameState);

            if (GameStateChecks.CurrentPlayerCouldPurchaseLand(gameState))
            {
                PrintingUtility.PrintInsufficientFundsForAnyLandPurchase();
                return null;
            }

            int maxPurchaseableArea = LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState);
            var allowedPurchaseAmounts = new IntRange(0, maxPurchaseableArea);
            string prompt = $"{UnclaimedLandPurchasePrompt} ({allowedPurchaseAmounts.Minimum} to {allowedPurchaseAmounts.Maximum})";
            int areaToPurchase = UserInput.GetInteger(prompt, allowedPurchaseAmounts);
            if (areaToPurchase < 1)
            {
                return null;
            }

            decimal totalPurchasePrice = LandAreaCalculator.CalculateTotalPurchasePrice(gameState, areaToPurchase);
            string confirmationPrompt = $"This transaction will cost you {totalPurchasePrice:C}. Proceed?";
            return UserInput.GetConfirmation(confirmationPrompt)
                ? new PurchaseUnclaimedLandInput() with { AreaToPurchase = areaToPurchase }
                : null;
        }

        private static ResearchNukesInput? GetResearchNukesInput(GameState gameState)
        {
            int currentResearchLevel = gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel;

            if (GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
            {
                PrintingUtility.PrintNukesResearchAlreadyMaxedOut();
                return null;
            }

            if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                PrintingUtility.PrintDoNotHaveASecretBase();
                return null;
            }

            decimal researchPrice = NukeConstants.NukeResearchLevelCost;

            if (researchPrice > gameState.CurrentPlayer.State.Money)
            {
                PrintingUtility.PrintInsufficientFundsForResearchingNukes(researchPrice);
                return null;
            }

            string prompt = $"You can advance your nukes research to Level {currentResearchLevel + 1:N0} for {researchPrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new ResearchNukesInput()
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
                ? new SellLandInput() with { AreaToSell = areaToSell }
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
            SecretBase? secretBase = gameState.CurrentPlayer.State.SecretBase;

            if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
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

            string prompt = $"You can upgrade your secret base to Level {secretBase!.Level + 1:N0} for {upgradePrice:C}. Proceed?";

            return UserInput.GetConfirmation(prompt)
                ? new UpgradeSecretBaseInput()
                : null;
        }
    }
}
