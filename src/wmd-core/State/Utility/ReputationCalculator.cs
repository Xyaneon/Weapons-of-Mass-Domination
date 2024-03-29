﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Utility;

/// <summary>
/// Provides methods for performing propaganda- and reputation-related calculations.
/// </summary>
public static class ReputationCalculator
{
    private const string ArgumentOutOfRangeException_playerIndex_outOfBounds = "The index of the player to calculate for was not in bounds.";

    /// <summary>
    /// Initializes static members of the <see cref="ReputationCalculator"/> class.
    /// </summary>
    static ReputationCalculator()
    {
        _random = new Random();
    }

    /// <summary>
    /// Calculates the base reputation percentage a given player will naturally tend towards based on their current state.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="playerIndex">The index of the player to calculate for.</param>
    /// <returns>The player's current base reputation percentage.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="playerIndex"/> is out of range for the provided <paramref name="gameState"/>.
    /// </exception>
    /// <remarks>
    /// A player's base reputation percentage is affected by the following factors:
    /// <list type="bullet">
    /// <item>Whether the player has a secret base, and how far they have leveled it up</item>
    /// <item>Whether the player owns the most land area</item>
    /// <item>Whether the player has the most money</item>
    /// <item>Whether the player has the highest pay rate for their henchmen</item>
    /// <item>Whether the player has the most henchmen</item>
    /// <item>The player's current nukes research level</item>
    /// <item>How many nukes the player currently has in their stockpile</item>
    /// </list>
    /// </remarks>
    public static int CalculateBaseReputationPercentage([DisallowNull] GameState gameState, int playerIndex)
    {
        if (!GameStateChecks.PlayerIndexIsInBounds(gameState, playerIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, ArgumentOutOfRangeException_playerIndex_outOfBounds);
        }

        PlayerState playerState = gameState.Players[playerIndex].State;

        int percentageAmountFromSecretBase = playerState.SecretBase != null ? 10 + Math.Min(playerState.SecretBase.Level, 10) : 0;
        int percentageAmountFromMostLand = GameStateChecks.FindIndicesOfPlayersWithTheMostLand(gameState).Contains(playerIndex) ? 5 : 0;
        int percentageAmountFromMostMoney = GameStateChecks.FindIndicesOfPlayersWithTheMostMoney(gameState).Contains(playerIndex) ? 5 : 0;
        int percentageAmountFromHighestPayRate = GameStateChecks.FindIndicesOfPlayersWithTheHighestPayRate(gameState).Contains(playerIndex) ? 5 : 0;
        int percentageAmountFromMostHenchmen = GameStateChecks.FindIndicesOfPlayersWithTheMostHenchmen(gameState).Contains(playerIndex) ? 5 : 0;
        int percentageAmountFromNukeResearch = playerState.ResearchState.NukeResearchLevel;
        int percentageAmountFromNukes = playerState.Nukes > 0 ? 10 : 0;

        return percentageAmountFromSecretBase
            + percentageAmountFromMostLand
            + percentageAmountFromMostMoney
            + percentageAmountFromHighestPayRate
            + percentageAmountFromMostHenchmen
            + percentageAmountFromNukeResearch
            + percentageAmountFromNukes;
    }

    /// <summary>
    /// Calculates the amount of reputation a player gains by spending on propaganda.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="playerIndex">The index of the player spending on propaganda.</param>
    /// <param name="moneySpent">The amount of money the player is spending on propaganda.</param>
    /// <returns>The amount of reputation the player will gain.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="playerIndex"/> is out of range for the provided <paramref name="gameState"/>.
    /// </exception>
    public static int CalculateReputationGainedFromSpendingOnPropaganda([DisallowNull] GameState gameState, int playerIndex, decimal moneySpent)
    {
        if (!GameStateChecks.PlayerIndexIsInBounds(gameState, playerIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, ArgumentOutOfRangeException_playerIndex_outOfBounds);
        }

        int currentReputation = gameState.Players[playerIndex].State.ReputationPercentage;
        int maxReputationToBeGained = ReputationConstants.MaxReputationPercentage - currentReputation;

        double percentageOfEffectiveSpendingAmount = Math.Min((double)moneySpent / (double)ReputationConstants.MaxEffectiveSpendingAmountOnPropagandaDistribution, 1.0);
        int maxGainableReputationFromAmountSpent = (int)Math.Floor(percentageOfEffectiveSpendingAmount * ReputationConstants.MaxGainableReputationFromPropagandaDistribution);
        int actualReputationGained = (int)Math.Floor(_random.NextDouble() * maxGainableReputationFromAmountSpent);

        return Math.Min(actualReputationGained, maxReputationToBeGained);
    }

    /// <summary>
    /// Takes a potential change in reputation amount and returns an amount which would stay within the allowed bounds.
    /// </summary>
    /// <param name="potentialChangeAmount">The amount by which reputation could change if there was no limitation.</param>
    /// <param name="currentReputation">The player's reputation before the change.</param>
    /// <returns>
    /// <paramref name="potentialChangeAmount"/> if the resulting total would stay within the bounds indicated by
    /// <see cref="ReputationConstants.MinReputationPercentage"/> and <see cref="ReputationConstants.MaxReputationPercentage"/>;
    /// otherwise, the largest possible value with the same sign which would give such a total.
    /// </returns>
    public static int ClampReputationChangeAmount(int potentialChangeAmount, int currentReputation) => potentialChangeAmount switch
    {
        _ when potentialChangeAmount < 0 => Math.Max(potentialChangeAmount, ReputationConstants.MinReputationPercentage - currentReputation),
        _ when potentialChangeAmount > 0 => Math.Min(potentialChangeAmount, ReputationConstants.MaxReputationPercentage - currentReputation),
        _ => 0,
    };

    private static Random _random;
}
