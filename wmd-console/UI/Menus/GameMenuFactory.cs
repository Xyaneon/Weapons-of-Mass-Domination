using WMD.Console.UI.Core;
using WMD.Game.Actions;

namespace WMD.Console.UI.Menus
{
    static class GameMenuFactory
    {
        public static Menu CreateMainMenu()
        {
            var mainMenuItems = new MenuItem[]
            {
                new MenuItem("New single player game", MainMenuActions.StartNewSinglePlayerGame),
                new MenuItem("Exit", MainMenuActions.ExitGame)
            };

            return new Menu().AddPage("Main menu", mainMenuItems);
        }

        public static Menu CreatePlayerActionMenu()
        {
            var menu = new Menu();

            var landActionItems = new MenuItem[]
            {
                new MenuItem("Purchase unclaimed land", () => menu.SetResultAndClose(PlayerActionKind.PurchaseUnclaimedLand)),
                new MenuItem("Sell land", () => menu.SetResultAndClose(PlayerActionKind.SellLand)),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var landActionsPage = new MenuPage(menu, "Land", landActionItems);

            var secretBaseActionItems = new MenuItem[]
            {
                new MenuItem("Build a secret base", () => menu.SetResultAndClose(PlayerActionKind.BuildSecretBase)),
                new MenuItem("Upgrade your secret base", () => menu.SetResultAndClose(PlayerActionKind.UpgradeSecretBase)),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var secretBasePage = new MenuPage(menu, "Secret Base", secretBaseActionItems);

            var mainActionItems = new MenuItem[]
            {
                new MenuItem("Steal money", () => menu.SetResultAndClose(PlayerActionKind.StealMoney)),
                new MenuItem("Land...", () => menu.NavigateTo(landActionsPage)),
                new MenuItem("Hire henchmen", () => menu.SetResultAndClose(PlayerActionKind.HireHenchmen)),
                new MenuItem("Secret base...", () => menu.NavigateTo(secretBasePage)),
                new MenuItem("Skip turn", () => menu.SetResultAndClose(PlayerActionKind.Skip)),
                new MenuItem("Resign", () => menu.SetResultAndClose(PlayerActionKind.Resign))
            };

            menu.AddPage("Actions", mainActionItems).AddPage(landActionsPage).AddPage(secretBasePage);
            return menu;
        }
    }
}
