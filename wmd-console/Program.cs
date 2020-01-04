using System;
using wmd.console.ui;

namespace wmd.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Weapons of Mass Domination");

            MenuPrinter menuPrinter = new MenuPrinter();
            MenuRunner menuRunner = new MenuRunner(menuPrinter);

            Menu mainMenu = CreateMainMenu();

            int mainMenuChoice = 0;
            while(mainMenuChoice != mainMenu.Options.Count)
            {
                mainMenuChoice = menuRunner.ShowMenuAndGetChoice(mainMenu);
            }

            Console.WriteLine("Thanks for playing!");
        }

        private static Menu CreateMainMenu()
        {
            return new Menu(
                new MenuOption("New single player game"),
                new MenuOption("Exit")
                );
        }
    }
}
