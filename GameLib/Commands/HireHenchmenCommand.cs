﻿using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player hiring henchmen.
    /// </summary>
    public class HireHenchmenCommand : GameCommand<HireHenchmenInput, HireHenchmenResult>
    {
        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, HireHenchmenInput input)
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

        public override HireHenchmenResult Execute(GameState gameState, HireHenchmenInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            int henchmenHired = input.OpenPositionsOffered;
            gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen += henchmenHired;
            return new HireHenchmenResult(gameState.CurrentPlayer, gameState, henchmenHired);
        }
    }
}
