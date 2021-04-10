using WMD.Console.UI;
using WMD.Console.UI.Commands;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console
{
    sealed class CpuTurnRunner : PlayerTurnRunner
    {
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

        private static CommandResult SelectCommandAndRetrieveResult(GameState gameState)
        {
            (IGameCommand Command, object Input) = ChooseCommandAndInputForGameState(gameState);
            return RunSelectedCommandWithInput(gameState, Command, Input);
        }

        private static (IGameCommand, object) ChooseCommandAndInputForGameState(GameState gameState) => (new SkipTurnCommand(), new SkipTurnInput());

        private static CommandResult RunSelectedCommandWithInput(GameState gameState, IGameCommand command, object commandInput) =>
            (CommandResult)command.Execute(gameState, commandInput);
    }
}
