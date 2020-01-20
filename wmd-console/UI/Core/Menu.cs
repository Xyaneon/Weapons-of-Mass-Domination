﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMD.Console.UI.Core
{
    class Menu
    {
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

        public MenuPage? ActivePage { get; private set; }

        public MenuPage? InitialPage { get; }

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
            if (_history.Count < 1)
            {
                throw new InvalidOperationException("There are no pages left in the history to navigate back to.");
            }

            MenuPage pageToNavigateBackTo = _history.Pop();
            NavigateTo(pageToNavigateBackTo);
        }

        public void NavigateTo(MenuPage page)
        {
            if (!Pages.Contains(page))
            {
                throw new ArgumentException("The provided page was not found in this menu.", nameof(page));
            }

            _history.Push(ActivePage);
            ActivePage = page;
        }

        public void Run()
        {
            if (Pages.Count < 1)
            {
                throw new InvalidOperationException("Cannot run a menu with no pages.");
            }

            ActivePage = InitialPage ?? Pages.First();

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
            System.Console.WriteLine(ActivePage.Title);
            System.Console.WriteLine(new string('-', ActivePage.Title.Length));

            int menuItemWidth = ActivePage.MenuItems.Select(x => x.Text.Length).Max();

            for (int i = 0; i < ActivePage.MenuItems.Count; i++)
            {
                PrintMenuItem(ActivePage.MenuItems[i], menuItemWidth, i == ActivePage.HighlightedMenuItemIndex);
            }

            return 2 + ActivePage.MenuItems.Count;
        }

        private void PrintMenuItem(MenuItem menuItem, int width, bool isHighlighted)
        {
            if (isHighlighted)
            {
                ActivateHighlightColors();
            }
            System.Console.WriteLine($"{menuItem.Text.PadRight(width, ' ')}");
            System.Console.ResetColor();
        }
    }
}
