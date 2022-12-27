using WMD.Console.UI;
using WMD.Console.UI.Menus;

PrintingUtility.SetOutputEncoding();
PrintingUtility.PrintTitle();

GameMenuFactory.CreateMainMenu()
    .Run();
