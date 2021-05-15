using System;

namespace WMD.Game.State.Data.Governments
{
    /// <summary>
    /// Represents the current state of the government.
    /// </summary>
    public record GovernmentState
    {
        private const string ArgumentOutOfRangeException_NumberOfSoldiers_LessThanZero = "The number of soldiers cannot be less than zero.";

        /// <summary>
        /// Initializes a new instance of the <see cref="GovernmentState"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor will initialize the <see cref="NumberOfSoldiers"/> property to zero.
        /// </remarks>
        public GovernmentState()
        {
            NumberOfSoldiers = 0;
        }

        /// <summary>
        /// Gets or initializes the number of soldiers in the government's army.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than zero.
        /// </exception>
        public long NumberOfSoldiers
        {
            get => _numberOfSoldiers;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NumberOfSoldiers_LessThanZero);
                }
                _numberOfSoldiers = value;
            }
        }

        private long _numberOfSoldiers;
    }
}
