using WMD.Console.Miscellaneous;
using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands;

class SellLandInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (gameState.CurrentPlayer.State.Land <= 0)
        {
            PrintingUtility.PrintNoLandToSell();
            return null;
        }

        var pricePerSquareKilometer = gameState.UnclaimedLandPurchasePrice;
        var allowedSaleAmounts = new IntRange(0, gameState.CurrentPlayer.State.Land);
        var prompt = $"Land is currently selling at {pricePerSquareKilometer:C}/km². How much do you want to sell? ({allowedSaleAmounts.Minimum} to {allowedSaleAmounts.Maximum})";
        var areaToSell = UserInput.GetInteger(prompt, allowedSaleAmounts);
        if (areaToSell <= 0)
        {
            return null;
        }

        var totalSalePrice = areaToSell * pricePerSquareKilometer;
        var confirmationPrompt = $"This transaction will earn you {totalSalePrice:C}. Proceed?";
        return UserInput.GetConfirmation(confirmationPrompt)
            ? new SellLandInput() with { AreaToSell = areaToSell }
            : null;
    }
}
