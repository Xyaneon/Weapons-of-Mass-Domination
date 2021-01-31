using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Research;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player researching nukes.
    /// </summary>
    public class ResearchNukesCommand : GameCommand<ResearchNukesInput, ResearchNukesResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            throw new NotImplementedException();
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, ResearchNukesInput input)
        {
            return !(CurrentPlayerDoesNotHaveSecretBase(gameState) || CurrentPlayerHasInsufficientFunds(gameState) || CurrentPlayerIsAtMaxNukesResearchLevel(gameState));
        }

        public override ResearchNukesResult Execute([DisallowNull] GameState gameState, ResearchNukesInput input)
        {
            decimal totalResearchCost = CalculateResearchPrice(gameState);

            if(CurrentPlayerIsAtMaxNukesResearchLevel(gameState))
            {
                throw new InvalidOperationException("The current player has already maxed out their nukes research.");
            }

            if(CurrentPlayerDoesNotHaveSecretBase(gameState))
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
            return ResearchState.NukeResearchLevelCost;
        }

        private static bool CurrentPlayerHasInsufficientFunds(GameState gameState)
        {
            return CalculateResearchPrice(gameState) > gameState.CurrentPlayer.State.Money;
        }

        private static bool CurrentPlayerIsAtMaxNukesResearchLevel(GameState gameState)
        {
            return gameState.CurrentPlayer.State.ResearchState.NukeResearchLevel >= ResearchState.MaxNukeResearchLevel;
        }

        private static bool CurrentPlayerDoesNotHaveSecretBase(GameState gameState)
        {
            return gameState.CurrentPlayer.State.SecretBase == null;
        }
    }
}
