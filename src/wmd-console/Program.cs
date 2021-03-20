using WMD.Console.UI;
using WMD.Console.UI.Menus;
using Xyaneon.Console.Menus;

namespace WMD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintingUtility.PrintTitle();

            Menu mainMenu = GameMenuFactory.CreateMainMenu();
            mainMenu.Run();
        }
    }
}
