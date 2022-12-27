using System;

namespace WMD.Console.Miscellaneous;

class DecimalRange
{
    private const string ArgumentException_minimumGreaterThanMaximum = "The range minimum cannot be greater than the range maximum.";

    public DecimalRange(decimal minimum, decimal maximum)
    {
        if (minimum > maximum)
        {
            throw new ArgumentException(ArgumentException_minimumGreaterThanMaximum);
        }

        Maximum = maximum;
        Minimum = minimum;
    }

    public decimal Maximum { get; }

    public decimal Minimum { get; }

    public bool ContainsValueInclusive(decimal value)
    {
        return value >= Minimum && value <= Maximum;
    }
}
