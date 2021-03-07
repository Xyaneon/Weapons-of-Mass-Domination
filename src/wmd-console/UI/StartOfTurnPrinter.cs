using System;
using System.Collections.Generic;
using System.ComponentModel;
using WMD.Console.Miscellaneous;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.Research;
using WMD.Game.State.Data.SecretBases;

namespace WMD.Console.UI
{
    static class StartOfTurnPrinter
    {
        public static void PrintStartOfTurn(GameState gameState)
        {
            Player currentPlayer = gameState.CurrentPlayer;

            System.Console.Clear();

            PrintHeader(currentPlayer, gameState.CurrentRound);

            System.Console.WriteLine();

            PrintPlayerStats(currentPlayer);
            PrintRealWorldLocationComparison(currentPlayer.State.Land);
            PrintSecretBaseInfo(currentPlayer.State.SecretBase);
            if (currentPlayer.State.SecretBase != null)
            {
                PrintNukeResearchLevel(currentPlayer.State.ResearchState);
            }

            System.Console.WriteLine();

            PrintSummary(gameState.Planet);

            System.Console.WriteLine();
        }

        private static void PrintHeader(Player player, int currentRound)
        {
            string headerText = $"{player.Identification.Name}'s turn (Day {currentRound})";
            string topLine = "╔" + new string('═', headerText.Length + 2) + "╗";
            string bottomLine = "╚" + new string('═', headerText.Length + 2) + "╝";

            System.Console.WriteLine(topLine);
            System.Console.Write("║");
            System.Console.BackgroundColor = ConvertPlayerColorToConsoleColor(player.Identification.Color);
            System.Console.Write($" {headerText} ");
            System.Console.ResetColor();
            System.Console.WriteLine("║");
            System.Console.WriteLine(bottomLine);
        }

        private static void PrintNukeResearchLevel(ResearchState researchState)
        {
            string researchProgressFlavorText = researchState.NukeResearchLevel switch
            {
                0 => "not yet started",
                1 => "begun with online web searches",
                2 => "drawing up blueprints",
                3 => "constructing missile silos",
                4 => "obtaining uranium",
                5 => "enriching uranium",
                6 => "constructing a prototype warhead",
                7 => "constructing a prototype missile",
                8 => "setting up a manufacturing process",
                9 => "performing final tests",
                10 => "you can build and use nukes!",
                _ => "?",
            };
            System.Console.WriteLine($"Your nuke research is at Level {researchState.NukeResearchLevel:N0} ({researchProgressFlavorText}).");
        }

        private static void PrintPlayerStats(Player player)
        {
            PlayerState state = player.State;
            var stats = new List<string>(new string[]
            {
                $"Money: {state.Money:C}",
                $"Henchmen: {state.WorkforceState.NumberOfHenchmen:N0}",
                $"Land: {state.Land:N0} km²"
            });
            if (state.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel)
            {
                stats.Add($"Nukes: {state.Nukes:N0}");
            }

            System.Console.WriteLine(string.Join(" | ", stats));
        }

        private static void PrintRealWorldLocationComparison(int landArea)
        {
            string location = RealWorldComparisons.GetComparableRealWorldLocationByLandAreaInSquareKilometers(landArea);
            System.Console.WriteLine($"You control a land area comparable to {location}.");
        }

        private static void PrintSecretBaseInfo(SecretBase? secretBase)
        {
            string secretBaseString = secretBase == null
                ? "You do not have your own secret base yet."
                : $"Your secret base is at Level {secretBase.Level:N0}.";
            System.Console.WriteLine(secretBaseString);
        }

        private static void PrintSummary(Planet planet)
        {
            string summaryString = $"{planet.UnclaimedLandArea:N0} km² of land on {planet.Name} remains uncontrolled ({planet.PercentageOfLandStillUnclaimed:P2}).";
            System.Console.WriteLine(summaryString);
        }

        private static ConsoleColor ConvertPlayerColorToConsoleColor(PlayerColor color)
        {
            return color switch
            {
                PlayerColor.Red => ConsoleColor.DarkRed,
                PlayerColor.Green => ConsoleColor.DarkGreen,
                PlayerColor.Blue => ConsoleColor.DarkBlue,
                PlayerColor.Yellow => ConsoleColor.DarkYellow,
                _ => throw new InvalidEnumArgumentException(nameof(color), (int)color, typeof(PlayerColor))
            };
        }
    }
}
