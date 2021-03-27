using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal abstract class RoundUpdateResultOccurrencesCreator
    {
        public abstract IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState);

        protected static IEnumerable<int> CreateRangeOfPlayerIndices(GameState gameState) => Enumerable.Range(0, gameState.Players.Count);
    }
}
