using System;
using WMD.Game.State.Data;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player attacking the government army.
    /// </summary>
    public record AttackGovernmentArmyResult : CommandResult
    {
        private const string ArgumentOutOfRangeException_henchmenAttackerLost = "The number of henchmen the attacker lost cannot be less than zero.";
        private const string ArgumentOutOfRangeException_soldiersGovernmentArmyLost = "The number of soldiers the government army lost cannot be less than zero.";
        private const string ArgumentOutOfRangeException_numberOfAttackingHenchmen = "The number of henchmen used to attack cannot be less than one.";

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackGovernmentArmyResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="numberOfAttackingHenchmen">The number of henchmen used by the attacker.</param>
        /// <param name="henchmenAttackerLost">The number of henchmen the attacker lost.</param>
        /// <param name="soldiersGovernmentArmyLost">The number of soldiers the government army lost.</param>
        /// <param name="reputationChangeForAttacker">The amount by which the attacker's reputation changed because of the attack.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfAttackingHenchmen"/> is less than one.
        /// -or-
        /// <paramref name="henchmenAttackerLost"/> is less than zero.
        /// -or-
        /// <paramref name="soldiersGovernmentArmyLost"/> is less than zero.
        /// </exception>
        public AttackGovernmentArmyResult(
            GameState updatedGameState,
            int playerIndex,
            long numberOfAttackingHenchmen,
            long henchmenAttackerLost,
            long soldiersGovernmentArmyLost,
            int reputationChangeForAttacker
        ) : base(updatedGameState, playerIndex)
        {
            if (numberOfAttackingHenchmen < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfAttackingHenchmen), numberOfAttackingHenchmen, ArgumentOutOfRangeException_numberOfAttackingHenchmen);
            }

            if (henchmenAttackerLost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(henchmenAttackerLost), henchmenAttackerLost, ArgumentOutOfRangeException_henchmenAttackerLost);
            }

            if (soldiersGovernmentArmyLost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(soldiersGovernmentArmyLost), soldiersGovernmentArmyLost, ArgumentOutOfRangeException_soldiersGovernmentArmyLost);
            }

            HenchmenAttackerLost = henchmenAttackerLost;
            NumberOfAttackingHenchmen = numberOfAttackingHenchmen;
            ReputationChangeForAttacker = reputationChangeForAttacker;
            SoldiersGovernmentArmyLost = soldiersGovernmentArmyLost;
        }

        /// <summary>
        /// Gets the number of henchmen the attacker lost.
        /// </summary>
        public long HenchmenAttackerLost { get; init; }

        /// <summary>
        /// Gets the number of henchmen the attacker used.
        /// </summary>
        public long NumberOfAttackingHenchmen { get; init; }

        /// <summary>
        /// Gets the amount by which the attacker's reputation changed because of the attack.
        /// </summary>
        public int ReputationChangeForAttacker { get; init; }

        /// <summary>
        /// Gets the number of soldiers the defending government army lost.
        /// </summary>
        public long SoldiersGovernmentArmyLost { get; init; }
    }
}
