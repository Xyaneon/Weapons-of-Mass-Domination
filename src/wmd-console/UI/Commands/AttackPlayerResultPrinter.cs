using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class AttackPlayerResultPrinter : CommandResultPrinter<AttackPlayerResult>
    {
        private const string PrintFormatString = "{0} attacked {1} with {2:N0} henchmen; the former lost {3:N0} henchmen and the latter lost {4:N0} henchmen.";
        private const string PlayerGainedReputationFormatString = "{0} gained {1}% reputation.";
        private const string PlayerLostReputationFormatString = "{0} lost {1}% reputation.";

        public override void PrintCommandResult(AttackPlayerResult result)
        {
            string formattedString = string.Format(
                PrintFormatString,
                RetrieveNameOfPlayerWhoActed(result),
                result.TargetPlayerName,
                result.NumberOfAttackingHenchmen,
                result.HenchmenAttackerLost,
                result.HenchmenDefenderLost
            );
            System.Console.WriteLine(formattedString);

            bool hasReputationChangesToPrint = false;
            if (TryCreateAttackerReputationChangeString(result, out string attackerResultString))
            {
                hasReputationChangesToPrint = true;
                System.Console.Write(attackerResultString);
            }
            if (TryCreateDefenderReputationChangeString(result, out string defenderResultString)) {
                if (hasReputationChangesToPrint)
                {
                    System.Console.Write(" ");
                }
                hasReputationChangesToPrint = true;
                System.Console.Write(defenderResultString);
            }
            if (hasReputationChangesToPrint)
            {
                System.Console.WriteLine();
            }
        }

        private static bool TryCreateAttackerReputationChangeString(AttackPlayerResult result, out string resultString)
        {
            return TryCreateReputationChangeString(result.ReputationChangeForAttacker, RetrieveNameOfPlayerWhoActed(result), out resultString);
        }

        private static bool TryCreateDefenderReputationChangeString(AttackPlayerResult result, out string resultString)
        {
            return TryCreateReputationChangeString(result.ReputationChangeForDefender, result.TargetPlayerName, out resultString);
        }

        private static bool TryCreateReputationChangeString(int reputationChangeAmount, string playerName, out string resultString)
        {
            resultString = reputationChangeAmount switch
            {
                _ when reputationChangeAmount > 0 => string.Format(PlayerGainedReputationFormatString, playerName, reputationChangeAmount),
                _ when reputationChangeAmount < 0 => string.Format(PlayerLostReputationFormatString, playerName, -1 * reputationChangeAmount),
                _ => "",
            };
            return !string.IsNullOrWhiteSpace(resultString);
        }
    }
}
