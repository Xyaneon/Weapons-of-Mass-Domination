using System;
using WMD.Game;

namespace WMD.Console.UI.Menus
{
    static class MainMenuActions
    {
        public static void ExitGame()
        {
            System.Console.WriteLine("Thanks for playing!");
            Environment.Exit(0);
        }

        public static void StartNewSinglePlayerGame()
        {
            GameState gameState = GameSetup.CreateInitialStateForSinglePlayerGame();
            GameRunner gameRunner = new GameRunner { CurrentGameState = gameState };
            gameRunner.Run();
        }
    }
}
