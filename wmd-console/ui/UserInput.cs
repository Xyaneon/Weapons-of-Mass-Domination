using System;
using System.Collections.Generic;
using System.Text;
using wmd.console.Miscellaneous;

namespace wmd.console.ui
{
    class UserInput
    {
        public UserInput() { }

        public int GetInteger(string requestText, IntRange range)
        {
            int number = 0;
            bool result = false;

            while (!result)
            {
                PrintPrompt(requestText);
                string input = Console.ReadLine();

                result = int.TryParse(input, out number);
                if (result && !range.ContainsValueInclusive(number))
                {
                    result = false;
                }
            }

            return number;
        }

        public string GetString(string requestText)
        {
            PrintPrompt(requestText);
            return Console.ReadLine();
        }

        private void PrintPrompt(string requestText)
        {
            Console.Write($"{requestText}: >");
        }
    }
}
