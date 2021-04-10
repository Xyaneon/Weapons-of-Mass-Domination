using WMD.Console.UI;
using WMD.Console.UI.Commands;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console
{
    sealed class HumanTurnRunner : PlayerTurnRunner
    {
        public override GameState RunTurn(GameState gameState)
        {
            StartOfTurnPrinter.PrintStartOfTurn(gameState);

            if (gameState.CurrentPlayer.State.HasResigned)
            {
                PrintingUtility.PrintCurrentPlayerHasResignedAndCannotTakeTurn(gameState.CurrentPlayer.Identification.Name);
                return gameState;
            }
            else
            {
                CommandResult? commandResult = null;
                while (commandResult == null)
                {
                    var command = UserInput.GetCommand(gameState);
                    commandResult = CommandRunner.RunSelectedCommand(gameState, command);
                }
                GameState updatedGameState = commandResult.UpdatedGameState;
                CommandResultPrinterFactory.CreateICommandResultPrinter(commandResult.GetType()).PrintCommandResult(commandResult);
                PrintingUtility.PrintEndOfTurn();
                UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
                return updatedGameState;
            }
        }
    }
}
