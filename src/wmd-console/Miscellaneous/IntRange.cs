using System;

namespace WMD.Console.Miscellaneous;

class IntRange
{
    private const string ArgumentException_minimumGreaterThanMaximum = "The range minimum cannot be greater than the range maximum.";

    public IntRange(int minimum, int maximum)
    {
        if (minimum > maximum)
        {
            throw new ArgumentException(ArgumentException_minimumGreaterThanMaximum);
        }

        Maximum = maximum;
        Minimum = minimum;
    }

    public int Maximum { get; }

    public int Minimum { get; }

    public bool ContainsValueInclusive(int value)
    {
        return value >= Minimum && value <= Maximum;
    }
}
