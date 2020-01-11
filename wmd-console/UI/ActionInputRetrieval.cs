using System;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console.UI
{
    static class ActionInputRetrieval
    {
        public static PurchaseUnclaimedLandInput GetPurchaseUnclaimedLandInput(GameState gameState)
        {
            // TODO: Collect more input.
            throw new NotImplementedException();
        }

        public static ResignInput GetResignInput(GameState gameState) => new ResignInput();

        public static SkipTurnInput GetSkipTurnInput(GameState gameState) => new SkipTurnInput();

        public static StealMoneyInput GetStealMoneyInput(GameState gameState)
        {
            // TODO: Collect more input.
            return new StealMoneyInput();
        }
    }
}
