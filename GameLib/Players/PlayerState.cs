namespace WMD.Game.Players
{
    /// <summary>
    /// Holds current state for an individual <see cref="Player"/>.
    /// </summary>
    public class PlayerState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerState"/> class.
        /// </summary>
        public PlayerState()
        {
            HasResigned = false;
            Land = 0;
            Henchmen = 0;
            Money = 0;
            SecretBase = null;
        }

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
        /// Gets this player's secret base.
        /// </summary>
        /// <remarks>
        /// The value of this property is <see langword="null"/> if the player
        /// does not have a base.
        /// </remarks>
        public SecretBase SecretBase { get; internal set; }
    }
}
