using System;
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

            CommandResult? commandResult = null;
            while (commandResult == null)
            {
                var command = UserInput.GetCommand(gameState);
                commandResult = RunSelectedCommand(gameState, command);
            }
            GameState updatedGameState = commandResult.UpdatedGameState;
            CommandResultPrinterFactory.CreateICommandResultPrinter(commandResult.GetType()).PrintCommandResult(commandResult);
            PrintingUtility.PrintEndOfTurn();
            UserInput.WaitForPlayerAcknowledgementOfTurnEnd();
            return updatedGameState;
        }

        private static CommandResult? RunSelectedCommand(GameState gameState, IGameCommand command)
        {
            Type inputType;

            try
            {
                inputType = CommandUtility.GetInputType(command);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, nameof(command), ex);
            }

            CommandInput? retrievedInput = CommandInputRetrieverFactory.CreateICommandInputRetriever(inputType).GetCommandInput(gameState);

            if (retrievedInput == null)
            {
                return null;
            }

            return (CommandResult)command.Execute(gameState, retrievedInput);
        }
    }
}
