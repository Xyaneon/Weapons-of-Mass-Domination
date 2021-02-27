using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Updates
{
    internal static class GameStateUpdater
    {
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

        public static GameState IncrementPlayerNukesResearchLevel([DisallowNull] GameState gameState, int playerIndex)
        {
            if (gameState.Players[playerIndex].State.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel)
            {
                throw new InvalidOperationException("The player has already maxed out their nukes research.");
            }

            var currentPlayerState = gameState.Players[playerIndex].State;
            var updatedResearchState = currentPlayerState.ResearchState with { NukeResearchLevel = currentPlayerState.ResearchState.NukeResearchLevel + 1 };
            var updatedPlayerState = currentPlayerState with { ResearchState = updatedResearchState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustMoneyForPlayer([DisallowNull] GameState gameState, int playerIndex, decimal adjustmentAmount)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            var updatedPlayerState = currentPlayerState with { Money = currentPlayerState.Money + adjustmentAmount };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustNukesForPlayer([DisallowNull] GameState gameState, int playerIndex, int adjustmentAmount)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            int updatedNukesQuantity = currentPlayerState.Nukes + adjustmentAmount;

            if (updatedNukesQuantity < 0)
            {
                throw new InvalidOperationException("The player cannot have a negative quantity of nukes.");
            }

            var updatedPlayerState = currentPlayerState with { Nukes = updatedNukesQuantity };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

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

            return UpdatePlanetState(gameState, updatedPlanetState);
        }

        public static GameState IncrementSecretBaseLevel([DisallowNull] GameState gameState, int playerIndex)
        {
            var currentPlayerState = gameState.Players[playerIndex].State;
            var currentSecretBase = currentPlayerState.SecretBase ?? throw new InvalidOperationException("The player does not have a secret base to level up.");
            var updatedSecretBase = currentSecretBase with { Level = currentSecretBase.Level + 1 };
            var updatedPlayerState = currentPlayerState with { SecretBase = updatedSecretBase };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState UpdatePlanetState([DisallowNull] GameState gameState, [DisallowNull] Planet planet)
        {
            return gameState with { Planet = planet };
        }

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
