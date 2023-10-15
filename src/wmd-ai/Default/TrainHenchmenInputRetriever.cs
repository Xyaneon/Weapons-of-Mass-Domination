using System;
using System.Collections.Generic;
using System.Linq;
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
        Specialization = DetermineTrainingSpecialization(),
    };

    private static long CalculateNumberOfTrainees(GameState gameState) =>
        _random.NextInt64(1, gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount);

    private static Specialization DetermineTrainingSpecialization() =>
        new List<Specialization>
        {
            Specialization.Soldier,
            Specialization.Thief
        }
        .OrderBy(_ => _random.Next())
        .First();

    private static readonly Random _random;
}
