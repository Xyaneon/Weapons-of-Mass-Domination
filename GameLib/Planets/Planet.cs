using System;

namespace WMD.Game.Planets
{
    /// <summary>
    /// Represents a planet.
    /// </summary>
    /// <remarks>
    /// All surface area properties are in square kilometers.
    /// </remarks>
    public abstract class Planet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Planet"/> class.
        /// </summary>
        /// <param name="totalLandArea">The total land area in square kilometers.</param>
        /// <param name="totalSurfaceArea">The total surface area in square kilometers.</param>
        /// <param name="totalWaterArea">The total water area in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="totalLandArea"/>, <paramref name="totalSurfaceArea"/>, or
        /// <paramref name="totalWaterArea"/> are less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="totalSurfaceArea"/> is not equal to the total of
        /// <paramref name="totalLandArea"/> and <paramref name="totalWaterArea"/>.
        /// </exception>
        public Planet(int totalLandArea, int totalSurfaceArea, int totalWaterArea)
        {
            if (totalLandArea < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalLandArea), "The total land area cannot be negative.");
            }

            if (totalSurfaceArea < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalSurfaceArea), "The total surface area cannot be negative.");
            }

            if (totalWaterArea < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalWaterArea), "The total water area cannot be negative.");
            }

            if (totalLandArea + totalWaterArea != totalSurfaceArea)
            {
                throw new ArgumentException("The land and water areas do not add up to the total surface area.");
            }

            TotalLandArea = totalLandArea;
            TotalSurfaceArea = totalSurfaceArea;
            TotalWaterArea = totalWaterArea;

            UnclaimedLandArea = totalLandArea;
        }

        /// <summary>
        /// Gets the total land area in square kilometers.
        /// </summary>
        public int TotalLandArea { get; }
        
        /// <summary>
        /// Gets the total surface area in square kilometers.
        /// </summary>
        public int TotalSurfaceArea { get; }

        /// <summary>
        /// Gets the total water area in square kilometers.
        /// </summary>
        public int TotalWaterArea { get; }

        /// <summary>
        /// Gets or sets the amount of unclaimed land in square kilometers.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than zero.
        /// -or-
        /// The provided value exceeds the value of the
        /// <see cref="TotalLandArea"/> property.
        /// </exception>
        public int UnclaimedLandArea
        {
            get => _unclaimedLandArea;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The amount of unclaimed land cannot be negative.");
                }

                if (value > TotalLandArea)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The amount of unclaimed land cannot exceed the total land area.");
                }

                _unclaimedLandArea = value;
            }
        }

        private int _unclaimedLandArea;
    }
}
