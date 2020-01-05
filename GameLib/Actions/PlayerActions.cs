namespace WMD.Game.Actions
{
    public static class PlayerActions
    {
        public static SkipTurnResult CurrentPlayerSkipsTurn(GameState gameState) => new SkipTurnResult(gameState.CurrentPlayer);

        public static StealMoneyResult CurrentPlayerStealsMoney(GameState gameState)
        {
            decimal moneyStolen = 200;
            gameState.CurrentPlayer.Money += moneyStolen;

            return new StealMoneyResult(gameState.CurrentPlayer, moneyStolen);
        }
    }
}
