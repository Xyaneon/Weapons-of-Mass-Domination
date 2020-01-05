using System;
using System.Collections.Generic;
using System.Text;
using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;
using WMD.Game;

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

        private void CurrentPlayerSkipsTurn()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            System.Console.WriteLine($"{CurrentGameState.CurrentPlayer.Name} skipped their turn and wasted a whole day.");
        }

        private void CurrentPlayerStealsMoney()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            decimal moneyStolen = 200;
            CurrentGameState.CurrentPlayer.Money += moneyStolen;

            System.Console.WriteLine($"{CurrentGameState.CurrentPlayer.Name} stole {moneyStolen:C}. They now have {CurrentGameState.CurrentPlayer.Money:C}.");
        }

        private int GetPlayerAction()
        {
            MenuPrinter menuPrinter = new MenuPrinter();
            UserInput userInput = new UserInput();
            MenuRunner menuRunner = new MenuRunner(menuPrinter, userInput);

            var menu = new PlayerActionMenu();
            return menuRunner.ShowMenuAndGetChoice(menu);
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
        }

        private void RunTurn()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            PrintStartOfTurn(CurrentGameState);

            int menuChoice = GetPlayerAction();
            switch (menuChoice)
            {
                case 1:
                    CurrentPlayerStealsMoney();
                    break;
                case 2:
                    CurrentPlayerSkipsTurn();
                    break;
            }
        }
    }
}
