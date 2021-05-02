using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility;

namespace WMD.Game.State.Updates
{
    internal static class GameStateUpdater
    {
        private const string ArgumentOfOfRangeException_landToGiveUp_cannotBeNegative = "The amount of land to have a player give up cannot be negative.";
        private const string ArgumentOutOfRangeException_landToGiveUp_cannotExceedAmountInPossession = "The amount of land to have a player give up cannot exceed the actual amount they have.";
        private const string ArgumentOutOfRangeException_playerIndex_outOfBounds = "The player index is out of bounds.";
        private const string ArgumentOutOfRangeException_unclaimedLandToGive_cannotBeNegative = "The amount of unclaimed land to give to a player cannot be negative.";
        private const string ArgumentOutOfRangeException_unclaimedLandToGive_cannotExceedAmountLeft = "The amount of unclaimed land to give to a player cannot exceed the actual amount left.";
        private const string InvalidOperationException_playerDoesNotHaveASecretBaseToLevelUp = "The player does not have a secret base to level up.";
        private const string InvalidOperationException_playerHenchmenQuantity_cannotBeNegative = "The number of henchmen a player has cannot become negative.";
        private const string InvalidOperationException_playerNukesQuantity_cannotBeNegative = "The player cannot have a negative quantity of nukes.";
        private const string InvalidOperationException_playerNukesResearch_alreadyMaxedOut = "The player has already maxed out their nukes research.";
        private const string InvalidOperationException_reputationPercentage_wouldBeAboveMaximum = "The player cannot have a reputation percentage above 100%.";
        private const string InvalidOperationException_neutralPopulation_wouldCreateInvalidPlanetState_formatString = "Planet state after population adjustment would be invalid: {0}";
        private const string InvalidOperationException_unclaimedLandAdjustment_wouldCreateInvalidPlanetState_formatString = "Planet state after unclaimed land area adjustment would be invalid: {0}";

        public static GameState AdjustPlayerStatesAfterAttack(GameState gameState, int attackerIndex, int defenderIndex, AttackCalculationsResult calculationsResult)
        {
            GameState updatedGameState = AdjustHenchmenForPlayer(gameState, attackerIndex, -1 * calculationsResult.HenchmenAttackerLost);
            updatedGameState = AdjustHenchmenForPlayer(updatedGameState, defenderIndex, -1 * calculationsResult.HenchmenDefenderLost);
            updatedGameState = AdjustReputationForPlayer(updatedGameState, attackerIndex, calculationsResult.ReputationChangeForAttacker);
            updatedGameState = AdjustReputationForPlayer(updatedGameState, defenderIndex, calculationsResult.ReputationChangeForDefender);

            return updatedGameState;
        }

        public static GameState GiveUnclaimedLandToPlayer(GameState gameState, int playerIndex, int area)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), ArgumentOutOfRangeException_unclaimedLandToGive_cannotBeNegative);
            }

            if (area > gameState.Planet.UnclaimedLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(area), ArgumentOutOfRangeException_unclaimedLandToGive_cannotExceedAmountLeft);
            }

            var gameStateWithAdjustedUnclaimedLand = AdjustUnclaimedLandArea(gameState, -1 * area);
            var playerState = gameStateWithAdjustedUnclaimedLand.Players[playerIndex].State;
            var updatedPlayerState = playerState with { Land = playerState.Land + area };

            return UpdatePlayerState(gameStateWithAdjustedUnclaimedLand, playerIndex, updatedPlayerState);
        }

        public static GameState HavePlayerGiveUpLand(GameState gameState, int playerIndex, int area)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            if (area < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(area), ArgumentOfOfRangeException_landToGiveUp_cannotBeNegative);
            }

            if (area > gameState.Players[playerIndex].State.Land)
            {
                throw new ArgumentOutOfRangeException(nameof(area), ArgumentOutOfRangeException_landToGiveUp_cannotExceedAmountInPossession);
            }

            var gameStateWithAdjustedUnclaimedLand = AdjustUnclaimedLandArea(gameState, area);
            var currentPlayerState = gameStateWithAdjustedUnclaimedLand.Players[playerIndex].State;
            var updatedPlayerState = currentPlayerState with { Land = currentPlayerState.Land - area };

            return UpdatePlayerState(gameStateWithAdjustedUnclaimedLand, playerIndex, updatedPlayerState);
        }

        public static GameState IncrementPlayerNukesResearchLevel(GameState gameState, int playerIndex)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            if (gameState.Players[playerIndex].State.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel)
            {
                throw new InvalidOperationException(InvalidOperationException_playerNukesResearch_alreadyMaxedOut);
            }

            var currentPlayerState = gameState.Players[playerIndex].State;
            var updatedResearchState = currentPlayerState.ResearchState with { NukeResearchLevel = currentPlayerState.ResearchState.NukeResearchLevel + 1 };
            var updatedPlayerState = currentPlayerState with { ResearchState = updatedResearchState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustMoneyForPlayer(GameState gameState, int playerIndex, decimal adjustmentAmount)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            var currentPlayerState = gameState.Players[playerIndex].State;
            var updatedPlayerState = currentPlayerState with { Money = currentPlayerState.Money + adjustmentAmount };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustNukesForPlayer(GameState gameState, int playerIndex, int adjustmentAmount)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            var currentPlayerState = gameState.Players[playerIndex].State;
            int updatedNukesQuantity = currentPlayerState.Nukes + adjustmentAmount;

            if (updatedNukesQuantity < 0)
            {
                throw new InvalidOperationException(InvalidOperationException_playerNukesQuantity_cannotBeNegative);
            }

            var updatedPlayerState = currentPlayerState with { Nukes = updatedNukesQuantity };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustHenchmenForPlayer(GameState gameState, int playerIndex, int adjustmentAmount)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            var currentPlayerState = gameState.Players[playerIndex].State;
            var currentWorkforceState = currentPlayerState.WorkforceState;

            var updatedHenchmenAmount = currentWorkforceState.NumberOfHenchmen + adjustmentAmount;
            if (updatedHenchmenAmount < 0)
            {
                throw new InvalidOperationException(InvalidOperationException_playerHenchmenQuantity_cannotBeNegative);
            }

            var updatedWorkforceState = currentWorkforceState with { NumberOfHenchmen = updatedHenchmenAmount };
            var updatedPlayerState = currentPlayerState with { WorkforceState = updatedWorkforceState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustReputationForPlayer(GameState gameState, int playerIndex, int adjustmentPercentage)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            var currentPlayerState = gameState.Players[playerIndex].State;
            int updatedReputationPercentage = currentPlayerState.ReputationPercentage + adjustmentPercentage;

            if (updatedReputationPercentage > 100)
            {
                throw new InvalidOperationException(InvalidOperationException_reputationPercentage_wouldBeAboveMaximum);
            }

            var updatedPlayerState = currentPlayerState with { ReputationPercentage = updatedReputationPercentage };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState AdjustUnclaimedLandArea(GameState gameState, int adjustmentAmount)
        {
            var currentPlanetState = gameState.Planet;
            Planet updatedPlanetState;

            try
            {
                updatedPlanetState = currentPlanetState with { UnclaimedLandArea = currentPlanetState.UnclaimedLandArea + adjustmentAmount };
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException(string.Format(InvalidOperationException_unclaimedLandAdjustment_wouldCreateInvalidPlanetState_formatString, ex.Message), ex);
            }

            return UpdatePlanetState(gameState, updatedPlanetState);
        }

        public static GameState AdjustNeutralPopulation(GameState gameState, long adjustmentAmount)
        {
            var currentPlanetState = gameState.Planet;
            Planet updatedPlanetState;

            try
            {
                updatedPlanetState = currentPlanetState with { NeutralPopulation = currentPlanetState.NeutralPopulation + adjustmentAmount };
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException(string.Format(InvalidOperationException_neutralPopulation_wouldCreateInvalidPlanetState_formatString, ex.Message), ex);
            }

            return UpdatePlanetState(gameState, updatedPlanetState);
        }

        public static GameState IncrementSecretBaseLevel(GameState gameState, int playerIndex)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            var currentPlayerState = gameState.Players[playerIndex].State;
            var currentSecretBase = currentPlayerState.SecretBase ?? throw new InvalidOperationException(InvalidOperationException_playerDoesNotHaveASecretBaseToLevelUp);
            var updatedSecretBase = currentSecretBase with { Level = currentSecretBase.Level + 1 };
            var updatedPlayerState = currentPlayerState with { SecretBase = updatedSecretBase };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState SetDailyWageForPlayer(GameState gameState, int playerIndex, decimal dailyWage)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

            PlayerState currentPlayerState = gameState.Players[playerIndex].State;
            WorkforceState currentWorkforceState = currentPlayerState.WorkforceState;
            WorkforceState updatedWorkforceState = currentWorkforceState with { DailyPayRate = dailyWage };
            PlayerState updatedPlayerState = currentPlayerState with { WorkforceState = updatedWorkforceState };

            return UpdatePlayerState(gameState, playerIndex, updatedPlayerState);
        }

        public static GameState UpdatePlanetState(GameState gameState, Planet planet) =>
            gameState with { Planet = planet };

        public static GameState UpdatePlayerState(GameState gameState, int playerIndex, PlayerState playerState)
        {
            ThrowIfPlayerIndexIsOutOfBounds(gameState, nameof(playerIndex), playerIndex);

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

        private static void ThrowIfPlayerIndexIsOutOfBounds([DisallowNull] GameState gameState, string nameOfPlayerIndexArgument, int playerIndexArgumentValue)
        {
            if (!GameStateChecks.PlayerIndexIsInBounds(gameState, playerIndexArgumentValue))
            {
                throw new ArgumentOutOfRangeException(nameOfPlayerIndexArgument, playerIndexArgumentValue, ArgumentOutOfRangeException_playerIndex_outOfBounds);
            }
        }
    }
}
