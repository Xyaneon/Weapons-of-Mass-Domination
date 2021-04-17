using System;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Console.UI
{
    public static class EndOfRoundPrinter
    {
        private const string EndOfRoundFooter = "The day has ended. Press any key to continue...";
        private const string EndOfRoundHeaderFormattingString = "End of Day {0:N0}";
        private const char HeaderSeparator = '=';
        private const string NoEndOfRoundUpdateItems = "Nothing noteworthy happened today.";

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
            switch (item)
            {
                case PlayerHenchmenPaid playerHenchmenPaid:
                    System.Console.WriteLine($"{gameState.Players[playerHenchmenPaid.PlayerIndex].Identification.Name} paid each of their {playerHenchmenPaid.NumberOfHenchmenPaid:N0} henchmen their daily pay of {playerHenchmenPaid.DailyPayRate:C}, for a total of {playerHenchmenPaid.TotalPaidAmount:C}.");
                    break;
                case PlayerHenchmenQuit playerHenchmenQuit:
                    System.Console.WriteLine($"{playerHenchmenQuit.NumberOfHenchmenQuit:N0} of {gameState.Players[playerHenchmenQuit.PlayerIndex].Identification.Name}'s henchmen quit.");
                    break;
                case ReputationDecay reputationDecay:
                    System.Console.WriteLine($"{gameState.Players[reputationDecay.PlayerIndex].Identification.Name} lost {reputationDecay.ReputationPercentageLost}% reputation due to time.");
                    break;
                case GovernmentIntervention intervention:
                    PrintGovernmentIntervention(gameState, intervention);
                    break;
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {item.GetType().Name}.");
            }
            System.Console.WriteLine();
        }

        private static void PrintGovernmentIntervention(GameState gameState, GovernmentIntervention intervention)
        {
            string interventionText = intervention switch
            {
                GovernmentTakesBackMoney occurrence => $"A government seized {occurrence.AmountTaken:C} from {gameState.Players[occurrence.PlayerIndex].Identification.Name}.",
                _ => throw new ArgumentException($"Unrecognized {typeof(GovernmentIntervention).Name} subclass: {intervention.GetType().Name}."),
            };

            System.Console.WriteLine(interventionText);
            System.Console.WriteLine();
        }
    }
}
