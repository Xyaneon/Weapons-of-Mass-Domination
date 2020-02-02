using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player resigning.
    /// </summary>
    public class ResignCommand : GameCommand<ResignInput, ResignResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, ResignInput input)
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

        public override ResignResult Execute(GameState gameState, ResignInput input)
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
