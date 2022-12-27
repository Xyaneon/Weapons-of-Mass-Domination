using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class DistributePropagandaResultPrinter : CommandResultPrinter<DistributePropagandaResult>
{
    private const string PrintFormatString = "{0} spent {1:C} and gained {2}% additional reputation.";

    public override void PrintCommandResult(DistributePropagandaResult result)
    {
        string formattedString = string.Format(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.MoneySpent,
            result.ReputationGained
        );
        System.Console.WriteLine(formattedString);
    }
}
