using System.Collections.Generic;

namespace WMD.Console.UI.Core
{
    class Menu
    {
        public Menu(params MenuOption[] options)
        {
            Options = new List<MenuOption>(options).AsReadOnly();
        }

        public IReadOnlyList<MenuOption> Options { get; }
    }
}
