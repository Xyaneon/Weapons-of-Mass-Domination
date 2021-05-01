using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class SellLandResultPrinter : CommandResultPrinter<SellLandResult>
    {
        private const string PrintFormatString = "{0} sold {1:N0} km² of land for {2:C}.";

        public override void PrintCommandResult(SellLandResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrieveNameOfPlayerWhoActed(result),
                result.LandAreaSold,
                result.TotalSalePrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
