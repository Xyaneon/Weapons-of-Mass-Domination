using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;
using WMD.Game.State.Utility;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player researching nukes.
    /// </summary>
    public class ResearchNukesCommand : GameCommand<ResearchNukesInput, ResearchNukesResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return GameStateChecks.CurrentPlayerHasASecretBase(gameState)
                && !CurrentPlayerHasInsufficientFunds(gameState)
                && !GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState);
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, ResearchNukesInput input)
        {
            return CanExecuteForState(gameState);
        }

        public override ResearchNukesResult Execute([DisallowNull] GameState gameState, ResearchNukesInput input)
        {
            decimal totalResearchCost = CalculateResearchPrice(gameState);

            if(GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
            {
                throw new InvalidOperationException("The current player has already maxed out their nukes research.");
            }

            if(!GameStateChecks.CurrentPlayerHasASecretBase(gameState))
            {
                throw new InvalidOperationException("The current player needs a secret base before they can research nukes.");
            }

            if(CurrentPlayerHasInsufficientFunds(gameState))
            {
                throw new InvalidOperationException("The current player does not have enough money for the nukes research.");
            }

            GameState updatedGameState = GameStateUpdater.IncrementPlayerNukesResearchLevel(gameState, gameState.CurrentPlayerIndex);
            updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(updatedGameState, gameState.CurrentPlayerIndex, -1 * totalResearchCost);

            return new ResearchNukesResult(updatedGameState, gameState.CurrentPlayerIndex, updatedGameState.CurrentPlayer.State.ResearchState.NukeResearchLevel, totalResearchCost);
        }

        private static decimal CalculateResearchPrice(GameState gameState)
        {
            return NukeConstants.NukeResearchLevelCost;
        }

        private static bool CurrentPlayerHasInsufficientFunds(GameState gameState)
        {
            return CalculateResearchPrice(gameState) > gameState.CurrentPlayer.State.Money;
        }
    }
}
