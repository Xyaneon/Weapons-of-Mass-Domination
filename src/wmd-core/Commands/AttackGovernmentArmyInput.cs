using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional input data for the attack government army action.
/// </summary>
public record AttackGovernmentArmyInput : CommandInput
{
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

    private long _numberOfAttackingHenchmen;
}
