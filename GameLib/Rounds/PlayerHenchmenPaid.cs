using WMD.Game.Players;

namespace WMD.Game.Rounds
{
    /// <summary>
    /// An occurrence of a player's henchmen getting paid.
    /// </summary>
    public class PlayerHenchmenPaid : RoundUpdateResultItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHenchmenPaid"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> who paid their henchmen.</param>
        public PlayerHenchmenPaid(Player player)
        {
            DailyPayRate = player.State.WorkforceState.DailyPayRate;
            NumberOfHenchmenPaid = player.State.WorkforceState.NumberOfHenchmen;
            Player = player;
        }

        /// <summary>
        /// Gets the player's daily pay rate for their henchmen.
        /// </summary>
        public decimal DailyPayRate { get; }

        /// <summary>
        /// Gets the number of henchmen the player paid.
        /// </summary>
        public int NumberOfHenchmenPaid { get; }

        /// <summary>
        /// Gets the player who paid their henchmen.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets the total amount of money the player paid their henchmen.
        /// </summary>
        public decimal TotalPaidAmount { get => DailyPayRate * NumberOfHenchmenPaid; }
    }
}
