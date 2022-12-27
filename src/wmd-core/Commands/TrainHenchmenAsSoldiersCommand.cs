using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player training henchmen as soldiers.
/// </summary>
public class TrainHenchmenAsSoldiersCommand : GameCommand<TrainHenchmenAsSoldiersInput, TrainHenchmenAsSoldiersResult>
{
    private const string InvalidOperationException_NoUntrainedHenchmen = "The current player does not have any untrained henchmen.";
    private const string InvalidOperationException_NotEnoughUntrainedHenchmen = "The current player does not have enough untrained henchmen for the requested training amount.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] TrainHenchmenAsSoldiersInput input) =>
        CanExecuteForState(gameState) && PlayerHasEnoughUntrainedHenchmenForTrainingAmount(gameState, input);

    public override TrainHenchmenAsSoldiersResult Execute([DisallowNull] GameState gameState, [DisallowNull] TrainHenchmenAsSoldiersInput input)
    {
        if (!GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState, HenchmenSpecialization.Untrained))
        {
            throw new InvalidOperationException(InvalidOperationException_NoUntrainedHenchmen);
        }

        if (!PlayerHasEnoughUntrainedHenchmenForTrainingAmount(gameState, input))
        {
            throw new InvalidOperationException(InvalidOperationException_NotEnoughUntrainedHenchmen);
        }

        GameState updatedGameState = new GameStateUpdater(gameState)
            .TrainPlayerHenchmenAsSoldiers(gameState.CurrentPlayerIndex, input.NumberToTrain)
            .AndReturnUpdatedGameState();

        return new TrainHenchmenAsSoldiersResult(updatedGameState, gameState.CurrentPlayerIndex, input.NumberToTrain);
    }

    private static bool PlayerHasEnoughUntrainedHenchmenForTrainingAmount(GameState gameState, TrainHenchmenAsSoldiersInput input) =>
        input.NumberToTrain <= gameState.CurrentPlayer.State.WorkforceState.GenericHenchmenCount;
}