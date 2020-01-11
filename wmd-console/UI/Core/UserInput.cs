﻿using WMD.Console.Miscellaneous;

namespace WMD.Console.UI.Core
{
    static class UserInput
    {
        public static int GetInteger(string requestText, IntRange range)
        {
            int number = 0;
            bool result = false;

            while (!result)
            {
                PrintPrompt(requestText);
                string input = System.Console.ReadLine();

                result = int.TryParse(input, out number);
                if (result && !range.ContainsValueInclusive(number))
                {
                    result = false;
                }
            }

            return number;
        }

        public static string GetString(string requestText)
        {
            PrintPrompt(requestText);
            return System.Console.ReadLine();
        }

        private static void PrintPrompt(string requestText)
        {
            System.Console.Write($"{requestText}: >");
        }
    }
}
