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
        public GameRunner(GameState initialGameState)
        {
            CurrentGameState = initialGameState;
        }

        public GameState CurrentGameState { get; private set; }

        public void Run()
        {
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
                        UserInput.WaitForPlayerAcknowledgementOfRoundEnd();
                    }
                }
            }

            winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Name;
            PrintingUtility.CongratulateWinningPlayer(winningPlayerName);
        }

        private PlayerActionKind GetPlayerActionKind()
        {
            Menu actionMenu = GameMenuFactory.CreatePlayerActionMenu();
            actionMenu.Run();
            if (actionMenu.Result != null)
            {
                return (PlayerActionKind)actionMenu.Result;
            }
            else
            {
                throw new InvalidOperationException($"No {typeof(PlayerActionKind).Name} result value found on action selection menu (this is a bug).");
            }
        }

        private void RunTurn()
        {
            StartOfTurnPrinter.PrintStartOfTurn(CurrentGameState);

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
                ActionResultPrinter.PrintActionResult(actionResult);
                PrintingUtility.PrintEndOfTurn();
                UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            }
        }
    }
}
