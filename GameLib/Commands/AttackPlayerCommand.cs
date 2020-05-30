using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player attacking another player.
    /// </summary>
    public class AttackPlayerCommand : GameCommand<AttackPlayerInput, AttackPlayerResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, AttackPlayerInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return !CurrentPlayerIsAttackingThemselves(gameState, input) && TargetPlayerFound(gameState, input);
        }

        public override AttackPlayerResult Execute(GameState gameState, AttackPlayerInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (CurrentPlayerIsAttackingThemselves(gameState, input))
            {
                throw new InvalidOperationException("A player cannot attack themselves.");
            }

            if (!TargetPlayerFound(gameState, input))
            {
                throw new InvalidOperationException("The target player index is outside the player list bounds.");
            }

            // TODO
            throw new NotImplementedException();
        }

        private static bool CurrentPlayerIsAttackingThemselves(GameState gameState, AttackPlayerInput input)
        {
            return gameState.CurrentPlayerIndex == input.TargetPlayerIndex;
        }

        private static bool TargetPlayerFound(GameState gameState, AttackPlayerInput input)
        {
            return input.TargetPlayerIndex < gameState.Players.Count;
        }
    }
}
