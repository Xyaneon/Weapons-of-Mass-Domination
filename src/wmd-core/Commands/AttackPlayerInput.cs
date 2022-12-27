using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional input data for the attack player action.
/// </summary>
public record AttackPlayerInput : CommandInput
{
    private const string ArgumentOutOfRangeException_TargetPlayerIndex_LessThanZero = "The target player index cannot be less than zero.";
    private const string ArgumentOutOfRangeException_NumberOfAttackingHenchmen_LessThanOne = "The number of attacking henchmen cannot be less than one.";

    /// <summary>
    /// Gets or initializes the number of henchmen used to carry out the attack.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than one.
    /// </exception>
    public long NumberOfAttackingHenchmen
    {
        get => _numberOfAttackingHenchmen;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NumberOfAttackingHenchmen_LessThanOne);
            }
            _numberOfAttackingHenchmen = value;
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
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_TargetPlayerIndex_LessThanZero);
            }
            _targetPlayerIndex = value;
        }
    }

    private long _numberOfAttackingHenchmen;
    private int _targetPlayerIndex;
}
