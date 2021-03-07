using WMD.Console.UI.Core;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.Console.UI.Commands
{
    class AttackPlayerInputRetriever : ICommandInputRetriever
    {
        public CommandInput? GetCommandInput(GameState gameState)
        {
            var targetPlayerIndex = UserInput.GetAttackTargetPlayerIndex(gameState);
            return targetPlayerIndex.HasValue
                ? new AttackPlayerInput() { TargetPlayerIndex = targetPlayerIndex.Value }
                : null;
        }
    }
}
