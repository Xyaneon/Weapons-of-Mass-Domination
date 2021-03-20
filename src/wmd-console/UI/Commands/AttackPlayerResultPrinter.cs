using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class AttackPlayerResultPrinter : CommandResultPrinter
    {
        private const string PrintFormatString = "{0} attacked {1}; the former lost {2:N0} henchmen and the latter lost {3:N0} henchmen.";

        public override void PrintCommandResult(CommandResult result)
        {
            AttackPlayerResult? typedResult = result as AttackPlayerResult;
            if (typedResult == null)
            {
                throw new ArgumentException($"The supplied {typeof(CommandResult).Name} is not an instance of the {typeof(AttackPlayerResult).Name} class.", nameof(result));
            }

            PrintCommandResult(typedResult);
        }

        private void PrintCommandResult(AttackPlayerResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrievePlayerWhoActed(result).Identification.Name,
                result.TargetPlayerName,
                result.HenchmenAttackerLost,
                result.HenchmenDefenderLost
            );
            System.Console.WriteLine(formattedString);
        }
    }
}
