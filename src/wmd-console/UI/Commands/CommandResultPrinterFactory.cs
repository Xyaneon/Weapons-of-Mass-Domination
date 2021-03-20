using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands
{
    class CommandResultPrinterFactory
    {
        private const string ArgumentException_commandResultTypeNotRecognized = "Command result type not recognized.";

        public static ICommandResultPrinter CreateICommandResultPrinter(Type commandResultType)
        {
            return commandResultType switch
            {
                _ when commandResultType == typeof(AttackPlayerResult) => new AttackPlayerResultPrinter(),
                _ when commandResultType == typeof(BuildSecretBaseResult) => new BuildSecretBaseResultPrinter(),
                _ when commandResultType == typeof(DistributePropagandaResult) => new DistributePropagandaResultPrinter(),
                _ when commandResultType == typeof(HireHenchmenResult) => new HireHenchmenResultPrinter(),
                _ when commandResultType == typeof(LaunchNukesResult) => new LaunchNukesResultPrinter(),
                _ when commandResultType == typeof(ManufactureNukesResult) => new ManufactureNukesResultPrinter(),
                _ when commandResultType == typeof(PurchaseUnclaimedLandResult) => new PurchaseUnclaimedLandResultPrinter(),
                _ when commandResultType == typeof(ResearchNukesResult) => new ResearchNukesResultPrinter(),
                _ when commandResultType == typeof(ResignResult) => new ResignResultPrinter(),
                _ when commandResultType == typeof(SellLandResult) => new SellLandResultPrinter(),
                _ when commandResultType == typeof(SkipTurnResult) => new SkipTurnResultPrinter(),
                _ when commandResultType == typeof(StealMoneyResult) => new StealMoneyResultPrinter(),
                _ when commandResultType == typeof(UpgradeSecretBaseResult) => new UpgradeSecretBaseResultPrinter(),
                _ => throw new ArgumentException(ArgumentException_commandResultTypeNotRecognized, nameof(commandResultType))
            };
        }
    }
}
