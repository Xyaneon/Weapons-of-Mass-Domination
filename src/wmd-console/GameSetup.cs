using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data;

namespace WMD.Console
{
    static class GameSetup
    {
        private const string ArgumentOutOfRangeException_TooFewComputerPlayersForSinglePlayerGame = "There must be at least one computer player in a single-player game.";
        private const string ArgumentOutOfRangeException_NumberOfComputerPlayersCannotBeNegative = "The number of computer players in a multiplayer game cannot be negative.";

        private const string ComputerOpponentsQuantityPromptForMultiplayerFormatString = "Enter the number of computer opponents (zero or no more than {0:N0})";
        private const string ComputerOpponentsQuantityPromptForSinglePlayerFormatString = "Enter the number of computer opponents (at least 1, no more than {0:N0})";
        private const string ComputerPlayerNameFormatString = "CPU {0}";
        private const string HumanPlayersQuantityPromptForMultiplayerFormatString = "Enter the number of human players (at least 1, no more than {0:N0})";
        private const string HumanPlayerNamePrompt = "Please enter your name";
        private const int MaximumNumberOfPlayers = 4;

        private static PlayerColor _nextAvailableColor = 0;

        public static GameState CreateInitialStateForSinglePlayerGame()
        {
            _nextAvailableColor = 0;

            Player humanPlayer = SetUpHumanPlayer(Array.Empty<string>());

            int computerPlayerCount = AskForNumberOfComputerPlayers(true, MaximumNumberOfPlayers - 1);

            IList<Player> players = CreatePlayerList(humanPlayer, computerPlayerCount);
            return new GameState(players, new Earth());
        }

        public static GameState CreateInitialStateForMultiplayerGame()
        {
            _nextAvailableColor = 0;

            int humanPlayerCount = AskForNumberOfHumanPlayers(MaximumNumberOfPlayers);
            var humanPlayers = new Queue<Player>(humanPlayerCount);
            for (int i = 0; i < humanPlayerCount; i++)
            {
                ICollection<string> takenNames = humanPlayers.Select(player => player.Identification.Name).ToList();
                Player humanPlayer = SetUpHumanPlayer(takenNames);
                humanPlayers.Enqueue(humanPlayer);
            }

            int maximumNumberOfComputerPlayers = MaximumNumberOfPlayers - humanPlayerCount;
            int computerPlayerCount = maximumNumberOfComputerPlayers > 0 ? AskForNumberOfComputerPlayers(false, maximumNumberOfComputerPlayers) : 0;

            IList<Player> players = CreatePlayerList(humanPlayers.ToList(), computerPlayerCount);
            return new GameState(players, new Earth());
        }

        private static int AskForNumberOfComputerPlayers(bool singlePlayerGame, int maximumAllowed)
        {
            if (maximumAllowed == 0)
            {
                return 0;
            }

            string requestText;
            IntRange allowedRange;

            if (singlePlayerGame)
            {
                requestText = ComputerOpponentsQuantityPromptForSinglePlayerFormatString;
                allowedRange = new IntRange(1, maximumAllowed);
            }
            else
            {
                requestText = ComputerOpponentsQuantityPromptForMultiplayerFormatString;
                allowedRange = new IntRange(0, maximumAllowed);
            }

            return UserInput.GetInteger(string.Format(requestText, maximumAllowed), allowedRange);
        }

        private static int AskForNumberOfHumanPlayers(int maximumAllowed)
        {
            if (maximumAllowed == 0)
            {
                return 0;
            }

            var prompt = string.Format(HumanPlayersQuantityPromptForMultiplayerFormatString, maximumAllowed);
            IntRange allowedRange = new IntRange(1, maximumAllowed);

            return UserInput.GetInteger(prompt, allowedRange);
        }

        private static Player SetUpHumanPlayer(ICollection<string> takenNames)
        {
            var name = RetrieveHumanPlayerName(takenNames);

            return new Player(new PlayerIdentification(name, GetNextAvailableColor(), true));
        }

        private static string RetrieveHumanPlayerName(ICollection<string> takenNames)
        {
            string? name;

            while (true)
            {
                name = UserInput.GetString(HumanPlayerNamePrompt);

                if (name != null && !takenNames.Contains(name))
                {
                    break;
                }
            }

            return name;
        }

        private static IList<Player> CreatePlayerList(Player humanPlayer, int computerPlayerCount)
        {
            if (computerPlayerCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(computerPlayerCount), ArgumentOutOfRangeException_TooFewComputerPlayersForSinglePlayerGame);
            }

            IList<Player> players = CreateComputerPlayers(computerPlayerCount);
            players.Insert(0, humanPlayer);
            return players;
        }

        private static IList<Player> CreatePlayerList(IList<Player> humanPlayers, int computerPlayerCount)
        {
            if (computerPlayerCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(computerPlayerCount), ArgumentOutOfRangeException_NumberOfComputerPlayersCannotBeNegative);
            }

            return humanPlayers.Concat(CreateComputerPlayers(computerPlayerCount).ToList()).ToList();
        }

        private static IList<Player> CreateComputerPlayers(int computerPlayerCount) =>
            Enumerable.Range(1, computerPlayerCount)
                .Select(playerNumber => CreateComputerPlayer(playerNumber))
                .ToList();

        private static Player CreateComputerPlayer(int playerNumber) =>
            new(new PlayerIdentification(string.Format(ComputerPlayerNameFormatString, playerNumber), GetNextAvailableColor(), false));

        private static PlayerColor GetNextAvailableColor()
        {
            PlayerColor color = _nextAvailableColor;
            _nextAvailableColor++;
            return color;
        }
    }
}
