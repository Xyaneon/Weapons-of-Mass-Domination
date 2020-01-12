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
        /// Gets the <see cref="Planet"/> where this game is taking place.
        /// </summary>
        public Planet Planet { get; }

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

        /// <summary>
        /// Gives the specified amount of unclaimed land to a player.
        /// </summary>
        /// <param name="playerIndex">The index of the player receiving the land.</param>
        /// <param name="area">The amount of land to give, in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of unclaimed land left.
        /// </exception>
        /// <seealso cref="HavePlayerGiveUpLand(int, int)"/>
        public void GiveUnclaimedLandToPlayer(int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot be negative.");
            }

            if (area > Planet.UnclaimedLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot exceed the actual amount left.");
            }

            Planet.UnclaimedLandArea -= area;
            Players[playerIndex].Land += area;
        }

        /// <summary>
        /// Have a player give up the specified amount of land.
        /// </summary>
        /// <param name="playerIndex">The index of the player losing the land.</param>
        /// <param name="area">The amount of land to lose, in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of land left in the player's control.
        /// </exception>
        /// <seealso cref="GiveUnclaimedLandToPlayer(int, int)"/>
        public void HavePlayerGiveUpLand(int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot be negative.");
            }

            if (area > Players[playerIndex].Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot exceed the actual amount they have.");
            }

            Planet.UnclaimedLandArea += area;
            Players[playerIndex].Land -= area;
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
