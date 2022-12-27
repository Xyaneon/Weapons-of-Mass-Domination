using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Utility;

/// <summary>
/// Provides utlity methods for checking the game state.
/// </summary>
public static class GameStateChecks
{
    /// <summary>
    /// Initializes static members of the <see cref="GameStateChecks"/> class.
    /// </summary>
    static GameStateChecks() => _random = new Random();

    /// <summary>
    /// Determines whether the current player could purchase any unclaimed land with their current funds.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player could purchase any unclaimed land with their current funds; otherwise, <see langword="false"/>.</returns>
    /// <remarks>This method does not take into account the actual amount of remaining land area available for purchase.</remarks>
    public static bool CurrentPlayerCouldPurchaseLand([DisallowNull] GameState gameState) =>
        LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState) > 0;

    /// <summary>
    /// Determines whether the current player has a secret base.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player has a secret base; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasASecretBase([DisallowNull] GameState gameState) =>
        gameState.CurrentPlayer.State.SecretBase != null;

    /// <summary>
    /// Determines whether the current player has any henchmen, regardless of specialization.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player has any henchmen; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasAnyHenchmen([DisallowNull] GameState gameState) =>
        gameState.CurrentPlayer.State.WorkforceState.TotalHenchmenCount > 0;

    /// <summary>
    /// Determines whether the current player has any henchmen of a given specialization.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="specialization">The specialization of henchmen to check for.</param>
    /// <returns><see langword="true"/> if the current player has any henchmen of the matching specialization; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasAnyHenchmen([DisallowNull] GameState gameState, HenchmenSpecialization specialization) => specialization switch
    {
        HenchmenSpecialization.Untrained => gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount > 0,
        HenchmenSpecialization.Soldier => gameState.CurrentPlayer.State.WorkforceState.SoldierCount > 0,
        _ => throw new ArgumentOutOfRangeException(nameof(specialization), $"Unsupported specialization value: {specialization}"),
    };

    /// <summary>
    /// Determines whether the current player has any nukes in their inventory.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player has any nukes in their inventory; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasAnyNukes([DisallowNull] GameState gameState) =>
        gameState.CurrentPlayer.State.Nukes > 0;

    /// <summary>
    /// Determines whether the current player has completed their nukes research.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player has completed their nukes research; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasCompletedNukesResearch([DisallowNull] GameState gameState) =>
        gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel >= NukeConstants.MaxNukeResearchLevel;

    /// <summary>
    /// Determines whether the current player has no money.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the current player has no money; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerHasNoMoney([DisallowNull] GameState gameState) =>
        gameState.CurrentPlayer.State.Money <= 0;

    /// <summary>
    /// Determines whether the current player is attacking themselves.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="targetPlayerIndex">The index of the player being attacked.</param>
    /// <returns><see langword="true"/> if the current player is attacking themselves; otherwise, <see langword="false"/>.</returns>
    public static bool CurrentPlayerIsAttackingThemselves([DisallowNull] GameState gameState, int targetPlayerIndex) =>
        gameState.CurrentPlayerIndex == targetPlayerIndex;

    /// <summary>
    /// Determines whether the provided <paramref name="playerIndex"/> is in bounds.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="targetPlayerIndex">The index of the player.</param>
    /// <returns><see langword="true"/> if the player index is in bounds; otherwise, <see langword="false"/>.</returns>
    public static bool PlayerIndexIsInBounds([DisallowNull] GameState gameState, int playerIndex) =>
        playerIndex >= 0 && playerIndex < gameState.Players.Count;

    /// <summary>
    /// Gets a random player index other than that of the current player.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A random player index other than that of the current player.</returns>
    public static int SelectRandomNonCurrentPlayerIndex([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersOtherThanCurrent(gameState).OrderBy(_ => _random.NextDouble()).First();

    /// <summary>
    /// Returns a collection of indices of all players other than the current player.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A collection of indices of all players other than the current player.</returns>
    public static IEnumerable<int> FindIndicesOfPlayersOtherThanCurrent([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersOtherThanGiven(gameState, gameState.CurrentPlayerIndex);

    /// <summary>
    /// Returns a collection of indices of all players other than the one to exclude.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <param name="indexToExclude">The index to exclude from the results.</param>
    /// <returns>A collection of indices of all players other than <paramref name="indexToExclude"/>.</returns>
    public static IEnumerable<int> FindIndicesOfPlayersOtherThanGiven([DisallowNull] GameState gameState, int indexToExclude) =>
        Enumerable.Range(0, gameState.Players.Count).Where(index => index != indexToExclude);

    /// <summary>
    /// Returns a collection of indices of the players with the highest pay rate.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A collection of indices of the players with the highest pay rate.</returns>
    /// <remarks>The collection returned will be empty if all players pay nothing.</remarks>
    public static IEnumerable<int> FindIndicesOfPlayersWithTheHighestPayRate([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersWithLargestQuantity(gameState, playerState => (double)playerState.WorkforceState.DailyPayRate);

    /// <summary>
    /// Returns a collection of indices of the players with the most land area.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A collection of indices of the players with the most land area.</returns>
    /// <remarks>The collection returned will be empty if all players have no land.</remarks>
    public static IEnumerable<int> FindIndicesOfPlayersWithTheMostLand([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersWithLargestQuantity(gameState, playerState => playerState.Land);

    /// <summary>
    /// Returns a collection of indices of the players with the most money.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A collection of indices of the players with the most money.</returns>
    /// <remarks>The collection returned will be empty if all players have no money.</remarks>
    public static IEnumerable<int> FindIndicesOfPlayersWithTheMostMoney([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersWithLargestQuantity(gameState, playerState => (double)playerState.Money);

    /// <summary>
    /// Returns a collection of indices of the players with the most henchmen.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A collection of indices of the players with the most henchmen.</returns>
    /// <remarks>The collection returned will be empty if all players have no henchmen.</remarks>
    public static IEnumerable<int> FindIndicesOfPlayersWithTheMostHenchmen([DisallowNull] GameState gameState) =>
        FindIndicesOfPlayersWithLargestQuantity(gameState, playerState => playerState.WorkforceState.TotalHenchmenCount);

    /// <summary>
    /// Determines whether the planet's neutral population has been depleted.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns><see langword="true"/> if the planet's neutral population has been depleted; otherwise, <see langword="false"/>.</returns>
    public static bool NeutralPopulationIsDepleted([DisallowNull] GameState gameState) =>
        gameState.Planet.NeutralPopulation == 0;

    private static IEnumerable<int> FindIndicesOfPlayersWithLargestQuantity([DisallowNull] GameState gameState, Func<PlayerState, double> quantityFunction)
    {
        int numberOfPlayers = gameState.Players.Count;
        double largestQuantity = 0;
        var playerIndices = new Queue<int>(numberOfPlayers);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            double quantity = quantityFunction(gameState.Players[i].State);
            if (quantity > 0)
            {
                if (quantity == largestQuantity)
                {
                    playerIndices.Enqueue(i);
                }
                else if (quantity > largestQuantity)
                {
                    largestQuantity = quantity;
                    playerIndices = new Queue<int>(numberOfPlayers - i);
                    playerIndices.Enqueue(i);
                }
            }
        }

        return playerIndices;
    }

    private static readonly Random _random;
}
