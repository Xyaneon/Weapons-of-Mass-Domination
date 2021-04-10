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
        private const string ComputerOpponentsQuantityPromptForMultiplayerFormatString = "Enter the number of computer opponents (zero or no more than {0:N0})";
        private const string ComputerOpponentsQuantityPromptForSinglePlayerFormatString = "Enter the number of computer opponents (at least 1, no more than {0:N0})";
        private const string ComputerPlayerNameFormatString = "CPU {0}";
        private const string HumanPlayersQuantityPromptForMultiplayerFormatString = "Enter the number of human players (at least {0:N0}, no more than {1:N0})";
        private const string HumanPlayerNamePromptFormatString = "Player {0:N0}, please enter your name";
        private const int MaximumNumberOfPlayers = 4;

        private static PlayerColor _nextAvailableColor = 0;

        public static GameState CreateInitialGameState(bool isSinglePlayer)
        {
            _nextAvailableColor = 0;

            IEnumerable<Player> humanPlayers = SetUpHumanPlayers(isSinglePlayer);
            IEnumerable<Player> computerPlayers = SetUpComputerPlayers(isSinglePlayer, humanPlayers.Count());
            IList<Player> players = humanPlayers.Concat(computerPlayers).ToList();

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

        private static int AskForNumberOfHumanPlayers(int minimumAllowed, int maximumAllowed)
        {
            if (maximumAllowed == 0)
            {
                return 0;
            }

            var prompt = string.Format(HumanPlayersQuantityPromptForMultiplayerFormatString, minimumAllowed, maximumAllowed);
            IntRange allowedRange = new IntRange(minimumAllowed, maximumAllowed);

            return UserInput.GetInteger(prompt, allowedRange);
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

        private static string RetrieveHumanPlayerName(int playerNumber, ICollection<string> takenNames)
        {
            string? name;

            while (true)
            {
                name = UserInput.GetString(string.Format(HumanPlayerNamePromptFormatString, playerNumber));

                if (name != null && !takenNames.Contains(name))
                {
                    break;
                }
            }

            return name;
        }

        private static IEnumerable<Player> SetUpComputerPlayers(bool isSinglePlayer, int numberOfHumanPlayers)
        {
            int maximumNumberOfComputerPlayers = MaximumNumberOfPlayers - numberOfHumanPlayers;
            int computerPlayerCount = maximumNumberOfComputerPlayers > 0 ? AskForNumberOfComputerPlayers(isSinglePlayer, maximumNumberOfComputerPlayers) : 0;
            return CreateComputerPlayers(computerPlayerCount);
        }

        private static Player CreateHumanPlayer(int playerNumber, ICollection<string> takenNames) =>
            new(new PlayerIdentification(RetrieveHumanPlayerName(playerNumber, takenNames), GetNextAvailableColor(), true));

        private static IEnumerable<Player> SetUpHumanPlayers(bool isSinglePlayer)
        {
            int minimumNumberOfHumanPlayers = isSinglePlayer ? 1 : 2;
            int humanPlayerCount = isSinglePlayer ? 1 : AskForNumberOfHumanPlayers(minimumNumberOfHumanPlayers, MaximumNumberOfPlayers);
            var humanPlayers = new Queue<Player>(humanPlayerCount);

            for (int i = 0; i < humanPlayerCount; i++)
            {
                ICollection<string> takenNames = humanPlayers.Select(player => player.Identification.Name).ToList();
                Player humanPlayer = CreateHumanPlayer(i + 1, takenNames);
                humanPlayers.Enqueue(humanPlayer);
            }

            return humanPlayers;
        }
    }
}
