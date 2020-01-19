using System.ComponentModel;
using WMD.Console.UI;
using WMD.Game;
using WMD.Game.Actions;

namespace WMD.Console
{
    static class PlayerActionRunner
    {
        public static ActionResult? RunSelectedAction(GameState gameState, PlayerActionKind selectedAction) => selectedAction switch
        {
            PlayerActionKind.HireHenchmen => RunHireHenchmen(gameState),
            PlayerActionKind.PurchaseUnclaimedLand => RunPurchaseUnclaimedLand(gameState),
            PlayerActionKind.Resign => RunResign(gameState),
            PlayerActionKind.SellLand => RunSellLand(gameState),
            PlayerActionKind.Skip => RunSkipTurn(gameState),
            PlayerActionKind.StealMoney => RunStealMoney(gameState),
            PlayerActionKind.UpgradeSecretBase => RunUpgradeSecretBase(gameState),
            _ => throw new InvalidEnumArgumentException($"Unrecognized player action selected.")
        };

        private static HireHenchmenResult? RunHireHenchmen(GameState gameState)
        {
            var input = ActionInputRetrieval.GetHireHenchmenInput(gameState);
            return input != null ? PlayerActions.CurrentPlayerHiresHenchmen(gameState, input) : null;
        }

        private static PurchaseUnclaimedLandResult? RunPurchaseUnclaimedLand(GameState gameState)
        {
            var input = ActionInputRetrieval.GetPurchaseUnclaimedLandInput(gameState);
            return input != null ? PlayerActions.CurrentPlayerPurchasesUnclaimedLand(gameState, input) : null;
        }

        private static ResignResult? RunResign(GameState gameState)
        {
            var input = ActionInputRetrieval.GetResignInput(gameState);
            return PlayerActions.CurrentPlayerResigns(gameState, input);
        }

        private static SellLandResult? RunSellLand(GameState gameState)
        {
            var input = ActionInputRetrieval.GetSellLandInput(gameState);
            return input != null ? PlayerActions.CurrentPlayerSellsLand(gameState, input) : null;
        }

        private static SkipTurnResult? RunSkipTurn(GameState gameState)
        {
            var input = ActionInputRetrieval.GetSkipTurnInput(gameState);
            return PlayerActions.CurrentPlayerSkipsTurn(gameState, input);
        }

        private static StealMoneyResult? RunStealMoney(GameState gameState)
        {
            var input = ActionInputRetrieval.GetStealMoneyInput(gameState);
            return PlayerActions.CurrentPlayerStealsMoney(gameState, input);
        }

        private static UpgradeSecretBaseResult? RunUpgradeSecretBase(GameState gameState)
        {
            var input = ActionInputRetrieval.GetUpgradeSecretBaseInput(gameState);
            return input != null ? PlayerActions.CurrentPlayerUpgradesTheirSecretBase(gameState, input) : null;
        }
    }
}
