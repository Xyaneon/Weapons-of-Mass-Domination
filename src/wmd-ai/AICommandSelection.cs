using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Commands;

namespace WMD.AI
{
    /// <summary>
    /// Provides data on the command a CPU player has selected, and the corresponding input data.
    /// </summary>
    public class AICommandSelection
    {
        private const string ArgumentNullException_Command = "The command selected by the CPU player AI cannot be null.";
        private const string ArgumentNullException_Input = "The input for the command selected by the CPU player AI cannot be null.";

        /// <summary>
        /// Initializes a new instance of the <see cref="AICommandSelection"/> class.
        /// </summary>
        /// <param name="command">The game command that the AI has chosen for this turn.</param>
        /// <param name="input">The additional command input data that the AI has specified for its command.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="command"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="input"/> is <see langword="null"/>.
        /// </exception>
        public AICommandSelection([DisallowNull] IGameCommand command, [DisallowNull] object input)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command), ArgumentNullException_Command);
            Input = input ?? throw new ArgumentNullException(nameof(input), ArgumentNullException_Input);
        }

        /// <summary>
        /// Gets the game command that the AI has chosen for this turn.
        /// </summary>
        public IGameCommand Command { get; }

        /// <summary>
        /// Gets the additional input data the AI has specified for <see cref="Command"/>.
        /// </summary>
        public object Input { get; }
    }
}
