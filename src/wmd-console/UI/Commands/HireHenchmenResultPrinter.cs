using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class HireHenchmenResultPrinter : CommandResultPrinter<HireHenchmenResult>
{
    private const string PrintFormatString = "{0} managed to hire {1:N0} new henchmen.";

    public override void PrintCommandResult(HireHenchmenResult result)
    {
        string formattedString = string.Format(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.HenchmenHired
        );
        System.Console.WriteLine(formattedString);
    }
}
