using System;

namespace WMD.Console.Miscellaneous
{
    class LongRange
    {
        private const string ArgumentException_minimumGreaterThanMaximum = "The range minimum cannot be greater than the range maximum.";

        public LongRange(long minimum, long maximum)
        {
            if (minimum > maximum)
            {
                throw new ArgumentException(ArgumentException_minimumGreaterThanMaximum);
            }

            Maximum = maximum;
            Minimum = minimum;
        }

        public long Maximum { get; }

        public long Minimum { get; }

        public bool ContainsValueInclusive(long value)
        {
            return value >= Minimum && value <= Maximum;
        }
    }
}
