using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class HireHenchmenResultPrinter : CommandResultPrinter<HireHenchmenResult>
    {
        private const string PrintFormatString = "{0} managed to hire {1:N0} new henchmen.";

        public override void PrintCommandResult(CommandResult result)
        {
            HireHenchmenResult? typedResult = result as HireHenchmenResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(HireHenchmenResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        public override void PrintCommandResult(HireHenchmenResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.HenchmenHired
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
