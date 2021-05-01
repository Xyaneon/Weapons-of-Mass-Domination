using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player attacking another player.
    /// </summary>
    public class AttackPlayerCommand : GameCommand<AttackPlayerInput, AttackPlayerResult>
    {
        private const string InvalidOperationException_playerAttackingThemselves = "A player cannot attack themselves.";
        private const string InvalidOperationException_playerHasNoHenchmen = "A player cannot attack when they have no henchmen.";
        private const string InvalidOperationException_targetPlayerIndexOutsideBounds = "The target player index is outside the player list bounds.";

        public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
            GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState);

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] AttackPlayerInput input) =>
            CanExecuteForState(gameState) && !CurrentPlayerIsAttackingThemselves(gameState, input) && TargetPlayerFound(gameState, input);

        public override AttackPlayerResult Execute([DisallowNull] GameState gameState, [DisallowNull] AttackPlayerInput input)
        {
            if (!GameStateChecks.CurrentPlayerHasAnyHenchmen(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_playerHasNoHenchmen);
            }

            if (CurrentPlayerIsAttackingThemselves(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_playerAttackingThemselves);
            }

            if (!TargetPlayerFound(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_targetPlayerIndexOutsideBounds);
            }

            AttackCalculationsResult calculationsResult = AttacksCalculator.CalculateChangesResultingFromAttack(gameState, input);
            GameState updatedGameState = CreateUpdatedGameState(gameState, input, calculationsResult);

            return new AttackPlayerResult(
                updatedGameState,
                gameState.CurrentPlayerIndex,
                input.TargetPlayerIndex,
                calculationsResult
            );
        }

        private static GameState CreateUpdatedGameState(GameState gameState, AttackPlayerInput input, AttackCalculationsResult calculationsResult) =>
            GameStateUpdater.AdjustPlayerStatesAfterAttack(gameState, gameState.CurrentPlayerIndex, input.TargetPlayerIndex, calculationsResult);

        private static bool CurrentPlayerIsAttackingThemselves(GameState gameState, AttackPlayerInput input) =>
            GameStateChecks.CurrentPlayerIsAttackingThemselves(gameState, input.TargetPlayerIndex);

        private static bool TargetPlayerFound(GameState gameState, AttackPlayerInput input) =>
            GameStateChecks.PlayerIndexIsInBounds(gameState, input.TargetPlayerIndex);
    }
}
