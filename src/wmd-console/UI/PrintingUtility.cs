using WMD.Game.State.Data;

namespace WMD.Console.UI
{
    static class PrintingUtility
    {
        public static void CongratulateWinningPlayer(string playerName)
        {
            System.Console.WriteLine($"Congratulations, {playerName}! You won!");
        }

        public static void PrintAlreadyHaveASecretBase()
        {
            System.Console.WriteLine("You already have a secret base. Maybe you should try upgrading it instead?");
        }

        public static void PrintChoseNoNukesToLaunch()
        {
            System.Console.WriteLine("You apparently changed your mind.");
        }

        public static void PrintCopyrightInfo()
        {
            System.Console.WriteLine("Copyright \u00a9 2021 Christopher K. Horton");
        }

        public static void PrintCurrentPlayerHasNoMoneyToSpend()
        {
            System.Console.WriteLine("You do not have any money to spend.");
        }

        public static void PrintCurrentPlayerHasResignedAndCannotTakeTurn(string playerName)
        {
            System.Console.WriteLine($"{playerName} resigned and cannot take any more actions.");
            System.Console.WriteLine();
        }

        public static void PrintCurrentUnclaimedLandAreaAndPrice(GameState gameState)
        {
            decimal pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
            System.Console.WriteLine($"There are {gameState.Planet.UnclaimedLandArea:N0} km² of unclaimed land left, priced at {pricePerSquareKilometer:C} each.");
        }

        public static void PrintDoNotHaveASecretBase()
        {
            System.Console.WriteLine("You don't have a secret base. Maybe you should try building one?");
        }

        public static void PrintEndOfTurn()
        {
            System.Console.WriteLine("The turn has ended. Press any key to continue...");
        }

        public static void PrintGameHasAlreadyBeenWon(string playerName)
        {
            System.Console.WriteLine($"This game was already won by {playerName}.");
        }

        public static void PrintHasNoNukesToLaunch()
        {
            System.Console.WriteLine("You do not have any nukes to launch.");
        }

        public static void PrintHasNoSecretBaseToLaunchNukesFrom()
        {
            System.Console.WriteLine("You don't have a secret base to launch nukes from. Maybe you should try building one?");
        }

        public static void PrintInsufficientFundsForAnyLandPurchase()
        {
            System.Console.WriteLine("You do not have enough money to purchase any unclaimed land.");
        }

        public static void PrintInsufficientFundsForBuildingSecretBase(decimal cost)
        {
            System.Console.WriteLine($"You do not have enough money to build a secret base ({cost:C} needed).");
        }

        public static void PrintInsufficientFundsForResearchingNukes(decimal cost)
        {
            System.Console.WriteLine($"You do not have enough money to continue researching nukes ({cost:C} needed).");
        }

        public static void PrintInsufficientFundsForUpgradingSecretBase(decimal cost)
        {
            System.Console.WriteLine($"You do not have enough money to upgrade your secret base ({cost:C} needed).");
        }

        public static void PrintMushroomCloud()
        {
            System.Console.WriteLine(
@"                            0000000000                          
     00                00000000000000000000                     
      00             000000000000000000000000                   
                   0000000000000000000000000000                 
                  000000000000000000000000000000      00000     
                 00000000000000000000000000000000      000      
                 00000000000000000000000000000000               
                  000000000000000000000000000000                
                    00000000000000000000000000                  
                        000000000000000000                      
                            0000000000                          
                             00000000                           
                              000000                            
                        000000000000000000                      
                       0      000000      0                     
                        000000000000000000                      
                              000000                            
                         \\   00000000   /                      
                         _ 000000000000 _                       
0 0 0 0 0 0 0 0 0 0 0 0 0 00000000000000 0 0 0 0 0 0 0 0 0 0 0 0
0000000000000000000000000000000000000000000000000000000000000000"
);
        }

        public static void PrintNoLandToSell()
        {
            System.Console.WriteLine("You don't have any land to sell.");
        }

        public static void PrintNoPositionsToOffer()
        {
            System.Console.WriteLine("You apparently changed your mind.");
        }

        public static void PrintNoNukesToManufacture()
        {
            System.Console.WriteLine("You apparently changed your mind.");
        }

        public static void PrintNoUnclaimedLandLeftToPurchase()
        {
            System.Console.WriteLine("There is no unclaimed land left to purchase.");
        }

        public static void PrintNukesResearchAlreadyMaxedOut()
        {
            System.Console.WriteLine($"Your nukes research is already complete.");
        }

        public static void PrintNukesResearchNotCompleted()
        {
            System.Console.WriteLine($"Your nukes research has not yet been completed.");
        }

        public static void SetOutputEncoding()
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        public static void PrintTitle()
        {
            System.Console.WriteLine("Weapons of Mass Domination");
            System.Console.WriteLine("**************************");
            PrintCopyrightInfo();
            System.Console.WriteLine();
            PrintMushroomCloud();
            System.Console.WriteLine();
        }
    }
}
