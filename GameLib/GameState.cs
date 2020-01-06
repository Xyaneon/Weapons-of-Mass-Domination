using System.Collections.Generic;

namespace WMD.Game
{
    /// <summary>
    /// Represents the current state of the game.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="players">The list of players to include in this game.</param>
        public GameState(IList<Player> players)
        {
            Players = new List<Player>(players).AsReadOnly();
        }

        /// <summary>
        /// Gets the list of players in this game.
        /// </summary>
        public IReadOnlyList<Player> Players;

        /// <summary>
        /// Gets the current game round.
        /// </summary>
        public int CurrentRound { get; private set; } = 1;

        /// <summary>
        /// Gets the current <see cref="Player"/> whose turn it is.
        /// </summary>
        public Player CurrentPlayer { get => Players[CurrentPlayerIndex]; }

        /// <summary>
        /// Gets the index of the current <see cref="Player"/> whose turn it is.
        /// </summary>
        public int CurrentPlayerIndex { get; private set; } = 0;

        /// <summary>
        /// Advances the game to the next turn.
        /// </summary>
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

        /// <summary>
        /// Determines whether the game has been won yet.
        /// </summary>
        /// <param name="winningPlayerIndex">An output parameter indicating the index of the winning <see cref="Player"/> if the game has been won yet.</param>
        /// <returns><see langword="true"/> if the game has been won; otherwise, <see langword="false"/>.</returns>
        public bool GameHasBeenWon(out int winningPlayerIndex)
        {
            winningPlayerIndex = -1;

            if (GameHasOnePlayerLeft(out int remainingPlayerIndex))
            {
                winningPlayerIndex = remainingPlayerIndex;
                return true;
            }

            // TODO: Additional win conditions.

            return false;
        }

        private bool GameHasOnePlayerLeft(out int remainingPlayerIndex)
        {
            remainingPlayerIndex = -1;

            for (int i = 0; i < Players.Count; i++)
            {
                if (!Players[i].HasResigned)
                {
                    if (remainingPlayerIndex >= 0)
                    {
                        remainingPlayerIndex = -1;
                        return false;
                    }

                    remainingPlayerIndex = i;
                }
            }

            return true;
        }
    }
}
