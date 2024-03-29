﻿using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class AttackPlayerInputRetriever : ICommandInputRetriever
{
    private const string HenchmenToAttackWithPromptFormatString = "Please enter how many henchmen you would like to attack with (between 1 and {0:N0})";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        long maximumAllowedHenchmen = gameState.CurrentPlayer.State.WorkforceState.TotalHenchmenCount;
        if (maximumAllowedHenchmen == 0)
        {
            return null;
        }

        var targetPlayerIndex = UserInput.GetAttackTargetPlayerIndex(gameState);
        if (!targetPlayerIndex.HasValue)
        {
            return null;
        }

        var allowedHenchmenToUse = new LongRange(1, maximumAllowedHenchmen);
        var henchmenToUse = UserInput.GetLong(string.Format(HenchmenToAttackWithPromptFormatString, maximumAllowedHenchmen), allowedHenchmenToUse);
        if (henchmenToUse <= 0)
        {
            PrintingUtility.PrintNoHenchmenSentToAttack();
            return null;
        }

        return new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex.Value, NumberOfAttackingHenchmen = henchmenToUse };
    }
}
