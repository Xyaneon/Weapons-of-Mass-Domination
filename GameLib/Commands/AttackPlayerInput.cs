using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for the attack player action.
    /// </summary>
    public class AttackPlayerInput : CommandInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttackPlayerInput"/> class.
        /// </summary>
        /// <param name="targetPlayerIndex">The index of the player being attacked.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="targetPlayerIndex"/> is less than zero.
        /// </exception>
        public AttackPlayerInput(int targetPlayerIndex) : base()
        {
            if (targetPlayerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPlayerIndex), targetPlayerIndex, "The target player index cannot be less than zero.");
            }
            TargetPlayerIndex = targetPlayerIndex;
        }

        /// <summary>
        /// Gets the index of the player being attacked.
        /// </summary>
        public int TargetPlayerIndex { get; }
    }
}
