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

            //var menuPrinter = new MenuPrinter<Action>();
            //var menuRunner = new MenuRunner<Action>(menuPrinter);

            //var mainMenu = GameMenuFactory.CreateMainMenu();
            //while (true)
            //{
            //    menuRunner.ShowMenuAndGetChoice(mainMenu).Invoke();
            //}

            Menu mainMenu = GameMenuFactory.CreateMainMenu();
            mainMenu.Run();
        }

        private static void ShowTitle()
        {
            System.Console.WriteLine("Weapons of Mass Domination");
        }
    }
}
