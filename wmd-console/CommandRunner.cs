using System;
using WMD.Console.UI;
using WMD.Game;
using WMD.Game.Commands;

namespace WMD.Console
{
    static class CommandRunner
    {
        public static CommandResult? RunSelectedAction(GameState gameState, IGameCommand<CommandInput, CommandResult> command)
        {
            Type inputType = command.GetType().GenericTypeArguments[0];
            CommandInput? retrievedInput = CommandInputRetrieval.GetCommandInput(gameState, inputType);

            if (retrievedInput == null)
            {
                return null;
            }

            return command.Execute(gameState, retrievedInput);
        }
    }
}
