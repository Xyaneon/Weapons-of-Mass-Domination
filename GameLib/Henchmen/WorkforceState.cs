using System;

namespace WMD.Game.Henchmen
{
    /// <summary>
    /// Represents the current state of a player's workforce.
    /// </summary>
    public class WorkforceState
    {
        /// <summary>
        /// The minimum daily wage. If a player chooses to pay less than this
        /// amount, then their henchmen will be much more likely to quit.
        /// </summary>
        public const decimal MinimumDailyWage = 7;

        private const decimal DefaultDailyPayRate = MinimumDailyWage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkforceState"/> class.
        /// </summary>
        public WorkforceState()
        {
            DailyPayRate = DefaultDailyPayRate;
            NumberOfHenchmen = 0;
        }

        /// <summary>
        /// Gets the daily pay rate of each henchman.
        /// </summary>
        public decimal DailyPayRate
        {
            get => _dailyPayRate;
            internal set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The daily pay rate cannot be less than zero.");
                }

                _dailyPayRate = value;
            }
        }

        /// <summary>
        /// Gets the number of henchmen in this workforce.
        /// </summary>
        public int NumberOfHenchmen
        {
            get => _numberOfHenchmen;
            internal set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The number of henchmen cannot be less than zero.");
                }

                _numberOfHenchmen = value;
            }
        }

        private decimal _dailyPayRate;
        private int _numberOfHenchmen;
    }
}
