using System;
using WMD.Console.UI;
using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;
using WMD.Game;
using WMD.Game.Actions;
using WMD.Game.Rounds;

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

            string winningPlayerName;


            int winningPlayerIndex;
            if (CurrentGameState.GameHasBeenWon(out winningPlayerIndex))
            {
                winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Name;
                PrintingUtility.PrintGameHasAlreadyBeenWon(winningPlayerName);
                return;
            }

            while (!CurrentGameState.GameHasBeenWon(out winningPlayerIndex))
            {
                RunTurn();
                if (!CurrentGameState.GameHasBeenWon(out _))
                {
                    RoundUpdateResult? roundUpdate = GameStateUpdater.AdvanceToNextTurn(CurrentGameState);

                    if (roundUpdate != null)
                    {
                        PrintingUtility.PrintEndOfRound(roundUpdate);
                        UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
                    }
                }
            }

            winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Name;
            PrintingUtility.CongratulateWinningPlayer(winningPlayerName);
        }

        private PlayerActionKind GetPlayerActionKind()
        {
            var menuPrinter = new MenuPrinter<PlayerActionKind>();
            var menuRunner = new MenuRunner<PlayerActionKind>(menuPrinter);

            var menu = GameMenuFactory.CreatePlayerActionMenu();
            return menuRunner.ShowMenuAndGetChoice(menu);
        }

        private void RunTurn()
        {
            if (CurrentGameState == null)
            {
                throw new InvalidOperationException("No game state set to run on.");
            }

            PrintingUtility.PrintStartOfTurn(CurrentGameState);

            if (CurrentGameState.CurrentPlayer.State.HasResigned)
            {
                PrintingUtility.PrintCurrentPlayerHasResignedAndCannotTakeTurn(CurrentGameState.CurrentPlayer.Name);
            }
            else
            {
                ActionResult? actionResult = null;
                while (actionResult == null)
                {
                    var selectedAction = GetPlayerActionKind();
                    actionResult = PlayerActionRunner.RunSelectedAction(CurrentGameState, selectedAction);
                }
                PrintingUtility.PrintActionResult(actionResult);
                PrintingUtility.PrintEndOfTurn();
                UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            }
        }
    }
}
