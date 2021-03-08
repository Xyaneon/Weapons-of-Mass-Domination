using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class HireHenchmenInputRetriever : ICommandInputRetriever
    {
        private const string PositionsToOfferPrompt = "Please enter how many open positions you would like to offer";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            var allowedPositionsToOffer = new IntRange(0, int.MaxValue);
            var openPositionsToOffer = UserInput.GetInteger(PositionsToOfferPrompt, allowedPositionsToOffer);
            if (openPositionsToOffer <= 0)
            {
                PrintingUtility.PrintNoPositionsToOffer();
                return null;
            }
            return UserInput.GetConfirmation($"You will be looking to fill {openPositionsToOffer:N0} positions. Continue?")
                ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer }
                : null;
        }
    }
}
