namespace WMD.Game
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The player's name.</param>
        public Player(string name)
        {
            HasResigned = false;
            Land = 0;
            Minions = 0;
            Money = 0;
            Name = name;
        }

        /// <summary>
        /// Gets or sets whether this player has resigned.
        /// </summary>
        public bool HasResigned { get; set; }

        /// <summary>
        /// The amount of land this player controls, in square kilometers.
        /// </summary>
        public int Land { get; set; }

        /// <summary>
        /// The number of minions this player has.
        /// </summary>
        public int Minions { get; set; }

        /// <summary>
        /// Gets or sets how much money this player has.
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Gets this player's name.
        /// </summary>
        public string Name { get; }
    }
}
