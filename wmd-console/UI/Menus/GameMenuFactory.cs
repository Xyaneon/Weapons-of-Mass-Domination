using System;
using WMD.Console.UI.Core;
using WMD.Game.Actions;

namespace WMD.Console.UI.Menus
{
    static class GameMenuFactory
    {
        public static Menu<Action> CreateMainMenu()
        {
            var options = new MenuOption<Action>[]
            {
                new MenuOption<Action>("New single player game", MainMenuActions.StartNewSinglePlayerGame),
                new MenuOption<Action>("Exit", MainMenuActions.ExitGame)
            };

            return new Menu<Action>(options);
        }

        public static Menu<PlayerActionKind> CreatePlayerActionMenu()
        {
            var options = new MenuOption<PlayerActionKind>[]
            {
                new MenuOption<PlayerActionKind>("Steal money", PlayerActionKind.StealMoney),
                new MenuOption<PlayerActionKind>("Purchase unclaimed land", PlayerActionKind.PurchaseUnclaimedLand),
                new MenuOption<PlayerActionKind>("Sell land", PlayerActionKind.SellLand),
                new MenuOption<PlayerActionKind>("Hire henchmen", PlayerActionKind.HireHenchmen),
                new MenuOption<PlayerActionKind>("Build/upgrade your secret base", PlayerActionKind.UpgradeSecretBase),
                new MenuOption<PlayerActionKind>("Skip turn", PlayerActionKind.Skip),
                new MenuOption<PlayerActionKind>("Resign", PlayerActionKind.Resign)
            };

            return new Menu<PlayerActionKind>(options);
        }
    }
}
