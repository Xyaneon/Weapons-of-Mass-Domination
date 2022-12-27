using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

interface ICommandResultPrinter
{
    public void PrintCommandResult(CommandResult result);
}
