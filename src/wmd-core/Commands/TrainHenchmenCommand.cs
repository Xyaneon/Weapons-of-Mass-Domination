using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player training henchmen in a specialty.
/// </summary>
public class TrainHenchmenCommand : GameCommand<TrainHenchmenInput, TrainHenchmenResult>
{
    private const string InvalidOperationException_NoUntrainedHenchmen = "The current player does not have any untrained henchmen.";
    private const string InvalidOperationException_NotEnoughUntrainedHenchmen = "The current player does not have enough untrained henchmen for the requested training amount.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] TrainHenchmenInput input) =>
        CanExecuteForState(gameState) && PlayerHasEnoughUntrainedHenchmenForTrainingAmount(gameState, input);

    public override TrainHenchmenResult Execute([DisallowNull] GameState gameState, [DisallowNull] TrainHenchmenInput input)
    {
        if (!GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState, Specialization.Untrained))
        {
            throw new InvalidOperationException(InvalidOperationException_NoUntrainedHenchmen);
        }

        if (!PlayerHasEnoughUntrainedHenchmenForTrainingAmount(gameState, input))
        {
            throw new InvalidOperationException(InvalidOperationException_NotEnoughUntrainedHenchmen);
        }

        GameState updatedGameState = new GameStateUpdater(gameState)
            .TrainPlayerHenchmen(gameState.CurrentPlayerIndex, input.NumberToTrain, input.Specialization)
            .AndReturnUpdatedGameState();

        return new TrainHenchmenResult(updatedGameState, gameState.CurrentPlayerIndex, input.NumberToTrain, input.Specialization);
    }

    private static bool PlayerHasEnoughUntrainedHenchmenForTrainingAmount(GameState gameState, TrainHenchmenInput input) =>
        input.NumberToTrain <= gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount;
}
