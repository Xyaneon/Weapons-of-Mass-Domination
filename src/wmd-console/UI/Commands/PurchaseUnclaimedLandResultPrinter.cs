using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class PurchaseUnclaimedLandResultPrinter : CommandResultPrinter<PurchaseUnclaimedLandResult>
    {
        private const string PrintFormatString = "{0} purchased {1:N0} km² of land for {2:C}.";

        public override void PrintCommandResult(PurchaseUnclaimedLandResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrieveNameOfPlayerWhoActed(result),
                result.LandAreaPurchased,
                result.TotalPurchasePrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
