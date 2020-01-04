using System.Collections.Generic;

namespace wmd.console.ui
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