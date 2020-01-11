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
                case StealMoneyResult result:
                    System.Console.WriteLine($"{result.Player.Name} stole {result.StolenAmount:C}. They now have {result.Player.Money:C}.");
                    break;
                case SkipTurnResult result:
                    System.Console.WriteLine($"{result.Player.Name} skipped their turn and wasted a whole day.");
                    break;
                case ResignResult result:
                    System.Console.WriteLine($"{result.Player.Name} resigned.");
                    break;
                default:
                    System.Console.WriteLine($"ERROR: Unsupported ActionResult type: {actionResult.GetType().FullName}");
                    break;
            }
            System.Console.WriteLine();
        }

        public static void PrintCurrentPlayerHasResignedAndCannotTakeTurn(string playerName)
        {
            System.Console.WriteLine($"{playerName} resigned and cannot take any more actions.");
            System.Console.WriteLine();
        }

        public static void PrintGameHasAlreadyBeenWon(string playerName)
        {
            System.Console.WriteLine($"This game was already won by {playerName}.");
        }

        public static void PrintStartOfTurn(GameState gameState)
        {
            Player currentPlayer = gameState.CurrentPlayer;
            string headerText = $"{currentPlayer.Name}'s turn (Day {gameState.CurrentRound})";
            string summaryString = string.Format("{0} has {1:C}.", currentPlayer.Name, currentPlayer.Money);

            System.Console.WriteLine();
            System.Console.WriteLine(headerText);
            System.Console.WriteLine(new string('=', headerText.Length));

            System.Console.WriteLine();
            System.Console.WriteLine(summaryString);
            System.Console.WriteLine();
        }
    }
}
