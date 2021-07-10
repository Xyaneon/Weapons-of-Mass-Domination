using System;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Console.UI
{
    public static class EndOfRoundPrinter
    {
        private const string EndOfRoundFooter = "The day has ended. Press any key to continue...";
        private const string EndOfRoundHeaderFormattingString = "End of Day {0:N0}";
        private const string GovernmentAttacksPlayerFormatString = "A government attacked {0}. {1:N0} soldiers and {2:N0} soldiers were lost in the attack.";
        private const string GovernmentDenouncesPlayerFormatString = "A government denounced {0}, causing them to lose {1:N0}% reputation.";
        private const string GovernmentTakesBackMoneyFormatString = "A government seized {0:C} from {1}.";
        private const char HeaderSeparator = '=';
        private const string NoEndOfRoundUpdateItems = "Nothing noteworthy happened today.";
        private const string PlayerGainedReputationFormatString = "{0} gained {1}% reputation due to their assets.";
        private const string PlayerHenchmenPaidFormatString = "{0} paid each of their {1:N0} henchmen their daily pay of {2:C}, for a total of {3:C}.";
        private const string PlayerHenchmenQuitFormatString = "{0:N0} of {1}'s henchmen quit.";
        private const string PlayerLostReputationFormatString = "{0} lost {1}% reputation due to time.";

        public static void PrintEndOfRound(RoundUpdateResult roundUpdate)
        {
            System.Console.Clear();
            string headerText = string.Format(EndOfRoundHeaderFormattingString, roundUpdate.RoundWhichEnded);
            System.Console.WriteLine(headerText);
            System.Console.WriteLine(new string(HeaderSeparator, headerText.Length));
            System.Console.WriteLine();

            if (roundUpdate.Items.Count == 0)
            {
                System.Console.WriteLine(NoEndOfRoundUpdateItems);
                System.Console.WriteLine();
            }
            else
            {
                foreach (RoundUpdateResultItem item in roundUpdate.Items)
                {
                    PrintEndOfRoundItem(roundUpdate.GameState, item);
                }
            }

            System.Console.WriteLine(EndOfRoundFooter);
        }

        private static void PrintEndOfRoundItem(GameState gameState, RoundUpdateResultItem item)
        {
            System.Console.WriteLine(CreateEndOfRoundItemText(gameState, item));
            System.Console.WriteLine();
        }

        private static string CreateEndOfRoundItemText(GameState gameState, RoundUpdateResultItem item) => item switch
        {
            PlayerHenchmenPaid playerHenchmenPaid => string.Format(PlayerHenchmenPaidFormatString, gameState.Players[playerHenchmenPaid.PlayerIndex].Identification.Name, playerHenchmenPaid.NumberOfHenchmenPaid, playerHenchmenPaid.DailyPayRate, playerHenchmenPaid.TotalPaidAmount),
            PlayerHenchmenQuit playerHenchmenQuit => string.Format(PlayerHenchmenQuitFormatString, playerHenchmenQuit.NumberOfHenchmenQuit, gameState.Players[playerHenchmenQuit.PlayerIndex].Identification.Name),
            ReputationChange reputationChange => CreateReputationChangeText(gameState, reputationChange),
            GovernmentIntervention intervention => CreateGovernmentInterventionText(gameState, intervention),
            _ => throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {item.GetType().Name}."),
        };

        private static string CreateGovernmentInterventionText(GameState gameState, GovernmentIntervention intervention)
        {
            return intervention switch
            {
                GovernmentAttacksPlayer occurrence => string.Format(
                    GovernmentAttacksPlayerFormatString,
                    gameState.Players[occurrence.PlayerIndex].Identification.Name,
                    occurrence.AttackCombatantsChanges.CombatantsLostByAttacker,
                    occurrence.AttackCombatantsChanges.CombatantsLostByDefender
                ),
                GovernmentDenouncesPlayer occurrence => string.Format(
                    GovernmentDenouncesPlayerFormatString,
                    gameState.Players[occurrence.PlayerIndex].Identification.Name,
                    occurrence.ReputationDecrease
                ),
                GovernmentTakesBackMoney occurrence => string.Format(
                    GovernmentTakesBackMoneyFormatString,
                    occurrence.AmountTaken,
                    gameState.Players[occurrence.PlayerIndex].Identification.Name
                ),
                _ => throw new ArgumentException($"Unrecognized {typeof(GovernmentIntervention).Name} subclass: {intervention.GetType().Name}."),
            };
        }

        private static string CreateReputationChangeText(GameState gameState, ReputationChange reputationChange)
        {
            string playerName = gameState.Players[reputationChange.PlayerIndex].Identification.Name;
            var changeAmount = reputationChange.ReputationPercentageChanged;
            return string.Format(changeAmount > 0 ? PlayerGainedReputationFormatString : PlayerLostReputationFormatString, playerName, Math.Abs(changeAmount));
        }
    }
}
