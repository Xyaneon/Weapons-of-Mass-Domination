using System.Collections.Generic;
using System.Linq;
using WMD.Console.UI.Core;

namespace WMD.Console.UI.Menus
{
    class MainMenu : Menu<int>
    {
        public MainMenu() : base(CreateMenuOptions()) { }

        private static MenuOption<int>[] CreateMenuOptions()
        {
            var options = new string[] {
                "New single player game",
                "Exit"
            };

            return Enumerable.Range(0, options.Length)
                .Select(index => new MenuOption<int>(options[index], index + 1))
                .ToArray();
        }
    }
}
