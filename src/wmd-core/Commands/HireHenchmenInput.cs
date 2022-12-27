using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional input data for the hire henchmen action.
/// </summary>
public record HireHenchmenInput : CommandInput
{
    private const string ArgumentOutOfRangeException_OpenPositionsOfferedLessThanOne = "The number of open positions offered must be greater than zero.";

    /// <summary>
    /// Gets or initializes the number of open positions to offer.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than one.
    /// </exception>
    public long OpenPositionsOffered
    {
        get => _openPositionsOffered;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_OpenPositionsOfferedLessThanOne);
            }
            _openPositionsOffered = value;
        }
    }

    private long _openPositionsOffered;
}
