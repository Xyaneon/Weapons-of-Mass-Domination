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
            SecretBase = null;
        }

        /// <summary>
        /// Gets or sets whether this player has resigned.
        /// </summary>
        public bool HasResigned { get; internal set; }

        /// <summary>
        /// The amount of land this player controls, in square kilometers.
        /// </summary>
        public int Land { get; internal set; }

        /// <summary>
        /// The number of minions this player has.
        /// </summary>
        public int Minions { get; internal set; }

        /// <summary>
        /// Gets or sets how much money this player has.
        /// </summary>
        public decimal Money { get; internal set; }

        /// <summary>
        /// Gets this player's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets this player's secret base.
        /// </summary>
        /// <remarks>
        /// The value of this property is <see langword="null"/> if the player
        /// does not have a base.
        /// </remarks>
        public SecretBase SecretBase { get; internal set; }
    }
}
