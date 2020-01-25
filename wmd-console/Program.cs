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

            Menu mainMenu = GameMenuFactory.CreateMainMenu();
            mainMenu.Run();
        }

        private static void ShowTitle()
        {
            System.Console.WriteLine("Weapons of Mass Domination");
        }
    }
}
