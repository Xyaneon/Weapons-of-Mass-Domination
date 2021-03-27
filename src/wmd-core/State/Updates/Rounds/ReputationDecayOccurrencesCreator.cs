using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class ReputationDecayOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(index => gameState.Players[index].State.ReputationPercentage > 0)
                .Select(index => new ReputationDecay(index, CalculateReputationLost(gameState, index)));

        private static int CalculateReputationLost(GameState gameState, int playerIndex) =>
            Math.Min(gameState.Players[playerIndex].State.ReputationPercentage, ReputationConstants.ReputationDecayRate);
    }
}
