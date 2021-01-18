using WMD.Console.UI;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Commands;
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

        private void RunTurn()
        {
            StartOfTurnPrinter.PrintStartOfTurn(CurrentGameState);

            if (CurrentGameState.CurrentPlayer.State.HasResigned)
            {
                PrintingUtility.PrintCurrentPlayerHasResignedAndCannotTakeTurn(CurrentGameState.CurrentPlayer.Name);
            }
            else
            {
                CommandResult? commandResult = null;
                while (commandResult == null)
                {
                    var command = UserInput.GetCommand(CurrentGameState);
                    commandResult = CommandRunner.RunSelectedCommand(CurrentGameState, command);
                }
                CommandResultPrinter.PrintCommandResult(commandResult);
                PrintingUtility.PrintEndOfTurn();
                UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            }
        }
    }
}
