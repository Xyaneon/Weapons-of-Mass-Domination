using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional input data for the manufacture nukes action.
/// </summary>
public record ManufactureNukesInput : CommandInput
{
    /// <summary>
    /// Gets or initializes the number of nukes to manufacture.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than one.
    /// </exception>
    public int NumberOfNukesToManufacture
    {
        get => _numberOfNukesToManufacture;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "The number of nukes to manufacture must be greater than zero.");
            }
            _numberOfNukesToManufacture = value;
        }
    }

    private int _numberOfNukesToManufacture;
}
