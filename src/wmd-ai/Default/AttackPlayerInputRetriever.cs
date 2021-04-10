using System;
using System.Linq;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default
{
    internal sealed class AttackPlayerInputRetriever : ICommandInputRetriever
    {
        static AttackPlayerInputRetriever() => _random = new Random();

        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (!HasHenchmenToAttackWith(gameState))
            {
                return null;
            }
            int? targetPlayerIndex = ChooseTargetPlayerIndex(gameState);
            return targetPlayerIndex.HasValue ? new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex.Value } : null;
        }

        private static int? ChooseTargetPlayerIndex(GameState gameState)
        {
            return Enumerable.Range(0, gameState.Players.Count)
                .Where(playerIndex => playerIndex != gameState.CurrentPlayerIndex)
                .OrderBy(_ => _random.NextDouble())
                .First();
        }

        private static bool HasHenchmenToAttackWith(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen > 0;

        private static readonly Random _random;
    }
}
