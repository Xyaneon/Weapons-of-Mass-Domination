using WMD.Console.UI.Core;
using WMD.Console.UI.Menus;
using WMD.Game;

namespace WMD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowTitle();

            var menuPrinter = new MenuPrinter<int>();
            var userInput = new UserInput();
            var menuRunner = new MenuRunner<int>(menuPrinter, userInput);

            MainMenu mainMenu = new MainMenu();

            int mainMenuChoice = 0;
            while(true)
            {
                mainMenuChoice = menuRunner.ShowMenuAndGetChoice(mainMenu);

                switch(mainMenuChoice)
                {
                    case 1:
                        StartSinglePlayerGame();
                        break;
                    case 2:
                        ExitGame();
                        break;
                }
            }
        }

        private static void ExitGame()
        {
            System.Console.WriteLine("Thanks for playing!");
            System.Environment.Exit(0);
        }

        private static void ShowTitle()
        {
            System.Console.WriteLine("Weapons of Mass Domination");
        }

        private static void StartSinglePlayerGame()
        {
            GameState gameState = GameSetup.CreateInitialStateForSinglePlayerGame();
            GameRunner gameRunner = new GameRunner { CurrentGameState = gameState };
            gameRunner.Run();
        }
    }
}
