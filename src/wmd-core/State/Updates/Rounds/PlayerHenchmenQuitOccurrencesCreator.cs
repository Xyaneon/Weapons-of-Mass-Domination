using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class PlayerHenchmenQuitOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(index => gameState.Players[index].State.Money <= 0 && gameState.Players[index].State.WorkforceState.NumberOfHenchmen > 0)
                .Select(index => new PlayerHenchmenQuit(index, gameState.Players[index].State.WorkforceState.NumberOfHenchmen));
    }
}
