﻿using System;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Menus;
using WMD.Game;
using WMD.Game.Commands;

namespace WMD.Console.UI.Core
{
    static class UserInput
    {
        public static IGameCommand GetCommand(GameState gameState)
        {
            Menu actionMenu = GameMenuFactory.CreatePlayerActionMenu(gameState);
            actionMenu.Run();
            if (actionMenu.Result != null)
            {
                var command = (IGameCommand)actionMenu.Result;
                return command;
            }
            else
            {
                throw new InvalidOperationException($"No {typeof(IGameCommand).Name} result value found on action selection menu (this is a bug).");
            }
        }

        public static bool GetConfirmation(string requestText)
        {
            while (true)
            {
                string response = GetString($"{requestText} (Y[es]/n[o])").ToLower();
                switch (response)
                {
                    case "y":
                    case "yes":
                        return true;
                    case "n":
                    case "no":
                        return false;
                }
            }
        }

        public static int GetInteger(string requestText, IntRange range)
        {
            int number = 0;
            bool result = false;

            while (!result)
            {
                PrintPrompt(requestText);
                string input = System.Console.ReadLine();

                result = int.TryParse(input, out number);
                if (result && !range.ContainsValueInclusive(number))
                {
                    result = false;
                }
            }

            return number;
        }

        public static string GetString(string requestText)
        {
            PrintPrompt(requestText);
            return System.Console.ReadLine();
        }

        public static int GetAttackTargetPlayerIndex(GameState gameState)
        {
            Menu playerSelectMenu = GameMenuFactory.CreateAttackTargetPlayerMenu(gameState);
            playerSelectMenu.Run();
            if (playerSelectMenu.Result != null)
            {
                return (int)playerSelectMenu.Result;
            }
            else
            {
                throw new InvalidOperationException($"Menu result for target player selection was null.");
            }
        }

        public static void WaitForPlayerAcknowledgementOfRoundEnd()
        {
            System.Console.ReadKey();
        }

        public static void WaitForPlayerAcknowledgementOfTurnEnd()
        {
            System.Console.ReadKey();
        }

        private static void PrintPrompt(string requestText)
        {
            System.Console.Write($"{requestText}: >");
        }
    }
}
