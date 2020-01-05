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
            if (actionResult.GetType() == typeof(StealMoneyResult))
            {
                var stealMoneyResult = (StealMoneyResult)actionResult;
                System.Console.WriteLine($"{stealMoneyResult.Player.Name} stole {stealMoneyResult.StolenAmount:C}. They now have {stealMoneyResult.Player.Money:C}.");
            }
            else if (actionResult.GetType() == typeof(SkipTurnResult))
            {
                var skipTurnResult = (SkipTurnResult)actionResult;
                System.Console.WriteLine($"{skipTurnResult.Player.Name} skipped their turn and wasted a whole day.");
            }
            else
            {
                System.Console.WriteLine($"ERROR: Unsupported ActionResult type: {actionResult.GetType().FullName}");
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

            Func<GameState, ActionResult> menuChoice = GetPlayerAction();
            ActionResult actionResult = menuChoice(CurrentGameState);
            PrintActionResult(actionResult);
        }
    }
}
