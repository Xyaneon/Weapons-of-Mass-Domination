using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class SkipTurnResultPrinter : CommandResultPrinter<SkipTurnResult>
    {
        private const string PrintFormatString = "{0} skipped their turn and wasted a whole day.";

        public override void PrintCommandResult(SkipTurnResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
