using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class StealMoneyResultPrinter : CommandResultPrinter<StealMoneyResult>
    {
        private const string PrintFormatString = "{0} stole {1:C}. They now have {2:C}.";

        public override void PrintCommandResult(StealMoneyResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.StolenAmount,
                RetrievePlayerWhoActed(result).State.Money
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
