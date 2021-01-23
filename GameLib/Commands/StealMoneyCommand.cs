using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

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

        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] StealMoneyInput input)
        {
            return true;
        }

        public override StealMoneyResult Execute([DisallowNull] GameState gameState, [DisallowNull] StealMoneyInput input)
        {
            decimal moneyStolenByPlayer = (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolenByHenchmen = gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen * (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + 20 * _random.NextDouble(), 2);
            decimal moneyStolen = moneyStolenByPlayer + moneyStolenByHenchmen;

            GameState updatedGameState = GameStateUpdater.AdjustMoneyForPlayer(gameState, gameState.CurrentPlayerIndex, moneyStolen);

            return new StealMoneyResult(updatedGameState, gameState.CurrentPlayerIndex, moneyStolen);
        }
    }
}
