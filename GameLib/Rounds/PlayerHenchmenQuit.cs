using System;
using WMD.Game.Players;

namespace WMD.Game.Rounds
{
    /// <summary>
    /// An occurrence of a player's henchmen quitting.
    /// </summary>
    public class PlayerHenchmenQuit : RoundUpdateResultItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHenchmenQuit"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> whose henchmen quit.</param>
        /// <param name="numberOfHenchmenQuit">The number of henchmen who quit.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfHenchmenQuit"/> is less than one.
        /// </exception>
        public PlayerHenchmenQuit(Player player, int numberOfHenchmenQuit)
        {
            if (numberOfHenchmenQuit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfHenchmenQuit), numberOfHenchmenQuit, "The number of henchmen who quit must be at least one.");
            }

            Player = player;
            NumberOfHenchmenQuit = numberOfHenchmenQuit;
        }

        /// <summary>
        /// Gets the number of henchmen who quit.
        /// </summary>
        public int NumberOfHenchmenQuit { get; }

        /// <summary>
        /// Gets the <see cref="Player"/> who lost henchmen.
        /// </summary>
        public Player Player { get; }
    }
}
