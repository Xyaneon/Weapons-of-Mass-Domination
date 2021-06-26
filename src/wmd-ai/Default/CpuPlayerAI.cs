using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default
{
    /// <summary>
    /// Provides a default AI for computer players.
    /// </summary>
    public sealed class CpuPlayerAI : ICpuPlayerAI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CpuPlayerAI"/> class.
        /// </summary>
        public CpuPlayerAI()
        {
            _random = new Random();
        }

        public AICommandSelection ChooseCommandAndInputForGameState([DisallowNull] GameState gameState)
        {
            IReadOnlyList<IGameCommand> selectableCommandsList = DetermineValidCommandsInRandomOrder(gameState);

            foreach (var command in selectableCommandsList)
            {
                if (TryGetValidInputForCommand(gameState, command, out var input))
                {
                    return new AICommandSelection(command, input!);
                }
            }

            return new AICommandSelection(new SkipTurnCommand(), new SkipTurnInput());
        }

        private static IEnumerable<IGameCommand> DetermineValidCommands(GameState gameState) =>
            CommandUtility.GetAllEffectiveCommands().Where(command => command.CanExecuteForState(gameState));

        private IReadOnlyList<IGameCommand> DetermineValidCommandsInRandomOrder(GameState gameState) =>
            DetermineValidCommands(gameState).OrderBy(_ => _random.Next()).ToList().AsReadOnly();

        private static bool TryGetValidInputForCommand(GameState gameState, IGameCommand command, out CommandInput? input)
        {
            input = null;

            if (TryGetInputRetrieverForCommand(command, out var inputRetriever))
            {
                input = inputRetriever!.GetCommandInput(gameState);

                if (input == null)
                {
                    return false;
                }

                if (!command.CanExecuteForStateAndInput(gameState, input!))
                {
                    input = null;
                }
            }

            return input != null;
        }

        private static bool TryGetInputRetrieverForCommand(IGameCommand command, out ICommandInputRetriever? inputRetriever)
        {
            Type inputType;

            try
            {
                inputType = CommandUtility.GetInputType(command);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, nameof(command), ex);
            }

            try
            {
                inputRetriever = CommandInputRetrieverFactory.CreateICommandInputRetriever(inputType);
            }
            catch (ArgumentException)
            {
                inputRetriever = null;
            }

            return inputRetriever != null;
        }

        private readonly Random _random;
    }
}
