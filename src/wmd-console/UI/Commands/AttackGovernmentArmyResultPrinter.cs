using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class AttackGovernmentArmyResultPrinter : CommandResultPrinter<AttackGovernmentArmyResult>
{
    private const string PrintFormatString = "{0} attacked the government army with {1:N0} henchmen; they lost {2:N0} henchmen and the government lost {3:N0} soldiers.";
    private const string PlayerGainedReputationFormatString = "{0} gained {1}% reputation.";
    private const string PlayerLostReputationFormatString = "{0} lost {1}% reputation.";

    public override void PrintCommandResult(AttackGovernmentArmyResult result)
    {
        string formattedString = string.Format(
            PrintFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.NumberOfAttackingHenchmen,
            result.HenchmenAttackerLost,
            result.SoldiersGovernmentArmyLost
        );
        System.Console.WriteLine(formattedString);

        if (TryCreateAttackerReputationChangeString(result, out string attackerResultString))
        {
            System.Console.WriteLine(attackerResultString);
        }
    }

    private static bool TryCreateAttackerReputationChangeString(AttackGovernmentArmyResult result, out string resultString)
    {
        return TryCreateReputationChangeString(result.ReputationChangeForAttacker, RetrieveNameOfPlayerWhoActed(result), out resultString);
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
