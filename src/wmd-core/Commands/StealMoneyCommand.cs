using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands;

/// <summary>
/// The command for the current player stealing money.
/// </summary>
public class StealMoneyCommand : GameCommand<StealMoneyInput, StealMoneyResult>
{
    private const decimal BaseMoneyStealAmount = 200;

    static StealMoneyCommand() => _random = new Random();

    private static readonly Random _random;

    public override bool CanExecuteForState([DisallowNull] GameState gameState) => true;

    public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] StealMoneyInput input) => true;

    public override StealMoneyResult Execute([DisallowNull] GameState gameState, [DisallowNull] StealMoneyInput input)
    {
        decimal moneyStolen = CalculateMoneyStolenByPlayer();

        GameState updatedGameState = new GameStateUpdater(gameState)
            .AdjustMoneyForPlayer(gameState.CurrentPlayerIndex, moneyStolen)
            .AndReturnUpdatedGameState();

        return new StealMoneyResult(updatedGameState, gameState.CurrentPlayerIndex, moneyStolen);
    }

    private static decimal CalculateMoneyStolenByPlayer() => (decimal)Math.Round((double)BaseMoneyStealAmount - 10 + (20 * _random.NextDouble()), 2);
}
