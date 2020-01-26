using System;
using WMD.Game.Commands;

namespace WMD.Console.UI
{
    static class ActionResultPrinter
    {
        public static void PrintActionResult(CommandResult actionResult)
        {
            switch (actionResult)
            {
                case BuildSecretBaseResult result:
                    PrintBuildSecretBaseResult(result);
                    break;
                case HireHenchmenResult result:
                    PrintHireHenchmenResult(result);
                    break;
                case PurchaseUnclaimedLandResult result:
                    PrintPurchaseUnclaimedLandResult(result);
                    break;
                case ResignResult result:
                    PrintResignResult(result);
                    break;
                case SellLandResult result:
                    PrintSellLandResult(result);
                    break;
                case SkipTurnResult result:
                    PrintSkipTurnResult(result);
                    break;
                case StealMoneyResult result:
                    PrintStealMoneyResult(result);
                    break;
                case UpgradeSecretBaseResult result:
                    PrintUpgradeSecretBaseResult(result);
                    break;
                default:
                    throw new ArgumentException($"Unsupported {typeof(CommandResult).Name} type: {actionResult.GetType().FullName}");
            }

            System.Console.WriteLine();
        }

        private static void PrintBuildSecretBaseResult(BuildSecretBaseResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} built their own secret base for {result.BuildPrice:C}.");
        }

        private static void PrintHireHenchmenResult(HireHenchmenResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} managed to hire {result.HenchmenHired:N0} new henchmen.");
        }

        private static void PrintPurchaseUnclaimedLandResult(PurchaseUnclaimedLandResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} purchased {result.LandAreaPurchased:N0} km² of land for {result.TotalPurchasePrice:C}.");
        }

        private static void PrintResignResult(ResignResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} resigned.");
        }

        private static void PrintSellLandResult(SellLandResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} sold {result.LandAreaSold:N0} km² of land for {result.TotalSalePrice:C}.");
        }

        private static void PrintSkipTurnResult(SkipTurnResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} skipped their turn and wasted a whole day.");
        }

        private static void PrintStealMoneyResult(StealMoneyResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} stole {result.StolenAmount:C}. They now have {result.Player.State.Money:C}.");
        }

        private static void PrintUpgradeSecretBaseResult(UpgradeSecretBaseResult result)
        {
            System.Console.WriteLine($"{result.Player.Name} upgraded their secret base to Level {result.NewLevel:N0} for {result.UpgradePrice:C}.");
        }
    }
}
