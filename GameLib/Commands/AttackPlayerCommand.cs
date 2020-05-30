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

            // TODO
            throw new NotImplementedException();
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

            // TODO
            throw new NotImplementedException();
        }
    }
}
