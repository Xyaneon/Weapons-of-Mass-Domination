using System;
using System.Collections.Generic;
using System.Linq;
using WMD.Game.Constants;
using WMD.Game.Extensions;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Updates.Rounds;

internal sealed class PlayerHenchmenQuitOccurrencesCreator : RoundUpdateResultOccurrencesCreator
{
    public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
        CreateRangeOfPlayerIndices(gameState)
            .Select(index => CreatePlayerHenchmenQuitOccurrence(gameState, index))
            .WhereNotNull();

    private static PlayerHenchmenQuit? CreatePlayerHenchmenQuitOccurrence(GameState gameState, int index)
    {
        PlayerState playerState = gameState.Players[index].State;

        long henchmenQuit = playerState switch
        {
            _ when PlayerCannotPayTheirHenchmen(playerState) => playerState.WorkforceState.NumberOfHenchmen,
            _ when PlayerHenchmenAreUnderpaid(playerState) => CalculateHenchmenQuitDueToUnderpay(playerState.WorkforceState),
            _ => 0,
        };

        return henchmenQuit > 0 ? new(index, henchmenQuit) : null;
    }

    private static bool PlayerCannotPayTheirHenchmen(PlayerState playerState) =>
        playerState.Money <= 0 && playerState.WorkforceState.NumberOfHenchmen > 0;

    private static bool PlayerHenchmenAreUnderpaid(PlayerState playerState) =>
        playerState.WorkforceState.DailyPayRate < HenchmenConstants.MinimumDailyWage && playerState.WorkforceState.NumberOfHenchmen > 0;

    private static int CalculateHenchmenQuitDueToUnderpay(WorkforceState workforceState)
    {
        double underpayPercentage = 1 - (double)workforceState.DailyPayRate / (double)HenchmenConstants.MinimumDailyWage;
        return (int)Math.Ceiling(workforceState.NumberOfHenchmen * underpayPercentage);
    }
}
