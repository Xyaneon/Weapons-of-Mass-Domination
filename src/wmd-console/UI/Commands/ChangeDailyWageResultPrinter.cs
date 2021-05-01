using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class ChangeDailyWageResultPrinter : CommandResultPrinter<ChangeDailyWageResult>
    {
        private const string PrintFormatString = "{0} changed their henchmen's daily pay rate from {1:C} to {2:C}.";

        public override void PrintCommandResult(ChangeDailyWageResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrieveNameOfPlayerWhoActed(result),
                result.OldDailyWage,
                result.NewDailyWage
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
