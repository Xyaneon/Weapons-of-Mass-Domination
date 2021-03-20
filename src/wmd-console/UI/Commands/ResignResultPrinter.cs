using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class ResignResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} resigned.";

        public override void PrintCommandResult(CommandResult result)
        {
            ResignResult? typedResult = result as ResignResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(ResignResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(ResignResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
