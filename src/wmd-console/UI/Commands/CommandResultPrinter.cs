using System;
using WMD.Game.Commands;
using WMD.Game.State.Data.Players;

namespace WMD.Console.UI.Commands
{
    static class CommandResultPrinter
    {
        public static void PrintCommandResult(CommandResult actionResult)
        {
            switch (actionResult)
            {
                case AttackPlayerResult result:
                    PrintAttackPlayerResult(result);
                    break;
                case BuildSecretBaseResult result:
                    PrintBuildSecretBaseResult(result);
                    break;
                case DistributePropagandaResult result:
                    PrintDistributePropagandaResult(result);
                    break;
                case HireHenchmenResult result:
                    PrintHireHenchmenResult(result);
                    break;
                case LaunchNukesResult result:
                    PrintLaunchNukesResult(result);
                    break;
                case ManufactureNukesResult result:
                    PrintManufactureNukesResult(result);
                    break;
                case PurchaseUnclaimedLandResult result:
                    PrintPurchaseUnclaimedLandResult(result);
                    break;
                case ResearchNukesResult result:
                    PrintResearchNukesResult(result);
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

        private static void PrintAttackPlayerResult(AttackPlayerResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} attacked {result.TargetPlayerName}; the former lost {result.HenchmenAttackerLost:N0} henchmen and the latter lost {result.HenchmenDefenderLost:N0} henchmen.");
        }

        private static void PrintBuildSecretBaseResult(BuildSecretBaseResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} built their own secret base for {result.BuildPrice:C}.");
        }

        private static void PrintDistributePropagandaResult(DistributePropagandaResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} spent {result.MoneySpent:C} and gained {result.ReputationGained}% additional reputation.");
        }

        private static void PrintHireHenchmenResult(HireHenchmenResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} managed to hire {result.HenchmenHired:N0} new henchmen.");
        }
        
        private static void PrintLaunchNukesResult(LaunchNukesResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} launched {result.NukesLaunched:N0} nukes at {result.TargetPlayerName}.");
            if (result.SuccessfulNukeHits == 0)
            {
                System.Console.WriteLine($"Not a single one of them worked, though! {result.TargetPlayerName}'s henchmen escaped without a scratch.");
            }
            else if (result.SuccessfulNukeHits < result.NukesLaunched)
            {
                System.Console.WriteLine($"{result.SuccessfulNukeHits:N0} of the nukes worked as designed. {result.TargetPlayerName} lost {result.HenchmenDefenderLost:N0} henchmen in the attack.");
            }
            else
            {
                System.Console.WriteLine($"Every nuke successfully detonated on contact! {result.TargetPlayerName} lost {result.HenchmenDefenderLost:N0} henchmen in the attack.");
            }
        }

        private static void PrintManufactureNukesResult(ManufactureNukesResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} manufactured {result.NukesManufactured:N0} nukes.");
        }

        private static void PrintPurchaseUnclaimedLandResult(PurchaseUnclaimedLandResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} purchased {result.LandAreaPurchased:N0} km² of land for {result.TotalPurchasePrice:C}.");
        }

        private static void PrintResearchNukesResult(ResearchNukesResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} advanced their nukes research to Level {result.NewNukesResearchLevel:N0} for {result.TotalResearchPrice:C}.");
        }

        private static void PrintResignResult(ResignResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} resigned.");
        }

        private static void PrintSellLandResult(SellLandResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} sold {result.LandAreaSold:N0} km² of land for {result.TotalSalePrice:C}.");
        }

        private static void PrintSkipTurnResult(SkipTurnResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} skipped their turn and wasted a whole day.");
        }

        private static void PrintStealMoneyResult(StealMoneyResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} stole {result.StolenAmount:C}. They now have {RetrievePlayerWhoActed(result).State.Money:C}.");
        }

        private static void PrintUpgradeSecretBaseResult(UpgradeSecretBaseResult result)
        {
            System.Console.WriteLine($"{RetrievePlayerWhoActed(result).Identification.Name} upgraded their secret base to Level {result.NewLevel:N0} for {result.UpgradePrice:C}.");
        }

        private static Player RetrievePlayerWhoActed(CommandResult result)
        {
            return result.UpdatedGameState.Players[result.PlayerIndex];
        }
    }
}
