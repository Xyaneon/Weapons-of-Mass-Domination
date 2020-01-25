using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMD.Console.UI.Core
{
    class Menu
    {
        private const string BreadcrumbsSeparator = " > ";

        public Menu(string? title = null)
        {
            Title = title;
            _pages = new List<MenuPage>();
            _history = new Stack<MenuPage>();
            HasClosed = false;
            HighlightBackgroundColor = ConsoleColor.Gray;
            HighlightForegroundColor = ConsoleColor.Black;
            Result = null;
        }

        public MenuPage ActivePage { get => _history.Peek(); }

        public bool HasClosed { get; private set; }

        public ConsoleColor HighlightBackgroundColor { get; set; }

        public ConsoleColor HighlightForegroundColor { get; set; }

        public IReadOnlyList<MenuPage> Pages { get => _pages.AsReadOnly(); }

        public string? Title { get; }

        public object? Result { get; private set; }

        private Stack<MenuPage> _history;
        private List<MenuPage> _pages;

        public Menu AddPage(string title, params MenuItem[] menuItems)
        {
            var page = new MenuPage(this, title, menuItems);
            _pages.Add(page);
            return this;
        }

        public Menu AddPage(MenuPage page)
        {
            page.Presenter = this;
            _pages.Add(page);
            return this;
        }

        public void Close()
        {
            HasClosed = true;
        }

        public void NavigateBack()
        {
            if (_history.Count <= 1)
            {
                throw new InvalidOperationException("There are no pages left in the history to navigate back to.");
            }

            _history.Pop();
        }

        public void NavigateTo(MenuPage page)
        {
            if (!Pages.Contains(page))
            {
                throw new ArgumentException("The provided page was not found in this menu.", nameof(page));
            }

            _history.Push(page);
        }

        public void Run()
        {
            if (Pages.Count < 1)
            {
                throw new InvalidOperationException("Cannot run a menu with no pages.");
            }

            NavigateTo(Pages.First());

            bool cursorWasVisible = System.Console.CursorVisible;
            System.Console.CursorVisible = false;
            while (!HasClosed)
            {
                int menuLines = PrintActivePageAndGetNumberOfLines();

                bool keyRecognized = false;

                while (!keyRecognized)
                {
                    ConsoleKeyInfo pressedKey = System.Console.ReadKey();

                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.UpArrow:
                            keyRecognized = true;
                            ClearCurrentMenuView(menuLines);
                            ActivePage.MoveSelectionUp();
                            break;
                        case ConsoleKey.DownArrow:
                            keyRecognized = true;
                            ClearCurrentMenuView(menuLines);
                            ActivePage.MoveSelectionDown();
                            break;
                        case ConsoleKey.Enter:
                            keyRecognized = true;
                            ClearCurrentMenuView(menuLines);
                            ActivePage.ActivateSelection();
                            break;
                    } 
                }
            }
            System.Console.CursorVisible = cursorWasVisible;
        }

        public void SetResultAndClose(object? result)
        {
            Result = result;
            Close();
        }

        private void ActivateHighlightColors()
        {
            System.Console.BackgroundColor = HighlightBackgroundColor;
            System.Console.ForegroundColor = HighlightForegroundColor;
        }

        private string BuildBreadcrumbsString()
        {
            return string.Join(BreadcrumbsSeparator, _history.Select(x => x.Title).Reverse());
        }

        private void ClearCurrentLine()
        {
            int currentCursorTop = System.Console.CursorTop;
            System.Console.SetCursorPosition(0, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth));
            System.Console.SetCursorPosition(0, currentCursorTop);
        }

        private void ClearCurrentMenuView(int menuLines)
        {
            for (int i = 0; i < menuLines; i++)
            {
                MoveCursorUp();
                ClearCurrentLine();
            }
        }

        private void MoveCursorUp()
        {
            System.Console.SetCursorPosition(System.Console.CursorLeft, System.Console.CursorTop - 1);
        }

        private int PrintActivePageAndGetNumberOfLines()
        {
            string breadcrumbs = BuildBreadcrumbsString();
            int breadcrumbsWidth = breadcrumbs.Length;
            int menuItemWidth = ActivePage.MenuItems.Select(x => x.Text.Length).Max();
            int totalContentWidth = Math.Max(breadcrumbsWidth, menuItemWidth);
            int borderLinesCount = 3;

            var headerLines = new List<string>(new string[]
            {
                breadcrumbs
            });

            string topBorderLine = "╔" + new string('═', totalContentWidth + 2) + "╗";
            string middleBorderLine = "╠" + new string('═', totalContentWidth + 2) + "╣";
            string bottomBorderLine = "╚" + new string('═', totalContentWidth + 2) + "╝";

            System.Console.WriteLine(topBorderLine);

            headerLines.ForEach(x => System.Console.WriteLine($"║ {x.PadRight(totalContentWidth, ' ')} ║"));

            System.Console.WriteLine(middleBorderLine);

            for (int i = 0; i < ActivePage.MenuItems.Count; i++)
            {
                PrintMenuItem(ActivePage.MenuItems[i], totalContentWidth, i == ActivePage.HighlightedMenuItemIndex);
            }

            System.Console.WriteLine(bottomBorderLine);

            return borderLinesCount + headerLines.Count + ActivePage.MenuItems.Count;
        }

        private void PrintMenuItem(MenuItem menuItem, int width, bool isHighlighted)
        {
            System.Console.Write("║");
            if (isHighlighted)
            {
                ActivateHighlightColors();
            }
            System.Console.Write($" {menuItem.Text.PadRight(width, ' ')} ");
            System.Console.ResetColor();
            System.Console.WriteLine("║");
        }
    }
}
