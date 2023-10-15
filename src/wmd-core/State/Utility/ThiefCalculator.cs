using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Utility;

/// <summary>
/// Provides methods for performing thief-related calculations.
/// </summary>
public static class ThiefCalculator
{
    private static readonly Random _random;

    static ThiefCalculator()
    {
        _random = new Random();
    }

    /// <summary>
    /// Calculates the amount of money the player will make this turn from their thieves.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="playerIndex">The index of the player whose thieves stole money.</param>
    /// <returns>The amount of money the player will make this turn from their thieves.</returns>
    public static decimal CalculateMoneyStolenByThieves([DisallowNull] GameState gameState, int playerIndex)
    {
        PlayerState playerState = gameState.Players[playerIndex].State;
        decimal payRate = playerState.WorkforceState.DailyPayRate;
        long numberOfThieves = playerState.WorkforceState.ThiefCount;
        int secretBaseLevel = playerState.SecretBase?.Level ?? 0;

        decimal amountStolenByEachThief = Math.Max(payRate / 2 + Math.Round((decimal) (_random.NextDouble() * (50 + 10 * secretBaseLevel)), 2), 0);

        return numberOfThieves * amountStolenByEachThief;
    }
}
