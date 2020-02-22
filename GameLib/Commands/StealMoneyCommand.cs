using System;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player stealing money.
    /// </summary>
    public class StealMoneyCommand : GameCommand<StealMoneyInput, StealMoneyResult>
    {
        private const decimal BaseMoneyStealAmount = 200;

        static StealMoneyCommand()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, StealMoneyInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return true;
        }

        public override StealMoneyResult Execute(GameState gameState, StealMoneyInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            decimal moneyStolenByPlayer = (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolenByHenchmen = gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen * (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolen = moneyStolenByPlayer + moneyStolenByHenchmen;
            gameState.CurrentPlayer.State.Money += moneyStolen;

            return new StealMoneyResult(gameState.CurrentPlayer, gameState, moneyStolen);
        }
    }
}
