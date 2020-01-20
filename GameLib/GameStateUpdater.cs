using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.Rounds;

namespace WMD.Game
{
    /// <summary>
    /// Performs updates on <see cref="GameState"/> instances.
    /// </summary>
    public static class GameStateUpdater
    {
        private const string ArgumentNull_GameState = "The game state to update cannot be null.";

        /// <summary>
        /// Advances the game to the next turn. If a new round starts, a <see cref="RoundUpdateResult"/> will be returned.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <returns>
        /// A new <see cref="RoundUpdateResult"/> if the game has advanced to the next round; otherwise, <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameState"/> is <see langword="null"/>.
        /// </exception>
        public static RoundUpdateResult AdvanceToNextTurn(GameState gameState)
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
                return AdvanceToNextRound(gameState);
            }

            return null;
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
            gameState.Players[playerIndex].State.Land += area;
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

            if (area > gameState.Players[playerIndex].State.Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "The amount of land to have a player give up cannot exceed the actual amount they have.");
            }

            gameState.Planet.UnclaimedLandArea += area;
            gameState.Players[playerIndex].State.Land -= area;
        }

        private static RoundUpdateResult AdvanceToNextRound(GameState gameState)
        {
            RoundUpdateResult result = CreateRoundUpdateResult(gameState);
            ApplyRoundUpdates(gameState, result);
            gameState.CurrentRound++;

            return result;
        }

        private static void ApplyRoundUpdates(GameState gameState, RoundUpdateResult roundUpdates)
        {
            try
            {
                foreach (RoundUpdateResultItem roundUpdate in roundUpdates.Items)
                {
                    ApplyRoundUpdateItem(gameState, roundUpdate);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The provided round update result contained an invalid item.", nameof(roundUpdates), ex);
            }
        }

        private static void ApplyRoundUpdateItem(GameState gameState, RoundUpdateResultItem roundUpdate)
        {
            switch (roundUpdate)
            {
                case PlayerHenchmenPaid playerHenchmenPaid:
                    playerHenchmenPaid.Player.State.Money -= playerHenchmenPaid.TotalPaidAmount;
                    break;
                case PlayerHenchmenQuit playerHenchmenQuit:
                    playerHenchmenQuit.Player.State.Henchmen -= playerHenchmenQuit.NumberOfHenchmenQuit;
                    break;
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}.");
            }
        }

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState)
        {
            var henchmenPayments = gameState.Players
                .Where(player => player.State.Henchmen > 0)
                .Select(player => new PlayerHenchmenPaid(player));

            var henchmenQuittings = gameState.Players
                .Where(player => player.State.Money <= 0 && player.State.Henchmen > 0)
                .Select(player => new PlayerHenchmenQuit(player, player.State.Henchmen));

            var allUpdates = new List<RoundUpdateResultItem>()
                .Concat(henchmenPayments)
                .Concat(henchmenQuittings);

            return new RoundUpdateResult(gameState.CurrentRound, allUpdates);
        }
    }
}
