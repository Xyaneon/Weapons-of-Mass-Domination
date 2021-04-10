using System.Collections.Generic;
using System.Linq;
using WMD.Console.UI;
using WMD.Console.UI.Core;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Updates;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Console
{
    class GameRunner
    {
        public GameRunner(GameState initialGameState)
        {
            CurrentGameState = initialGameState;
            TurnRunners = CreateTurnRunners(initialGameState);
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
                CurrentGameState = TurnRunners[CurrentGameState.CurrentPlayerIndex].RunTurn(CurrentGameState);
                if (!CurrentGameState.GameHasBeenWon(out _))
                {
                    GameState gameState = CurrentGameState;
                    (GameState GameState, RoundUpdateResult? RoundUpdateResult) resultTuple = GameStateTurnAdvancer.AdvanceToNextTurn(gameState);
                    CurrentGameState = resultTuple.GameState;

                    if (resultTuple.RoundUpdateResult != null)
                    {
                        EndOfRoundPrinter.PrintEndOfRound(resultTuple.RoundUpdateResult);
                        UserInput.WaitForPlayerAcknowledgementOfRoundEnd();
                    }
                }
            }

            winningPlayerName = CurrentGameState.Players[winningPlayerIndex].Identification.Name;
            PrintingUtility.CongratulateWinningPlayer(winningPlayerName);
        }

        private IReadOnlyList<PlayerTurnRunner> TurnRunners { get; set; }

        private static IReadOnlyList<PlayerTurnRunner> CreateTurnRunners(GameState gameState) =>
            gameState.Players
                .Select(player => CreateTurnRunnerForPlayer(player))
                .ToList()
                .AsReadOnly();

        private static PlayerTurnRunner CreateTurnRunnerForPlayer(Player player) => new HumanTurnRunner();
    }
}
