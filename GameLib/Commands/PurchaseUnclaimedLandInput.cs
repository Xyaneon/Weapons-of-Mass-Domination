﻿using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for purchasing unclaimed land.
    /// </summary>
    public class PurchaseUnclaimedLandInput : CommandInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseUnclaimedLandInput"/> class.
        /// </summary>
        /// <param name="areaToPurchase">The amount of land to purchase in square kilometers.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="areaToPurchase"/> is less than zero.
        /// </exception>
        public PurchaseUnclaimedLandInput(int areaToPurchase) : base()
        {
            if (areaToPurchase < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(areaToPurchase), "The area of land to purchase cannot be negative.");
            }
            AreaToPurchase = areaToPurchase;
        }

        /// <summary>
        /// Gets the amount of land to purchase in square kilometers.
        /// </summary>
        public int AreaToPurchase { get; }
    }
}