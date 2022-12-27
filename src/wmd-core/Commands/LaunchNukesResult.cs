using System;
using WMD.Game.State.Data;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player launching nukes.
/// </summary>
public record LaunchNukesResult : CommandResult
{
    private const string ArgumentOutOfRangeException_nukesLaunched = "The number of nukes launched cannot be less than one.";
    private const string ArgumentOutOfRangeException_successfulNukeHits_lessThanZero = "The number of successful nuke hits cannot be less than zero.";
    private const string ArgumentOutOfRangeException_successfulNukeHits_greaterThanNukesLaunched = "The number of successful nuke hits cannot be greater than the number of nukes launched.";
    private const string ArgumentOutOfRangeException_henchmenDefenderLost = "The number of henchmen the defender lost cannot be less than zero.";
    private const string ArgumentOutOfRangeException_targetPlayerIndex = "The index of the attacked player cannot be less than zero.";

    /// <summary>
    /// Initializes a new instance of the <see cref="LaunchNukesResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="targetPlayerIndex">The index of the player who was attacked.</param>
    /// <param name="nukesLaunched">The number of nukes launched.</param>
    /// <param name="successfulNukeHits">The number of successful nuke hits.</param>
    /// <param name="henchmenDefenderLost">The number of henchmen the defender lost.</param>
    /// <param name="reputationChangeAmount">The amount by which the attacker's reputation changed.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="targetPlayerIndex"/> is less than zero.
    /// -or-
    /// <paramref name="nukesLaunched"/> is less than one.
    /// -or-
    /// <paramref name="successfulNukeHits"/> is less than zero.
    /// -or-
    /// <paramref name="successfulNukeHits"/> is greater than <paramref name="nukesLaunched"/>.
    /// -or-
    /// <paramref name="henchmenDefenderLost"/> is less than zero.
    /// </exception>
    public LaunchNukesResult(
        GameState updatedGameState,
        int playerIndex,
        int targetPlayerIndex,
        int nukesLaunched,
        int successfulNukeHits,
        long henchmenDefenderLost,
        int reputationChangeAmount)
        : base(updatedGameState, playerIndex)
    {
        if (targetPlayerIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetPlayerIndex), targetPlayerIndex, ArgumentOutOfRangeException_targetPlayerIndex);
        }

        if (nukesLaunched < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nukesLaunched), nukesLaunched, ArgumentOutOfRangeException_nukesLaunched);
        }

        if (successfulNukeHits < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(successfulNukeHits), successfulNukeHits, ArgumentOutOfRangeException_successfulNukeHits_lessThanZero);
        }

        if (successfulNukeHits > nukesLaunched)
        {
            throw new ArgumentOutOfRangeException(nameof(successfulNukeHits), successfulNukeHits, ArgumentOutOfRangeException_successfulNukeHits_greaterThanNukesLaunched);
        }

        if (henchmenDefenderLost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(henchmenDefenderLost), henchmenDefenderLost, ArgumentOutOfRangeException_henchmenDefenderLost);
        }

        NukesLaunched = nukesLaunched;
        SuccessfulNukeHits = successfulNukeHits;
        HenchmenDefenderLost = henchmenDefenderLost;
        TargetPlayerIndex = targetPlayerIndex;
        ReputationChangeAmount = reputationChangeAmount;
    }

    /// <summary>
    /// Gets the number of nukes launched.
    /// </summary>
    public int NukesLaunched { get; init; }

    /// <summary>
    /// Gets the number of henchmen the defender lost.
    /// </summary>
    public long HenchmenDefenderLost { get; init; }

    /// <summary>
    /// Gets the amount by which the attacker's reputation changed.
    /// </summary>
    public int ReputationChangeAmount { get; init; }

    /// <summary>
    /// Gets the number of nukes which successfully hit.
    /// </summary>
    public int SuccessfulNukeHits { get; init; }

    /// <summary>
    /// Gets the index of the player who was attacked.
    /// </summary>
    public int TargetPlayerIndex { get; init; }

    /// <summary>
    /// Gets the name of the player who was attacked.
    /// </summary>
    public string TargetPlayerName { get => UpdatedGameState.Players[TargetPlayerIndex].Identification.Name; }
}
