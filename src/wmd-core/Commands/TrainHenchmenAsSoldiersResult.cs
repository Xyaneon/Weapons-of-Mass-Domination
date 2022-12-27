using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player training henchmen to be soldiers.
/// </summary>
public record TrainHenchmenAsSoldiersResult : CommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrainHenchmenAsSoldiersResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="henchmenHired">The number of henchmen trained.</param>
    public TrainHenchmenAsSoldiersResult(GameState updatedGameState, int playerIndex, long henchmenTrained) : base(updatedGameState, playerIndex)
    {
        HenchmenTrained = henchmenTrained;
    }

    /// <summary>
    /// Gets the number of henchmen trained.
    /// </summary>
    public long HenchmenTrained { get; init; }
}
