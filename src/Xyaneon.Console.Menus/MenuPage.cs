﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Console.Menus;

public class MenuPage
{
    private const string ArgumentException_MenuPageCannotBeEmpty = "The menu page must have at least one menu item.";
    private const string ArgumentException_MenuPageTitleIsBlank = "The menu page title cannot be blank.";
    private const string ArgumentOutOfRangeException_HighlightedMenuItemIndex_LessThanZero = "The highlighted menu item index cannot be less than zero.";
    private const string ArgumentOutOfRangeException_HighlightedMenuItemIndex_OutsideEndOfMenu = "The highlighted menu item index cannot be greater than or equal to the number of menu items.";

    public MenuPage(Menu? menu, string title, params MenuItem[] menuItems)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(ArgumentException_MenuPageTitleIsBlank, nameof(title));
        }

        if (menuItems.Length < 1)
        {
            throw new ArgumentException(ArgumentException_MenuPageCannotBeEmpty, nameof(menuItems));
        }

        Menu = menu;
        Title = title;
        MenuItems = new List<MenuItem>(menuItems).AsReadOnly();
        HighlightedMenuItem = menuItems.First();
    }
    
    public MenuPage(Menu? menu, string title, IEnumerable<MenuItem> menuItems)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(ArgumentException_MenuPageTitleIsBlank, nameof(title));
        }

        if (!menuItems.Any())
        {
            throw new ArgumentException(ArgumentException_MenuPageCannotBeEmpty, nameof(menuItems));
        }

        Menu = menu;
        Title = title;
        MenuItems = new List<MenuItem>(menuItems).AsReadOnly();
        HighlightedMenuItem = menuItems.First();
    }

    public string Title { get; }

    public MenuItem HighlightedMenuItem { get; private set; }

    public IReadOnlyList<MenuItem> MenuItems { get; }

    public Menu? Menu { get; set; }

    public int HighlightedMenuItemIndex
    {
        get => _highlightedMenuItemIndex;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), ArgumentOutOfRangeException_HighlightedMenuItemIndex_LessThanZero);
            }

            if (value >= MenuItems.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(value), ArgumentOutOfRangeException_HighlightedMenuItemIndex_OutsideEndOfMenu);
            }

            _highlightedMenuItemIndex = value;
            HighlightedMenuItem = MenuItems[_highlightedMenuItemIndex];
        }
    }

    private int _highlightedMenuItemIndex;

    public void ActivateSelection()
    {
        if (HighlightedMenuItem.Enabled)
        {
            HighlightedMenuItem.Action.Invoke();
        }
        else
        {
            System.Console.Beep();
        }
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
