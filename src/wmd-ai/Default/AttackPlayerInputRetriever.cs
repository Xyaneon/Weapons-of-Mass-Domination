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
            return new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex };
        }

        private static int ChooseTargetPlayerIndex(GameState gameState) =>
            GameStateChecks.SelectRandomNonCurrentPlayerIndex(gameState);

        private static bool HasHenchmenToAttackWith(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen > 0;
    }
}
