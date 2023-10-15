using System.Collections.Generic;
using System.Linq;
using WMD.Game.Extensions;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Game.State.Updates.Rounds;

internal sealed class PlayerThievesEarnedMoneyOccurrencesCreator : RoundUpdateResultOccurrencesCreator
{
    public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
        CreateRangeOfPlayerIndices(gameState)
            .Where(index => gameState.Players[index].State.WorkforceState.ThiefCount > 0)
            .Select(index => CreateMoneyEarnedFromThievesOccurrence(gameState, index))
            .WhereNotNull();

    private static PlayerEarnedMoneyFromThieves? CreateMoneyEarnedFromThievesOccurrence(GameState gameState, int index)
    {
        decimal amount = ThiefCalculator.CalculateMoneyStolenByThieves(gameState, index);

        return amount != 0 ? new PlayerEarnedMoneyFromThieves(index, amount) : null;
    }
}
