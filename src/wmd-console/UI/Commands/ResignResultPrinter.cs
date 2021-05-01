using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class ResignResultPrinter : CommandResultPrinter<ResignResult>
    {
        private const string PrintFormatString = "{0} resigned.";

        public override void PrintCommandResult(ResignResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrieveNameOfPlayerWhoActed(result)
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
