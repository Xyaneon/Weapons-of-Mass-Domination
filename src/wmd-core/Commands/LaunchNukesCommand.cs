using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player launching nukes at another player.
    /// </summary>
    public class LaunchNukesCommand : GameCommand<LaunchNukesInput, LaunchNukesResult>
    {
        private const string InvalidOperationException_playerAttackingThemselves = "A player cannot attack themselves.";
        private const string InvalidOperationException_playerHasNoNukes = "A player cannot launch nukes if they do not currently have any.";
        private const string InvalidOperationException_playerHasNoSecretBase = "A player cannot launch nukes without a secret base to launch them from.";
        private const string InvalidOperationException_targetPlayerIndexOutsideBounds = "The target player index is outside the player list bounds.";

        public override bool CanExecuteForState([DisallowNull] GameState gameState) =>
            GameStateChecks.CurrentPlayerHasAnyNukes(gameState) && GameStateChecks.CurrentPlayerHasASecretBase(gameState);

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, LaunchNukesInput input) =>
            CanExecuteForState(gameState)
                && !GameStateChecks.CurrentPlayerIsAttackingThemselves(gameState, input.TargetPlayerIndex)
                && GameStateChecks.PlayerIndexIsInBounds(gameState, input.TargetPlayerIndex);

        public override LaunchNukesResult Execute([DisallowNull] GameState gameState, LaunchNukesInput input)
        {
            if (GameStateChecks.CurrentPlayerIsAttackingThemselves(gameState, input.TargetPlayerIndex))
            {
                throw new InvalidOperationException(InvalidOperationException_playerAttackingThemselves);
            }

            if (!GameStateChecks.PlayerIndexIsInBounds(gameState, input.TargetPlayerIndex))
            {
                throw new InvalidOperationException(InvalidOperationException_targetPlayerIndexOutsideBounds);
            }

            if (!GameStateChecks.CurrentPlayerHasAnyNukes(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_playerHasNoNukes);
            }

            if (!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                throw new InvalidOperationException(InvalidOperationException_playerHasNoSecretBase);
            }

            int numberOfSuccessfulHits = NukesCalculator.CalculateNumberOfSuccessfulNukeHits(gameState, input.NumberOfNukesLaunched);
            long numberOfHenchmenDefenderLost = NukesCalculator.CalculateNumberOfHenchmenLostToNukes(gameState, input.TargetPlayerIndex, numberOfSuccessfulHits);
            int reputationChangeAmount = NukesCalculator.CalculateReputationChangeAmount(gameState, gameState.CurrentPlayerIndex, numberOfSuccessfulHits);

            GameState updatedGameState = new GameStateUpdater(gameState)
                .AdjustNukesForPlayer(gameState.CurrentPlayerIndex, -1 * input.NumberOfNukesLaunched)
                .AdjustHenchmenForPlayer(input.TargetPlayerIndex, -1 * numberOfHenchmenDefenderLost)
                .AdjustReputationForPlayer(gameState.CurrentPlayerIndex, reputationChangeAmount)
                .AndReturnUpdatedGameState();

            return new LaunchNukesResult(
                updatedGameState,
                gameState.CurrentPlayerIndex,
                input.TargetPlayerIndex,
                input.NumberOfNukesLaunched,
                numberOfSuccessfulHits,
                numberOfHenchmenDefenderLost,
                reputationChangeAmount
            );
        }
    }
}
