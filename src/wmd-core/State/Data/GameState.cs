using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data.Governments;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Data;

/// <summary>
/// Represents the current state of the game.
/// </summary>
public record GameState
{
    private const int IndexNotFound = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameState"/> class.
    /// </summary>
    /// <param name="players">The list of players to include in this game.</param>
    /// <param name="planet">The planet where this game is taking place.</param>
    public GameState([DisallowNull] IList<Player> players, [DisallowNull] Planet planet)
    {
        Players = new List<Player>(players).AsReadOnly();
        long numberOfSoldiers = (long)Math.Floor(planet.NeutralPopulation * GovernmentConstants.InitialPercentageOfPlanetPopulationInGovernmentArmy);
        Planet = planet with { NeutralPopulation = planet.NeutralPopulation - numberOfSoldiers };
        GovernmentState = new GovernmentState() with { NumberOfSoldiers = numberOfSoldiers };
        CurrentRound = 1;
        CurrentPlayerIndex = 0;
    }

    /// <summary>
    /// Gets the list of players in this game.
    /// </summary>
    public IReadOnlyList<Player> Players { get; init; }

    /// <summary>
    /// Gets the current game round.
    /// </summary>
    public int CurrentRound { get; init; }

    /// <summary>
    /// Gets the current <see cref="Player"/> whose turn it is.
    /// </summary>
    public Player CurrentPlayer { get => Players[CurrentPlayerIndex]; }

    /// <summary>
    /// Gets the index of the current <see cref="Player"/> whose turn it is.
    /// </summary>
    public int CurrentPlayerIndex { get; init; }

    /// <summary>
    /// Gets or initializes the current government state.
    /// </summary>
    public GovernmentState GovernmentState { get; init; }

    /// <summary>
    /// Gets the <see cref="Planet"/> where this game is taking place.
    /// </summary>
    public Planet Planet { get; init; }

    /// <summary>
    /// Gets the current price per square kilometer of unclaimed land.
    /// </summary>
    public decimal UnclaimedLandPurchasePrice => LandConstants.LandBasePrice + LandPriceIncreaseFromScarcity;

    /// <summary>
    /// Determines whether the game has been won yet.
    /// </summary>
    /// <param name="winningPlayerIndex">An output parameter indicating the index of the winning <see cref="Player"/> if the game has been won yet.</param>
    /// <returns><see langword="true"/> if the game has been won; otherwise, <see langword="false"/>.</returns>
    public bool GameHasBeenWon(out int winningPlayerIndex)
    {
        winningPlayerIndex = FindIndexOfWinningPlayer();
        return winningPlayerIndex != IndexNotFound;
    }

    /// <summary>
    /// Determines whether the government has been defeated.
    /// </summary>
    /// <returns><see langword="true"/> if there are no soldiers left in the government army; otherwise, <see langword="false"/>.</returns>
    public bool GovernmentDefeated { get => GovernmentState.NumberOfSoldiers == 0; }

    private decimal LandPriceIncreaseFromScarcity => (decimal)Math.Round((double)LandConstants.MaxLandPriceIncreaseFromScarcity * Planet.PercentageOfLandClaimed, 2);

    private int FindIndexOfLastRemainingPlayer()
    {
        var remainingPlayerIndex = -1;

        for (var i = 0; i < Players.Count; i++)
        {
            if (!Players[i].State.HasResigned)
            {
                if (remainingPlayerIndex >= 0)
                {
                    return IndexNotFound;
                }

                remainingPlayerIndex = i;
            }
        }

        return remainingPlayerIndex;
    }

    private int FindIndexOfWinningPlayer()
    {
        if (GameHasOnePlayerLeft(out var remainingPlayerIndex))
        {
            return remainingPlayerIndex;
        }

        for (var i = 0; i < Players.Count; i++)
        {
            if (Players[i].State.Land == Planet.TotalLandArea)
            {
                return i;
            }
        }

        return IndexNotFound;
    }

    private bool GameHasOnePlayerLeft(out int remainingPlayerIndex)
    {
        remainingPlayerIndex = FindIndexOfLastRemainingPlayer();
        return remainingPlayerIndex != IndexNotFound;
    }
}
