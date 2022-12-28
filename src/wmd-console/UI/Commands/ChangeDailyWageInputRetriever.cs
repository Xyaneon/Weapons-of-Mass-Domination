using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class ChangeDailyWageInputRetriever : ICommandInputRetriever
{
    private const string ConfirmationPromptFormatString = "Your henchmen's new daily wage will be {0:C}. You currently have {1:N0} henchmen, meaning you will spend {2:C} total each day on labor costs. Confirm?";
    private const string GetNewDailyWagePromptFormatString = "You currently pay your henchmen {0:C} each every day. Please enter the new amount ({1:C} or above)";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        decimal currentDailyWage = gameState.CurrentPlayer.State.WorkforceState.DailyPayRate;
        var allowedSpendingAmounts = new DecimalRange(0.0M, decimal.MaxValue);
        var prompt = string.Format(GetNewDailyWagePromptFormatString, currentDailyWage, allowedSpendingAmounts.Minimum);

        var newDailyWage = UserInput.GetDecimal(prompt, allowedSpendingAmounts);
        if (newDailyWage < 0.0M)
        {
            return null;
        }

        long currentNumberOfHenchmen = gameState.CurrentPlayer.State.WorkforceState.TotalHenchmenCount;

        var confirmationPrompt = string.Format(ConfirmationPromptFormatString, newDailyWage, currentNumberOfHenchmen, currentNumberOfHenchmen * newDailyWage);
        return UserInput.GetConfirmation(confirmationPrompt)
            ? new ChangeDailyWageInput() with { NewDailyWage = newDailyWage }
            : null;
    }
}
