using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class ResearchNukesResultPrinter : CommandResultPrinter<ResearchNukesResult>
    {
        private const string PrintFormatString = "{0} advanced their nukes research to Level {1:N0} for {2:C}.";

        public override void PrintCommandResult(CommandResult result)
        {
            ResearchNukesResult? typedResult = result as ResearchNukesResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(ResearchNukesResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        public override void PrintCommandResult(ResearchNukesResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.NewNukesResearchLevel,
                result.TotalResearchPrice
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
