using System;
using WMD.Console.UI;
using WMD.Game;
using WMD.Game.Commands;

namespace WMD.Console
{
    static class CommandRunner
    {
        public static CommandResult? RunSelectedCommand(GameState gameState, IGameCommand command)
        {
            Type? baseCommandType = command.GetType().BaseType;
            if (baseCommandType == null)
            {
                throw new ArgumentException($"The supplied command does not inherit from {typeof(GameCommand<,>).Name}.", nameof(command));
            }
            Type inputType = baseCommandType.GenericTypeArguments[0];
            CommandInput? retrievedInput = CommandInputRetrieval.GetCommandInput(gameState, inputType);

            if (retrievedInput == null)
            {
                return null;
            }

            return (CommandResult)command.Execute(gameState, retrievedInput);
        }
    }
}
