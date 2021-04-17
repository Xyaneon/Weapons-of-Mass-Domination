using System;
using WMD.Game.State.Data;

namespace WMD.Game.State.Updates.Rounds
{
    /// <summary>
    /// An occurrence of a government taking back money from a player.
    /// </summary>
    public record GovernmentTakesBackMoney : GovernmentIntervention
    {
        private const string ArgumentOutOfRangeException_AmountTakenExceedsPlayerAmount = "The amount of money taken cannot exceed the amount the player actually has.";

        /// <summary>
        /// Initializes a new instance of the <see cref="GovernmentTakesBackMoney"/> class.
        /// </summary>
        /// <param name="gameState">The <see cref="GameState"/>.</param>
        /// <param name="playerIndex">The index of the player who had their money taken.</param>
        /// <param name="amountTaken">The amount of money that was taken.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="amountTaken"/> is greater than the amount of money the player actually has.
        /// </exception>
        public GovernmentTakesBackMoney(GameState gameState, int playerIndex, decimal amountTaken)
        {
            var playerState = gameState.Players[playerIndex].State;
            if (amountTaken > playerState.Money)
            {
                throw new ArgumentOutOfRangeException(nameof(amountTaken), amountTaken, ArgumentOutOfRangeException_AmountTakenExceedsPlayerAmount);
            }
            PlayerIndex = playerIndex;
            AmountTaken = amountTaken;
        }

        /// <summary>
        /// Gets the amount of money that was taken.
        /// </summary>
        public decimal AmountTaken { get; init; }

        /// <summary>
        /// Gets the index of the player whose money was taken.
        /// </summary>
        public int PlayerIndex { get; init; }
    }
}
