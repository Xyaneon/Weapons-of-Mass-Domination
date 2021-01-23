using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Henchmen;
using WMD.Game.Players;
using WMD.Game.Rounds;

namespace WMD.Game
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
        public static (GameState, RoundUpdateResult?) AdvanceToNextTurn([DisallowNull, NotNull] GameState gameState)
        {
            gameState = gameState with
            {
                CurrentPlayerIndex = gameState.CurrentPlayerIndex >= gameState.Players.Count - 1 ? 0 : gameState.CurrentPlayerIndex + 1
            };

            return gameState.CurrentPlayerIndex == 0
                ? AdvanceToNextRound(gameState)
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
        public static GameState GiveUnclaimedLandToPlayer([DisallowNull, NotNull] GameState gameState, int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot be negative.");
            }

            if (area > gameState.Planet.UnclaimedLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of unclaimed land to give to a player cannot exceed the actual amount left.");
            }

            gameState.Planet.UnclaimedLandArea -= area;
            PlayerState playerState = gameState.Players[playerIndex].State;
            PlayerState updatedPlayerState = playerState with { Land = playerState.Land + area };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
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
        public static GameState HavePlayerGiveUpLand([DisallowNull, NotNull] GameState gameState, int playerIndex, int area)
        {
            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot be negative.");
            }

            if (area > gameState.Players[playerIndex].State.Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot exceed the actual amount they have.");
            }

            gameState.Planet.UnclaimedLandArea += area;
            PlayerState currentPlayerState = gameState.Players[playerIndex].State;
            PlayerState updatedPlayerState = currentPlayerState with { Land = currentPlayerState.Land - area };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Adjusts the amount of money a player has by the specified amount.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player whose money to adjust.</param>
        /// <param name="adjustmentAmount">The amount to adjust by (positive to give money to the player, negative to take it away).</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        public static GameState AdjustMoneyForPlayer([DisallowNull, NotNull] GameState gameState, int playerIndex, decimal adjustmentAmount)
        {
            PlayerState currentPlayerState = gameState.Players[playerIndex].State;
            PlayerState updatedPlayerState = currentPlayerState with { Money = currentPlayerState.Money + adjustmentAmount };

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
        public static GameState AdjustHenchmenForPlayer([DisallowNull, NotNull] GameState gameState, int playerIndex, int adjustmentAmount)
        {
            PlayerState currentPlayerState = gameState.Players[playerIndex].State;
            WorkforceState currentWorkforceState = currentPlayerState.WorkforceState;

            int updatedHenchmenAmount = currentWorkforceState.NumberOfHenchmen + adjustmentAmount;
            if (updatedHenchmenAmount < 0)
            {
                throw new InvalidOperationException("The number of henchmen a player has cannot become negative.");
            }

            WorkforceState updatedWorkforceState = currentWorkforceState with { NumberOfHenchmen = updatedHenchmenAmount };
            PlayerState updatedPlayerState = currentPlayerState with { WorkforceState = updatedWorkforceState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        /// <summary>
        /// Increments the level of a player's secret base.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player whose secret base to level up.</param>
        /// <returns>An updated copy of <paramref name="gameState"/>.</returns>
        /// <exception cref="InvalidOperationException">The player does not have a secret base.</exception>
        public static GameState IncrementSecretBaseLevel([DisallowNull, NotNull] GameState gameState, int playerIndex)
        {
            PlayerState currentPlayerState = gameState.Players[playerIndex].State;
            SecretBase currentSecretBase = currentPlayerState.SecretBase ?? throw new InvalidOperationException("The player does not have a secret base to level up.");
            SecretBase updatedSecretBase = currentSecretBase with { Level = currentSecretBase.Level + 1 };
            PlayerState updatedPlayerState = currentPlayerState with { SecretBase = updatedSecretBase };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
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
        public static GameState UpdatePlayerState([DisallowNull, NotNull] GameState gameState, int playerIndex, [DisallowNull, NotNull] PlayerState playerState)
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

        private static (GameState, RoundUpdateResult) AdvanceToNextRound(GameState gameState)
        {
            RoundUpdateResult result = CreateRoundUpdateResult(gameState);
            GameState updatedGameState = ApplyRoundUpdates(gameState, result);
            updatedGameState = updatedGameState with { CurrentRound = updatedGameState.CurrentRound + 1 };

            return (updatedGameState, result);
        }

        private static GameState ApplyRoundUpdates(GameState gameState, RoundUpdateResult roundUpdates)
        {
            GameState updatedGameState = gameState;

            try
            {
                foreach (RoundUpdateResultItem roundUpdate in roundUpdates.Items)
                {
                    updatedGameState = ApplyRoundUpdateItem(updatedGameState, roundUpdate);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The provided round update result contained an invalid item.", nameof(roundUpdates), ex);
            }

            return updatedGameState;
        }

        private static GameState ApplyRoundUpdateItem(GameState gameState, RoundUpdateResultItem roundUpdate)
        {
            switch (roundUpdate)
            {
                case PlayerHenchmenPaid playerHenchmenPaid:
                    return AdjustMoneyForPlayer(gameState, playerHenchmenPaid.PlayerIndex, -1 * playerHenchmenPaid.TotalPaidAmount);
                case PlayerHenchmenQuit playerHenchmenQuit:
                    return AdjustHenchmenForPlayer(gameState, playerHenchmenQuit.PlayerIndex, -1 * playerHenchmenQuit.NumberOfHenchmenQuit);
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}.");
            }
        }

        private static IReadOnlyList<Player> CreatePlayerListCopyWithUpdatedStateForPlayer(IReadOnlyList<Player> players, int playerIndex, PlayerState state)
        {
            Queue<Player> updatedPlayers = new Queue<Player>(players.Count);

            for (int i = 0; i < players.Count; i++)
            {
                updatedPlayers.Enqueue(i == playerIndex ? players[i] with { State = state } : players[i]);
            }

            return updatedPlayers.ToList().AsReadOnly();
        }

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState)
        {
            var henchmenPayments = Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenPaid(gameState, index));

            var henchmenQuittings = Enumerable.Range(0, gameState.Players.Count)
                .Where(index => gameState.Players[index].State.Money <= 0 && gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenQuit(index, gameState.Players[index].State.WorkforceState.NumberOfHenchmen));

            var allUpdates = new List<RoundUpdateResultItem>()
                .Concat(henchmenPayments)
                .Concat(henchmenQuittings);

            return new RoundUpdateResult(gameState, gameState.CurrentRound, allUpdates);
        }
    }
}
