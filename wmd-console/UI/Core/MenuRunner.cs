using WMD.Console.Miscellaneous;

namespace WMD.Console.UI.Core
{
    class MenuRunner<T>
    {
        public MenuRunner(MenuPrinter<T> menuPrinter)
        {
            MenuPrinter = menuPrinter;
        }

        public T ShowMenuAndGetChoice(Menu<T> menu)
        {
            int maxChoice = menu.Options.Count;
            MenuPrinter.PrintMenu(menu);
            return menu.Options[GetChoice(maxChoice) - 1].SelectionValue;
        }

        private MenuPrinter<T> MenuPrinter { get; }

        private int GetChoice(int maxChoice)
        {
            var range = new IntRange(1, maxChoice);
            return UserInput.GetInteger("Please enter your selection", range);
        }
    }
}
