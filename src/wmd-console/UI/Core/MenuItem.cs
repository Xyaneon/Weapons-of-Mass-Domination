using System;

namespace WMD.Console.UI.Core
{
    class MenuItem
    {
        public MenuItem(string text, Action action)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("The menu item text cannot be blank.", nameof(text));
            }

            Text = text;
            Action = action;
        }

        public Action Action { get; }

        public string Text { get; }
    }
}
