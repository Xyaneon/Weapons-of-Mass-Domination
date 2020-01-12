using System;
using WMD.Console.Miscellaneous;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console.UI
{
    static class PrintingUtility
    {
        public static void CongratulateWinningPlayer(string playerName)
        {
            System.Console.WriteLine($"Congratulations, {playerName}! You won!");
        }

        public static void PrintActionResult(ActionResult actionResult)
        {
            switch (actionResult)
            {
                case HireMinionsResult result:
                    System.Console.WriteLine($"{result.Player.Name} managed to hire {result.MinionsHired:N0} new minions.");
                    break;
                case PurchaseUnclaimedLandResult result:
                    System.Console.WriteLine($"{result.Player.Name} purchased {result.LandAreaPurchased:N0} km² of land for {result.TotalPurchasePrice:C}.");
                    break;
                case ResignResult result:
                    System.Console.WriteLine($"{result.Player.Name} resigned.");
                    break;
                case SellLandResult result:
                    System.Console.WriteLine($"{result.Player.Name} sold {result.LandAreaSold:N0} km² of land for {result.TotalSalePrice:C}.");
                    break;
                case SkipTurnResult result:
                    System.Console.WriteLine($"{result.Player.Name} skipped their turn and wasted a whole day.");
                    break;
                case StealMoneyResult result:
                    System.Console.WriteLine($"{result.Player.Name} stole {result.StolenAmount:C}. They now have {result.Player.Money:C}.");
                    break;
                default:
                    throw new ArgumentException($"Unsupported ActionResult type: {actionResult.GetType().FullName}");
            }
            System.Console.WriteLine();
        }

        public static void PrintCurrentPlayerHasResignedAndCannotTakeTurn(string playerName)
        {
            System.Console.WriteLine($"{playerName} resigned and cannot take any more actions.");
            System.Console.WriteLine();
        }

        public static void PrintCurrentUnclaimedLand(GameState gameState)
        {
            decimal pricePerSquareKilometer = gameState.CalculateUnclaimedLandPurchasePrice();
            System.Console.WriteLine($"There are {gameState.Planet.UnclaimedLandArea:N0} km² of unclaimed land left, priced at {pricePerSquareKilometer:C} each.");
        }

        public static void PrintEndOfTurn()
        {
            System.Console.WriteLine("The turn has ended. Press any key to continue...");
        }

        public static void PrintGameHasAlreadyBeenWon(string playerName)
        {
            System.Console.WriteLine($"This game was already won by {playerName}.");
        }

        public static void PrintInsufficientFundsForAnyLandPurchase()
        {
            System.Console.WriteLine("You do not have enough money to purchase any unclaimed land.");
        }

        public static void PrintNoLandToSell()
        {
            System.Console.WriteLine("You don't have any land to sell.");
        }

        public static void PrintNoUnclaimedLandLeftToPurchase()
        {
            System.Console.WriteLine("There is no unclaimed land left to purchase.");
        }

        public static void PrintStartOfTurn(GameState gameState)
        {
            Player currentPlayer = gameState.CurrentPlayer;
            string headerText = $"{currentPlayer.Name}'s turn (Day {gameState.CurrentRound})";
            string statsString = $"Money: {currentPlayer.Money:C} | Minions: {currentPlayer.Minions:N0} | Land: {currentPlayer.Land:N0} km²";
            string landAreaComparisonString = $"You control a land area comparable to {RealWorldComparisons.GetComparableRealWorldLocationByLandAreaInSquareKilometers(currentPlayer.Land)}.";
            string summaryString = $"\n{gameState.Planet.UnclaimedLandArea:N0} km² of land remains uncontrolled ({gameState.Planet.PercentageOfLandStillUnclaimed:P2}).";

            string topLine = "╔" + new string('═', headerText.Length + 2) + "╗";
            string bottomLine = "╚" + new string('═', headerText.Length + 2) + "╝";

            System.Console.Clear();
            System.Console.WriteLine();
            System.Console.WriteLine(topLine);
            System.Console.WriteLine($"║ {headerText} ║");
            System.Console.WriteLine(bottomLine);

            System.Console.WriteLine();
            System.Console.WriteLine(statsString);
            System.Console.WriteLine(landAreaComparisonString);
            System.Console.WriteLine(summaryString);
            System.Console.WriteLine();
        }
    }
}
