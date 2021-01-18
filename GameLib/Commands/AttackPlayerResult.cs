using System;
using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player attacking another player.
    /// </summary>
    public class AttackPlayerResult: CommandResult
    {
        private const string ArgumentOutOfRangeException_henchmenAttackerLost = "The number of henchmen the attacker lost cannot be less than zero.";
        private const string ArgumentOutOfRangeException_henchmenDefenderLost = "The number of henchmen the defender lost cannot be less than zero.";
        private const string ArgumentOutOfRangeException_targetPlayerIndex = "The index of the attacked player cannot be less than zero.";

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackPlayerResult"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="gameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="targetPlayerIndex">The index of the player who was attacked.</param>
        /// <param name="henchmenAttackerLost">The number of henchmen the attacker lost.</param>
        /// <param name="henchmenDefenderLost">The number of henchmen the defender lost.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="targetPlayerIndex"/> is less than zero.
        /// -or-
        /// <paramref name="henchmenAttackerLost"/> is less than zero.
        /// -or-
        /// <paramref name="henchmenDefenderLost"/> is less than zero.
        /// </exception>
        public AttackPlayerResult(Player player, GameState gameState, int targetPlayerIndex, int henchmenAttackerLost, int henchmenDefenderLost) : base(player, gameState)
        {
            if (targetPlayerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPlayerIndex), targetPlayerIndex, ArgumentOutOfRangeException_targetPlayerIndex);
            }

            if (henchmenAttackerLost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(henchmenAttackerLost), henchmenAttackerLost, ArgumentOutOfRangeException_henchmenAttackerLost);
            }

            if (henchmenDefenderLost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(henchmenDefenderLost), henchmenDefenderLost, ArgumentOutOfRangeException_henchmenDefenderLost);
            }

            HenchmenAttackerLost = henchmenAttackerLost;
            HenchmenDefenderLost = henchmenDefenderLost;
            TargetPlayerIndex = targetPlayerIndex;
            TargetPlayerName = gameState.Players[targetPlayerIndex].Name;
        }

        /// <summary>
        /// Gets the number of henchmen the attacker lost.
        /// </summary>
        public int HenchmenAttackerLost { get; }

        /// <summary>
        /// Gets the number of henchmen the defender lost.
        /// </summary>
        public int HenchmenDefenderLost { get; }

        /// <summary>
        /// Gets the index of the player who was attacked.
        /// </summary>
        public int TargetPlayerIndex { get; }

        /// <summary>
        /// Gets the name of the player who was attacked.
        /// </summary>
        public string TargetPlayerName { get; }
    }
}
