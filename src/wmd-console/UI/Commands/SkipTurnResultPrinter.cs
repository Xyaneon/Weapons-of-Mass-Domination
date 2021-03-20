using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class SkipTurnResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} skipped their turn and wasted a whole day.";

        public override void PrintCommandResult(CommandResult result)
        {
            SkipTurnResult? typedResult = result as SkipTurnResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(SkipTurnResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(SkipTurnResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
