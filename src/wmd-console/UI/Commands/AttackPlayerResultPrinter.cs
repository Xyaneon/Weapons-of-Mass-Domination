﻿using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class AttackPlayerResultPrinter : CommandResultPrinter<AttackPlayerResult>
    {
        private const string PrintFormatString = "{0} attacked {1}; the former lost {2:N0} henchmen and the latter lost {3:N0} henchmen.";

        public override void PrintCommandResult(AttackPlayerResult result)
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