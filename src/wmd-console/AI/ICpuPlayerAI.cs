using WMD.Game.State.Data;

namespace WMD.Console.AI
{
    interface ICpuPlayerAI
    {
        AICommandSelection ChooseCommandAndInputForGameState(GameState gameState);
    }
}
