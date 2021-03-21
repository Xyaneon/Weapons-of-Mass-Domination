using System;

namespace Xyaneon.Console.Menus
{
    /// <summary>
    /// Provides visual theme data for a <see cref="Menu"/>.
    /// </summary>
    public record MenuTheme
    {
        /// <summary>
        /// Gets the separator used between breadcrumb items.
        /// </summary>
        public string BreadcrumbsSeparator { get; init; } = " > ";

        /// <summary>
        /// Gets the character used to display the bottom edge of the menu border.
        /// </summary>
        public char BottomBorderEdge { get; init; } = '═';

        /// <summary>
        /// Gets the character used to display the bottom-left corner of the menu border.
        /// </summary>
        public char BottomLeftBorderCorner { get; init; } = '╚';

        /// <summary>
        /// Gets the character used to display the bottom-right corner of the menu border.
        /// </summary>
        public char BottomRightBorderCorner { get; init; } = '╝';

        /// <summary>
        /// Gets the background color for disabled menu items.
        /// </summary>
        public ConsoleColor DisabledBackgroundColor { get; init; } = ConsoleColor.Black;

        /// <summary>
        /// Gets the foreground color for disabled menu items.
        /// </summary>
        public ConsoleColor DisabledForegroundColor { get; init; } = ConsoleColor.DarkGray;

        /// <summary>
        /// Gets the character used to display the left edge of the menu header.
        /// </summary>
        public char HeaderLeftEdge { get; init; } = '║';
        
        /// <summary>
        /// Gets the character used to display the right edge of the menu header.
        /// </summary>
        public char HeaderRightEdge { get; init; } = '║';

        /// <summary>
        /// Gets the background color for highlighted disabled menu items.
        /// </summary>
        public ConsoleColor HighlightDisabledBackgroundColor { get; init; } = ConsoleColor.Gray;

        /// <summary>
        /// Gets the foreground color for highlighted disabled menu items.
        /// </summary>
        public ConsoleColor HighlightDisabledForegroundColor { get; init; } = ConsoleColor.Black;

        /// <summary>
        /// Gets the background color for highlighted enabled menu items.
        /// </summary>
        public ConsoleColor HighlightEnabledBackgroundColor { get; init; } = ConsoleColor.Gray;

        /// <summary>
        /// Gets the foreground color for highlighted enabled menu items.
        /// </summary>
        public ConsoleColor HighlightEnabledForegroundColor { get; init; } = ConsoleColor.Black;

        /// <summary>
        /// Gets the character used to display the left edge of a menu item.
        /// </summary>
        public char MenuItemLeftEdge { get; init; } = '║';

        /// <summary>
        /// Gets the character used to display the right edge of a menu item.
        /// </summary>
        public char MenuItemRightEdge { get; init; } = '║';

        /// <summary>
        /// Gets the character used to display the middle edge of the menu border.
        /// </summary>
        public char MiddleBorderEdge { get; init; } = '═';
        
        /// <summary>
        /// Gets the character used to display the left intersection of the middle edge of the menu border.
        /// </summary>
        public char MiddleLeftBorderIntersection { get; init; } = '╠';

        /// <summary>
        /// Gets the character used to display the right intersection of the middle edge of the menu border.
        /// </summary>
        public char MiddleRightBorderIntersection { get; init; } = '╣';

        /// <summary>
        /// Gets the character used to display the top edge of the menu border.
        /// </summary>
        public char TopBorderEdge { get; init; } = '═';

        /// <summary>
        /// Gets the character used to display the top-left corner of the menu border.
        /// </summary>
        public char TopLeftBorderCorner { get; init; } = '╔';
        
        /// <summary>
        /// Gets the character used to display the top-right corner of the menu border.
        /// </summary>
        public char TopRightBorderCorner { get; init; } = '╗';
    }
}
