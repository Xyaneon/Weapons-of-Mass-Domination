using System;

namespace WMD.Game
{
    /// <summary>
    /// Represents a player's secret base of operations.
    /// </summary>
    public class SecretBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecretBase"/> class.
        /// </summary>
        public SecretBase()
        {
            Level = 1;
        }

        /// <summary>
        /// Gets or sets the base's level, which starts at one.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than one.
        /// </exception>
        public int Level
        {
            get => _level;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "A secret base's level cannot be less than one.");
                }
                _level = value;
            }
        }

        private int _level;
    }
}
