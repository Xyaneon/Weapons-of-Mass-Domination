using System;
using System.Collections.Generic;
using WMD.Game.Planets;

namespace WMD.Game
{
    /// <summary>
    /// Represents the current state of the game.
    /// </summary>
    public class GameState
    {
        private const decimal LandBasePrice = 200;
        private const decimal MaxLandPriceIncreaseFromScarcity = 1000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="players">The list of players to include in this game.</param>
        /// <param name="planet">The planet where this game is taking place.</param>
        public GameState(IList<Player> players, Planet planet)
        {
            Players = new List<Player>(players).AsReadOnly();
            Planet = planet;
        }

        /// <summary>
        /// Gets the list of players in this game.
        /// </summary>
        public IReadOnlyList<Player> Players;

        /// <summary>
        /// Gets the current game round.
        /// </summary>
        public int CurrentRound { get; internal set; } = 1;

        /// <summary>
        /// Gets the current <see cref="Player"/> whose turn it is.
        /// </summary>
        public Player CurrentPlayer { get => Players[CurrentPlayerIndex]; }

        /// <summary>
        /// Gets the index of the current <see cref="Player"/> whose turn it is.
        /// </summary>
        public int CurrentPlayerIndex { get; internal set; } = 0;

        /// <summary>
        /// Gets the <see cref="Planet"/> where this game is taking place.
        /// </summary>
        public Planet Planet { get; }

        /// <summary>
        /// Calculates the current price per square kilometer of unclaimed land.
        /// </summary>
        /// <returns>The current price per square kilometer of unclaimed land.</returns>
        public decimal CalculateUnclaimedLandPurchasePrice()
        {
            double percentageOfLandClaimed = 1 - Planet.PercentageOfLandStillUnclaimed;
            decimal priceIncreaseFromScarcity = (decimal)Math.Round((double)MaxLandPriceIncreaseFromScarcity * percentageOfLandClaimed, 2);
            return LandBasePrice + priceIncreaseFromScarcity;
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

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Land == Planet.TotalLandArea)
                {
                    winningPlayerIndex = i;
                    return true;
                }
            }

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
