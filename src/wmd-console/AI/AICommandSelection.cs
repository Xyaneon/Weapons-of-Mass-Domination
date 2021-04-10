using System;
using WMD.Game.Commands;

namespace WMD.Console.AI
{
    class AICommandSelection
    {
        private const string ArgumentNullException_Command = "The command selected by the CPU player AI cannot be null.";
        private const string ArgumentNullException_Input = "The input for the command selected by the CPU player AI cannot be null.";

        public AICommandSelection(IGameCommand command, object input)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command), ArgumentNullException_Command);
            Input = input ?? throw new ArgumentNullException(nameof(input), ArgumentNullException_Input);
        }

        public IGameCommand Command { get; }

        public object Input { get; }
    }
}
