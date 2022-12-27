using System;
using System.Collections.Generic;
using System.Linq;

namespace WMD.Console.Miscellaneous;

static class RealWorldComparisons
{
    static RealWorldComparisons()
    {
        _locationsAndSizes = ConstructLocationsAndSizesDictionary();
        _negligibleSizeLocations = ConstructNegligibleSizeLocationsList();
        _random = new Random();
    }

    private static readonly IReadOnlyDictionary<int, string> _locationsAndSizes;
    private static readonly IReadOnlyList<string> _negligibleSizeLocations;
    private static readonly Random _random;

    public static string GetComparableRealWorldLocationByLandAreaInSquareKilometers(int area)
    {
        if (area < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(area), area, "The land area to compare cannot be negative.");
        }

        if (area == 0)
        {
            return SelectRandomNegligibleSizeLocation();
        }

        int closest = _locationsAndSizes.Keys.Aggregate((x, y) => Math.Abs(x - area) < Math.Abs(y - area) ? x : y);
        return _locationsAndSizes[closest];
    }

    private static IReadOnlyDictionary<int, string> ConstructLocationsAndSizesDictionary()
    {
        // Country areas below from https://en.wikipedia.org/wiki/List_of_countries_and_dependencies_by_area .
        return new Dictionary<int, string>()
        {
            { 17098246, "Russia" },
            { 14000000, "Antarctica" },
            { 9984670, "Canada" },
            { 9596961, "China" },
            { 9525067, "United States" },
            { 8515767, "Brazil" },
            { 7692024, "Australia" },
            { 3287263, "India" },
            { 2780400, "Argentina" },
            { 2724900, "Kazakhstan" },
            { 2381741, "Algeria" },
            { 2344858, "Democratic Republic of the Congo" },
            { 2149690, "Saudi Arabia" },
            { 1964375, "Mexico" },
            { 1910931, "Indonesia" },
            { 1861484, "Sudan" },
            { 1759540, "Libya" },
            { 1648195, "Iran" },
            { 1564110, "Mongolia" },
            { 1285216, "Peru" },
            { 1284000, "Chad" },
            { 1267000, "Niger" },
            { 1246700, "Angola" },
            { 1240192, "Mali" },
            { 1221037, "South Africa" },
            { 1141748, "Colombia" },
            { 1104300, "Ethiopia" },
            { 1098581, "Bolivia" },
            { 1030700, "Mauritania" },
            { 1002450, "Egypt" },
            { 945087, "Tanzania" },
            { 923768, "Nigeria" },
            { 916445, "Venezuela" },
            { 907132, "Pakistan" },
            { 825615, "Namibia" },
            { 801590, "Mozambique" },
            { 783562, "Turkey" },
            { 756102, "Chile" },
            { 752612, "Zambia" },
            { 676578, "Myanmar" },
            { 652230, "Afghanistan" },
            { 644329, "South Sudan" },
            { 640679, "France" },
            { 637657, "Somalia" },
            { 622984, "Central African Republic" },
            { 603500, "Ukraine" },
            { 587041, "Madagascar" },
            { 581730, "Botswana" },
            { 580367, "Kenya" },
            { 527968, "Yemen" },
            { 513120, "Thailand" },
            { 505992, "Spain" },
            { 488100, "Turkmenistan" },
            { 475442, "Cameroon" },
            { 462840, "Papua New Guinea" },
            { 450295, "Sweden" },
            { 447400, "Uzbekistan" },
            { 446550, "Morocco" },
            { 438317, "Iraq" },
            { 406752, "Paraguay" },
            { 390757, "Zimbabwe" },
            { 377975, "Japan" },
            { 357114, "Germany" },
            { 342000, "Republic of the Congo" },
            { 338424, "Finland" },
            { 331212, "Vietnam" },
            { 330803, "Malaysia" },
            { 323802, "Norway" },
            { 322463, "Ivory Coast" },
            { 312696, "Poland" },
            { 309500, "Oman" },
            { 301339, "Italy" },
            { 300000, "Philippines" },
            { 276841, "Ecuador" },
            { 274222, "Burkina Faso" },
            { 270467, "New Zealand" },
            { 267668, "Gabon" },
            { 245857, "Guinea" },
            { 242495, "United Kingdom" },
            { 241550, "Uganda" },
            { 238533, "Ghana" },
            { 238397, "Romania" },
            { 236800, "Laos" },
            { 214969, "Guyana" },
            { 207600, "Belarus" },
            { 199951, "Kyrgyzstan" },
            { 196722, "Senegal" },
            { 185180, "Syria" },
            { 181035, "Cambodia" },
            { 176215, "Uruguay" },
            { 163820, "Suriname" },
            { 163610, "Tunisia" },
            { 147570, "Bangladesh" },
            { 147181, "Nepal" },
            { 143100, "Tajikistan" },
            { 131957, "Greece" },
            { 130373, "Nicaragua" },
            { 120540, "North Korea" },
            { 118484, "Malawi" },
            { 117600, "Eritrea" },
            { 114763, "Benin" },
            { 112492, "Honduras" },
            { 111369, "Liberia" },
            { 111002, "Bulgaria" },
            { 109884, "Cuba" },
            { 108889, "Guatemala" },
            { 103000, "Iceland" },
            { 100210, "South Korea" },
            { 93028, "Hungary" },
            { 92090, "Portugal" },
            { 89342, "Jordan" },
            { 88361, "Serbia" },
            { 86600, "Azerbaijan" },
            { 83871, "Austria" },
            { 83600, "United Arab Emirates" },
            { 78865, "Czech Republic" },
            { 75417, "Panama" },
            { 71740, "Sierra Leone" },
            { 70273, "Ireland" },
            { 69700, "Georgia" },
            { 65610, "Sri Lanka" },
            { 65300, "Lithuania" },
            { 64559, "Latvia" },
            { 56785, "Togo" },
            { 56594, "Croatia" },
            { 51209, "Bosnia and Herzegovina" },
            { 51100, "Costa Rica" },
            { 49037, "Slovakia" },
            { 48671, "Dominican Republic" },
            { 45227, "Estonia" },
            { 43094, "Denmark" },
            { 41850, "Netherlands" },
            { 41284, "Switzerland" },
            { 38394, "Bhutan" },
            { 36125, "Guinea-Bissau" },
            { 33846, "Moldova" },
            { 30528, "Belgium" },
            { 30355, "Lesotho" },
            { 29743, "Armenia" },
            { 28896, "Solomon Islands" },
            { 28748, "Albania" },
            { 28051, "Equatorial Guinea" },
            { 27834, "Burundi" },
            { 27750, "Haiti" },
            { 26338, "Rwanda" },
            { 25713, "North Macedonia" },
            { 23200, "Djibouti" },
            { 22966, "Belize" },
            { 21041, "El Salvador" },
            { 20770, "Israel" },
            { 20273, "Slovenia" },
            { 18272, "Fiji" },
            { 17818, "Kuwait" },
            { 17364, "Eswatini" },
            { 14919, "East Timor" },
            { 13943, "The Bahamas" },
            { 13812, "Montenegro" },
            { 12189, "Vanuatu" },
            { 11586, "Qatar" },
            { 11295, "The Gambia" },
            { 10991, "Jamaica" },
            { 10452, "Lebanon" },
            { 9251, "Cyprus" },
            { 5765, "Brunei" },
            { 5130, "Trinidad and Tobago" },
            { 4033, "Cape Verde" },
            { 2842, "Samoa" },
            { 2586, "Luxembourg" },
            { 2040, "Mauritius" },
            { 1862, "Comoros" },
            { 964, "São Tomé and Príncipe" },
            { 811, "Kiribati" },
            { 778, "Bahrain" },
            { 751, "Dominica" },
            { 747, "Tonga" },
            { 716, "Singapore" },
            { 616, "Saint Lucia" },
            { 468, "Andorra" },
            { 459, "Palau" },
            { 452, "Seychelles" },
            { 442, "Antigua and Barbuda" },
            { 430, "Barbados" },
            { 389, "Saint Vincent and the Grenadines" },
            { 344, "Grenada" },
            { 316, "Malta" },
            { 300, "Maldives" },
            { 261, "Saint Kitts and Nevis" },
            { 181, "Marshall Islands" },
            { 160, "Liechtenstein" },
            { 61, "San Marino" },
            { 26, "Tuvalu" },
            { 21, "Nauru" },
            { 2, "Monaco" },
            { 1, "Vatican City" }
        };
    }

    private static IReadOnlyList<string> ConstructNegligibleSizeLocationsList()
    {
        return new string[]
        {
            "a cardboard box by the side of the freeway",
            "a back alleyway",
            "a dilapidated house",
            "a dumpster behind your nearest fast food restaurant",
            "a school playground",
            "a teepee",
            "an outhouse",
            "the space under a bridge",
            "your mom's basement"
        };
    }

    private static string SelectRandomNegligibleSizeLocation()
    {
        return _negligibleSizeLocations[_random.Next(_negligibleSizeLocations.Count)];
    }
}
