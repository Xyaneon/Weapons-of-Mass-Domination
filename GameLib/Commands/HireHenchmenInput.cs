using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for the hire henchmen action.
    /// </summary>
    public class HireHenchmenInput : CommandInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HireHenchmenInput"/> class.
        /// </summary>
        /// <param name="openPositionsOffered">The number of open positions to offer.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="openPositionsOffered"/> is less than one.
        /// </exception>
        public HireHenchmenInput(int openPositionsOffered) : base()
        {
            if (openPositionsOffered < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(openPositionsOffered), openPositionsOffered, "The number of open positions offered must be greater than zero.");
            }
            OpenPositionsOffered = openPositionsOffered;
        }

        /// <summary>
        /// Gets the number of open positions to offer.
        /// </summary>
        public int OpenPositionsOffered { get; }
    }
}
