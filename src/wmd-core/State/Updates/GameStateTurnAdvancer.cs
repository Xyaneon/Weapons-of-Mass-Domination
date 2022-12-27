using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.State.Updates;

/// <summary>
/// Advances <see cref="GameState"/> objects to the next turn.
/// </summary>
public static class GameStateTurnAdvancer
{
    /// <summary>
    /// Advances the game to the next turn, and possibly to the next round.
    /// </summary>
    /// <param name="gameState">The <see cref="GameState"/> to update.</param>
    /// <returns>
    /// A tuple containing the updated <see cref="GameState"/> and possibly a <see cref="RoundUpdateResult"/> if a new
    /// round has started, or otherwise <see langword="null"/>.
    /// </returns>
    public static (GameState, RoundUpdateResult?) AdvanceToNextTurn([DisallowNull] GameState gameState)
    {
        gameState = gameState with
        {
            CurrentPlayerIndex = gameState.CurrentPlayerIndex >= gameState.Players.Count - 1 ? 0 : gameState.CurrentPlayerIndex + 1
        };

        return gameState.CurrentPlayerIndex == 0
            ? GameStateRoundAdvancer.AdvanceToNextRound(gameState)
            : (gameState, null);
    }
}
