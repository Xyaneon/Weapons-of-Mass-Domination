using WMD.Game.State.Data;

namespace WMD.AI
{
    public interface ICpuPlayerAI
    {
        AICommandSelection ChooseCommandAndInputForGameState(GameState gameState);
    }
}
