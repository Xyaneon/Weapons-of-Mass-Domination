using WMD.Game.State.Data;

namespace WMD.AI;

/// <summary>
/// The interface for classes which implement an AI for computer players.
/// </summary>
public interface ICpuPlayerAI
{
    /// <summary>
    /// Has the AI select its command and input data for the current turn in <paramref name="gameState"/>.
    /// </summary>
    /// <param name="gameState">The current <see cref="GameState"/>.</param>
    /// <returns>A new <see cref="AICommandSelection"/> instance providing information on the AI's decision.</returns>
    AICommandSelection ChooseCommandAndInputForGameState(GameState gameState);
}
