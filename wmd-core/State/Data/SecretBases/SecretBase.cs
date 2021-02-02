using System;

namespace WMD.Game.State.Data.SecretBases
{
    /// <summary>
    /// Represents a player's secret base of operations.
    /// </summary>
    public record SecretBase
    {
        /// <summary>
        /// The price to pay for building a new secret base.
        /// </summary>
        public const decimal SecretBaseBuildPrice = 500;

        /// <summary>
        /// The multiplier used for calculating subsequent secret base upgrades.
        /// </summary>
        public const decimal SecretBaseUpgradeFactor = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretBase"/> class.
        /// </summary>
        public SecretBase()
        {
            Level = 1;
        }

        /// <summary>
        /// Gets the base's level, which starts at one.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than one.
        /// </exception>
        public int Level
        {
            get => _level;
            init
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "A secret base's level cannot be less than one.");
                }
                _level = value;
            }
        }

        private int _level;

        /// <summary>
        /// Calculates the price of upgrading the provided <see cref="SecretBase"/>.
        /// </summary>
        /// <param name="secretBase">The <see cref="SecretBase"/> which could be upgraded (or built if <see langword="null"/>).</param>
        /// <returns>The price of upgrading the secret base.</returns>
        public static decimal CalculateUpgradePrice(SecretBase secretBase)
        {
            return secretBase?.Level * SecretBaseUpgradeFactor ?? SecretBaseBuildPrice;
        }
    }
}
