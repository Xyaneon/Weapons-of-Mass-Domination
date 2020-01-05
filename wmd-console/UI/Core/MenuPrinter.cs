namespace WMD.Console.UI.Core
{
    class MenuPrinter<T>
    {
        public MenuPrinter() {}

        public void PrintMenu(Menu<T> menu)
        {
            for (int i = 0; i < menu.Options.Count; i++)
            {
                PrintMenuOption(i + 1, menu.Options[i].Name);
            }
        }

        private void PrintMenuOption(int optionNumber, string optionName)
        {
            System.Console.WriteLine($"{optionNumber}.) {optionName}");
        }
    }
}
