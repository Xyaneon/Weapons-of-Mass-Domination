using System;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    /// <summary>
    /// An occurrence of a government attacking a player.
    /// </summary>
    public record GovernmentAttacksPlayer : GovernmentIntervention
    {
        private const string ArgumentOutOfRangeException_AttackingSoldiersExceedsTotalAmount = "The number of soldiers used by the government to attack cannot exceed the actual total number of soldiers they have.";

        /// <summary>
        /// Initializes a new instance of the <see cref="GovernmentAttacksPlayer"/> class.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player who was attacked.</param>
        /// <param name="attackingSoldiers">The number of soldiers the government used for the attack.</param>
        public GovernmentAttacksPlayer(GameState gameState, int playerIndex, long attackingSoldiers)
        {
            if (attackingSoldiers > gameState.GovernmentState.NumberOfSoldiers)
            {
                throw new ArgumentOutOfRangeException(nameof(attackingSoldiers), attackingSoldiers, ArgumentOutOfRangeException_AttackingSoldiersExceedsTotalAmount);
            }
            PlayerIndex = playerIndex;
            AttackingSoldiers = attackingSoldiers;
        }

        /// <summary>
        /// Gets the number of soldiers the government used for the attack.
        /// </summary>
        public long AttackingSoldiers { get; init; }

        /// <summary>
        /// Gets the index of the player who was attacked.
        /// </summary>
        public int PlayerIndex { get; init; }
    }
}
