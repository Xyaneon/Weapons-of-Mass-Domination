namespace WMD.Console.UI.Core
{
    class MenuOption<T>
    {
        public MenuOption(string name, T selectionValue)
        {
            Name = name;
            SelectionValue = selectionValue;
        }

        public string Name { get; }

        public T SelectionValue { get; }
    }
}
