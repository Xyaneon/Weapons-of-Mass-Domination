using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class TrainHenchmenInputRetriever : ICommandInputRetriever
{
    private const string HenchmenToTrainPromptFormatString = "Please enter how many henchmen you would like to train (between 1 and {0:N0})";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        long maximumAllowedTrainees = gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount;
        if (maximumAllowedTrainees <= 0)
        {
            return null;
        }

        var specialization = UserInput.GetTrainingSpecialization(gameState);
        if (!specialization.HasValue)
        {
            return null;
        }

        var allowedHenchmenToTrain = new LongRange(1, maximumAllowedTrainees);
        var henchmenToTrain = UserInput.GetLong(string.Format(HenchmenToTrainPromptFormatString, maximumAllowedTrainees), allowedHenchmenToTrain);
        if (henchmenToTrain <= 0)
        {
            PrintingUtility.PrintNoHenchmenTrained();
            return null;
        }

        return new TrainHenchmenInput { NumberToTrain = henchmenToTrain, Specialization = specialization.Value };
    }
}
