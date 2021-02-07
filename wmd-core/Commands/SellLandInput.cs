using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for selling land.
    /// </summary>
    public record SellLandInput : CommandInput
    {
        /// <summary>
        /// Gets or initializes the amount of land to sell in square kilometers.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thr provided value is less than zero.
        /// </exception>
        public int AreaToSell
        {
            get => _areaToSell;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The amount of land area to sell cannot be less than zero.");
                }
                _areaToSell = value;
            }
        }

        private int _areaToSell;
    }
}
