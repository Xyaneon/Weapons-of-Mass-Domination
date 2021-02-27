using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player manufacturing nukes.
    /// </summary>
    public class ManufactureNukesCommand : GameCommand<ManufactureNukesInput, ManufactureNukesResult>
    {
        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, ManufactureNukesInput input)
        {
            return !CurrentPlayerDoesNotHaveEnoughMoney(gameState, input.NumberOfNukesToManufacture);
        }

        public override ManufactureNukesResult Execute([DisallowNull] GameState gameState, ManufactureNukesInput input)
        {
            if (CurrentPlayerDoesNotHaveEnoughMoney(gameState, input.NumberOfNukesToManufacture))
            {
                throw new InvalidOperationException("The current player does not have enough money to manufacture the requested quantity of nukes.");
            }

            GameState updatedGameState = gameState;
            decimal manufacturingPrice = CalculateManufacturingPrice(gameState, input.NumberOfNukesToManufacture);
            updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(updatedGameState, gameState.CurrentPlayerIndex, -1 * manufacturingPrice);
            updatedGameState = GameStateUpdater.AdjustNukesForPlayer(updatedGameState, gameState.CurrentPlayerIndex, input.NumberOfNukesToManufacture);

            return new ManufactureNukesResult(updatedGameState, gameState.CurrentPlayerIndex, input.NumberOfNukesToManufacture);
        }

        private static decimal CalculateManufacturingPrice(GameState gameState, int quantity)
        {
            return NukeConstants.ManufacturingPrice * quantity;
        }

        private static bool CurrentPlayerDoesNotHaveEnoughMoney(GameState gameState, int quantity)
        {
            return CalculateManufacturingPrice(gameState, quantity) > gameState.CurrentPlayer.State.Money;
        }
    }
}
