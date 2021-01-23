using WMD.Console.UI;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Updates.Rounds;

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
                winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Identification.Name;
                PrintingUtility.PrintGameHasAlreadyBeenWon(winningPlayerName);
                return;
            }

            while (!CurrentGameState.GameHasBeenWon(out winningPlayerIndex))
            {
                RunTurn();
                if (!CurrentGameState.GameHasBeenWon(out _))
                {
                    GameState gameState = CurrentGameState;
                    (GameState GameState, RoundUpdateResult? RoundUpdateResult) resultTuple = GameStateTurnAdvancer.AdvanceToNextTurn(gameState);
                    CurrentGameState = resultTuple.GameState;

                    if (resultTuple.RoundUpdateResult != null)
                    {
                        PrintingUtility.PrintEndOfRound(resultTuple.RoundUpdateResult);
                        UserInput.WaitForPlayerAcknowledgementOfRoundEnd();
                    }
                }
            }

            winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Identification.Name;
            PrintingUtility.CongratulateWinningPlayer(winningPlayerName);
        }

        private void RunTurn()
        {
            StartOfTurnPrinter.PrintStartOfTurn(CurrentGameState);

            if (CurrentGameState.CurrentPlayer.State.HasResigned)
            {
                PrintingUtility.PrintCurrentPlayerHasResignedAndCannotTakeTurn(CurrentGameState.CurrentPlayer.Identification.Name);
            }
            else
            {
                CommandResult? commandResult = null;
                while (commandResult == null)
                {
                    var command = UserInput.GetCommand(CurrentGameState);
                    commandResult = CommandRunner.RunSelectedCommand(CurrentGameState, command);
                }
                CurrentGameState = commandResult.UpdatedGameState;
                CommandResultPrinter.PrintCommandResult(commandResult);
                PrintingUtility.PrintEndOfTurn();
                UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            }
        }
    }
}
