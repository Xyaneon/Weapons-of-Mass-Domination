using System;
using WMD.Console.UI.Core;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console.UI.Menus
{
    class PlayerActionMenu : Menu<Func<GameState, ActionResult>>
    {
        public PlayerActionMenu() : base(CreateMenuOptions()) { }

        private static MenuOption<Func<GameState, ActionResult>>[] CreateMenuOptions()
        {
            return new MenuOption<Func<GameState, ActionResult>>[]
            {
                CreateMenuOption("Steal money", PlayerActions.CurrentPlayerStealsMoney),
                CreateMenuOption("Skip turn", PlayerActions.CurrentPlayerSkipsTurn),
                CreateMenuOption("Resign", PlayerActions.CurrentPlayerResigns)
            };
        }

        private static MenuOption<Func<GameState, ActionResult>> CreateMenuOption(string optionName, Func<GameState, ActionResult> func)
        {
            return new MenuOption<Func<GameState, ActionResult>>(optionName, func);
        }
    }
}
