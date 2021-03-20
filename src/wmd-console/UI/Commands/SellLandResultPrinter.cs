using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class SellLandResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} sold {1:N0} km² of land for {2:C}.";

        public override void PrintCommandResult(CommandResult result)
        {
            SellLandResult? typedResult = result as SellLandResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(SellLandResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(SellLandResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.LandAreaSold,
                result.TotalSalePrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
