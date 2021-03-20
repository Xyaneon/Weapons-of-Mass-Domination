using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class UpgradeSecretBaseResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} upgraded their secret base to Level {1:N0} for {2:C}.";

        public override void PrintCommandResult(CommandResult result)
        {
            UpgradeSecretBaseResult? typedResult = result as UpgradeSecretBaseResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(UpgradeSecretBaseResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(UpgradeSecretBaseResult result)
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
