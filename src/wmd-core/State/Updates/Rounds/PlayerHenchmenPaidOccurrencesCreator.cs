using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds;

internal sealed class PlayerHenchmenPaidOccurrencesCreator : RoundUpdateResultOccurrencesCreator
{
    public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
        CreateRangeOfPlayerIndices(gameState)
            .Where(index => gameState.Players[index].State.WorkforceState.TotalHenchmenCount > 0)
            .Select(index => new PlayerHenchmenPaid(gameState, index));
}
