using System;
using WMD.Game.Commands;
using WMD.Game.State.Data.Players;

namespace WMD.Console.UI.Commands
{
    abstract class CommandResultPrinter : ICommandResultPrinter
    {
        public abstract void PrintCommandResult(CommandResult result);

        protected static Player RetrievePlayerWhoActed(CommandResult result)
        {
            return result.UpdatedGameState.Players[result.PlayerIndex];
        }

        protected static void ThrowIfCommandResultArgumentIsNotOfSubclass(CommandResult result, string nameOfResultArgument, Type commandResultSubclassType)
        {
            var commandResultType = typeof(CommandResult);

            if (!commandResultSubclassType.IsSubclassOf(commandResultType))
            {
                throw new ArgumentOutOfRangeException(nameof(commandResultSubclassType), $"{commandResultSubclassType.Name} does not derive from the {commandResultType.Name} class.");
            }

            if (!result.GetType().IsSubclassOf(commandResultType))
            {
                throw new ArgumentException($"The supplied {commandResultType.Name} is not an instance of the {commandResultSubclassType.Name} subclass.", nameOfResultArgument);
            }
        }
    }
}
