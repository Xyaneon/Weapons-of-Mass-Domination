﻿namespace WMD.Game.Commands
{
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
        bool CanExecuteForState(GameState gameState);

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
        bool CanExecuteForStateAndInput(GameState gameState, object input);

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
        bool CanExecuteForStateAndInput(GameState gameState, TInput input);

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
        TOutput Execute(GameState gameState, TInput input);
    }
}
