using System;
using System.Diagnostics.CodeAnalysis;

namespace WMD.Game.State.Data.Planets;

/// <summary>
/// Represents a planet.
/// </summary>
/// <remarks>
/// All surface area properties are in square kilometers.
/// </remarks>
public abstract record Planet
{
    private const string ArgumentOutOfRangeException_NeutralPopulationCannotBeNegative = "The neutral population cannot be negative.";

    /// <summary>
    /// Initializes a new instance of the <see cref="Planet"/> class.
    /// </summary>
    /// <param name="name">The planet's name.</param>
    /// <param name="totalLandArea">The total land area in square kilometers.</param>
    /// <param name="totalSurfaceArea">The total surface area in square kilometers.</param>
    /// <param name="totalWaterArea">The total water area in square kilometers.</param>
    /// <param name="neutralPopulation">The planet's total neutral population.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="totalLandArea"/>, <paramref name="totalSurfaceArea"/>,
    /// <paramref name="totalWaterArea"/>, or <paramref name="neutralPopulation"/> are less than zero.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="name"/> is empty or all whitespace.
    /// -or-
    /// <paramref name="totalSurfaceArea"/> is not equal to the total of
    /// <paramref name="totalLandArea"/> and <paramref name="totalWaterArea"/>.
    /// </exception>
    public Planet([DisallowNull] string name, int totalLandArea, int totalSurfaceArea, int totalWaterArea, long neutralPopulation)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The name of the planet cannot be empty or all whitespace.", nameof(name));
        }

        if (totalLandArea < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalLandArea), "The total land area cannot be negative.");
        }

        if (totalSurfaceArea < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalSurfaceArea), "The total surface area cannot be negative.");
        }

        if (totalWaterArea < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalWaterArea), "The total water area cannot be negative.");
        }

        if (totalLandArea + totalWaterArea != totalSurfaceArea)
        {
            throw new ArgumentException("The land and water areas do not add up to the total surface area.");
        }

        if (neutralPopulation < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(neutralPopulation), ArgumentOutOfRangeException_NeutralPopulationCannotBeNegative);
        }

        Name = name.Trim();

        TotalLandArea = totalLandArea;
        TotalSurfaceArea = totalSurfaceArea;
        TotalWaterArea = totalWaterArea;
        NeutralPopulation = neutralPopulation;

        UnclaimedLandArea = totalLandArea;
    }

    /// <summary>
    /// Gets the name of this planet.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the percentage of land claimed as a number between 0 and 1.
    /// </summary>
    /// <seealso cref="PercentageOfLandStillUnclaimed"/>
    public double PercentageOfLandClaimed => 1 - PercentageOfLandStillUnclaimed;

    /// <summary>
    /// Gets the percentage of land still unclaimed as a number between 0 and 1.
    /// </summary>
    /// <seealso cref="PercentageOfLandClaimed"/>
    public double PercentageOfLandStillUnclaimed => UnclaimedLandArea / (double)TotalLandArea;

    /// <summary>
    /// Gets or initializes the total neutral population.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than zero.
    /// </exception>
    public long NeutralPopulation
    {
        get => _neutralPopulation;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), ArgumentOutOfRangeException_NeutralPopulationCannotBeNegative);
            }
            _neutralPopulation = value;
        }
    }

    /// <summary>
    /// Gets the total land area in square kilometers.
    /// </summary>
    public int TotalLandArea { get; }

    /// <summary>
    /// Gets the total surface area in square kilometers.
    /// </summary>
    public int TotalSurfaceArea { get; }

    /// <summary>
    /// Gets the total water area in square kilometers.
    /// </summary>
    public int TotalWaterArea { get; }

    /// <summary>
    /// Gets the amount of unclaimed land in square kilometers.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than zero.
    /// -or-
    /// The provided value exceeds the value of the
    /// <see cref="TotalLandArea"/> property.
    /// </exception>
    public int UnclaimedLandArea
    {
        get => _unclaimedLandArea;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "The amount of unclaimed land cannot be negative.");
            }

            if (value > TotalLandArea)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "The amount of unclaimed land cannot exceed the total land area.");
            }

            _unclaimedLandArea = value;
        }
    }

    private long _neutralPopulation;
    private int _unclaimedLandArea;
}
