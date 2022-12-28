using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;

namespace WMD.AI.Default;

internal sealed class TrainHenchmenInputRetriever : ICommandInputRetriever
{
    static TrainHenchmenInputRetriever() => _random = new Random();

    public CommandInput? GetCommandInput(GameState gameState) => new TrainHenchmenInput
    {
        NumberToTrain = CalculateNumberOfTrainees(gameState),
        Specialization = DetermineTrainingSpecialization(gameState),
    };

    private static long CalculateNumberOfTrainees(GameState gameState) =>
        _random.NextInt64(1, gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount);

    private static Specialization DetermineTrainingSpecialization(GameState gameState)
    {
        // TODO: Make this return a random value other than Untrained once more specializations are available.
        return Specialization.Soldier;
    }

    private static readonly Random _random;
}
