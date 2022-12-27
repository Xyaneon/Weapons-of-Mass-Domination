using WMD.Game.Commands;
using WMD.Game.State.Data;
using WMD.Game.State.Data.SecretBases;
using WMD.Game.State.Utility;

namespace WMD.AI.Default;

internal sealed class BuildSecretBaseInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState) =>
        CanBuildSecretBaseThisTurn(gameState) ? new BuildSecretBaseInput() : null;

    private static bool CanBuildSecretBaseThisTurn(GameState gameState) =>
        !GameStateChecks.CurrentPlayerHasASecretBase(gameState)
        && SecretBase.SecretBaseBuildPrice <= gameState.CurrentPlayer.State.Money;
}
