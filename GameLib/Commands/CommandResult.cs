namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player's action.
    /// This class cannot be instantiated.
    /// <param name="UpdatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    public abstract record CommandResult(GameState UpdatedGameState, int PlayerIndex);
}
