using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Commands;

namespace WMD.Console.UI.Menus
{
    static class GameMenuFactory
    {
        public static Menu CreateAttackTargetPlayerMenu(GameState gameState)
        {
            var menu = new Menu();
            Queue<MenuItem> menuItems = new Queue<MenuItem>();

            Enumerable.Range(0, gameState.Players.Count).ToList().ForEach(index =>
            {
                if (index != gameState.CurrentPlayerIndex)
                {
                    var playerChoiceMenuItem = new MenuItem(gameState.Players[index].Name, () => menu.SetResultAndClose(index));
                    menuItems.Enqueue(playerChoiceMenuItem);
                }
            });

            menu.AddPage("Choose a Target Player", menuItems.ToArray());

            return menu;
        }

        public static Menu CreateMainMenu()
        {
            var mainMenuItems = new MenuItem[]
            {
                new MenuItem("New single player game", MainMenuActions.StartNewSinglePlayerGame),
                new MenuItem("Exit", MainMenuActions.ExitGame)
            };

            return new Menu().AddPage("Main menu", mainMenuItems);
        }

        public static Menu CreatePlayerActionMenu(GameState gameState)
        {
            var menu = new Menu();

            var landActionItems = new MenuItem[]
            {
                new MenuItem("Purchase unclaimed land", () => menu.SetResultAndClose(new PurchaseUnclaimedLandCommand())),
                new MenuItem("Sell land", () => menu.SetResultAndClose(new SellLandCommand())),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var landActionsPage = new MenuPage(menu, "Land", landActionItems);

            var secretBaseActionItems = new MenuItem[]
            {
                new MenuItem("Build a secret base", () => menu.SetResultAndClose(new BuildSecretBaseCommand())),
                new MenuItem("Upgrade your secret base", () => menu.SetResultAndClose(new UpgradeSecretBaseCommand())),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var secretBasePage = new MenuPage(menu, "Secret Base", secretBaseActionItems);

            var mainActionItems = new MenuItem[]
            {
                new MenuItem("Steal money", () => menu.SetResultAndClose(new StealMoneyCommand())),
                new MenuItem("Land...", () => menu.NavigateTo(landActionsPage)),
                new MenuItem("Attack another player...", () => menu.SetResultAndClose(new AttackPlayerCommand())),
                new MenuItem("Hire henchmen", () => menu.SetResultAndClose(new HireHenchmenCommand())),
                new MenuItem("Secret base...", () => menu.NavigateTo(secretBasePage)),
                new MenuItem("Skip turn", () => menu.SetResultAndClose(new SkipTurnCommand())),
                new MenuItem("Resign", () => menu.SetResultAndClose(new ResignCommand()))
            };

            menu.AddPage("Actions", mainActionItems).AddPage(landActionsPage).AddPage(secretBasePage);
            return menu;
        }
    }
}
