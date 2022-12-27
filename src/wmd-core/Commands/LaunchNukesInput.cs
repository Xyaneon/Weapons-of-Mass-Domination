using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional input data for the launch nukes action.
/// </summary>
public record LaunchNukesInput : CommandInput
{
    private const string ArgumentOutOfRangeException_NumberOfNukes = "The number of nukes to launch cannot be less than one.";
    private const string ArgumentOutOfRangeException_TargetPlayerIndex = "The target player index cannot be less than zero.";

    /// <summary>
    /// Gets or initializes the number of nukes to launch.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than one.
    /// </exception>
    public int NumberOfNukesLaunched
    {
        get => _numberOfNukes;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NumberOfNukes);
            }
            _numberOfNukes = value;
        }
    }

    /// <summary>
    /// Gets or initializes the index of the player being attacked.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than zero.
    /// </exception>
    public int TargetPlayerIndex
    {
        get => _targetPlayerIndex;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_TargetPlayerIndex);
            }
            _targetPlayerIndex = value;
        }
    }

    private int _numberOfNukes;
    private int _targetPlayerIndex;
}
