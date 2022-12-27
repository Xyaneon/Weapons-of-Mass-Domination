using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player skipping their turn.
/// </summary>
public record SkipTurnResult : CommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SkipTurnResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    public SkipTurnResult(GameState updatedGameState, int playerIndex) : base(updatedGameState, playerIndex) { }
}
