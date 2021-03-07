using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands
{
    class ManufactureNukesInputRetriever : ICommandInputRetriever
    {
        private const string NukesToManufacturePrompt = "Please enter how many nukes you would like to manufacture";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (!GameStateChecks.CurrentPlayerHasCompletedNukesResearch(gameState))
            {
                PrintingUtility.PrintNukesResearchNotCompleted();
                return null;
            }

            var maximumAllowedNukeQuantity = NukesCalculator.CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture(gameState);
            var allowedAmounts = new IntRange(0, maximumAllowedNukeQuantity);

            var prompt = $"{NukesToManufacturePrompt} ({allowedAmounts.Minimum} to {allowedAmounts.Maximum})";
            var nukesToManufacture = UserInput.GetInteger(prompt, allowedAmounts);

            if (nukesToManufacture <= 0)
            {
                PrintingUtility.PrintNoNukesToManufacture();
                return null;
            }

            var manufacturingPrice = NukesCalculator.CalculateTotalManufacturingPrice(gameState, nukesToManufacture);

            return UserInput.GetConfirmation($"You will manufacture {nukesToManufacture:N0} new nukes for {manufacturingPrice:C}. Continue?")
                ? new ManufactureNukesInput() with { NumberOfNukesToManufacture = nukesToManufacture }
                : null;
        }
    }
}
