using System.Collections.Generic;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using Xyaneon.Console.Menus;

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
                    var playerChoiceMenuItem = new MenuItem(gameState.Players[index].Identification.Name, () => menu.SetResultAndClose(index));
                    menuItems.Enqueue(playerChoiceMenuItem);
                }
            });

            var playerChoiceMenuItem = new MenuItem("Cancel", () => menu.SetResultAndClose(null));
            menuItems.Enqueue(playerChoiceMenuItem);

            menu.AddPage("Choose a Target Player", menuItems.ToArray());

            return menu;
        }

        public static Menu CreateMainMenu()
        {
            var mainMenuItems = new MenuItem[]
            {
                new MenuItem("New single player game", MainMenuActions.StartNewSinglePlayerGame),
                new MenuItem("New local multiplayer game", MainMenuActions.StartNewMultiplayerGame),
                new MenuItem("Exit", MainMenuActions.ExitGame)
            };

            return new Menu().AddPage("Main menu", mainMenuItems);
        }

        public static Menu CreatePlayerActionMenu(GameState gameState)
        {
            var menu = new Menu();

            var landActionItems = new MenuItem[]
            {
                new MenuItem("Purchase unclaimed land", () => menu.SetResultAndClose(new PurchaseUnclaimedLandCommand()), new PurchaseUnclaimedLandCommand().CanExecuteForState(gameState)),
                new MenuItem("Sell land", () => menu.SetResultAndClose(new SellLandCommand()), new SellLandCommand().CanExecuteForState(gameState)),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var landActionsPage = new MenuPage(menu, "Land", landActionItems);

            var secretBaseActionItems = new MenuItem[]
            {
                new MenuItem("Build a secret base", () => menu.SetResultAndClose(new BuildSecretBaseCommand()), new BuildSecretBaseCommand().CanExecuteForState(gameState)),
                new MenuItem("Upgrade your secret base", () => menu.SetResultAndClose(new UpgradeSecretBaseCommand()), new UpgradeSecretBaseCommand().CanExecuteForState(gameState)),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var secretBasePage = new MenuPage(menu, "Secret Base", secretBaseActionItems);

            var nukeActionItems = new MenuItem[]
            {
                new MenuItem("Research", () => menu.SetResultAndClose(new ResearchNukesCommand()), new ResearchNukesCommand().CanExecuteForState(gameState)),
                new MenuItem("Manufacture...", () => menu.SetResultAndClose(new ManufactureNukesCommand()), new ManufactureNukesCommand().CanExecuteForState(gameState)),
                new MenuItem("Launch...", () => menu.SetResultAndClose(new LaunchNukesCommand()), new LaunchNukesCommand().CanExecuteForState(gameState)),
                new MenuItem("Back", () => menu.NavigateBack())
            };

            var nukePage = new MenuPage(menu, "Nukes", nukeActionItems);

            var mainActionItems = new MenuItem[]
            {
                new MenuItem("Steal money", () => menu.SetResultAndClose(new StealMoneyCommand()), new StealMoneyCommand().CanExecuteForState(gameState)),
                new MenuItem("Land...", () => menu.NavigateTo(landActionsPage), new PurchaseUnclaimedLandCommand().CanExecuteForState(gameState) || new SellLandCommand().CanExecuteForState(gameState)),
                new MenuItem("Attack another player...", () => menu.SetResultAndClose(new AttackPlayerCommand()), new AttackPlayerCommand().CanExecuteForState(gameState)),
                new MenuItem("Hire henchmen", () => menu.SetResultAndClose(new HireHenchmenCommand()), new HireHenchmenCommand().CanExecuteForState(gameState)),
                new MenuItem("Distribute propaganda", () => menu.SetResultAndClose(new DistributePropagandaCommand()), new DistributePropagandaCommand().CanExecuteForState(gameState)),
                new MenuItem("Secret base...", () => menu.NavigateTo(secretBasePage), new BuildSecretBaseCommand().CanExecuteForState(gameState) || new UpgradeSecretBaseCommand().CanExecuteForState(gameState)),
                new MenuItem("Nukes...", () => menu.NavigateTo(nukePage), new ResearchNukesCommand().CanExecuteForState(gameState) || new ManufactureNukesCommand().CanExecuteForState(gameState) || new LaunchNukesCommand().CanExecuteForState(gameState)),
                new MenuItem("Skip turn", () => menu.SetResultAndClose(new SkipTurnCommand()), new SkipTurnCommand().CanExecuteForState(gameState)),
                new MenuItem("Resign", () => menu.SetResultAndClose(new ResignCommand()), new ResignCommand().CanExecuteForState(gameState))
            };

            menu.AddPage("Actions", mainActionItems).AddPage(landActionsPage).AddPage(secretBasePage).AddPage(nukePage);
            return menu;
        }
    }
}
