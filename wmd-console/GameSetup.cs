﻿using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Planets;

namespace WMD.Console
{
    static class GameSetup
    {
        private const int MaximumNumberOfPlayers = 4;

        public static GameState CreateInitialStateForSinglePlayerGame()
        {
            Player humanPlayer = SetUpHumanPlayer(new string[] { });
            int computerPlayerCount = AskForNumberOfComputerPlayers(true, MaximumNumberOfPlayers - 1);
            IList<Player> players = CreatePlayerList(humanPlayer, computerPlayerCount);
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
                requestText = $"Enter the number of computer opponents (at least 1, no more than {maximumAllowed})";
                allowedRange = new IntRange(1, maximumAllowed);
            }
            else
            {
                requestText = $"Enter the number of computer opponents (zero or no more than {maximumAllowed})";
                allowedRange = new IntRange(0, maximumAllowed);
            }

            return UserInput.GetInteger(requestText, allowedRange);
        }

        private static Player SetUpHumanPlayer(ICollection<string> takenNames)
        {
            string name;

            while(true)
            {
                name = UserInput.GetString("Please enter your name");
                if (!takenNames.Contains(name))
                {
                    break;
                }
            }

            return new Player(name);
        }

        private static IList<Player> CreatePlayerList(Player humanPlayer, int computerPlayerCount)
        {
            if (computerPlayerCount < 1)
            {
                throw new ArgumentOutOfRangeException("There must be at least one computer player in a single-player game.");
            }

            IList<Player> players = CreateComputerPlayers(computerPlayerCount);
            players.Insert(0, humanPlayer);
            return players;
        }

        private static IList<Player> CreateComputerPlayers(int computerPlayerCount)
        {
            return Enumerable.Range(1, computerPlayerCount)
                .Select(playerNumber => new Player($"CPU {playerNumber}"))
                .ToList();
        }
    }
}
