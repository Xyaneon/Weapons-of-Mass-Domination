using System;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Console.Menus
{
    public class Menu
    {
        private const string ArgumentException_PageNotFoundInThisMenu = "The provided page was not found in this menu.";
        private const string InvalidOperationException_CannotRunMenuWithNoPages = "Cannot run a menu with no pages.";
        private const string InvalidOperationException_NoPagesInHistoryToNavigateBackTo = "There are no pages left in the history to navigate back to.";

        public Menu(string? title = null, MenuTheme? theme = null)
        {
            Title = title;
            Theme = theme ?? new MenuTheme();
            _pages = new List<MenuPage>();
            _history = new Stack<MenuPage>();
            HasClosed = false;
            Result = null;
        }

        public MenuPage ActivePage { get => _history.Peek(); }

        public bool HasClosed { get; private set; }

        public IReadOnlyList<MenuPage> Pages { get => _pages.AsReadOnly(); }

        public MenuTheme Theme { get; }

        public string? Title { get; }

        public object? Result { get; private set; }

        private int _height;
        private Stack<MenuPage> _history;
        private List<MenuPage> _pages;
        private int _width;

        public Menu AddPage(string title, params MenuItem[] menuItems)
        {
            var page = new MenuPage(this, title, menuItems);
            _pages.Add(page);
            return this;
        }

        public Menu AddPage(MenuPage page)
        {
            page.Menu = this;
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
                throw new InvalidOperationException(InvalidOperationException_NoPagesInHistoryToNavigateBackTo);
            }

            _history.Pop();
        }

        public void NavigateTo(MenuPage page)
        {
            if (!Pages.Contains(page))
            {
                throw new ArgumentException(ArgumentException_PageNotFoundInThisMenu, nameof(page));
            }

            _history.Push(page);
        }

        public void Run()
        {
            if (Pages.Count < 1)
            {
                throw new InvalidOperationException(InvalidOperationException_CannotRunMenuWithNoPages);
            }

            NavigateTo(Pages.First());

            bool cursorWasVisible = System.Console.CursorVisible;
            System.Console.CursorVisible = false;

            while (!HasClosed)
            {
                PrintActivePageAndUpdateSize();

                bool keyRecognized = false;

                while (!keyRecognized)
                {
                    ConsoleKeyInfo pressedKey = System.Console.ReadKey(true);

                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.UpArrow:
                            keyRecognized = true;
                            ClearCurrentMenuView();
                            ActivePage.MoveSelectionUp();
                            break;
                        case ConsoleKey.DownArrow:
                            keyRecognized = true;
                            ClearCurrentMenuView();
                            ActivePage.MoveSelectionDown();
                            break;
                        case ConsoleKey.Enter:
                            keyRecognized = true;
                            ClearCurrentMenuView();
                            ActivePage.ActivateSelection();
                            break;
                        case ConsoleKey.Escape:
                            keyRecognized = true;
                            ClearCurrentMenuView();
                            if (_history.Count >= 2)
                            {
                                NavigateBack();
                            }
                            else
                            {
                                System.Console.Beep();
                            }
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

        private void ActivateDisabledColors()
        {
            System.Console.BackgroundColor = Theme.DisabledBackgroundColor;
            System.Console.ForegroundColor = Theme.DisabledForegroundColor;
        }

        private void ActivateHighlightColors(bool isEnabled)
        {
            System.Console.BackgroundColor = isEnabled ? Theme.HighlightEnabledBackgroundColor : Theme.HighlightDisabledBackgroundColor;
            System.Console.ForegroundColor = isEnabled ? Theme.HighlightEnabledForegroundColor : Theme.HighlightDisabledForegroundColor;
        }

        private string BuildBreadcrumbsString()
        {
            return string.Join(Theme.BreadcrumbsSeparator, _history.Select(x => x.Title).Reverse());
        }

        private void ClearCurrentLine()
        {
            int currentCursorTop = System.Console.CursorTop;
            System.Console.SetCursorPosition(0, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth));
            System.Console.SetCursorPosition(0, currentCursorTop);
        }

        private void ClearCurrentMenuView()
        {
            for (int i = 0; i < _height; i++)
            {
                MoveCursorUp();
                ClearCurrentLine();
            }
        }

        private void MoveCursorUp()
        {
            System.Console.SetCursorPosition(System.Console.CursorLeft, System.Console.CursorTop - 1);
        }

        private void PrintActivePageAndUpdateSize()
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

            _width = totalContentWidth + 4;
            _height = borderLinesCount + headerLines.Count + ActivePage.MenuItems.Count;

            string topBorderLine = Theme.TopLeftBorderCorner + new string(Theme.TopBorderEdge, totalContentWidth + 2) + Theme.TopRightBorderCorner;
            string middleBorderLine = Theme.MiddleLeftBorderIntersection + new string(Theme.MiddleBorderEdge, totalContentWidth + 2) + Theme.MiddleRightBorderIntersection;
            string bottomBorderLine = Theme.BottomLeftBorderCorner + new string(Theme.BottomBorderEdge, totalContentWidth + 2) + Theme.BottomRightBorderCorner;

            System.Console.WriteLine(topBorderLine);

            headerLines.ForEach(x => System.Console.WriteLine($"{Theme.HeaderLeftEdge} {x.PadRight(totalContentWidth, ' ')} {Theme.HeaderRightEdge}"));

            System.Console.WriteLine(middleBorderLine);

            for (int i = 0; i < ActivePage.MenuItems.Count; i++)
            {
                PrintMenuItem(ActivePage.MenuItems[i], totalContentWidth, i == ActivePage.HighlightedMenuItemIndex);
            }

            System.Console.WriteLine(bottomBorderLine);
        }

        private void PrintMenuItem(MenuItem menuItem, int width, bool isHighlighted)
        {
            System.Console.Write(Theme.MenuItemLeftEdge);
            if (!menuItem.Enabled)
            {
                ActivateDisabledColors();
            }
            if (isHighlighted)
            {
                ActivateHighlightColors(menuItem.Enabled);
            }
            System.Console.Write($" {menuItem.Text.PadRight(width, ' ')} ");
            System.Console.ResetColor();
            System.Console.WriteLine(Theme.MenuItemRightEdge);
        }
    }
}
