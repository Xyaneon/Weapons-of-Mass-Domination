using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player resigning.
    /// </summary>
    public class ResignCommand : IGameCommand<ResignInput, ResignResult>
    {
        public bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public bool CanExecuteForStateAndInput(GameState gameState, ResignInput input)
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

        public ResignResult Execute(GameState gameState, ResignInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            gameState.CurrentPlayer.State.HasResigned = true;
            return new ResignResult(gameState.CurrentPlayer, gameState);
        }
    }
}
