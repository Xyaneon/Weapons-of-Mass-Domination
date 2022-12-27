using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Console.UI.Commands;

class PurchaseUnclaimedLandInputRetriever : ICommandInputRetriever
{
    private const string UnclaimedLandPurchasePrompt = "Please enter how many square kilometers of land you would like to purchase";

    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (gameState.Planet.UnclaimedLandArea < 1)
        {
            PrintingUtility.PrintNoUnclaimedLandLeftToPurchase();
            return null;
        }

        PrintingUtility.PrintCurrentUnclaimedLandAreaAndPrice(gameState);

        if (!GameStateChecks.CurrentPlayerCouldPurchaseLand(gameState))
        {
            PrintingUtility.PrintInsufficientFundsForAnyLandPurchase();
            return null;
        }

        var maxPurchaseableArea = LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState);
        var allowedPurchaseAmounts = new IntRange(0, maxPurchaseableArea);
        var prompt = $"{UnclaimedLandPurchasePrompt} ({allowedPurchaseAmounts.Minimum} to {allowedPurchaseAmounts.Maximum})";
        var areaToPurchase = UserInput.GetInteger(prompt, allowedPurchaseAmounts);
        if (areaToPurchase < 1)
        {
            return null;
        }

        var totalPurchasePrice = LandAreaCalculator.CalculateTotalPurchasePrice(gameState, areaToPurchase);
        var confirmationPrompt = $"This transaction will cost you {totalPurchasePrice:C}. Proceed?";
        return UserInput.GetConfirmation(confirmationPrompt)
            ? new PurchaseUnclaimedLandInput() with { AreaToPurchase = areaToPurchase }
            : null;
    }
}
