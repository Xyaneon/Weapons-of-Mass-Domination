using WMD.Game.State.Data;
using WMD.Game.State.Utility.AttackCalculations;

namespace WMD.Game.State.Updates.Rounds;

/// <summary>
/// An occurrence of a government attacking a player.
/// </summary>
public record GovernmentAttacksPlayer : GovernmentIntervention
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GovernmentAttacksPlayer"/> class.
    /// </summary>
    /// <param name="gameState">The <see cref="GameState"/>.</param>
    /// <param name="playerIndex">The index of the player who was attacked.</param>
    /// <param name="attackCombatantsChanges">The numbers of combatants involved and lost in the attack.</param>
    public GovernmentAttacksPlayer(GameState gameState, int playerIndex, AttackCombatantsChanges attackCombatantsChanges)
    {
        PlayerIndex = playerIndex;
        AttackCombatantsChanges = attackCombatantsChanges;
    }

    /// <summary>
    /// Gets the numbers of combatants involved and lost in the attack.
    /// </summary>
    public AttackCombatantsChanges AttackCombatantsChanges { get; init; }

    /// <summary>
    /// Gets the index of the player who was attacked.
    /// </summary>
    public int PlayerIndex { get; init; }
}
