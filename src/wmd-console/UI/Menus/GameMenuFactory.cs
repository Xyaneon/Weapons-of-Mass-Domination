using System.Collections.Generic;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;
using Xyaneon.Console.Menus;

namespace WMD.Console.UI.Menus
{
    static class GameMenuFactory
    {
        private const string MenuPageItemLabelFormatString = "{0} >";

        private static class MenuItemLabels
        {
            public const string AttackPlayer = "Attack another player...";
            public const string Back = "Back";
            public const string BuildSecretBase = "Build a secret base";
            public const string Cancel = "Cancel";
            public const string ChangeDailyWage = "Change daily wage...";
            public const string DistributePropaganda = "Distribute propaganda";
            public const string Exit = "Exit";
            public const string HireHenchmen = "Hire henchmen...";
            public const string Launch = "Launch...";
            public const string Manufacture = "Manufacture...";
            public const string NewLocalMultiplayerGame = "New local multiplayer game";
            public const string NewSinglePlayerGame = "New single player game";
            public const string PurchaseUnclaimedLand = "Purchase unclaimed land";
            public const string Research = "Research";
            public const string Resign = "Resign";
            public const string SellLand = "Sell land";
            public const string SkipTurn = "Skip turn";
            public const string StealMoney = "Steal money";
            public const string UpgradeSecretBase = "Upgrade your secret base";
        }

        private static class MenuPageTitles
        {
            public const string Actions = "Actions";
            public const string ChooseTargetPlayer = "Choose a target player";
            public const string Henchmen = "Henchmen";
            public const string Land = "Land";
            public const string MainMenu = "Main menu";
            public const string Nukes = "Nukes";
            public const string SecretBase = "Secret base";
        }

        public static Menu CreateAttackTargetPlayerMenu(GameState gameState)
        {
            var menu = new Menu();
            var menuItems = new Queue<MenuItem>();

            GameStateChecks.FindIndicesOfPlayersOtherThanCurrent(gameState).ToList().ForEach(index =>
            {
                var playerChoiceMenuItem = new MenuItem(gameState.Players[index].Identification.Name, () => menu.SetResultAndClose(index));
                menuItems.Enqueue(playerChoiceMenuItem);
            });

            menuItems.Enqueue(new MenuItem(MenuItemLabels.Cancel, () => menu.SetResultAndClose(null)));

            menu.AddPage(MenuPageTitles.ChooseTargetPlayer, menuItems.ToArray());

            return menu;
        }

        public static Menu CreateMainMenu()
        {
            var mainMenuItems = new MenuItem[]
            {
                new MenuItem(MenuItemLabels.NewSinglePlayerGame, MainMenuActions.StartNewSinglePlayerGame),
                new MenuItem(MenuItemLabels.NewLocalMultiplayerGame, MainMenuActions.StartNewMultiplayerGame),
                new MenuItem(MenuItemLabels.Exit, MainMenuActions.ExitGame)
            };

            return new Menu().AddPage(MenuPageTitles.MainMenu, mainMenuItems);
        }

        public static Menu CreatePlayerActionMenu(GameState gameState)
        {
            var menu = new Menu();

            MenuPage landActionsPage = CreateLandCommandsMenuPage(menu, gameState);

            MenuPage henchmenActionsPage = CreateHenchmenCommandsMenuPage(menu, gameState);

            MenuPage secretBasePage = CreateSecretBaseCommandsMenuPage(menu, gameState);

            MenuPage nukePage = CreateNukeCommandsMenuPage(menu, gameState);

            var mainActionItems = new MenuItem[]
            {
                CreateGameCommandMenuItem(MenuItemLabels.StealMoney, menu, gameState, new StealMoneyCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitles.Land, menu, landActionsPage, gameState, new PurchaseUnclaimedLandCommand(), new SellLandCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitles.Henchmen, menu, henchmenActionsPage, gameState, new HireHenchmenCommand(), new ChangeDailyWageCommand()),
                CreateGameCommandMenuItem(MenuItemLabels.AttackPlayer, menu, gameState, new AttackPlayerCommand()),
                CreateGameCommandMenuItem(MenuItemLabels.DistributePropaganda, menu, gameState, new DistributePropagandaCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitles.SecretBase, menu, secretBasePage, gameState, new BuildSecretBaseCommand(), new UpgradeSecretBaseCommand()),
                CreateGameCommandsPageMenuItem(MenuPageTitles.Nukes, menu, nukePage, gameState, new ResearchNukesCommand(), new ManufactureNukesCommand(), new LaunchNukesCommand()),
                CreateGameCommandMenuItem(MenuItemLabels.SkipTurn, menu, gameState, new SkipTurnCommand()),
                CreateGameCommandMenuItem(MenuItemLabels.Resign, menu, gameState, new ResignCommand()),
            };

            MenuPage mainPage = new MenuPage(menu, MenuPageTitles.Actions, mainActionItems);

            menu.AddPages(
                mainPage,
                landActionsPage,
                henchmenActionsPage,
                secretBasePage,
                nukePage
            );

            return menu;
        }

        private static MenuItem CreateBackMenuItem(Menu menu) => new(MenuItemLabels.Back, () => menu.NavigateBack());

        private static MenuItem CreateGameCommandsPageMenuItem(string menuItemLabel, Menu menu, MenuPage menuPage, GameState gameState, params IGameCommand[] gameCommands)
        {
            bool canExecuteAnyCommand = gameCommands.Any(gameCommand => gameCommand.CanExecuteForState(gameState));
            return new MenuItem(string.Format(MenuPageItemLabelFormatString, menuItemLabel), () => menu.NavigateTo(menuPage), canExecuteAnyCommand);
        }

        private static MenuItem CreateGameCommandMenuItem(string menuItemLabel, Menu menu, GameState gameState, IGameCommand gameCommand) =>
            new(menuItemLabel, () => menu.SetResultAndClose(gameCommand), gameCommand.CanExecuteForState(gameState));

        private static MenuPage CreateGameCommandsMenuPage(string menuPageTitle, Menu menu, params MenuItem[] menuItems) =>
            new(menu, menuPageTitle, menuItems.Concat(new MenuItem[] { CreateBackMenuItem(menu) }));

        private static MenuPage CreateHenchmenCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitles.Henchmen,
            menu,
            CreateGameCommandMenuItem(MenuItemLabels.HireHenchmen, menu, gameState, new HireHenchmenCommand()),
            CreateGameCommandMenuItem(MenuItemLabels.ChangeDailyWage, menu, gameState, new ChangeDailyWageCommand())
        );
        
        private static MenuPage CreateLandCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitles.Land,
            menu,
            CreateGameCommandMenuItem(MenuItemLabels.PurchaseUnclaimedLand, menu, gameState, new PurchaseUnclaimedLandCommand()),
            CreateGameCommandMenuItem(MenuItemLabels.SellLand, menu, gameState, new SellLandCommand())
        );

        private static MenuPage CreateNukeCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitles.Nukes,
            menu,
            CreateGameCommandMenuItem(MenuItemLabels.Research, menu, gameState, new ResearchNukesCommand()),
            CreateGameCommandMenuItem(MenuItemLabels.Manufacture, menu, gameState, new ManufactureNukesCommand()),
            CreateGameCommandMenuItem(MenuItemLabels.Launch, menu, gameState, new LaunchNukesCommand())
        );

        private static MenuPage CreateSecretBaseCommandsMenuPage(Menu menu, GameState gameState) => CreateGameCommandsMenuPage(
            MenuPageTitles.SecretBase,
            menu,
            CreateGameCommandMenuItem(MenuItemLabels.BuildSecretBase, menu, gameState, new BuildSecretBaseCommand()),
            CreateGameCommandMenuItem(MenuItemLabels.UpgradeSecretBase, menu, gameState, new UpgradeSecretBaseCommand())
        );
    }
}
