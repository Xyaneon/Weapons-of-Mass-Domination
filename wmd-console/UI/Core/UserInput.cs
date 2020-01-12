using WMD.Console.Miscellaneous;

namespace WMD.Console.UI.Core
{
    static class UserInput
    {
        public static bool GetConfirmation(string requestText)
        {
            while (true)
            {
                string response = GetString($"{requestText} (Y[es]/n[o]").ToLower();
                switch (response)
                {
                    case "y":
                    case "yes":
                        return true;
                    case "n":
                    case "no":
                        return false;
                }
            }
        }

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
