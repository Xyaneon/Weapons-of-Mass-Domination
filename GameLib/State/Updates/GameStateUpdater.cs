using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.State.Updates
{
    /// <summary>
    /// Performs updates on <see cref="GameState"/> instances.
    /// </summary>
    public static class GameStateUpdater
    {
        /// <summary>
        /// Advances the game to the next turn, and possibly to the next round.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <returns>
        /// A tuple containing the updated <see cref="GameState"/> and possibly a <see cref="RoundUpdateResult"/> if a new
        /// round has started, or otherwise <see langword="null"/>.
        /// </returns>
        public static (GameState, RoundUpdateResult?) AdvanceToNextTurn([DisallowNull] GameState gameState)
        {
            gameState = gameState with
            {
                CurrentPlayerIndex = gameState.CurrentPlayerIndex >= gameState.Players.Count - 1 ? 0 : gameState.CurrentPlayerIndex + 1
            };

            return gameState.CurrentPlayerIndex == 0
                ? GameStateRoundAdvancer.AdvanceToNextRound(gameState)
                : (gameState, null);
        }

        /// <summary>
        /// Gives the specified amount of unclaimed land to a player.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player receiving the land.</param>
        /// <param name="area">The amount of land to give, in square kilometers.</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of unclaimed land left.
        /// </exception>
        /// <seealso cref="HavePlayerGiveUpLand(GameState, int, int)"/>
        public static GameState GiveUnclaimedLandToPlayer([DisallowNull] GameState gameState, int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot be negative.");
            }

            if (area > gameState.Planet.UnclaimedLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot exceed the actual amount left.");
            }

            var gameStateWithAdjustedUnclaimedLand = AdjustUnclaimedLandArea(gameState, -1 * area);
            var playerState = gameStateWithAdjustedUnclaimedLand.Players[playerIndex].State;
            var updatedPlayerState = playerState with { Land = playerState.Land + area };

            return UpdatePlayerState(gameStateWithAdjustedUnclaimedLand, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Have a player give up the specified amount of land.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player losing the land.</param>
        /// <param name="area">The amount of land to lose, in square kilometers.</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of land left in the player's control.
        /// </exception>
        /// <seealso cref="GiveUnclaimedLandToPlayer(GameState, int, int)"/>
        public static GameState HavePlayerGiveUpLand([DisallowNull] GameState gameState, int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot be negative.");
            }

            if (area > gameState.Players[playerIndex].State.Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot exceed the actual amount they have.");
            }

            var gameStateWithAdjustedUnclaimedLand = AdjustUnclaimedLandArea(gameState, area);
            var currentPlayerState = gameStateWithAdjustedUnclaimedLand.Players[playerIndex].State;
            var updatedPlayerState = currentPlayerState with { Land = currentPlayerState.Land - area };

            return UpdatePlayerState(gameStateWithAdjustedUnclaimedLand, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Adjusts the amount of money a player has by the specified amount.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player whose money to adjust.</param>
        /// <param name="adjustmentAmount">The amount to adjust by (positive to give money to the player, negative to take it away).</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        public static GameState AdjustMoneyForPlayer([DisallowNull] GameState gameState, int playerIndex, decimal adjustmentAmount)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            var updatedPlayerState = currentPlayerState with { Money = currentPlayerState.Money + adjustmentAmount };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Adjusts the number of henchmen a player has by the specified amount.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player whose number of henchmen to adjust.</param>
        /// <param name="adjustmentAmount">The number of henchmen to adjust by (positive to give henchmen to the player, negative to take it away).</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="adjustmentAmount"/> would cause the player's henchmen count to become negative.</exception>
        public static GameState AdjustHenchmenForPlayer([DisallowNull] GameState gameState, int playerIndex, int adjustmentAmount)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            var currentWorkforceState = currentPlayerState.WorkforceState;

            var updatedHenchmenAmount = currentWorkforceState.NumberOfHenchmen + adjustmentAmount;
            if (updatedHenchmenAmount < 0)
            {
                throw new InvalidOperationException("The number of henchmen a player has cannot become negative.");
            }

            var updatedWorkforceState = currentWorkforceState with { NumberOfHenchmen = updatedHenchmenAmount };
            var updatedPlayerState = currentPlayerState with { WorkforceState = updatedWorkforceState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Adjusts the unclaimed land area by the specified amount.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="adjustmentAmount">The number of square kilometers to adjust by (positive to increase unclaimed land area, negative to decrease it).</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="adjustmentAmount"/> would cause the unclaimed land area to be negative or exceed the total amount of land area.
        /// </exception>
        public static GameState AdjustUnclaimedLandArea([DisallowNull] GameState gameState, int adjustmentAmount)
        {
            var currentPlanetState = gameState.Planet;
            Planet updatedPlanetState;

            try
            {
                updatedPlanetState = currentPlanetState with { UnclaimedLandArea = currentPlanetState.UnclaimedLandArea + adjustmentAmount };
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException($"Planet state after unclaimed land area adjustment would be invalid: {ex.Message}", ex);
            }

            return gameState with { Planet = updatedPlanetState };
        }

        /// <summary>
        /// Increments the level of a player's secret base.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player whose secret base to level up.</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="InvalidOperationException">The player does not have a secret base.</exception>
        public static GameState IncrementSecretBaseLevel([DisallowNull] GameState gameState, int playerIndex)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            var currentSecretBase = currentPlayerState.SecretBase ?? throw new InvalidOperationException("The player does not have a secret base to level up.");
            var updatedSecretBase = currentSecretBase with { Level = currentSecretBase.Level + 1 };
            var updatedPlayerState = currentPlayerState with { SecretBase = updatedSecretBase };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Creates a shallow copy of the given <see cref="GameState"/> with the planet's state updated.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="planete">The new <see cref="Planet"/> to use.</param>
        /// <returns>A shallow copy of <paramref name="gameState"/> with the applied state update.</returns>
        public static GameState UpdatePlanetState([DisallowNull] GameState gameState, [DisallowNull] Planet planet)
        {
            return gameState with { Planet = planet };
        }

        /// <summary>
        /// Creates a shallow copy of the given <see cref="GameState"/> with a player's state updated.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose state to update.</param>
        /// <param name="playerState">The new <see cref="PlayerState"/> to give to the indicated <see cref="Player"/>.</param>
        /// <returns>A shallow copy of <paramref name="gameState"/> with the applied state update for the player.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="playerIndex"/> is less than zero.
        /// -or-
        /// <paramref name="playerIndex"/> is greater than or equal to the number of players in <paramref name="state"/>.
        /// </exception>
        public static GameState UpdatePlayerState([DisallowNull] GameState gameState, int playerIndex, [DisallowNull] PlayerState playerState)
        {
            if (playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, "The player index cannot be less than zero.");
            }

            if (playerIndex >= gameState.Players.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, "The player index cannot be greater than or equal to the number of players.");
            }

            return gameState with { Players = CreatePlayerListCopyWithUpdatedStateForPlayer(gameState.Players, playerIndex, playerState) };
        }

        private static IReadOnlyList<Player> CreatePlayerListCopyWithUpdatedStateForPlayer(IReadOnlyList<Player> players, int playerIndex, PlayerState state)
        {
            var updatedPlayers = new Queue<Player>(players.Count);

            for (var i = 0; i < players.Count; i++)
            {
                updatedPlayers.Enqueue(i == playerIndex ? players[i] with { State = state } : players[i]);
            }

            return updatedPlayers.ToList().AsReadOnly();
        }
    }
}
