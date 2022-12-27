using System;
using WMD.Game.Commands;
using WMD.Game.State.Data.Players;

namespace WMD.Console.UI.Commands;

abstract class CommandResultPrinter<TCommandResult> : ICommandResultPrinter where TCommandResult : CommandResult
{
    private const string ArgumentOutOfRangeException_commandResultTypeNotSubclassOfCommandResult_formatString = "{0} does not derive from the {1} class.";
    private const string ArgumentOutOfRangeException_commandResultNotAnInstanceOfCommandResultSubclass_formatString = "The supplied {0} is not an instance of the {1} subclass.";

    public void PrintCommandResult(CommandResult result)
    {
        ThrowIfCommandResultArgumentIsNotOfSubclass(result, nameof(result), typeof(TCommandResult));
        PrintCommandResult((TCommandResult)result);
    }

    public abstract void PrintCommandResult(TCommandResult result);

    protected static string RetrieveNameOfPlayerWhoActed(CommandResult result) => RetrievePlayerWhoActed(result).Identification.Name;

    protected static Player RetrievePlayerWhoActed(CommandResult result) => result.UpdatedGameState.Players[result.PlayerIndex];

    private static void ThrowIfCommandResultArgumentIsNotOfSubclass(CommandResult result, string nameOfResultArgument, Type commandResultSubclassType)
    {
        var commandResultType = typeof(CommandResult);

        if (!commandResultSubclassType.IsSubclassOf(commandResultType))
        {
            string errorMessage = createCommandResultTypeNotSubclassOfCommandResultFormattedString(commandResultSubclassType, commandResultType);
            throw new ArgumentOutOfRangeException(nameof(commandResultSubclassType), errorMessage);
        }

        if (!result.GetType().IsSubclassOf(commandResultType))
        {
            string errorMessage = createCommandResultNotAnInstanceOfCommandResultSubclassFormattedString(commandResultType, commandResultSubclassType);
            throw new ArgumentException(errorMessage, nameOfResultArgument);
        }
    }

    private static string createCommandResultTypeNotSubclassOfCommandResultFormattedString(Type commandResultSubclassType, Type commandResultType) => string.Format(
        ArgumentOutOfRangeException_commandResultTypeNotSubclassOfCommandResult_formatString,
        commandResultSubclassType.Name,
        commandResultType.Name
    );

    private static string createCommandResultNotAnInstanceOfCommandResultSubclassFormattedString(Type commandResultType, Type commandResultSubclassType) => string.Format(
        ArgumentOutOfRangeException_commandResultNotAnInstanceOfCommandResultSubclass_formatString,
        commandResultType.Name,
        commandResultSubclassType.Name
    );
}
