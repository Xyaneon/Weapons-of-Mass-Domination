using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class AttackGovernmentArmyInputRetriever : ICommandInputRetriever
    {
        private const string HenchmenToAttackWithPromptFormatString = "Please enter how many henchmen you would like to attack with (between 1 and {0:N0})";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            long maximumAllowedHenchmen = gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen;
            if (maximumAllowedHenchmen == 0)
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

            return new AttackGovernmentArmyInput() { NumberOfAttackingHenchmen = henchmenToUse };
        }
    }
}
