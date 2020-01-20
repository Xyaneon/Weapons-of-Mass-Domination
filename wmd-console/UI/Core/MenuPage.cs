using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMD.Console.UI.Core
{
    class MenuPage
    {
        public MenuPage(Menu? presenter, string title, params MenuItem[] menuItems)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("The menu page title cannot be blank.", nameof(title));
            }

            if (menuItems.Length < 1)
            {
                throw new ArgumentException("The menu page must have at least one menu item.", nameof(menuItems));
            }

            Presenter = presenter;
            Title = title;
            MenuItems = new List<MenuItem>(menuItems).AsReadOnly();
            HighlightedMenuItem = menuItems.First();
        }

        public string Title { get; }

        public MenuItem HighlightedMenuItem { get; private set; }

        public IReadOnlyList<MenuItem> MenuItems { get; }

        public Menu? Presenter { get; set; }

        public int HighlightedMenuItemIndex
        {
            get => _highlightedMenuItemIndex;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The highlighted menu item index cannot be less than zero.");
                }

                if (value >= MenuItems.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The highlighted menu item index cannot be greater than or equal to the number of menu items.");
                }

                _highlightedMenuItemIndex = value;
                HighlightedMenuItem = MenuItems[_highlightedMenuItemIndex];
            }
        }

        private int _highlightedMenuItemIndex;

        public void ActivateSelection()
        {
            HighlightedMenuItem.Action.Invoke();
        }

        public void MoveSelectionDown()
        {
            if (HighlightedMenuItemIndex < MenuItems.Count - 1)
            {
                HighlightedMenuItemIndex++;
            }
        }

        public void MoveSelectionUp()
        {
            if (HighlightedMenuItemIndex > 0)
            {
                HighlightedMenuItemIndex--;
            }
        }
    }
}
