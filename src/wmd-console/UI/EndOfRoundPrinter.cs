using System;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Console.UI
{
    public static class EndOfRoundPrinter
    {
        public static void PrintEndOfRound(RoundUpdateResult roundUpdate)
        {
            System.Console.Clear();
            string headerText = $"End of Day {roundUpdate.RoundWhichEnded:N0}";
            System.Console.WriteLine(headerText);
            System.Console.WriteLine(new string('=', headerText.Length));
            System.Console.WriteLine();

            if (roundUpdate.Items.Count == 0)
            {
                System.Console.WriteLine("Nothing noteworthy happened today.");
                System.Console.WriteLine();
            }
            else
            {
                foreach (RoundUpdateResultItem item in roundUpdate.Items)
                {
                    PrintEndOfRoundItem(roundUpdate.GameState, item);
                }
            }

            System.Console.WriteLine("The day has ended. Press any key to continue...");
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
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {item.GetType().Name}.");
            }
            System.Console.WriteLine();
        }
    }
}
