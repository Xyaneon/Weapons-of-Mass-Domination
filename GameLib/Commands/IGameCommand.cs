namespace WMD.Game.Commands
{
    /// <summary>
    /// Interface for game commands.
    /// </summary>
    /// <typeparam name="TInput">The type of <see cref="CommandInput"/> this command accepts.</typeparam>
    /// <typeparam name="TOutput">The type of <see cref="CommandResult"/> this command returns.</typeparam>
    public interface IGameCommand<TInput, TOutput>
        where TInput : CommandInput
        where TOutput : CommandResult
    {
        bool CanExecuteForState(GameState gameState);

        bool CanExecuteForStateAndInput(GameState gameState, TInput input);

        TOutput Execute(GameState gameState, TInput input);
    }
}
