using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Provides common utility methods for working with game commands.
    /// </summary>
    public static class CommandUtility
    {
        /// <summary>
        /// Initializes static members of the <see cref="CommandUtility"/> class.
        /// </summary>
        static CommandUtility()
        {
            _allCommands = new List<IGameCommand>()
            {
                new AttackPlayerCommand(),
                new BuildSecretBaseCommand(),
                new DistributePropagandaCommand(),
                new HireHenchmenCommand(),
                new LaunchNukesCommand(),
                new ManufactureNukesCommand(),
                new PurchaseUnclaimedLandCommand(),
                new ResearchNukesCommand(),
                new ResignCommand(),
                new SellLandCommand(),
                new SkipTurnCommand(),
                new StealMoneyCommand(),
                new UpgradeSecretBaseCommand(),
            }.AsReadOnly();

            _allEffectiveCommands = _allCommands.Where(command => IsAnEffectiveCommand(command)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a read-only collection of all available game commands.
        /// </summary>
        /// <returns>A read-only collection of all available game commands.</returns>
        /// <seealso cref="GetAllEffectiveCommands"/>
        public static IEnumerable<IGameCommand> GetAllCommands() => _allCommands;

        /// <summary>
        /// Gets a read-only collection of all effective game commands (i.e., all available commands other than <see cref="SkipTurnCommand"/> and <see cref="ResignCommand"/>).
        /// </summary>
        /// <returns>A read-only collection of all effective game commands.</returns>
        /// <seealso cref="GetAllCommands"/>
        public static IEnumerable<IGameCommand> GetAllEffectiveCommands() => _allEffectiveCommands;

        /// <summary>
        /// Gets the <see cref="CommandInput"/> subclass <see cref="Type"/> for the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">The command to retrieve the input type for.</param>
        /// <returns>The type of the input class associated with <paramref name="command"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="command"/> does not inherit from <see cref="GameCommand{TInput, TOutput}"/>.
        /// </exception>
        public static Type GetInputType([DisallowNull] IGameCommand command)
        {
            Type? baseCommandType = command.GetType().BaseType;
            if (baseCommandType == null)
            {
                throw new ArgumentException($"The supplied command does not inherit from {typeof(GameCommand<,>).Name}.", nameof(command));
            }
            return baseCommandType.GenericTypeArguments[0];
        }

        private static bool IsAnEffectiveCommand(IGameCommand command) =>
            command.GetType() != typeof(ResignCommand) && command.GetType() != typeof(SkipTurnCommand);

        private static readonly IReadOnlyList<IGameCommand> _allCommands;
        private static readonly IReadOnlyList<IGameCommand> _allEffectiveCommands;
    }
}
