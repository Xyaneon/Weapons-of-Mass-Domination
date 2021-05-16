using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Utility;

namespace WMD.AI.Default
{
    internal sealed class AttackPlayerInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState)
        {
            if (!HasHenchmenToAttackWith(gameState))
            {
                return null;
            }

            int targetPlayerIndex = ChooseTargetPlayerIndex(gameState);
            long henchmenToAttackWith = CalculateHenchmenToAttackWith(gameState);
            
            return new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex, NumberOfAttackingHenchmen = henchmenToAttackWith };
        }

        private static long CalculateHenchmenToAttackWith(GameState gameState) =>
            Math.Max(gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen / 2, 1);

        private static int ChooseTargetPlayerIndex(GameState gameState) =>
            GameStateChecks.SelectRandomNonCurrentPlayerIndex(gameState);

        private static bool HasHenchmenToAttackWith(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen > 0;
    }
}
