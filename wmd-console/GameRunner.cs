using System;
using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console
{
    class GameRunner
    {
        public GameRunner() { }

        public GameState? CurrentGameState { get; set; }

        public void Run()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            int winningPlayerIndex = -1;
            string winningPlayerName;

            if (CurrentGameState.GameHasBeenWon(out winningPlayerIndex))
            {
                winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Name;
                System.Console.WriteLine($"This game was already won by {winningPlayerName}.");
                return;
            }

            while (!CurrentGameState.GameHasBeenWon(out winningPlayerIndex))
            {
                RunTurn();
                if (!CurrentGameState.GameHasBeenWon(out _))
                {
                    CurrentGameState.AdvanceToNextTurn();
                }
            }

            winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Name;
            CongratulateWinningPlayer(winningPlayerName);
        }

        private void CongratulateWinningPlayer(string winningPlayerName)
        {
            System.Console.WriteLine($"Congratulations, {winningPlayerName}! You won!");
        }

        private Func<GameState, ActionResult> GetPlayerAction()
        {
            var menuPrinter = new MenuPrinter<Func<GameState, ActionResult>>();
            var userInput = new UserInput();
            var menuRunner = new MenuRunner<Func<GameState, ActionResult>>(menuPrinter, userInput);

            var menu = new PlayerActionMenu();
            return menuRunner.ShowMenuAndGetChoice(menu);
        }

        private static void PrintActionResult(ActionResult actionResult)
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

        private static void PrintStartOfTurn(GameState gameState)
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

        private void RunTurn()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            PrintStartOfTurn(CurrentGameState);

            if (CurrentGameState.CurrentPlayer.HasResigned)
            {
                System.Console.WriteLine($"{CurrentGameState.CurrentPlayer.Name} resigned and cannot take any more actions.");
                System.Console.WriteLine();
            }
            else
            {
                Func<GameState, ActionResult> menuChoice = GetPlayerAction();
                ActionResult actionResult = menuChoice(CurrentGameState);
                PrintActionResult(actionResult);
            }
        }
    }
}
