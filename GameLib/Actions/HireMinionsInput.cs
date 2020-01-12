using System;

namespace WMD.Game.Actions
{
    /// <summary>
    /// Additional input data for the hire minions action.
    /// </summary>
    public class HireMinionsInput : ActionInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HireMinionsInput"/> class.
        /// </summary>
        /// <param name="openPositionsOffered">The number of open positions to offer.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="openPositionsOffered"/> is less than one.
        /// </exception>
        public HireMinionsInput(int openPositionsOffered) : base()
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
