using System;

namespace WMD.Game.State.Utility.AttackCalculations
{
    /// <summary>
    /// Contains information on how many combatants each side in an attack had and how many were lost.
    /// </summary>
    public record AttackCombatantsChanges
    {
        private const string ArgumentException_StartingCombatantsOnAttackingSide_LessThanCombatantsLostByAttacker = "The number of combatants lost by the attacker cannot be greater than the number they started with.";
        private const string ArgumentException_StartingCombatantsOnDefendingSide_LessThanCombatantsLostByDefender = "The number of combatants lost by the defender cannot be greater than the number they started with.";
        private const string ArgumentOutOfRangeException_CombatantsLostByAttacker_LessThanZero = "The number of combatants lost on the attacker's side cannot be less than zero.";
        private const string ArgumentOutOfRangeException_CombatantsLostByDefender_LessThanZero = "The number of combatants lost on the defender's side cannot be less than zero.";
        private const string ArgumentOutOfRangeException_StartingCombatantsOnAttackingSide_LessThanZero = "The number of starting combatants on the attacker's side cannot be less than zero.";
        private const string ArgumentOutOfRangeException_StartingCombatantsOnDefendingSide_LessThanZero = "The number of starting combatants on the defender's side cannot be less than zero.";

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackCombatantsChanges"/> class.
        /// </summary>
        /// <param name="startingCombatantsOnAttackingSide">The initial number of combatants on the attacker's side.</param>
        /// <param name="startingCombatantsOnDefendingSide">The initial number of combatants on the defender's side.</param>
        /// <param name="combatantsLostByAttacker">The number of combatants lost by the attacker.</param>
        /// <param name="combatantsLostByDefender">The number of combatants lost by the defender.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startingCombatantsOnAttackingSide"/> is less than zero.
        /// -or-
        /// <paramref name="startingCombatantsOnDefendingSide"/> is less than zero.
        /// -or-
        /// <paramref name="combatantsLostByAttacker"/> is less than zero.
        /// -or-
        /// <paramref name="combatantsLostByDefender"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="combatantsLostByAttacker"/> is greater than <paramref name="startingCombatantsOnAttackingSide"/>.
        /// -or-
        /// <paramref name="combatantsLostByDefender"/> is greater than <paramref name="startingCombatantsOnDefendingSide"/>.
        /// </exception>
        public AttackCombatantsChanges(long startingCombatantsOnAttackingSide, long startingCombatantsOnDefendingSide, long combatantsLostByAttacker, long combatantsLostByDefender)
        {
            if (startingCombatantsOnAttackingSide < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startingCombatantsOnAttackingSide), startingCombatantsOnAttackingSide, ArgumentOutOfRangeException_StartingCombatantsOnAttackingSide_LessThanZero);
            }

            if (startingCombatantsOnDefendingSide < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startingCombatantsOnDefendingSide), startingCombatantsOnDefendingSide, ArgumentOutOfRangeException_StartingCombatantsOnDefendingSide_LessThanZero);
            }

            if (combatantsLostByAttacker < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(combatantsLostByAttacker), combatantsLostByAttacker, ArgumentOutOfRangeException_CombatantsLostByAttacker_LessThanZero);
            }

            if (combatantsLostByDefender < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(combatantsLostByDefender), combatantsLostByDefender, ArgumentOutOfRangeException_CombatantsLostByDefender_LessThanZero);
            }

            if (startingCombatantsOnAttackingSide < combatantsLostByAttacker)
            {
                throw new ArgumentException(ArgumentException_StartingCombatantsOnAttackingSide_LessThanCombatantsLostByAttacker, nameof(combatantsLostByAttacker));
            }

            if (startingCombatantsOnDefendingSide < combatantsLostByDefender)
            {
                throw new ArgumentException(ArgumentException_StartingCombatantsOnDefendingSide_LessThanCombatantsLostByDefender, nameof(combatantsLostByDefender));
            }

            StartingCombatantsOnAttackingSide = startingCombatantsOnAttackingSide;
            StartingCombatantsOnDefendingSide = startingCombatantsOnDefendingSide;
            CombatantsLostByAttacker = combatantsLostByAttacker;
            CombatantsLostByDefender = combatantsLostByDefender;
        }

        /// <summary>
        /// Gets the number of combatants lost by the attacker.
        /// </summary>
        public long CombatantsLostByAttacker { get; private init; }

        /// <summary>
        /// Gets the number of combatants lost by the defender.
        /// </summary>
        public long CombatantsLostByDefender { get; private init; }

        /// <summary>
        /// Gets the initial number of combatants on the attacker's side.
        /// </summary>
        public long StartingCombatantsOnAttackingSide { get; private init; }

        /// <summary>
        /// Gets the initial number of combatants on the defender's side.
        /// </summary>
        public long StartingCombatantsOnDefendingSide { get; private init; }
    }
}
