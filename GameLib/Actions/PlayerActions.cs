namespace WMD.Game.Actions
{
    /// <summary>
    /// Provides actions a player may take on their turn.
    /// </summary>
    /// <remarks>
    /// The actions this class provides are all meant to be used as
    /// <see cref="System.Func{T, TResult}"/> delegates which take the current
    /// <see cref="GameState"/> and produce an <see cref="ActionResult"/>
    /// subclass instance.
    /// </remarks>
    public static class PlayerActions
    {
        /// <summary>
        /// The action of the current player resigning.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <returns>A new <see cref="ResignResult"/> instance describing the result of the action.</returns>
        public static ResignResult CurrentPlayerResigns(GameState gameState)
        {
            gameState.CurrentPlayer.HasResigned = true;
            return new ResignResult(gameState.CurrentPlayer);
        }

        /// <summary>
        /// The action of the current player skipping their turn.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <returns>A new <see cref="SkipTurnResult"/> instance describing the result of the action.</returns>
        public static SkipTurnResult CurrentPlayerSkipsTurn(GameState gameState) => new SkipTurnResult(gameState.CurrentPlayer);

        /// <summary>
        /// The action of the current player stealing money.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/> to act on and update.</param>
        /// <returns>A new <see cref="StealMoneyResult"/> instance describing the result of the action.</returns>
        public static StealMoneyResult CurrentPlayerStealsMoney(GameState gameState)
        {
            decimal moneyStolen = 200;
            gameState.CurrentPlayer.Money += moneyStolen;

            return new StealMoneyResult(gameState.CurrentPlayer, moneyStolen);
        }
    }
}
