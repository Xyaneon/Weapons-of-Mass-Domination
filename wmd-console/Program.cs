using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;

namespace WMD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Weapons of Mass Domination");

            MenuPrinter menuPrinter = new MenuPrinter();
            UserInput userInput = new UserInput();
            MenuRunner menuRunner = new MenuRunner(menuPrinter, userInput);

            MainMenu mainMenu = new MainMenu();

            int mainMenuChoice = 0;
            while(mainMenuChoice != mainMenu.Options.Count)
            {
                mainMenuChoice = menuRunner.ShowMenuAndGetChoice(mainMenu);
            }

            System.Console.WriteLine("Thanks for playing!");
        }
    }
}
