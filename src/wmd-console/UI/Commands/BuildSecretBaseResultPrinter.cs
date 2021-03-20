using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class BuildSecretBaseResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} built their own secret base for {1:C}.";

        public override void PrintCommandResult(CommandResult result)
        {
            BuildSecretBaseResult? typedResult = result as BuildSecretBaseResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(BuildSecretBaseResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(BuildSecretBaseResult result)
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
