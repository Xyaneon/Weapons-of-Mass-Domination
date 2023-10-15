using System;
using System.Collections.Generic;
using System.ComponentModel;
using WMD.Console.Miscellaneous;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Governments;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.Research;
using WMD.Game.State.Data.SecretBases;

namespace WMD.Console.UI;

static class StartOfTurnPrinter
{
    private const string HeaderTextFormatString = "{0}'s turn (Day {1})";
    private const string GovernmentDefeated = "The government has been defeated.";
    private const string GovernmentSummaryFormatString = "The government has {0:N0} soldiers in its army.";
    private const string LandFormatString = "Land: {0:N0} km²";
    private const string MoneyFormatString = "Money: {0:C}";
    private const string NoHenchmen = "You have no henchmen.";
    private const string NoSecretBase = "You do not have your own secret base yet.";
    private const string NukesFormatString = "Nukes: {0:N0}";
    private const string NukeResearchFormatString = "Your nuke research is at Level {0:N0} ({1}).";
    private const string PlanetSummaryFormatString = "{0:N0} km² of land and {1:N0} people on {2} remain uncontrolled ({3:P2}).";
    private const string RealWorldComparisonFormatString = "You control a land area comparable to {0}.";
    private const string ReputationFormatString = "Reputation: {0:N0}%";
    private const string SecretBaseLevelFormatString = "Your secret base is at Level {0:N0}.";
    private const string SoldiersFormatString = "- Soldiers : {0:N0}";
    private const string StatsSeparator = " | ";
    private const string ThievesFormatString = "- Thieves : {0:N0}";
    private const string TotalHenchmenFormatString = "You have {0:N0} total henchmen, each paid {1:C} per day ({2:C} total). This includes:";
    private const string UntrainedHenchmenFormatString = "- Untrained: {0:N0}";

    private static class NukeResearchFlavorTextCreator
    {
        private const string Level0 = "not yet started";
        private const string Level1 = "begun with online web searches";
        private const string Level2 = "drawing up blueprints";
        private const string Level3 = "constructing missile silos";
        private const string Level4 = "obtaining uranium";
        private const string Level5 = "enriching uranium";
        private const string Level6 = "constructing a prototype warhead";
        private const string Level7 = "constructing a prototype missile";
        private const string Level8 = "setting up a manufacturing process";
        private const string Level9 = "performing final tests";
        private const string Level10 = "you can build and use nukes!";
        private const string LevelUnknown = "?";

        public static string SelectResearchProgressFlavorText(ResearchState researchState) => researchState.NukeResearchLevel switch
        {
            0 => Level0,
            1 => Level1,
            2 => Level2,
            3 => Level3,
            4 => Level4,
            5 => Level5,
            6 => Level6,
            7 => Level7,
            8 => Level8,
            9 => Level9,
            10 => Level10,
            _ => LevelUnknown,
        };
    }

    public static void PrintStartOfTurn(GameState gameState)
    {
        Player currentPlayer = gameState.CurrentPlayer;

        System.Console.Clear();

        PrintHeader(currentPlayer, gameState.CurrentRound);

        System.Console.WriteLine();

        PrintPlayerStats(currentPlayer);
        PrintRealWorldLocationComparison(currentPlayer.State.Land);
        PrintWorkforceStats(currentPlayer.State.WorkforceState);
        PrintSecretBaseInfo(currentPlayer.State.SecretBase);
        if (currentPlayer.State.SecretBase != null)
        {
            PrintNukeResearchLevel(currentPlayer.State.ResearchState);
        }

        System.Console.WriteLine();

        PrintPlanetSummary(gameState.Planet);
        PrintGovernmentSummary(gameState.GovernmentState);

        System.Console.WriteLine();
    }

    private static void PrintHeader(Player player, int currentRound)
    {
        string headerText = string.Format(HeaderTextFormatString, player.Identification.Name, currentRound);
        int headerTextLengthWithPadding = headerText.Length + 2;
        string topLine = "╔" + new string('═', headerTextLengthWithPadding) + "╗";
        string bottomLine = "╚" + new string('═', headerTextLengthWithPadding) + "╝";

        System.Console.WriteLine(topLine);
        System.Console.Write("║");
        System.Console.BackgroundColor = ConvertPlayerColorToConsoleColor(player.Identification.Color);
        System.Console.Write($" {headerText} ");
        System.Console.ResetColor();
        System.Console.WriteLine("║");
        System.Console.WriteLine(bottomLine);
    }

    private static void PrintGovernmentSummary(GovernmentState governmentState) =>
        System.Console.WriteLine(governmentState.NumberOfSoldiers != 0
            ? string.Format(GovernmentSummaryFormatString, governmentState.NumberOfSoldiers)
            : GovernmentDefeated
        );

    private static void PrintNukeResearchLevel(ResearchState researchState) =>
        System.Console.WriteLine(NukeResearchFormatString, researchState.NukeResearchLevel, NukeResearchFlavorTextCreator.SelectResearchProgressFlavorText(researchState));

    private static void PrintPlayerStats(Player player)
    {
        PlayerState state = player.State;
        var stats = new List<string>(new string[]
        {
            string.Format(MoneyFormatString, state.Money),
            string.Format(LandFormatString, state.Land),
            string.Format(ReputationFormatString, state.ReputationPercentage),
        });

        if (state.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel)
        {
            stats.Add(string.Format(NukesFormatString, state.Nukes));
        }

        System.Console.WriteLine(string.Join(StatsSeparator, stats));
    }

    private static void PrintRealWorldLocationComparison(int landArea) =>
        System.Console.WriteLine(RealWorldComparisonFormatString, RealWorldComparisons.GetComparableRealWorldLocationByLandAreaInSquareKilometers(landArea));

    private static void PrintSecretBaseInfo(SecretBase? secretBase) =>
        System.Console.WriteLine(secretBase != null ? string.Format(SecretBaseLevelFormatString, secretBase.Level) : NoSecretBase);
    
    private static void PrintWorkforceStats(WorkforceState workforce)
    {
        if (workforce.TotalHenchmenCount <= 0)
        {
            System.Console.WriteLine(NoHenchmen);
            return;
        }

        System.Console.WriteLine(TotalHenchmenFormatString, workforce.TotalHenchmenCount, workforce.DailyPayRate, workforce.TotalDailyPay);
        System.Console.WriteLine(UntrainedHenchmenFormatString, workforce.GenericHenchmenCount);
        if (workforce.SoldierCount > 0)
        {
            System.Console.WriteLine(SoldiersFormatString, workforce.SoldierCount);
        }
        if (workforce.ThiefCount > 0)
        {
            System.Console.WriteLine(ThievesFormatString, workforce.ThiefCount);
        }
    }

    private static void PrintPlanetSummary(Planet planet) =>
        System.Console.WriteLine(PlanetSummaryFormatString, planet.UnclaimedLandArea, planet.NeutralPopulation, planet.Name, planet.PercentageOfLandStillUnclaimed);

    private static ConsoleColor ConvertPlayerColorToConsoleColor(PlayerColor color) => color switch
    {
        PlayerColor.Red => ConsoleColor.DarkRed,
        PlayerColor.Green => ConsoleColor.DarkGreen,
        PlayerColor.Blue => ConsoleColor.DarkBlue,
        PlayerColor.Yellow => ConsoleColor.DarkYellow,
        _ => throw new InvalidEnumArgumentException(nameof(color), (int)color, typeof(PlayerColor))
    };
}
