using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class HireHenchmenInputRetriever : ICommandInputRetriever
    {
        private const string ConfirmationPromptFormatString = "You will be looking to fill {0:N0} positions. Continue?";
        private const string PositionsToOfferPrompt = "Please enter how many open positions you would like to offer";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            var allowedPositionsToOffer = new LongRange(0, long.MaxValue);
            var openPositionsToOffer = UserInput.GetLong(PositionsToOfferPrompt, allowedPositionsToOffer);
            if (openPositionsToOffer <= 0)
            {
                PrintingUtility.PrintNoPositionsToOffer();
                return null;
            }
            return UserInput.GetConfirmation(string.Format(ConfirmationPromptFormatString, openPositionsToOffer))
                ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer }
                : null;
        }
    }
}
