using WMD.AI;
using WMD.Console.UI;
using WMD.Console.UI.Commands;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console
{
    sealed class CpuTurnRunner : PlayerTurnRunner
    {
        public CpuTurnRunner(ICpuPlayerAI ai)
        {
            AI = ai;
        }

        public override GameState RunTurn(GameState gameState)
        {
            StartOfTurnPrinter.PrintStartOfTurn(gameState);

            if (gameState.CurrentPlayer.State.HasResigned)
            {
                PrintingUtility.PrintCurrentPlayerHasResignedAndCannotTakeTurn(gameState.CurrentPlayer.Identification.Name);
                return gameState;
            }

            CommandResult commandResult = SelectCommandAndRetrieveResult(gameState);
            GameState updatedGameState = commandResult.UpdatedGameState;
            CommandResultPrinterFactory.CreateICommandResultPrinter(commandResult.GetType()).PrintCommandResult(commandResult);
            PrintingUtility.PrintEndOfTurn();
            UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            return updatedGameState;
        }

        private ICpuPlayerAI AI { get; }

        private CommandResult SelectCommandAndRetrieveResult(GameState gameState)
        {
            AICommandSelection aiSelection = AI.ChooseCommandAndInputForGameState(gameState);
            return (CommandResult)aiSelection.Command.Execute(gameState, aiSelection.Input);
        }
    }
}
