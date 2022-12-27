using System;
using WMD.Game.Constants;

namespace WMD.Game.State.Data.Henchmen;

/// <summary>
/// Represents the current state of a player's workforce.
/// </summary>
public record WorkforceState
{
    private const string ArgumentOutOfRangeException_DailyPayRateLessThanZero = "The daily pay rate cannot be less than zero.";
    private const string ArgumentOutOfRangeException_GenericHenchmenCountLessThanZero = "The number of generic henchmen cannot be less than zero.";
    private const string ArgumentOutOfRangeException_SoldierCountLessThanZero = "The number of soldiers cannot be less than zero.";
    
    private const decimal DefaultDailyPayRate = HenchmenConstants.MinimumDailyWage;

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
    public long TotalHenchmenCount
    {
        get => _genericHenchmenCount + _soldierCount;
    }

    /// <summary>
    /// Gets the number of generic henchmen in this workforce.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value to initialize this property with is less than zero.
    /// </exception>
    /// <remarks>
    /// These are the henchmen a player has who have not been specialized yet.
    /// </remarks>
    public long GenericHenchmenCount
    {
        get => _genericHenchmenCount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_GenericHenchmenCountLessThanZero);
            }

            _genericHenchmenCount = value;
        }
    }

    /// <summary>
    /// Gets the number of soldiers in this workforce.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value to initialize this property with is less than zero.
    /// </exception>
    public long SoldierCount
    {
        get => _soldierCount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_SoldierCountLessThanZero);
            }

            _soldierCount = value;
        }
    }

    /// <summary>
    /// Gets the total daily pay for the entire workforce.
    /// </summary>
    /// <remarks>
    /// The value of this property depends on the current values of the <see cref="DailyPayRate"/> and <see cref="TotalHenchmenCount"/> properties.
    /// </remarks>
    public decimal TotalDailyPay { get => DailyPayRate * TotalHenchmenCount; }

    private decimal _dailyPayRate = DefaultDailyPayRate;
    private long _genericHenchmenCount;
    private long _soldierCount;
}
