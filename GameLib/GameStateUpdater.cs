using System;

namespace WMD.Game
{
    /// <summary>
    /// Performs updates on <see cref="GameState"/> instances.
    /// </summary>
    public static class GameStateUpdater
    {
        private const string ArgumentNull_GameState = "The game state to update cannot be null.";

        /// <summary>
        /// Advances the game to the next turn.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameState"/> is <see langword="null"/>.
        /// </exception>
        public static void AdvanceToNextTurn(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState), ArgumentNull_GameState);
            }

            gameState.CurrentPlayerIndex = gameState.CurrentPlayerIndex >= gameState.Players.Count - 1
                ? 0
                : gameState.CurrentPlayerIndex + 1;

            if (gameState.CurrentPlayerIndex == 0)
            {
                AdvanceToNextRound(gameState);
            }
        }

        /// <summary>
        /// Gives the specified amount of unclaimed land to a player.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player receiving the land.</param>
        /// <param name="area">The amount of land to give, in square kilometers.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameState"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of unclaimed land left.
        /// </exception>
        /// <seealso cref="HavePlayerGiveUpLand(GameState, int, int)"/>
        public static void GiveUnclaimedLandToPlayer(GameState gameState, int playerIndex, int area)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState), ArgumentNull_GameState);
            }

            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot be negative.");
            }

            if (area > gameState.Planet.UnclaimedLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot exceed the actual amount left.");
            }

            gameState.Planet.UnclaimedLandArea -= area;
            gameState.Players[playerIndex].Land += area;
        }

        /// <summary>
        /// Have a player give up the specified amount of land.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player losing the land.</param>
        /// <param name="area">The amount of land to lose, in square kilometers.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameState"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of land left in the player's control.
        /// </exception>
        /// <seealso cref="GiveUnclaimedLandToPlayer(GameState, int, int)"/>
        public static void HavePlayerGiveUpLand(GameState gameState, int playerIndex, int area)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState), ArgumentNull_GameState);
            }

            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot be negative.");
            }

            if (area > gameState.Players[playerIndex].Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot exceed the actual amount they have.");
            }

            gameState.Planet.UnclaimedLandArea += area;
            gameState.Players[playerIndex].Land -= area;
        }

        private static void AdvanceToNextRound(GameState gameState)
        {
            gameState.CurrentRound++;
        }
    }
}
