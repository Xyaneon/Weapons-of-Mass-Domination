using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;
using WMD.Game.State.Utility.AttackCalculations;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player attacking the government army.
/// </summary>
public class AttackGovernmentArmyCommand : GameCommand<AttackGovernmentArmyInput, AttackGovernmentArmyResult>
{
    private const string InvalidOperationException_playerDoesNotHaveEnoughHenchmenForNumberOfHenchmenToUse = "The player does not have enough henchmen for the specified number of henchmen to use in the attack.";
    private const string InvalidOperationException_playerHasNoHenchmen = "A player cannot attack when they have no henchmen.";

    public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
        GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState);

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, AttackGovernmentArmyInput input) =>
        CanExecuteForState(gameState)
            && CurrentPlayerHasEnoughHenchmenForNumberRequestedInAttack(gameState, input);

    public override AttackGovernmentArmyResult Execute([DisallowNull] GameState gameState, AttackGovernmentArmyInput input)
    {
        if (!GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState))
        {
            throw new InvalidOperationException(InvalidOperationException_playerHasNoHenchmen);
        }

        if (!CurrentPlayerHasEnoughHenchmenForNumberRequestedInAttack(gameState, input))
        {
            throw new InvalidOperationException(InvalidOperationException_playerDoesNotHaveEnoughHenchmenForNumberOfHenchmenToUse);
        }

        PlayerOnGovernmentArmyAttackCalculationsResult calculationResult = PlayerOnGovernmentArmyAttacksCalculator.CalculateChangesResultingFromAttack(gameState, input);
        GameState updatedGameState = new GameStateUpdater(gameState)
            .AdjustStateAfterPlayerAttackOnGovernmentArmy(gameState.CurrentPlayerIndex, calculationResult)
            .AndReturnUpdatedGameState();

        return new AttackGovernmentArmyResult(
            updatedGameState,
            gameState.CurrentPlayerIndex,
            input.NumberOfAttackingHenchmen,
            calculationResult.HenchmenAttackerLost,
            calculationResult.SoldiersGovernmentArmyLost,
            calculationResult.ReputationChangeForAttacker
        );
    }

    private static bool CurrentPlayerHasEnoughHenchmenForNumberRequestedInAttack(GameState gameState, AttackGovernmentArmyInput input) =>
        gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen >= input.NumberOfAttackingHenchmen;
}
