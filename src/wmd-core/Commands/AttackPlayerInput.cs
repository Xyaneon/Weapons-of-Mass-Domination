using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for the attack player action.
    /// </summary>
    public record AttackPlayerInput : CommandInput
    {
        /// <summary>
        /// Gets or initializes the index of the player being attacked.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than zero.
        /// </exception>
        public int TargetPlayerIndex
        {
            get => _targetPlayerIndex;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The target player index cannot be less than zero.");
                }
                _targetPlayerIndex = value;
            }
        }

        private int _targetPlayerIndex;
    }
}
