using WMD.Game.State.Data;

namespace WMD.Console;

abstract class PlayerTurnRunner
{
    public abstract GameState RunTurn(GameState gameState);
}
