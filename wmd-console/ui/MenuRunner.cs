using System;

namespace wmd.console.ui
{
    class MenuRunner
    {
        public MenuRunner(MenuPrinter menuPrinter)
        {
            MenuPrinter = menuPrinter;
        }

        public int ShowMenuAndGetChoice(Menu menu)
        {
            int maxChoice = menu.Options.Count;
            MenuPrinter.PrintMenu(menu);
            return GetChoice(maxChoice);
        }

        private MenuPrinter MenuPrinter { get; }

        private int GetChoice(int maxChoice)
        {
            int choice = 0;
            bool result = false;

            while (!result)
            {
                Console.Write("Please enter your selection: > ");
                string input = Console.ReadLine();

                result = int.TryParse(input, out choice);
                if (result && (choice < 1 || choice > maxChoice))
                {
                    result = false;
                }
            }
            
            return choice;
        }
    }
}