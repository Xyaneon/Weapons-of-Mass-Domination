namespace WMD.Game.Players
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
            Name = name;
            State = new PlayerState();
        }

        /// <summary>
        /// Gets this player's color as a <see cref="PlayerColor"/> value.
        /// </summary>
        public PlayerColor Color { get; }

        /// <summary>
        /// Gets this player's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets current state information for this player.
        /// </summary>
        public PlayerState State { get; }
    }
}
