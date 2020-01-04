using WMD.Console.UI.Core;

namespace WMD.Console.UI.Menus
{
    class MainMenu : Menu
    {
        public MainMenu() : base(new MenuOption("New single player game"), new MenuOption("Exit")) { }
    }
}
