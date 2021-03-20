using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class DistributePropagandaResultPrinter : CommandResultPrinter<DistributePropagandaResult>
    {
        private const string PrintFormatString = "{0} spent {1:C} and gained {2}% additional reputation.";

        public override void PrintCommandResult(CommandResult result)
        {
            DistributePropagandaResult? typedResult = result as DistributePropagandaResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(DistributePropagandaResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        public override void PrintCommandResult(DistributePropagandaResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.MoneySpent,
                result.ReputationGained
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
