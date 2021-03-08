using System;
using WMD.Console.UI.Commands;
using WMD.Game.Commands;
using WMD.Game.State.Data;

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
            CommandInput? retrievedInput = CommandInputRetrieverFactory.CreateICommandInputRetriever(inputType).GetCommandInput(gameState);

            if (retrievedInput == null)
            {
                return null;
            }

            return (CommandResult)command.Execute(gameState, retrievedInput);
        }
    }
}
