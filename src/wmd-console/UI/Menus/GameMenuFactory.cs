using System.Collections.Generic;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using Xyaneon.Console.Menus;

namespace WMD.Console.UI.Menus
{
    static class GameMenuFactory
    {
        private const string MenuItemLabel_AttackPlayer = "Attack another player...";
        private const string MenuItemLabel_Back = "Back";
        private const string MenuItemLabel_BuildSecretBase = "Build a secret base";
        private const string MenuItemLabel_Cancel = "Cancel";
        private const string MenuItemLabel_DistributePropaganda = "Distribute propaganda";
        private const string MenuItemLabel_Exit = "Exit";
        private const string MenuItemLabel_HireHenchmen = "Hire henchmen";
        private const string MenuItemLabel_Launch = "Launch...";
        private const string MenuItemLabel_Manufacture = "Manufacture...";
        private const string MenuItemLabel_NewLocalMultiplayerGame = "New local multiplayer game";
        private const string MenuItemLabel_NewSinglePlayerGame = "New single player game";
        private const string MenuItemLabel_PurchaseUnclaimedLand = "Purchase unclaimed land";
        private const string MenuItemLabel_Research = "Research";
        private const string MenuItemLabel_Resign = "Resign";
        private const string MenuItemLabel_SellLand = "Sell land";
        private const string MenuItemLabel_SkipTurn = "Skip turn";
        private const string MenuItemLabel_StealMoney = "Steal money";
        private const string MenuItemLabel_UpgradeSecretBase = "Upgrade your secret base";
        private const string MenuPageTitle_Actions = "Actions";
        private const string MenuPageTitle_ChooseTargetPlayer = "Choose a target player";
        private const string MenuPageTitle_Land = "Land";
        private const string MenuPageTitle_MainMenu = "Main menu";
        private const string MenuPageTitle_Nukes = "Nukes";
        private const string MenuPageTitle_SecretBase = "Secret base";

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

            var playerChoiceMenuItem = new MenuItem(MenuItemLabel_Cancel, () => menu.SetResultAndClose(null));
            menuItems.Enqueue(playerChoiceMenuItem);

            menu.AddPage(MenuPageTitle_ChooseTargetPlayer, menuItems.ToArray());

            return menu;
        }

        public static Menu CreateMainMenu()
        {
            var mainMenuItems = new MenuItem[]
            {
                new MenuItem(MenuItemLabel_NewSinglePlayerGame, MainMenuActions.StartNewSinglePlayerGame),
                new MenuItem(MenuItemLabel_NewLocalMultiplayerGame, MainMenuActions.StartNewMultiplayerGame),
                new MenuItem(MenuItemLabel_Exit, MainMenuActions.ExitGame)
            };

            return new Menu().AddPage(MenuPageTitle_MainMenu, mainMenuItems);
        }

        public static Menu CreatePlayerActionMenu(GameState gameState)
        {
            var menu = new Menu();

            MenuPage landActionsPage = CreateLandCommandsMenuPage(menu, gameState);

            MenuPage secretBasePage = CreateSecretBaseCommandsMenuPage(menu, gameState);

            MenuPage nukePage = CreateNukeCommandsMenuPage(menu, gameState);

            var mainActionItems = new MenuItem[]
            {
                CreateGameCommandMenuItem(MenuItemLabel_StealMoney, menu, gameState, new StealMoneyCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitle_Land, menu, landActionsPage, gameState, new PurchaseUnclaimedLandCommand(), new SellLandCommand()),
                CreateGameCommandMenuItem(MenuItemLabel_AttackPlayer, menu, gameState, new AttackPlayerCommand()),
                CreateGameCommandMenuItem(MenuItemLabel_HireHenchmen, menu, gameState, new HireHenchmenCommand()),
                CreateGameCommandMenuItem(MenuItemLabel_DistributePropaganda, menu, gameState, new DistributePropagandaCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitle_SecretBase, menu, secretBasePage, gameState, new BuildSecretBaseCommand(), new UpgradeSecretBaseCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitle_Nukes, menu, nukePage, gameState, new ResearchNukesCommand(), new ManufactureNukesCommand(), new LaunchNukesCommand()),
                CreateGameCommandMenuItem(MenuItemLabel_SkipTurn, menu, gameState, new SkipTurnCommand()),
                CreateGameCommandMenuItem(MenuItemLabel_Resign, menu, gameState, new ResignCommand()),
            };

            menu.AddPage(MenuPageTitle_Actions, mainActionItems).AddPage(landActionsPage).AddPage(secretBasePage).AddPage(nukePage);
            return menu;
        }

        private static MenuItem CreateBackMenuItem(Menu menu)
        {
            return new MenuItem(MenuItemLabel_Back, () => menu.NavigateBack());
        }

        private static MenuItem CreateGameCommandsPageMenuItem(string menuItemLabel, Menu menu, MenuPage menuPage, GameState gameState, params IGameCommand[] gameCommands)
        {
            bool canExecuteAnyCommand = gameCommands.Any(gameCommand => gameCommand.CanExecuteForState(gameState));
            return new MenuItem($"{menuItemLabel} >", () => menu.NavigateTo(menuPage), canExecuteAnyCommand);
        }
        
        private static MenuItem CreateGameCommandMenuItem(string menuItemLabel, Menu menu, GameState gameState, IGameCommand gameCommand)
        {
            return new MenuItem(menuItemLabel, () => menu.SetResultAndClose(gameCommand), gameCommand.CanExecuteForState(gameState));
        }

        private static MenuPage CreateGameCommandsMenuPage(string menuPageTitle, Menu menu, params MenuItem[] menuItems)
        {
            var standardMenuItems = new MenuItem[]
            {
                CreateBackMenuItem(menu),
            };

            return new MenuPage(menu, menuPageTitle, menuItems.Concat(standardMenuItems));
        }

        private static MenuPage CreateLandCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitle_Land,
            menu,
            CreateGameCommandMenuItem(MenuItemLabel_PurchaseUnclaimedLand, menu, gameState, new PurchaseUnclaimedLandCommand()),
            CreateGameCommandMenuItem(MenuItemLabel_SellLand, menu, gameState, new SellLandCommand())
        );

        private static MenuPage CreateNukeCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitle_Nukes,
            menu,
            CreateGameCommandMenuItem(MenuItemLabel_Research, menu, gameState, new ResearchNukesCommand()),
            CreateGameCommandMenuItem(MenuItemLabel_Manufacture, menu, gameState, new ManufactureNukesCommand()),
            CreateGameCommandMenuItem(MenuItemLabel_Launch, menu, gameState, new LaunchNukesCommand())
        );

        private static MenuPage CreateSecretBaseCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitle_SecretBase,
            menu,
            CreateGameCommandMenuItem(MenuItemLabel_BuildSecretBase, menu, gameState, new BuildSecretBaseCommand()),
            CreateGameCommandMenuItem(MenuItemLabel_UpgradeSecretBase, menu, gameState, new UpgradeSecretBaseCommand())
        );
    }
}
