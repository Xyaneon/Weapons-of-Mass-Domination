namespace WMD.Game.Players
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public record Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="identification">Information that can visually identify the player.</param>
        public Player(PlayerIdentification identification)
        {
            Identification = identification;
            State = new PlayerState();
        }

        /// <summary>
        /// Gets information that can visually identify the player.
        /// </summary>
        public PlayerIdentification Identification { get; init; }

        /// <summary>
        /// Gets current state information for this player.
        /// </summary>
        public PlayerState State { get; init; }
    }
}
