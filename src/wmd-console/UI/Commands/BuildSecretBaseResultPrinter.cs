using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class BuildSecretBaseResultPrinter : CommandResultPrinter<BuildSecretBaseResult>
    {
        private const string PrintFormatString = "{0} built their own secret base for {1:C}.";

        public override void PrintCommandResult(BuildSecretBaseResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.BuildPrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
