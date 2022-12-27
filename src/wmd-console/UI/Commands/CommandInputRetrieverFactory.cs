using System;
using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

static class CommandInputRetrieverFactory
{
    private const string ArgumentException_commandInputTypeNotRecognized = "Command input type not recognized.";

    public static ICommandInputRetriever CreateICommandInputRetriever(Type commandInputType) => commandInputType switch
    {
        _ when commandInputType == typeof(AttackPlayerInput) => new AttackPlayerInputRetriever(),
        _ when commandInputType == typeof(AttackGovernmentArmyInput) => new AttackGovernmentArmyInputRetriever(),
        _ when commandInputType == typeof(BuildSecretBaseInput) => new BuildSecretBaseInputRetriever(),
        _ when commandInputType == typeof(ChangeDailyWageInput) => new ChangeDailyWageInputRetriever(),
        _ when commandInputType == typeof(DistributePropagandaInput) => new DistributePropagandaInputRetriever(),
        _ when commandInputType == typeof(HireHenchmenInput) => new HireHenchmenInputRetriever(),
        _ when commandInputType == typeof(LaunchNukesInput) => new LaunchNukesInputRetriever(),
        _ when commandInputType == typeof(ManufactureNukesInput) => new ManufactureNukesInputRetriever(),
        _ when commandInputType == typeof(PurchaseUnclaimedLandInput) => new PurchaseUnclaimedLandInputRetriever(),
        _ when commandInputType == typeof(ResearchNukesInput) => new ResearchNukesInputRetriever(),
        _ when commandInputType == typeof(ResignInput) => new ResignInputRetriever(),
        _ when commandInputType == typeof(SellLandInput) => new SellLandInputRetriever(),
        _ when commandInputType == typeof(SkipTurnInput) => new SkipTurnInputRetriever(),
        _ when commandInputType == typeof(StealMoneyInput) => new StealMoneyInputRetriever(),
        _ when commandInputType == typeof(TrainHenchmenInput) => new TrainHenchmenInputRetriever(),
        _ when commandInputType == typeof(UpgradeSecretBaseInput) => new UpgradeSecretBaseInputRetriever(),
        _ => throw new ArgumentException(ArgumentException_commandInputTypeNotRecognized, nameof(commandInputType))
    };
}
