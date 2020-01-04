using WMD.Console.Miscellaneous;

namespace WMD.Console.UI.Core
{
    class MenuRunner
    {
        public MenuRunner(MenuPrinter menuPrinter, UserInput userInput)
        {
            MenuPrinter = menuPrinter;
            UserInput = userInput;
        }

        public int ShowMenuAndGetChoice(Menu menu)
        {
            int maxChoice = menu.Options.Count;
            MenuPrinter.PrintMenu(menu);
            return GetChoice(maxChoice);
        }

        private MenuPrinter MenuPrinter { get; }

        private UserInput UserInput { get; }

        private int GetChoice(int maxChoice)
        {
            var range = new IntRange(1, maxChoice);
            return UserInput.GetInteger("Please enter your selection", range);
        }
    }
}
