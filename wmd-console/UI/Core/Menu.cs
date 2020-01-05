using System.Collections.Generic;

namespace WMD.Console.UI.Core
{
    class Menu<T>
    {
        public Menu(params MenuOption<T>[] options)
        {
            Options = new List<MenuOption<T>>(options).AsReadOnly();
        }

        public IReadOnlyList<MenuOption<T>> Options { get; }
    }
}
