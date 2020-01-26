namespace WMD.Game.Commands
{
    /// <summary>
    /// Base class for game commands.
    /// This class cannot be directly instantiated.
    /// </summary>
    /// <typeparam name="TInput">The type of <see cref="CommandInput"/> this command accepts.</typeparam>
    /// <typeparam name="TOutput">The type of <see cref="CommandResult"/> this command returns.</typeparam>
    public abstract class GameCommand<TInput, TOutput>
        where TInput : CommandInput
        where TOutput : CommandResult
    {
        public abstract bool CanExecuteForState(GameState gameState);

        public abstract bool CanExecuteForStateAndInput(GameState gameState, TInput input);

        public abstract TOutput Execute(GameState gameState, TInput input);
    }
}
