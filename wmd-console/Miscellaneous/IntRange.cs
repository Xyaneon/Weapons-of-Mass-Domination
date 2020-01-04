using System;
using System.Collections.Generic;
using System.Text;

namespace WMD.Console.Miscellaneous
{
    class IntRange
    {
        public IntRange(int minimum, int maximum)
        {
            if (minimum > maximum)
            {
                throw new ArgumentException("The range minimum cannot be greater than the range maximum.");
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
}
