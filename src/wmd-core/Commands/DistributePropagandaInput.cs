using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Additional input data for the distribute propaganda action.
    /// </summary>
    public record DistributePropagandaInput : CommandInput
    {
        private const string ArgumentOutOfRangeException_MoneyToSpend = "The amount of money to spend on distributing propaganda cannot be less than zero.";

        /// <summary>
        /// Gets the amount of money the player is going to spend on propaganda.
        /// </summary>
        public decimal MoneyToSpend
        {
            get => _moneyToSpend;
            init
            {
                if (value < 0.0M)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_MoneyToSpend);
                }
                _moneyToSpend = value;
            }
        }

        private decimal _moneyToSpend = 0.0M;
    }
}
