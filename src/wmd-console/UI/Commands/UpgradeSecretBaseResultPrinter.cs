using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class UpgradeSecretBaseResultPrinter : CommandResultPrinter<UpgradeSecretBaseResult>
    {
        private const string PrintFormatString = "{0} upgraded their secret base to Level {1:N0} for {2:C}.";

        public override void PrintCommandResult(UpgradeSecretBaseResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.NewLevel,
                result.UpgradePrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
