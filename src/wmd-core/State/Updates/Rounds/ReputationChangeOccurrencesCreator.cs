using System.Collections.Generic;
using WMD.Game.Constants;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.Game.State.Updates.Rounds;

internal sealed class ReputationChangeOccurrencesCreator : RoundUpdateResultOccurrencesCreator
{
    public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState)
    {
        foreach (int playerIndex in CreateRangeOfPlayerIndices(gameState))
        {
            int reputationChangeAmount = CalculateReputationChange(gameState, playerIndex);
            if (reputationChangeAmount != 0)
            {
                yield return new ReputationChange(playerIndex, reputationChangeAmount);
            }
        }
    }

    private static int CalculateReputationChange(GameState gameState, int playerIndex)
    {
        int currentReputationPercentage = gameState.Players[playerIndex].State.ReputationPercentage;
        int baseReputationPercentage = ReputationCalculator.CalculateBaseReputationPercentage(gameState, playerIndex);

        if (currentReputationPercentage > baseReputationPercentage && currentReputationPercentage > ReputationConstants.ReputationChangeRate)
        {
            return -1 * ReputationConstants.ReputationChangeRate;
        }

        if (currentReputationPercentage < baseReputationPercentage && currentReputationPercentage < ReputationConstants.MaxReputationPercentage - ReputationConstants.ReputationChangeRate)
        {
            return ReputationConstants.ReputationChangeRate;
        }

        return 0;
    }
}
