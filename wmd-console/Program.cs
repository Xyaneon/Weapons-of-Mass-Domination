using System;
using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;

namespace WMD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowTitle();

            var menuPrinter = new MenuPrinter<Action>();
            var userInput = new UserInput();
            var menuRunner = new MenuRunner<Action>(menuPrinter, userInput);

            var mainMenu = new MainMenu();
            while (true)
            {
                menuRunner.ShowMenuAndGetChoice(mainMenu).Invoke();
            }
        }

        private static void ShowTitle()
        {
            System.Console.WriteLine("Weapons of Mass Domination");
        }
    }
}
