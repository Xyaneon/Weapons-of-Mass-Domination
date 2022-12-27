using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Utility;

namespace WMD.AI.Default;

internal sealed class UpgradeSecretBaseInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState) =>
        CanUpgradeSecretBaseThisTurn(gameState) ? new UpgradeSecretBaseInput() : null;

    private static bool CanUpgradeSecretBaseThisTurn(GameState gameState) =>
        GameStateChecks.CurrentPlayerHasASecretBase(gameState)
        && PlayerHasEnoughMoneyToUpgradeTheirSecretBase(gameState.CurrentPlayer.State);

    private static bool PlayerHasEnoughMoneyToUpgradeTheirSecretBase(PlayerState playerState) =>
        SecretBase.CalculateUpgradePrice(playerState.SecretBase) <= playerState.Money;
}
