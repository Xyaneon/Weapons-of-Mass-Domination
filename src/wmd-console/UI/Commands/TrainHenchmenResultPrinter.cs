using System.ComponentModel;
using WMD.Game.Commands;
using WMD.Game.State.Data.Henchmen;

namespace WMD.Console.UI.Commands;

class TrainHenchmenResultPrinter : CommandResultPrinter<TrainHenchmenResult>
{
    private const string PrintFormatString = "{0} trained {1:N0} henchmen in the {2} specialization.";

    public override void PrintCommandResult(TrainHenchmenResult result)
    {
        System.Console.WriteLine(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.HenchmenTrained,
            GetSpecializationText(result.Specialization)
        );
    }

    private static string GetSpecializationText(Specialization specialization) => specialization switch
    {
        Specialization.Untrained => "untrained",
        Specialization.Soldier => "soldier",
        _ => throw new InvalidEnumArgumentException(nameof(specialization), (int)specialization, typeof(Specialization)),
    };
}
