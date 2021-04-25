using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for the change daily wage action.
    /// </summary>
    public record ChangeDailyWageInput : CommandInput
    {
        private const string ArgumentOutOfRangeException_NewDailyWageLessThanZero = "The new daily wage cannot be less than zero.";

        /// <summary>
        /// Gets or initializes the new daily wage.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The provided value is less than zero.
        /// </exception>
        public decimal NewDailyWage
        {
            get => _newDailyWage;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NewDailyWageLessThanZero);
                }
                _newDailyWage = value;
            }
        }

        private decimal _newDailyWage;
    }
}
