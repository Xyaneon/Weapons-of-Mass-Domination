using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for purchasing unclaimed land.
    /// </summary>
    public record PurchaseUnclaimedLandInput : CommandInput
    {
        /// <summary>
        /// Gets or initializes the amount of land to purchase in square kilometers.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than zero.
        /// </exception>
        public int AreaToPurchase
        {
            get => _areaToPurchase;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The area of land to purchase cannot be negative.");
                }
                _areaToPurchase = value;
            }
        }

        private int _areaToPurchase;
    }
}
