using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player skipping their turn.
    /// </summary>
    public class SkipTurnCommand : IGameCommand<SkipTurnInput, SkipTurnResult>
    {
        public bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public bool CanExecuteForStateAndInput(GameState gameState, SkipTurnInput input)
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

        public SkipTurnResult Execute(GameState gameState, SkipTurnInput input)
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
