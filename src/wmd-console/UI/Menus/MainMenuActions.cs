using System;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Menus
{
    static class MainMenuActions
    {
        private const string ExitMessage = "Thanks for playing!";

        public static void ExitGame()
        {
            System.Console.WriteLine(ExitMessage);
            Environment.Exit(0);
        }

        public static void StartNewSinglePlayerGame()
        {
            GameState gameState = GameSetup.CreateInitialGameState(true);
            new GameRunner(gameState).Run();
        }

        public static void StartNewMultiplayerGame()
        {
            GameState gameState = GameSetup.CreateInitialGameState(false);
            new GameRunner(gameState).Run();
        }
    }
}
