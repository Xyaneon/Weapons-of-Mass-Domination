using System.Collections.Generic;
using System.Linq;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;

namespace WMD.Game.State.Updates.Rounds
{
    internal sealed class PlayerHenchmenQuitOccurrencesCreator : RoundUpdateResultOccurrencesCreator
    {
        public override IEnumerable<RoundUpdateResultItem> CreateOccurrences(GameState gameState) =>
            CreateRangeOfPlayerIndices(gameState)
                .Where(index => PlayerCannotPayTheirHenchmen(gameState.Players[index].State))
                .Select(index => CreatePlayerHenchmenQuitOccurrence(gameState, index));

        private static PlayerHenchmenQuit CreatePlayerHenchmenQuitOccurrence(GameState gameState, int index) =>
            new(index, gameState.Players[index].State.WorkforceState.NumberOfHenchmen);

        private static bool PlayerCannotPayTheirHenchmen(PlayerState playerState) =>
            playerState.Money <= 0 && playerState.WorkforceState.NumberOfHenchmen > 0;
    }
}
