using System;
using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class HireHenchmenInputRetriever : ICommandInputRetriever
    {
        private const string ConfirmationPromptFormatString = "You will be looking to fill {0:N0} positions. Continue?";
        private const string PositionsToOfferPromptFormatString = "Please enter how many open positions you would like to offer (between 1 and {0:N0})";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            long maximumAllowedPositions = CalculateMaximumAllowedPositions(gameState);
            if (maximumAllowedPositions == 0)
            {
                return null;
            }

            var allowedPositionsToOffer = new LongRange(0, maximumAllowedPositions);
            var openPositionsToOffer = UserInput.GetLong(string.Format(PositionsToOfferPromptFormatString, maximumAllowedPositions), allowedPositionsToOffer);
            if (openPositionsToOffer <= 0)
            {
                PrintingUtility.PrintNoPositionsToOffer();
                return null;
            }

            return UserInput.GetConfirmation(string.Format(ConfirmationPromptFormatString, openPositionsToOffer))
                ? new HireHenchmenInput() with { OpenPositionsOffered = openPositionsToOffer }
                : null;
        }

        private static long CalculateMaximumAllowedPositions(GameState gameState) =>
            (long)Math.Floor(gameState.CurrentPlayer.State.Money / gameState.CurrentPlayer.State.WorkforceState.DailyPayRate);
    }
}
