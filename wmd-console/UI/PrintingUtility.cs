using System;
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
                case PurchaseUnclaimedLandResult result:
                    System.Console.WriteLine($"{result.Player.Name} purchased {result.LandAreaPurchased:N0} km² of land for {result.TotalPurchasePrice:C}.");
                    break;
                case ResignResult result:
                    System.Console.WriteLine($"{result.Player.Name} resigned.");
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

        public static void PrintNoUnclaimedLandLeftToPurchase()
        {
            System.Console.WriteLine("There is no unclaimed land left to purchase.");
        }

        public static void PrintStartOfTurn(GameState gameState)
        {
            Player currentPlayer = gameState.CurrentPlayer;
            string headerText = $"{currentPlayer.Name}'s turn (Day {gameState.CurrentRound})";
            string summaryString = $"{currentPlayer.Name} has {currentPlayer.Money:C} and controls {currentPlayer.Land:N0} km².";
            summaryString += $"\n{gameState.Planet.UnclaimedLandArea:N0} km² of land remains uncontrolled.";

            System.Console.Clear();
            System.Console.WriteLine();
            System.Console.WriteLine(headerText);
            System.Console.WriteLine(new string('=', headerText.Length));

            System.Console.WriteLine();
            System.Console.WriteLine(summaryString);
            System.Console.WriteLine();
        }
    }
}
