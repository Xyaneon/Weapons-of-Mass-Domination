using System;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility.AttackCalculations;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player attacking another player.
/// </summary>
public record AttackPlayerResult : CommandResult
{
    private const string ArgumentOutOfRangeException_henchmenAttackerLost = "The number of henchmen the attacker lost cannot be less than zero.";
    private const string ArgumentOutOfRangeException_henchmenDefenderLost = "The number of henchmen the defender lost cannot be less than zero.";
    private const string ArgumentOutOfRangeException_numberOfAttackingHenchmen = "The number of henchmen used to attack cannot be less than one.";
    private const string ArgumentOutOfRangeException_targetPlayerIndex = "The index of the attacked player cannot be less than zero.";

    /// <summary>
    /// Initializes a new instance of the <see cref="AttackPlayerResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="targetPlayerIndex">The index of the player who was attacked.</param>
    /// <param name="numberOfAttackingHenchmen">The number of henchmen used by the attacker.</param>
    /// <param name="henchmenAttackerLost">The number of henchmen the attacker lost.</param>
    /// <param name="henchmenDefenderLost">The number of henchmen the defender lost.</param>
    /// <param name="landAreaChangeForDefender">The amount of land area lost by the defender.</param>
    /// <param name="reputationChangeForAttacker">The amount by which the attacker's reputation changed because of the attack.</param>
    /// <param name="reputationChangeForDefender">The amount by which the defender's reputation changed because of the attack.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="targetPlayerIndex"/> is less than zero.
    /// -or-
    /// <paramref name="numberOfAttackingHenchmen"/> is less than one.
    /// -or-
    /// <paramref name="henchmenAttackerLost"/> is less than zero.
    /// -or-
    /// <paramref name="henchmenDefenderLost"/> is less than zero.
    /// </exception>
    public AttackPlayerResult(
        GameState updatedGameState,
        int playerIndex,
        int targetPlayerIndex,
        long numberOfAttackingHenchmen,
        int henchmenAttackerLost,
        int henchmenDefenderLost,
        int landAreaChangeForDefender,
        int reputationChangeForAttacker,
        int reputationChangeForDefender
    ) : base(updatedGameState, playerIndex)
    {
        if (targetPlayerIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetPlayerIndex), targetPlayerIndex, ArgumentOutOfRangeException_targetPlayerIndex);
        }

        if (numberOfAttackingHenchmen < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfAttackingHenchmen), numberOfAttackingHenchmen, ArgumentOutOfRangeException_numberOfAttackingHenchmen);
        }

        if (henchmenAttackerLost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(henchmenAttackerLost), henchmenAttackerLost, ArgumentOutOfRangeException_henchmenAttackerLost);
        }

        if (henchmenDefenderLost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(henchmenDefenderLost), henchmenDefenderLost, ArgumentOutOfRangeException_henchmenDefenderLost);
        }

        HenchmenAttackerLost = henchmenAttackerLost;
        HenchmenDefenderLost = henchmenDefenderLost;
        LandAreaChangeForDefender = landAreaChangeForDefender;
        NumberOfAttackingHenchmen = numberOfAttackingHenchmen;
        ReputationChangeForAttacker = reputationChangeForAttacker;
        ReputationChangeForDefender = reputationChangeForDefender;
        TargetPlayerIndex = targetPlayerIndex;
    }

    internal AttackPlayerResult(GameState updatedGameState, int playerIndex, int targetPlayerIndex, long numberOfAttackingHenchmen, PlayerOnPlayerAttackCalculationsResult calculationsResult)
        : this(
              updatedGameState,
              playerIndex,
              targetPlayerIndex,
              numberOfAttackingHenchmen,
              calculationsResult.HenchmenAttackerLost,
              calculationsResult.HenchmenDefenderLost,
              calculationsResult.LandAreaChangeForDefender,
              calculationsResult.ReputationChangeForAttacker,
              calculationsResult.ReputationChangeForDefender
    )
    { }

    /// <summary>
    /// Gets the number of henchmen the attacker lost.
    /// </summary>
    public int HenchmenAttackerLost { get; init; }

    /// <summary>
    /// Gets the number of henchmen the defender lost.
    /// </summary>
    public int HenchmenDefenderLost { get; init; }

    /// <summary>
    /// Gets the land area that the defender lost.
    /// </summary>
    public int LandAreaChangeForDefender { get; init; }

    /// <summary>
    /// Gets the number of henchmen the attacker used.
    /// </summary>
    public long NumberOfAttackingHenchmen { get; init; }

    /// <summary>
    /// Gets the amount by which the attacker's reputation changed because of the attack.
    /// </summary>
    public int ReputationChangeForAttacker { get; init; }

    /// <summary>
    /// Gets the amount by which the defender's reputation changed because of the attack.
    /// </summary>
    public int ReputationChangeForDefender { get; init; }

    /// <summary>
    /// Gets the index of the player who was attacked.
    /// </summary>
    public int TargetPlayerIndex { get; init; }

    /// <summary>
    /// Gets the name of the player who was attacked.
    /// </summary>
    public string TargetPlayerName { get => UpdatedGameState.Players[TargetPlayerIndex].Identification.Name; }
}
