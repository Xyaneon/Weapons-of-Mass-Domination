using System;
using WMD.Console.UI.Core;

namespace WMD.Console.UI.Menus
{
    class MainMenu : Menu<Action>
    {
        public MainMenu() : base(CreateMenuOptions()) { }

        private static MenuOption<Action>[] CreateMenuOptions()
        {
            return new MenuOption<Action>[]
            {
                CreateMenuOption("New single player game", MainMenuActions.StartNewSinglePlayerGame),
                CreateMenuOption("Exit", MainMenuActions.ExitGame)
            };
        }

        private static MenuOption<Action> CreateMenuOption(string optionName, Action selectionAction)
        {
            return new MenuOption<Action>(optionName, selectionAction);
        }
    }
}
