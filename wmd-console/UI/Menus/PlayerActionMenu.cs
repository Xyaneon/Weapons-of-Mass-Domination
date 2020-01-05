using WMD.Console.UI.Core;

namespace WMD.Console.UI.Menus
{
    class PlayerActionMenu : Menu
    {
        public PlayerActionMenu() : base(new MenuOption("Steal money"), new MenuOption("Skip turn")) { }
    }
}
