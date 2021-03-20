using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands
{
    class DistributePropagandaInputRetriever : ICommandInputRetriever
    {
        private const string ConfirmationPromptFormatString = "This transaction will cost you {0:C}. Proceed?";
        private const string GetSpendingAmountPromptFormatString = "Please enter how much money you would like to spend on propaganda distribution ({0:C} to {1:C})";

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (GameStateChecks.CurrentPlayerHasNoMoney(gameState))
            {
                PrintingUtility.PrintCurrentPlayerHasNoMoneyToSpend();
                return null;
            }

            decimal maxAmountOfMoneyToSpend = gameState.CurrentPlayer.State.Money;
            var allowedSpendingAmounts = new DecimalRange(0.0M, maxAmountOfMoneyToSpend);
            var prompt = string.Format(GetSpendingAmountPromptFormatString, allowedSpendingAmounts.Minimum, allowedSpendingAmounts.Maximum);

            var amountToSpend = UserInput.GetDecimal(prompt, allowedSpendingAmounts);
            if (amountToSpend < 0.0M)
            {
                return null;
            }

            var confirmationPrompt = string.Format(ConfirmationPromptFormatString, amountToSpend);
            return UserInput.GetConfirmation(confirmationPrompt)
                ? new DistributePropagandaInput() with { MoneyToSpend = amountToSpend }
                : null;
        }
    }
}
