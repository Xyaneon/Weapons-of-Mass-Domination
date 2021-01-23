using System;
using WMD.Game.State.Data;

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
            var gameRunner = new GameRunner(gameState);
            gameRunner.Run();
        }
    }
}
