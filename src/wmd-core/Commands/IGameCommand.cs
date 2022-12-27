using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;

namespace WMD.Game.Commands;

/// <summary>
/// Non-generic interface for game commands.
/// </summary>
/// <seealso cref="IGameCommand{TInput, TOutput}"/>
public interface IGameCommand
{
    /// <summary>
    /// Determines whether this command can be executed for the given
    /// <see cref="GameState"/>.
    /// </summary>
    /// <param name="gameState">
    /// The <see cref="GameState"/> upon which the command would be
    /// executed.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the command can be executed for the
    /// given <paramref name="gameState"/>; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    bool CanExecuteForState([DisallowNull] GameState gameState);

    /// <summary>
    /// Determines whether this command can be executed for the given
    /// <see cref="GameState"/> and input.
    /// </summary>
    /// <param name="gameState">
    /// The <see cref="GameState"/> upon which the command would be
    /// executed.
    /// </param>
    /// <param name="input">
    /// Input data for the command.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the command can be executed for the
    /// given <paramref name="gameState"/> and <paramref name="input"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] object input);

    /// <summary>
    /// Executes this command using the given <see cref="GameState"/> and
    /// input data, and produces the result of the command.
    /// </summary>
    /// <param name="gameState">
    /// The <see cref="GameState"/> upon which the command is to be
    /// executed.
    /// </param>
    /// <param name="input">
    /// Input data for the command.
    /// </param>
    /// <returns>
    /// A new object describing the results of running the command.
    /// </returns>
    /// <remarks>
    /// This method may throw an exception if it is called with a <paramref name="gameState"/>
    /// and/or <paramref name="input"/> for which either the <see cref="CanExecuteForState"/>
    /// or <see cref="CanExecuteForStateAndInput"/> would return <see langword="false"/>.
    /// </remarks>
    object Execute([DisallowNull] GameState gameState, [DisallowNull] object input);
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
    /// <summary>
    /// Determines whether this command can be executed for the given
    /// <see cref="GameState"/> and input.
    /// </summary>
    /// <param name="gameState">
    /// The <see cref="GameState"/> upon which the command would be
    /// executed.
    /// </param>
    /// <param name="input">
    /// Input data for the command.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the command can be executed for the
    /// given <paramref name="gameState"/> and <paramref name="input"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] TInput input);

    /// <summary>
    /// Executes this command using the given <see cref="GameState"/> and
    /// input data, and produces the result of the command.
    /// </summary>
    /// <param name="gameState">
    /// The <see cref="GameState"/> upon which the command is to be
    /// executed.
    /// </param>
    /// <param name="input">
    /// Input data for the command.
    /// </param>
    /// <returns>
    /// A new object describing the results of running the command.
    /// </returns>
    /// <remarks>
    /// This method may throw an exception if it is called with a <paramref name="gameState"/>
    /// and/or <paramref name="input"/> for which either the <see cref="IGameCommand.CanExecuteForState(GameState)"/>
    /// or <see cref="CanExecuteForStateAndInput"/> would return <see langword="false"/>.
    /// </remarks>
    TOutput Execute([DisallowNull] GameState gameState, [DisallowNull] TInput input);
}
