using System;

namespace Xyaneon.Console.Menus;

public class MenuItem
{
    private const string ArgumentException_MenuItemText_CannotBeBlank = "The menu item text cannot be blank.";

    public MenuItem(string text, Action action, bool enabled = true)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(ArgumentException_MenuItemText_CannotBeBlank, nameof(text));
        }

        Text = text;
        Action = action;
        Enabled = enabled;
    }

    public Action Action { get; }

    public string Text { get; }

    public bool Enabled { get; }
}
