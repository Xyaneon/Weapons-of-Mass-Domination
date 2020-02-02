using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player skipping their turn.
    /// </summary>
    public class SkipTurnCommand : GameCommand<SkipTurnInput, SkipTurnResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, SkipTurnInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return true;
        }

        public override SkipTurnResult Execute(GameState gameState, SkipTurnInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new SkipTurnResult(gameState.CurrentPlayer, gameState);
        }
    }
}
