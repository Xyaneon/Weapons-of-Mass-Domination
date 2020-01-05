using System;
using System.Collections.Generic;

namespace WMD.Game
{
    public class GameState
    {
        public GameState(IList<Player> players)
        {
            Players = new List<Player>(players).AsReadOnly();
        }

        public IReadOnlyList<Player> Players;

        public int CurrentRound { get; private set; } = 1;

        public Player CurrentPlayer { get => Players[CurrentPlayerIndex]; }

        public int CurrentPlayerIndex { get; private set; } = 0;

        public void AdvanceToNextTurn()
        {
            CurrentPlayerIndex = CurrentPlayerIndex >= Players.Count - 1
                ? 0
                : CurrentPlayerIndex + 1;

            if (CurrentPlayerIndex == 0)
            {
                CurrentRound++;
            }
        }

        public bool GameHasBeenWon(out int winningPlayerIndex)
        {
            winningPlayerIndex = -1;
            // TODO
            return false;
        }
    }
}
