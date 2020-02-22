using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for selling land.
    /// </summary>
    public class SellLandInput : CommandInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellLandInput"/> class.
        /// </summary>
        /// <param name="areaToSell">The amount of land to sell in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="areaToSell"/> is less than zero.
        /// </exception>
        public SellLandInput(int areaToSell) : base()
        {
            if (areaToSell < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(areaToSell), areaToSell, "The amount of land area to sell cannot be less than zero.");
            }
            AreaToSell = areaToSell;
        }

        /// <summary>
        /// Gets the amount of land to sell in square kilometers.
        /// </summary>
        public int AreaToSell { get; }
    }
}
