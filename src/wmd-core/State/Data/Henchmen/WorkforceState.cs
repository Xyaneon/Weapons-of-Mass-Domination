using System;
using WMD.Game.Constants;

namespace WMD.Game.State.Data.Henchmen
{
    /// <summary>
    /// Represents the current state of a player's workforce.
    /// </summary>
    public record WorkforceState
    {
        private const string ArgumentOutOfRangeException_DailyPayRateLessThanZero = "The daily pay rate cannot be less than zero.";
        private const string ArgumentOutOfRangeException_NumberOfHenchmenLessThanZero = "The number of henchmen cannot be less than zero.";
        
        private const decimal DefaultDailyPayRate = HenchmenConstants.MinimumDailyWage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkforceState"/> class.
        /// </summary>
        /// <remarks>
        /// This will set the value of the <see cref="DailyPayRate"/> property to <see cref="HenchmenConstants.MinimumDailyWage"/> by default.
        /// </remarks>
        public WorkforceState()
        {
            DailyPayRate = DefaultDailyPayRate;
            NumberOfHenchmen = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkforceState"/> class.
        /// </summary>
        /// <param name="dailyPayRate">
        /// The daily pay rate of each henchman.
        /// </param>
        /// <param name="numberOfHenchmen">
        /// The number of henchmen in this workforce.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="dailyPayRate"/> is less than zero.
        /// -or-
        /// <paramref name="numberOfHenchmen"/> is less than zero.
        /// </exception>
        public WorkforceState(decimal dailyPayRate = DefaultDailyPayRate, long numberOfHenchmen = 0)
        {
            DailyPayRate = dailyPayRate;
            NumberOfHenchmen = numberOfHenchmen;
        }

        /// <summary>
        /// Gets the daily pay rate of each henchman.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value to initialize this property with is less than zero.
        /// </exception>
        /// <remarks>
        /// The default constructor will initialize this property value to <see cref="HenchmenConstants.MinimumDailyWage"/> by default.
        /// </remarks>
        public decimal DailyPayRate
        {
            get => _dailyPayRate;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_DailyPayRateLessThanZero);
                }

                _dailyPayRate = value;
            }
        }

        /// <summary>
        /// Gets the number of henchmen in this workforce.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value to initialize this property with is less than zero.
        /// </exception>
        public long NumberOfHenchmen
        {
            get => _numberOfHenchmen;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NumberOfHenchmenLessThanZero);
                }

                _numberOfHenchmen = value;
            }
        }

        private decimal _dailyPayRate;
        private long _numberOfHenchmen;
    }
}
