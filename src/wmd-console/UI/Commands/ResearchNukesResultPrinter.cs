using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class ResearchNukesResultPrinter : CommandResultPrinter<ResearchNukesResult>
{
    private const string PrintFormatString = "{0} advanced their nukes research to Level {1:N0} for {2:C}.";

    public override void PrintCommandResult(ResearchNukesResult result)
    {
        string formattedString = string.Format(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.NewNukesResearchLevel,
            result.TotalResearchPrice
        );
        System.Console.WriteLine(formattedString);
    }
}
