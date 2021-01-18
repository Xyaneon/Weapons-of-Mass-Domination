using System.Diagnostics.CodeAnalysis;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Generic base class for game commands.
    /// This class cannot be directly instantiated.
    /// </summary>
    /// <typeparam name="TInput">The type of <see cref="CommandInput"/> this command accepts.</typeparam>
    /// <typeparam name="TOutput">The type of <see cref="CommandResult"/> this command returns.</typeparam>
    public abstract class GameCommand<TInput, TOutput> : IGameCommand<TInput, TOutput>
        where TInput : CommandInput
        where TOutput : CommandResult
    {
        public abstract bool CanExecuteForState([DisallowNull] GameState gameState);

        public abstract bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, TInput input);

        public bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, object input)
        {
            return CanExecuteForStateAndInput(gameState, (TInput)input);
        }

        public abstract TOutput Execute([DisallowNull] GameState gameState, TInput input);

        public object Execute([DisallowNull] GameState gameState, object input)
        {
            return Execute(gameState, (TInput)input);
        }
    }
}
