namespace WMD.Game.Commands
{
    /// <summary>
    /// Non-generic interface for game commands.
    /// </summary>
    /// <seealso cref="IGameCommand{TInput, TOutput}"/>
    public interface IGameCommand
    {
        bool CanExecuteForState(GameState gameState);

        bool CanExecuteForStateAndInput(GameState gameState, object input);

        object Execute(GameState gameState, object input);
    }

    /// <summary>
    /// Generic interface for game commands.
    /// </summary>
    /// <typeparam name="TInput">The type of <see cref="CommandInput"/> this command accepts.</typeparam>
    /// <typeparam name="TOutput">The type of <see cref="CommandResult"/> this command returns.</typeparam>
    /// <seealso cref="IGameCommand"/>
    public interface IGameCommand<TInput, TOutput> : IGameCommand
        where TInput : CommandInput
        where TOutput : CommandResult
    {
        bool CanExecuteForStateAndInput(GameState gameState, TInput input);

        TOutput Execute(GameState gameState, TInput input);
    }
}
