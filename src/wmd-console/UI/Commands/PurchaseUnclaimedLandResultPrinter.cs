using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class PurchaseUnclaimedLandResultPrinter : CommandResultPrinter<PurchaseUnclaimedLandResult>
    {
        private const string PrintFormatString = "{0} purchased {1:N0} km² of land for {2:C}.";

        public override void PrintCommandResult(CommandResult result)
        {
            PurchaseUnclaimedLandResult? typedResult = result as PurchaseUnclaimedLandResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(PurchaseUnclaimedLandResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        public override void PrintCommandResult(PurchaseUnclaimedLandResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.LandAreaPurchased,
                result.TotalPurchasePrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
