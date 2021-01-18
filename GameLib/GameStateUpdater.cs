using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        /// Advances the game to the next turn. If a new round starts, a <see cref="RoundUpdateResult"/> will be returned.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <returns>
        /// A new <see cref="RoundUpdateResult"/> if the game has advanced to the next round; otherwise, <see langword="null"/>.
        /// </returns>
        public static RoundUpdateResult? AdvanceToNextTurn([DisallowNull, NotNull] ref GameState gameState)
        {
            gameState = gameState with
            {
                CurrentPlayerIndex = gameState.CurrentPlayerIndex >= gameState.Players.Count - 1 ? 0 : gameState.CurrentPlayerIndex + 1
            };

            if (gameState.CurrentPlayerIndex == 0)
            {
                return AdvanceToNextRound(ref gameState);
            }

            return null;
        }

        /// <summary>
        /// Gives the specified amount of unclaimed land to a player.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player receiving the land.</param>
        /// <param name="area">The amount of land to give, in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of unclaimed land left.
        /// </exception>
        /// <seealso cref="HavePlayerGiveUpLand(GameState, int, int)"/>
        public static void GiveUnclaimedLandToPlayer([DisallowNull, NotNull] ref GameState gameState, int playerIndex, int area)
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
            gameState.Players[playerIndex].State = playerState with { Land = playerState.Land + area };
        }

        /// <summary>
        /// Have a player give up the specified amount of land.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/> to update.</param>
        /// <param name="playerIndex">The index of the player losing the land.</param>
        /// <param name="area">The amount of land to lose, in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="area"/> is less than zero.
        /// -or-
        /// <paramref name="area"/> is more than the actual amount of land left in the player's control.
        /// </exception>
        /// <seealso cref="GiveUnclaimedLandToPlayer(GameState, int, int)"/>
        public static void HavePlayerGiveUpLand([DisallowNull, NotNull] ref GameState gameState, int playerIndex, int area)
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
            PlayerState playerState = gameState.Players[playerIndex].State;
            gameState.Players[playerIndex].State = playerState with { Land = playerState.Land - area };
        }

        private static RoundUpdateResult AdvanceToNextRound(ref GameState gameState)
        {
            RoundUpdateResult result = CreateRoundUpdateResult(gameState);
            ApplyRoundUpdates(ref gameState, result);
            gameState = gameState with { CurrentRound = gameState.CurrentRound + 1 };

            return result;
        }

        private static void ApplyRoundUpdates(ref GameState gameState, RoundUpdateResult roundUpdates)
        {
            try
            {
                foreach (RoundUpdateResultItem roundUpdate in roundUpdates.Items)
                {
                    ApplyRoundUpdateItem(ref gameState, roundUpdate);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The provided round update result contained an invalid item.", nameof(roundUpdates), ex);
            }
        }

        private static void ApplyRoundUpdateItem(ref GameState gameState, RoundUpdateResultItem roundUpdate)
        {
            switch (roundUpdate)
            {
                case PlayerHenchmenPaid playerHenchmenPaid:
                    playerHenchmenPaid.Player.State = playerHenchmenPaid.Player.State with
                    {
                        Money = playerHenchmenPaid.Player.State.Money - playerHenchmenPaid.TotalPaidAmount
                    };
                    break;
                case PlayerHenchmenQuit playerHenchmenQuit:
                    playerHenchmenQuit.Player.State = playerHenchmenQuit.Player.State with
                    {
                        WorkforceState = playerHenchmenQuit.Player.State.WorkforceState with
                        {
                            NumberOfHenchmen = playerHenchmenQuit.Player.State.WorkforceState.NumberOfHenchmen - playerHenchmenQuit.NumberOfHenchmenQuit
                        }
                    };
                    break;
                default:
                    throw new ArgumentException($"Unrecognized {typeof(RoundUpdateResultItem).Name} subclass: {roundUpdate.GetType().Name}.");
            }
        }

        private static RoundUpdateResult CreateRoundUpdateResult(GameState gameState)
        {
            var henchmenPayments = gameState.Players
                .Where(player => player.State.WorkforceState.NumberOfHenchmen > 0)
                .Select(player => new PlayerHenchmenPaid(player));

            var henchmenQuittings = gameState.Players
                .Where(player => player.State.Money <= 0 && player.State.WorkforceState.NumberOfHenchmen > 0)
                .Select(player => new PlayerHenchmenQuit(player, player.State.WorkforceState.NumberOfHenchmen));

            var allUpdates = new List<RoundUpdateResultItem>()
                .Concat(henchmenPayments)
                .Concat(henchmenQuittings);

            return new RoundUpdateResult(gameState.CurrentRound, allUpdates);
        }
    }
}
