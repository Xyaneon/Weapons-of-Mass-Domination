using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

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
        decimal payRate = gameState.Players[playerIndex].State.WorkforceState.DailyPayRate;
        long numberOfThieves = gameState.Players[playerIndex].State.WorkforceState.ThiefCount;
        decimal amountStolenByEachThief = Math.Max(payRate + Math.Round((decimal) -1 + _random.Next() * 3, 2), 0);

        return numberOfThieves * amountStolenByEachThief;
    }
}
