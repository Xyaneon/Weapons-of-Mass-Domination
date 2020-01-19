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
        /// <param name="color">The player's color as a <see cref="PlayerColor"/> value.</param>
        public Player(string name, PlayerColor color)
        {
            Color = color;
            HasResigned = false;
            Land = 0;
            Henchmen = 0;
            Money = 0;
            Name = name;
            SecretBase = null;
        }

        /// <summary>
        /// Gets this player's color as a <see cref="PlayerColor"/> value.
        /// </summary>
        public PlayerColor Color { get; }

        /// <summary>
        /// Gets whether this player has resigned.
        /// </summary>
        public bool HasResigned { get; internal set; }

        /// <summary>
        /// The amount of land this player controls, in square kilometers.
        /// </summary>
        public int Land { get; internal set; }

        /// <summary>
        /// The number of henchmen this player has.
        /// </summary>
        public int Henchmen { get; internal set; }

        /// <summary>
        /// Gets how much money this player has.
        /// </summary>
        public decimal Money { get; internal set; }

        /// <summary>
        /// Gets this player's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets this player's secret base.
        /// </summary>
        /// <remarks>
        /// The value of this property is <see langword="null"/> if the player
        /// does not have a base.
        /// </remarks>
        public SecretBase SecretBase { get; internal set; }
    }
}
