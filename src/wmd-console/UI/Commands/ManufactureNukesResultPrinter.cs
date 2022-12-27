using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class ManufactureNukesResultPrinter : CommandResultPrinter<ManufactureNukesResult>
{
    private const string PrintFormatString = "{0} manufactured {1:N0} nukes.";

    public override void PrintCommandResult(ManufactureNukesResult result)
    {
        string formattedString = string.Format(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.NukesManufactured
        );
        System.Console.WriteLine(formattedString);
    }
}
