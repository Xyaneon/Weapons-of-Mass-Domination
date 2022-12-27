using WMD.Game.State.Data;

namespace WMD.Game.Commands;

public record ChangeDailyWageResult : CommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeDailyWageResult"/> class.
    /// </summary>
    /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
    /// <param name="playerIndex">The index of the <see cref="Player"/> whose action this is the result of.</param>
    /// <param name="oldDailyWage">The old daily wage.</param>
    /// <param name="newDailyWage">The new daily wage.</param>
    public ChangeDailyWageResult(GameState updatedGameState, int playerIndex, decimal oldDailyWage, decimal newDailyWage) : base(updatedGameState, playerIndex)
    {
        OldDailyWage = oldDailyWage;
        NewDailyWage = newDailyWage;
    }

    /// <summary>
    /// Gets the new daily wage.
    /// </summary>
    public decimal NewDailyWage { get; init; }
    
    /// <summary>
    /// Gets the old daily wage.
    /// </summary>
    public decimal OldDailyWage { get; init; }
}
