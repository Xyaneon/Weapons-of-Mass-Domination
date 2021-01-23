using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player resigning.
    /// </summary>
    public class ResignCommand : GameCommand<ResignInput, ResignResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] ResignInput input)
        {
            return true;
        }

        public override ResignResult Execute([DisallowNull] GameState gameState, [DisallowNull] ResignInput input)
        {
            PlayerState updatedPlayerState = gameState.CurrentPlayer.State with { HasResigned = true };
            GameState updatedGameState = GameStateUpdater.UpdatePlayerState(gameState, gameState.CurrentPlayerIndex, updatedPlayerState);

            return new ResignResult(updatedGameState, gameState.CurrentPlayerIndex);
        }
    }
}
