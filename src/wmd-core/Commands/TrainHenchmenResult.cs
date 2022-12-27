using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Players;

namespace WMD.Game.Commands;

/// <summary>
/// Represents the result of a player training henchmen to be soldiers.
/// </summary>
public record TrainHenchmenResult : CommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrainHenchmenResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="henchmenHired">The number of henchmen trained.</param>
    /// <param name="specialization">The specialization the henchmen were trained in.</param>
    public TrainHenchmenResult(GameState updatedGameState, int playerIndex, long henchmenTrained, HenchmenSpecialization specialization) : base(updatedGameState, playerIndex)
    {
        HenchmenTrained = henchmenTrained;
        Specialization = specialization;
    }

    /// <summary>
    /// Gets the number of henchmen trained.
    /// </summary>
    public long HenchmenTrained { get; init; }

    /// <summary>
    /// Gets the specialization the henchmen were trained in.
    /// </summary>
    public HenchmenSpecialization Specialization { get; init; }
}
