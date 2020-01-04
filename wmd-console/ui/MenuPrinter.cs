using System;

namespace wmd.console.ui
{
    class MenuPrinter
    {
        public MenuPrinter() {}

        public void PrintMenu(Menu menu)
        {
            for (int i = 0; i < menu.Options.Count; i++)
            {
                PrintMenuOption(i + 1, menu.Options[i].Name);
            }
        }

        private void PrintMenuOption(int optionNumber, string optionName)
        {
            Console.WriteLine($"{optionNumber}.) {optionName}");
        }
    }
}