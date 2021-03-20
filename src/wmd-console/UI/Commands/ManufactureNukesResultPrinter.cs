using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class ManufactureNukesResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} manufactured {1:N0} nukes.";

        public override void PrintCommandResult(CommandResult result)
        {
            ManufactureNukesResult? typedResult = result as ManufactureNukesResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(ManufactureNukesResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(ManufactureNukesResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.NukesManufactured
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
